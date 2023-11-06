using RussSurvivor.Runtime.Gameplay.Common.Player;
using RussSurvivor.Runtime.Gameplay.Town.Characters;
using RussSurvivor.Runtime.Gameplay.Town.Dialogues.Data;
using RussSurvivor.Runtime.Gameplay.Town.Economics.Trade;
using UnityEngine;
using Zenject;

namespace RussSurvivor.Runtime.Gameplay.Town.NPC
{
  public class TraderNpcBehaviour : IntarectableNpcBehaviourBase
  {
    [SerializeField] private Actor _actor;
    private IActorRegistry _actorRegistry;
    private ITraderService _traderService;

    [Inject]
    private void Construct(IActorRegistry actorRegistry, ITraderService traderService)
    {
      _actorRegistry = actorRegistry;
      _traderService = traderService;
    }

    private void Awake()
    {
      _actorRegistry.RegisterActor(this, _actor.Id);
    }

    private void OnDestroy()
    {
      _actorRegistry.CleanActor(_actor.Id);
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
      if (_actor == null)
        return;
      if (_actor.IsTrader == false)
        Debug.LogError("Trader NPC should have IsTrader flag set to true");
    }
#endif

    protected override void PerformInteraction(PlayerTownBehaviour player)
    {
      _traderService.StartTrade();
    }

    protected override void PerformInteractionExit(PlayerTownBehaviour player)
    {
    }
  }
}