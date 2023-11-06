using System;
using UnityEngine;

namespace RussSurvivor.Runtime.Gameplay.Town.Dialogues.Data
{
  [CreateAssetMenu(fileName = "Actor", menuName = "RussSurvivor/Gameplay/Dialogues/Actor")]
  public class Actor : ScriptableObject
  {
    public string Name;
    public bool IsPlayer;
    public bool IsTrader;
    public Sprite Icon;
    public string Description;
    public GameObject DefaultAnimation;
    public Guid Id = Guid.NewGuid();
  }
}