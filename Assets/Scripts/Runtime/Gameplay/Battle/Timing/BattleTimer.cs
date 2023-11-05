using RussSurvivor.Runtime.Gameplay.Battle.Settings;
using RussSurvivor.Runtime.Gameplay.Battle.States;
using RussSurvivor.Runtime.Gameplay.Common.Timing;

namespace RussSurvivor.Runtime.Gameplay.Battle.Timing
{
  public class BattleTimer : IBattleTimer
  {
    private readonly ICooldownService _cooldownService;
    private readonly IDayTimer _dayTimer;
    private readonly IBattleSettingsService _battleSettingsService;
    private readonly IBattleStateMachine _battleStateMachine;

    public float TimeLeft => _dayTimer.TimeLeft;
    public bool IsReady => TimeLeft <= 0;

    private float _bossTimeLeft;
    private float _escapeTimeLeft;

    public BattleTimer(
      ICooldownService cooldownService,
      IDayTimer dayTimer,
      IBattleSettingsService battleSettingsService,
      IBattleStateMachine battleStateMachine)
    {
      _cooldownService = cooldownService;
      _dayTimer = dayTimer;
      _battleSettingsService = battleSettingsService;
      _battleStateMachine = battleStateMachine;
    }

    public void Initialize()
    {
      _escapeTimeLeft = _battleSettingsService.SettingsForBattle.EndSpawnDuration;
      _bossTimeLeft = _battleSettingsService.SettingsForBattle.BossStateDuration;
      _cooldownService.RegisterUpdatable(this);
    }

    public void UpdateCooldown(float deltaTime)
    {
      _battleStateMachine.CurrentState.Value.Execute(deltaTime);
      if (_battleStateMachine.CurrentState.Value is EndSpawningState)
      {
        _escapeTimeLeft -= deltaTime;

        if (_escapeTimeLeft < 0)
          _battleStateMachine.SetState<BossState>();
        return;
      }

      if (_battleStateMachine.CurrentState.Value is BossState)
      {
        _bossTimeLeft -= deltaTime;

        if (_bossTimeLeft < 0)
          _battleStateMachine.SetState<EndBattleState>();
      }
    }
  }
}