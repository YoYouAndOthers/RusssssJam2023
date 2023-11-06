using System;
using UnityEngine;

namespace RussSurvivor.Runtime.Gameplay.Town.Dialogues.Data.Actions
{
  [Serializable]
  public class PlayActorAnimation : DialogueActionBase
  {
    public string name;
    public GameObject AnimationPrefab;
  }
}