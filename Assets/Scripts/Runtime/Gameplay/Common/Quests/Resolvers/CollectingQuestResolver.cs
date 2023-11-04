using System;
using System.Collections.Generic;
using RussSurvivor.Runtime.Gameplay.Common.Items.Collectables;
using RussSurvivor.Runtime.Gameplay.Common.Quests.Data;
using RussSurvivor.Runtime.Gameplay.Common.Quests.StateMachine;
using RussSurvivor.Runtime.Infrastructure.Content;
using UniRx;
using UnityEngine;
using Zenject;

namespace RussSurvivor.Runtime.Gameplay.Common.Quests.Resolvers
{
  public class CollectingQuestResolver : MonoBehaviour
  {
    private readonly List<IDisposable> _disposables = new();
    private IQuestStateMachine _questStateMachine;
    private ICollectableItemPrefabProvider _prefabProvider;

    [SerializeField] private CollectableItemSpawnPoint[] _berrySpawnPoints;
    [SerializeField] private CollectableItemSpawnPoint[] _mushroomSpawnPoints;
    [SerializeField] private CollectableItemSpawnPoint[] _vedasSpawnPoints;

    private Dictionary<CollectItemsQuestDescription.CollectableType, IEnumerable<CollectableItemSpawnPoint>>
      _spawnPointsByType;

    private int _requiredAmount;
    private readonly List<CollectableItem> _collectables = new();

    [Inject]
    private void Construct(IQuestStateMachine questStateMachine, ICollectableItemPrefabProvider prefabProvider)
    {
      _questStateMachine = questStateMachine;
      _prefabProvider = prefabProvider;
    }

    public void Initialize()
    {
      _spawnPointsByType =
        new Dictionary<CollectItemsQuestDescription.CollectableType, IEnumerable<CollectableItemSpawnPoint>>()
        {
          [CollectItemsQuestDescription.CollectableType.Berries] = _berrySpawnPoints,
          [CollectItemsQuestDescription.CollectableType.Mushrooms] = _mushroomSpawnPoints,
          [CollectItemsQuestDescription.CollectableType.SlavicVedas] = _vedasSpawnPoints
        };

      _questStateMachine.CurrentState
        .Where(k => k is CollectingQuestState)
        .Subscribe(_ =>
        {
          var collecting = (CollectingQuestState)_questStateMachine.CurrentState.Value;
          foreach (CollectableItemSpawnPoint spawnPoint in _spawnPointsByType[collecting.CollectableType])
          {
            CollectableItem collectableItem = Instantiate(
              _prefabProvider.GetPrefab(collecting.CollectableType),
              spawnPoint.transform.position,
              Quaternion.identity,
              spawnPoint.transform);
            _collectables.Add(collectableItem);
            collectableItem.Initialize(this);
          }

          _requiredAmount = collecting.CollectablesCount;
        })
        .AddTo(_disposables);
    }

    public void RemoveCollectable(CollectableItem collectableItem)
    {
      if (--_requiredAmount == 0)
      {
        _questStateMachine.NextState();
        for (var i = 0; i < _collectables.Count; i++)
        {
          Destroy(_collectables[i].gameObject);
        }

        _collectables.Clear();
      }
      else
      {
        _collectables.Remove(collectableItem);
      }
    }

    public void Dispose()
    {
      foreach (IDisposable disposable in _disposables)
        disposable.Dispose();
      _disposables.Clear();
    }
  }
}