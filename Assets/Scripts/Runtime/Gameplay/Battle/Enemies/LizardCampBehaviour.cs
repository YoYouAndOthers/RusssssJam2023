using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace RussSurvivor.Runtime.Gameplay.Battle.Enemies
{
  public class LizardCampBehaviour : EnemyBehaviour
  {
    [SerializeField] private Image _healthBar;

    private void Awake()
    {
      CurrentHealth
        .ObserveEveryValueChanged(k => k.Value)
        .Subscribe(UpdateHealthBar)
        .AddTo(this);
    }

    private void UpdateHealthBar(float health)
    {
      _healthBar.fillAmount = health / MaxHealth;
    }
  }
}