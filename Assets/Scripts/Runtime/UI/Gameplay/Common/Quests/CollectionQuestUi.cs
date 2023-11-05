using System;
using System.Collections.Generic;
using AYellowpaper.SerializedCollections;
using RussSurvivor.Runtime.Gameplay.Common.Quests.Data;
using RussSurvivor.Runtime.Gameplay.Common.Quests.Resolvers;
using RussSurvivor.Runtime.Gameplay.Common.Quests.StateMachine;
using RussSurvivor.Runtime.Infrastructure.Content;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Zenject;

namespace RussSurvivor.Runtime.UI.Gameplay.Common.Quests
{
  public class CollectionQuestUi : MonoBehaviour
  {
    private readonly List<IDisposable> _disposables = new();
    [SerializeField] private Image _collectableItemIcon;
    [SerializeField] private TextMeshProUGUI _counterText;
    [SerializeField] private TextMeshProUGUI _returnToText;
    [SerializeField] private GameObject _counterContainer;

    [FormerlySerializedAs("_returnText"), SerializeField]
    private SerializedDictionary<CollectItemsQuestDescription.CollectableType, string> _returnTextByType = new();

    private Guid _currentQuestId;
    private ICollectableItemPrefabProvider _prefabProvider;

    private IQuestStateMachine _questStateMachine;
    private CollectingQuestResolver _resolver;

    [Inject]
    private void Construct(IQuestStateMachine questStateMachine, ICollectableItemPrefabProvider prefabProvider)
    {
      _questStateMachine = questStateMachine;
      _prefabProvider = prefabProvider;
    }

    public void Initialize(CollectingQuestResolver resolver)
    {
      if(_resolver != null)
        return;
      _resolver = resolver;
      _questStateMachine.CurrentState
        .Where(k => k is CollectingQuestState)
        .Subscribe(currentState =>
        {
          var collecting = (CollectingQuestState)currentState;
          _currentQuestId = collecting.QuestId;
          gameObject.SetActive(true);
          _counterContainer.SetActive(true);
          _collectableItemIcon.sprite = _prefabProvider.GetIcon(collecting.CollectableType);
          _counterText.text = $"{resolver.CollectedAmount.Value.ToString()}/{collecting.CollectablesCount.ToString()}";
          _returnToText.gameObject.SetActive(false);
          _returnToText.text = _returnTextByType[collecting.CollectableType];
        })
        .AddTo(_disposables);

      _questStateMachine.CurrentState
        .Where(k => (
                      k is not CollectingQuestState &&
                      k != null &&
                      k.QuestId == _currentQuestId
                    ) ||
                    k == null)
        .Subscribe(_ =>
        {
          _counterContainer.SetActive(false);
          _returnToText.gameObject.SetActive(true);
        })
        .AddTo(_disposables);

      _questStateMachine.CurrentState
        .Where(k => k == null || k.QuestId != _currentQuestId)
        .Subscribe(_ =>
        {
          _returnToText.gameObject.SetActive(false);
          gameObject.SetActive(false);
        })
        .AddTo(_disposables);

      _resolver.CollectedAmount
        .ObserveEveryValueChanged(k => k.Value)
        .Subscribe(newValue =>
        {
          _counterText.text = $"{newValue.ToString()}/{resolver.RequiredAmount.ToString()}";
        })
        .AddTo(_disposables);
    }
  }
}