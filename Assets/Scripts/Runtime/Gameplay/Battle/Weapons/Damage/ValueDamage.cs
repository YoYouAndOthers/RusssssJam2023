namespace RussSurvivor.Runtime.Gameplay.Battle.Weapons.Damage
{
  public class ValueDamage : IDamageMaker
  {
    private float _value;

    public ValueDamage(float value) =>
      _value = value;

    public bool TryApply(IDamagable damagable)
    {
      return damagable.TryTakeDamage(_value);
    }

    public void SetValue(float value)
    {
      _value = value;
    }
  }
}