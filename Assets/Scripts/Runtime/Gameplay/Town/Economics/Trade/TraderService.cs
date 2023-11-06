using UnityEngine;

namespace RussSurvivor.Runtime.Gameplay.Town.Economics.Trade
{
  public class TraderService : ITraderService
  {
    public void Initialize()
    {
      Debug.Log("Trader service initialized");
    }

    public void StartTrade()
    {
      Debug.Log("Trade started");
    }
  }
}