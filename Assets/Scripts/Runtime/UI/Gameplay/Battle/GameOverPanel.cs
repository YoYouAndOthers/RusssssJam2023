using RussSurvivor.Runtime.Gameplay.Battle.States;
using RussSurvivor.Runtime.Infrastructure.Installers;
using RussSurvivor.Runtime.Infrastructure.Scenes;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace RussSurvivor.Runtime.UI.Gameplay.Battle
{
  public class GameOverPanel : MonoBehaviour
  {
    [SerializeField] private GameObject _panel;
    [SerializeField] private Button _toMainMenuButton;
    private IBattleStateMachine _battleStateMachine;
    private ISceneLoader _sceneLoader;

    [Inject]
    private void Construct(ISceneLoader sceneLoader, IBattleStateMachine battleStateMachine)
    {
      _sceneLoader = sceneLoader;
      _battleStateMachine = battleStateMachine;
    }

    private void Awake()
    {
      _panel.SetActive(false);
      _battleStateMachine.CurrentState
        .Where(_ => _ is GameOverState)
        .Subscribe(state =>
        {
          Show();
        })
        .AddTo(this);

      _toMainMenuButton.onClick.AddListener(() =>
      {
        _sceneLoader.LoadScene(SceneEntrance.SceneName.MainMenu);
      });
    }

    private void Show()
    {
      _panel.SetActive(true);
    }
  }
}