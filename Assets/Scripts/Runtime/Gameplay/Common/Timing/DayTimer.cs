using System;
using UniRx;
using UnityEngine;

namespace RussSurvivor.Runtime.Gameplay.Common.Timing
{
  public class DayTimer : IDayTimer
  {
    private float MorningTimeSeconds { get; } = (float)TimeSpan.FromMinutes(6).TotalSeconds;
    private float AfternoonTimeSeconds { get; } = (float)TimeSpan.FromMinutes(12).TotalSeconds;
    private float EveningTimeSeconds { get; } = (float)TimeSpan.FromMinutes(18).TotalSeconds;
    public float NightTimeSeconds { get; } = WholeDayTimeSeconds;

    private static readonly float WholeDayTimeSeconds = (float)TimeSpan.FromMinutes(24).TotalSeconds;
    private float _currentTime;
    private DayTime _currentDayTime;
    public float TimeLeft => WholeDayTimeSeconds - _currentTime;
    public bool IsReady => false;
    public bool IsRunning => TimeLeft < WholeDayTimeSeconds;

    public DayTime CurrentDayTime
    {
      get => _currentDayTime;
      private set
      {
        Debug.Log($"Current day time: {value}");
        _currentDayTime = value;
      }
    }

    public FloatReactiveProperty TimeLeftReactiveProperty { get; } = new();

    public void UpdateCooldown(float deltaTime)
    {
      _currentTime += deltaTime;
      if (WholeDayTimeSeconds <= _currentTime)
        _currentTime = 0;

      if (_currentTime <= MorningTimeSeconds && CurrentDayTime != DayTime.Morning)
      {
        CurrentDayTime = DayTime.Morning;
      }
      else if (_currentTime <= AfternoonTimeSeconds && _currentTime > MorningTimeSeconds && CurrentDayTime != DayTime.Day)
      {
        CurrentDayTime = DayTime.Day;
      }
      else if (_currentTime <= EveningTimeSeconds && _currentTime > AfternoonTimeSeconds && CurrentDayTime != DayTime.Evening)
      {
        CurrentDayTime = DayTime.Evening;
      }
      else if (_currentTime <= NightTimeSeconds && _currentTime > EveningTimeSeconds && CurrentDayTime != DayTime.Night)
      {
        CurrentDayTime = DayTime.Night;
      }

      TimeLeftReactiveProperty.Value = TimeLeft;
    }
  }
}