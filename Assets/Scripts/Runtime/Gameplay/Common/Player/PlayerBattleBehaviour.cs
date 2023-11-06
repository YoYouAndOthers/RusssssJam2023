using RussSurvivor.Runtime.Gameplay.Battle.Combat;
using RussSurvivor.Runtime.Gameplay.Battle.States;
using RussSurvivor.Runtime.Gameplay.Battle.Weapons;
using RussSurvivor.Runtime.Gameplay.Battle.Weapons.Damage;
using UniRx;
using UnityEngine;
using Zenject;

namespace RussSurvivor.Runtime.Gameplay.Common.Player
{
  public class PlayerBattleBehaviour : PlayerBehaviourBase, IWeaponOwner, IDamagable, IHealth
  {
    [field: SerializeField] public WeaponConfig Fists { get; private set; }
    [field: SerializeField] public Transform WeaponsContainer { get; private set; }
    [field: SerializeField] public float CurrentHealthValue { get; private set; }
    [field: SerializeField] public float MaxHealth { get; private set; }
    [field: SerializeField] public float RegenerationPerSec { get; private set; }
    public float TimeLeft { get; }

    public FloatReactiveProperty CurrentHealth { get; } = new();
    public bool IsReady => CurrentHealth.Value >= MaxHealth;
    public Vector3 Position => transform.position;
    private IBattleStateMachine _battleStateMachine;

    private IPlayerWeaponService _playerWeaponService;

    [Inject]
    private void Construct(IBattleStateMachine battleStateMachine, IPlayerWeaponService playerWeaponService)
    {
      _battleStateMachine = battleStateMachine;
      _playerWeaponService = playerWeaponService;
    }

    private void Awake()
    {
      CurrentHealth.Value = CurrentHealthValue;
    }

    public bool TryTakeDamage(float damage, bool percent = false)
    {
      Debug.Log($"Player take damage: {damage}");
      if (percent)
        damage = MaxHealth * damage / 100f;

      CurrentHealth.Value -= damage;

      if (CurrentHealth.Value <= 0 && _battleStateMachine.CurrentState.Value is not GameOverState)
      {
        Kill();
        return false;
      }

      return true;
    }

    public void Kill()
    {
      _battleStateMachine.SetState<GameOverState>();
      Destroy(PlayerDash);
      Destroy(PlayerMovement);
      _playerWeaponService.ClearWeapons();
    }

    public void UpdateCooldown(float deltaTime)
    {
      CurrentHealth.Value += deltaTime * RegenerationPerSec;
      CurrentHealth.Value = Mathf.Clamp(CurrentHealth.Value, 0f, MaxHealth);
    }
  }
}