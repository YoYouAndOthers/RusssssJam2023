using RussSurvivor.Runtime.Gameplay.Battle.Weapons;

namespace RussSurvivor.Runtime.Gameplay.Battle.Timing
{
  public class CooldownService : ICooldownService
  {
    private readonly IEnemyWeaponService _enemyWeaponService;
    private readonly IPlayerWeaponService _playerWeaponService;
    
    private float _deltaTime;
    private int _counter;

    public CooldownService(IPlayerWeaponService playerWeaponService, IEnemyWeaponService enemyWeaponService)
    {
      _playerWeaponService = playerWeaponService;
      _enemyWeaponService = enemyWeaponService;
    }
    
    public void PerformTick(float deltaTime)
    {
      _deltaTime += deltaTime;
      if(_counter % 3 == 0)
      {
        _counter = 1;
        UpdateAll(_deltaTime);
        _deltaTime = 0;
      }
      else
        _counter++;
    }

    private void UpdateAll(float deltaTime)
    {
      foreach (WeaponBehaviourBase playerWeapon in _playerWeaponService.Weapons)
        playerWeapon.UpdateCoolDown(deltaTime);
    }
  }
}