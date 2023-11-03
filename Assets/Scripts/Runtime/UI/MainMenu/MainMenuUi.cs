using RussSurvivor.Runtime.Application;
using RussSurvivor.Runtime.Application.SaveLoad;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace RussSurvivor.Runtime.UI.MainMenu
{
  public class MainMenuUi : MonoBehaviour, IInitializable
  {
    [SerializeField] private Button _loadButton;
    [SerializeField] private Button _newGameButton;
    [SerializeField] private Button _quitButton;
    private IApplicationService _applicationService;

    private ILoadService _loadService;

    [Inject]
    private void Construct(ILoadService loadService, IApplicationService applicationService)
    {
      _loadService = loadService;
      _applicationService = applicationService;
    }

    public void Initialize()
    {
      _loadButton.interactable = _loadService.HasSave();
      _loadButton.onClick.AddListener(_applicationService.LoadGame);
      _newGameButton.onClick.AddListener(_applicationService.NewGame);
      _quitButton.onClick.AddListener(_applicationService.Quit);
    }
  }
}