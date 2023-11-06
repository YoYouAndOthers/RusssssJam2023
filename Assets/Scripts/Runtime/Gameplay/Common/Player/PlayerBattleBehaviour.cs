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
    public Vector3 Position => transform.position;
    [field: SerializeField] public float CurrentHealthValue { get; private set; }
    [field: SerializeField] public float MaxHealth { get; private set; }
    [field: SerializeField] public float RegenerationPerSec { get; private set; }
    public float TimeLeft { get; }
    
    public FloatReactiveProperty CurrentHealth { get; private set; } = new();
    public bool IsReady => CurrentHealth.Value >= MaxHealth;

    private IPlayerWeaponService _playerWeaponService;
    private IBattleStateMachine _battleStateMachine;

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

    public void UpdateCooldown(float deltaTime)
    {
      CurrentHealth.Value += deltaTime * RegenerationPerSec;
      CurrentHealth.Value = Mathf.Clamp(CurrentHealth.Value, 0f, MaxHealth);
    }

    public void Kill()
    {
      _battleStateMachine.SetState<GameOverState>();
      Destroy(PlayerDash);
      Destroy(PlayerMovement);
      _playerWeaponService.ClearWeapons();
    }
  }
}