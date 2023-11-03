namespace RussSurvivor.Runtime.Gameplay.Battle.Weapons.Damage
{
  public interface IDamageMaker
  {
    bool TryApply(IDamagable damagable);
    void SetValue(float value);
  }
}