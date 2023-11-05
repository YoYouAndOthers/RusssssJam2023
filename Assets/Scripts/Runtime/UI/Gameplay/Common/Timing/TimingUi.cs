using System;
using RussSurvivor.Runtime.Gameplay.Common.Timing;
using TMPro;
using UniRx;
using UnityEngine;
using Zenject;

namespace RussSurvivor.Runtime.UI.Gameplay.Common.Timing
{
  public class TimingUi : MonoBehaviour
  {
    [SerializeField] private TextMeshProUGUI _timeText;

    private IDayTimer _dayTimer;

    [Inject]
    private void Construct(IDayTimer dayTimer)
    {
      _dayTimer = dayTimer;
    }

    private void Awake()
    {
      _dayTimer.TimeLeftReactiveProperty.ObserveEveryValueChanged(x => x.Value)
        .Subscribe(x =>
        {
          TimeSpan timespan = TimeSpan.FromSeconds(x);
          _timeText.text = $"{timespan.Minutes.ToString()}:{timespan.Seconds.ToString()}";
        })
        .AddTo(this);
    }
  }
}