namespace RussSurvivor.Runtime.Gameplay.Battle.Weapons.Damage
{
  public interface IDamagable
  {
    bool TryTakeDamage(float damage, bool percent = false);
    void Kill();
  }
}