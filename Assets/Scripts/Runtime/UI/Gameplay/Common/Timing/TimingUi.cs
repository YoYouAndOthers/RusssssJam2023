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
    [SerializeField] private GameObject _pausePanel;

    private IDayTimer _dayTimer;
    private IPauseService _pauseService;

    [Inject]
    private void Construct(IDayTimer dayTimer, IPauseService pauseService)
    {
      _dayTimer = dayTimer;
      _pauseService = pauseService;
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

      _pauseService.IsPaused.Subscribe(x =>
        {
          _pausePanel.SetActive(x);
        })
        .AddTo(this);
    }
  }
}