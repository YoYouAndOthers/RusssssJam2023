using System;
using System.Collections.Generic;
using System.Linq;
using RussSurvivor.Runtime.Gameplay.Common.Items.Collectables;
using RussSurvivor.Runtime.Gameplay.Common.Quests.StateMachine;
using RussSurvivor.Runtime.Gameplay.Common.Transitions;
using RussSurvivor.Runtime.Infrastructure.Content;
using RussSurvivor.Runtime.Infrastructure.Installers;
using UniRx;
using UnityEngine;
using Zenject;

namespace RussSurvivor.Runtime.Gameplay.Common.Quests.Resolvers
{
  public class CollectingQuestResolver : MonoBehaviour, IDisposable
  {
    private readonly List<IDisposable> _disposables = new();
    private readonly HashSet<CollectableItem> _collectables = new();

    public IntReactiveProperty CollectedAmount { get; } = new();

    public int RequiredAmount => _collecting?.CollectablesCount ?? 0;

    private CollectableItemSpawnPoint[] _berrySpawnPoints;

    private CollectingQuestState _collecting;

    private IGameplayTransitionService _gameplayTransitionService;
    private CollectableItemSpawnPoint[] _mushroomSpawnPoints;

    private ICollectableItemPrefabProvider _prefabProvider;
    private IQuestStateMachine _questStateMachine;
    private CollectableItemSpawnPoint[] _vedasSpawnPoints;

    [Inject]
    private void Construct(
      IQuestStateMachine questStateMachine,
      IGameplayTransitionService gameplayTransitionService,
      ICollectableItemPrefabProvider prefabProvider)
    {
      _questStateMachine = questStateMachine;
      _gameplayTransitionService = gameplayTransitionService;
      _prefabProvider = prefabProvider;
    }

    public void Initialize()
    {
      Dispose();
      _questStateMachine.CurrentState
        .Where(k => k is CollectingQuestState)
        .Subscribe(InitializeCollectablesByQuest)
        .AddTo(_disposables);
    }

    public void RemoveCollectable(CollectableItem collectableItem)
    {
      bool removeSuccessful = _collectables.Remove(collectableItem);
      if (!removeSuccessful)
        return;
      Debug.Log("RemoveCollectable");
      CollectedAmount.Value++;
      _collecting.CollectedAmount++;
      if (CollectedAmount.Value == _collecting.CollectablesCount)
      {
        if (_gameplayTransitionService.CurrentScene == SceneEntrance.SceneName.Town)
          _questStateMachine.NextState<TalkToNpcQuestState>();
        else
          _questStateMachine.NextState<ReturnToTownQuestState>();

        CollectableItem[] collctablesArray = _collectables.ToArray();
        for (var i = 0; i < collctablesArray.Length; i++)
          Destroy(collctablesArray[i].gameObject);

        _collectables.Clear();
      }
    }

    public void Dispose()
    {
      _collectables.Clear();
      foreach (IDisposable disposable in _disposables)
        disposable.Dispose();
      _disposables.Clear();
    }

    private void InstantiateCollectables(CollectingQuestState collecting)
    {
      IEnumerable<CollectableItemSpawnPoint> spawnPoints = FindObjectsOfType<CollectableItemSpawnPoint>()
        .Where(k => k.Type == collecting.CollectableType);
      foreach (CollectableItemSpawnPoint spawnPoint in spawnPoints)
      {
        CollectableItem collectableItem = Instantiate(
          _prefabProvider.GetPrefab(collecting.CollectableType),
          spawnPoint.transform.position,
          Quaternion.identity,
          spawnPoint.transform);
        _collectables.Add(collectableItem);
        collectableItem.Initialize(this);
      }
    }

    private void InitializeCollectablesByQuest(QuestState state)
    {
      _collecting = (CollectingQuestState)state;
      InstantiateCollectables(_collecting);
      CollectedAmount.Value = _collecting.CollectedAmount;
    }
  }
}