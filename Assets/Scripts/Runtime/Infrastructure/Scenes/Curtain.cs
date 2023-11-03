using UnityEngine;

namespace RussSurvivor.Runtime.Infrastructure.Scenes
{
  public class Curtain : MonoBehaviour, ICurtain
  {
    public void Show()
    {
      gameObject.SetActive(true);
    }

    public void Hide()
    {
      gameObject.SetActive(false);
    }
  }
}