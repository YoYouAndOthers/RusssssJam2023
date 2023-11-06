using Zenject;

namespace RussSurvivor.Runtime.Gameplay.Town.Economics.Trade
{
  public interface ITraderService : IInitializable
  {
    void StartTrade();
  }
}