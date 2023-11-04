using System;
using UnityEngine;

namespace RussSurvivor.Runtime.Gameplay.Town.Dialogues.Data
{
  [CreateAssetMenu(fileName = "Actor", menuName = "RussSurvivor/Gameplay/Dialogues/Actor")]
  public class Actor : ScriptableObject
  {
    public Guid Id = Guid.NewGuid();
    public string Name;
    public bool IsPlayer;
    public Sprite Icon;
    public string Description;
  }
}