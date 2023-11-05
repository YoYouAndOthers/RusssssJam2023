using RussSurvivor.Runtime.Gameplay.Battle.States;
using RussSurvivor.Runtime.Gameplay.Common.Timing;

namespace RussSurvivor.Runtime.Gameplay.Battle.Timing
{
  public class BattleTimer : IBattleTimer
  {
    private readonly IBattleStateMachine _battleStateMachine;
    private readonly ICooldownService _cooldownService;
    private readonly IDayTimer _dayTimer;
    public float TimeLeft => _dayTimer.TimeLeft;
    public bool IsReady => TimeLeft <= 0;
    private float _battleTimeLeft;
    private float _bossTimeLeft;
    private float _escapeTimeLeft;

    public BattleTimer(ICooldownService cooldownService, IDayTimer dayTimer, IBattleStateMachine battleStateMachine)
    {
      _cooldownService = cooldownService;
      _dayTimer = dayTimer;
      _battleStateMachine = battleStateMachine;
    }

    public void Initialize(float battleTimeLeft, float escapeTimeLeft, float bossTimeLeft)
    {
      _escapeTimeLeft = escapeTimeLeft;
      _battleTimeLeft = battleTimeLeft;
      _bossTimeLeft = bossTimeLeft;
      _cooldownService.RegisterUpdatable(this);
    }

    public void UpdateCooldown(float deltaTime)
    {
      if (_battleStateMachine.CurrentState is MainBattleState)
      {
        _battleTimeLeft -= deltaTime;

        if (_battleTimeLeft < 0)
          _battleStateMachine.SetState<EndSpawningState>();
        return;
      }

      if (_battleStateMachine.CurrentState is EndSpawningState)
      {
        _escapeTimeLeft -= deltaTime;

        if (_escapeTimeLeft < 0)
          _battleStateMachine.SetState<BossState>();
        return;
      }

      if (_battleStateMachine.CurrentState is BossState)
      {
        _bossTimeLeft -= deltaTime;

        if (_bossTimeLeft < 0)
          _battleStateMachine.SetState<EndBattleState>();
      }
    }
  }
}