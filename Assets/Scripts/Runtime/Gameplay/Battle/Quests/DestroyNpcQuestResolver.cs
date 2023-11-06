using System.Collections.Generic;
using System.Linq;
using AYellowpaper.SerializedCollections;
using Cysharp.Threading.Tasks;
using RussSurvivor.Runtime.Gameplay.Battle.Enemies;
using RussSurvivor.Runtime.Gameplay.Common.Quests.Data;
using RussSurvivor.Runtime.Gameplay.Common.Quests.StateMachine;
using UniRx;
using UnityEngine;
using Zenject;

namespace RussSurvivor.Runtime.Gameplay.Battle.Quests
{
  public class DestroyNpcQuestResolver : MonoBehaviour
  {
    private static readonly Dictionary<DestructionQuestDescription.HostileObjectType, EnemyType>
      EnemyTypeToHostileObjectType =
        new()
        {
          { DestructionQuestDescription.HostileObjectType.LizardCamp, EnemyType.LizardCamp }
        };

    [SerializeField]
    private SerializedDictionary<DestructionQuestDescription.HostileObjectType, Transform> _transformsByType;

    public IntReactiveProperty NpcToDestroyCount { get; } = new();
    private EnemyFactory _enemyFactory;
    private IEnemyRegistry _enemyRegistry;
    private bool _initialized;
    private readonly List<EnemyBehaviour> _npcs = new();

    private IQuestStateMachine _questStateMachine;
    private DestroyNpcQuestState _state;

    [Inject]
    private void Construct(IQuestStateMachine questStateMachine, EnemyFactory enemyFactory,
      IEnemyRegistry enemyRegistry)
    {
      _questStateMachine = questStateMachine;
      _enemyFactory = enemyFactory;
      _enemyRegistry = enemyRegistry;
    }

    private void Update()
    {
      if (_questStateMachine.CurrentState.Value is DestroyNpcQuestState state && !_initialized)
      {
        _initialized = true;
        InitializeNpcByQuest(state);
        return;
      }

      if (_npcs.Count(k => k != null) == 0 && _initialized &&
          _questStateMachine.CurrentState.Value is DestroyNpcQuestState)
        _questStateMachine.NextState<ReturnToTownQuestState>();
    }

    public void Initialize()
    {
      NpcToDestroyCount
        .Where(k => k == 0)
        .Subscribe(_ => _questStateMachine.NextState<ReturnToTownQuestState>())
        .AddTo(this);
    }

    private async void InitializeNpcByQuest(QuestState state)
    {
      _state = (DestroyNpcQuestState)state;
      NpcToDestroyCount.Value = _state.NpcToDestroyCount;
      await InstantiateNpcs(_state);
    }

    private async UniTask InstantiateNpcs(DestroyNpcQuestState state)
    {
      await UniTask.Delay(1);
      for (var i = 0; i < state.NpcToDestroyCount; i++)
      {
        Debug.Log($"InstantiateNpcs {i}");
        EnemyBehaviour npc = _enemyFactory.Create(EnemyTypeToHostileObjectType[state.DestructionQuestHostileType]);
        npc.transform.position =
          _transformsByType[state.DestructionQuestHostileType].position + Vector3.left * i * 7;
        _enemyRegistry.Add(npc, EnemyTypeToHostileObjectType[state.DestructionQuestHostileType]);
        _npcs.Add(npc);
      }
    }
  }
}