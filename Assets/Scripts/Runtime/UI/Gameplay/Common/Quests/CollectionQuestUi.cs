using System;
using System.Collections.Generic;
using RussSurvivor.Runtime.Gameplay.Common.Quests.Resolvers;
using RussSurvivor.Runtime.Gameplay.Common.Quests.StateMachine;
using RussSurvivor.Runtime.Infrastructure.Content;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace RussSurvivor.Runtime.UI.Gameplay.Common.Quests
{
  public class CollectionQuestUi : MonoBehaviour
  {
    private readonly List<IDisposable> _disposables = new();
    [SerializeField] private Image _collectableItemIcon;
    [SerializeField] private TextMeshProUGUI _counterText;
    private Guid _currentQuestId;
    private ICollectableItemPrefabProvider _prefabProvider;

    private IQuestStateMachine _questStateMachine;

    [Inject]
    private void Construct(IQuestStateMachine questStateMachine, ICollectableItemPrefabProvider prefabProvider)
    {
      _questStateMachine = questStateMachine;
      _prefabProvider = prefabProvider;
    }

    public void Initialize(CollectingQuestResolver resolver)
    {
      _disposables.ForEach(k => k.Dispose());
      _disposables.Clear();

      _questStateMachine.CurrentState
        .Where(k => k is CollectingQuestState)
        .Subscribe(currentState =>
        {
          var collecting = (CollectingQuestState)currentState;
          _currentQuestId = collecting.QuestId;
          gameObject.SetActive(true);
          _collectableItemIcon.sprite = _prefabProvider.GetIcon(collecting.CollectableType);
          _counterText.text = $"{resolver.CollectedAmount.Value.ToString()}/{collecting.CollectablesCount.ToString()}";
        })
        .AddTo(_disposables, this);

      _questStateMachine.CurrentState
        .Where(k => k == null || k.QuestId != _currentQuestId)
        .Subscribe(_ => gameObject.SetActive(false))
        .AddTo(_disposables, this);

      resolver.CollectedAmount
        .ObserveEveryValueChanged(k => k.Value)
        .Subscribe(newValue => _counterText.text = $"{newValue.ToString()}/{resolver.RequiredAmount.ToString()}")
        .AddTo(_disposables, this);
    }
  }
}