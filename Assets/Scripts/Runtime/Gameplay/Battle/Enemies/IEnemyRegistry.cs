﻿namespace RussSurvivor.Runtime.Gameplay.Battle.Enemies
{
  public interface IEnemyRegistry
  {
    bool AllEnemiesDead { get; }
    bool NoBoss { get; }
  }
}