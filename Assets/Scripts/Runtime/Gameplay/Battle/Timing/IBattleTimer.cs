using RussSurvivor.Runtime.Gameplay.Common.Timing;
using Zenject;

namespace RussSurvivor.Runtime.Gameplay.Battle.Timing
{
  public interface IBattleTimer : ICooldownUpdatable, IInitializable
  {
  }
}