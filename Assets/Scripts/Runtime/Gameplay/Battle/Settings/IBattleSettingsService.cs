using System;
using System.Collections.Generic;
using Zenject;

namespace RussSurvivor.Runtime.Gameplay.Battle.Settings
{
  public interface IBattleSettingsService : IInitializable
  {
    BattleSettings SettingsForBattle { get; }
  }

  public class BattleSettingsService : IBattleSettingsService
  {
    public BattleSettings SettingsForBattle { get; private set; } = 
      BattleSettings.WithWaves(BattleSettings.DefaultEmpty, new []
      {
        new EnemyWaveSettings(new Dictionary<Guid, int>()
        {
          
        })
      });

    public BattleSettingsService()
    {
      
    }

    public void Initialize()
    {
    }
  }
}