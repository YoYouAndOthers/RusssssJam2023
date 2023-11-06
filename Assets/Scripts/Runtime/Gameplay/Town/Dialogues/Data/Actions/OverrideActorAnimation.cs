using System;
using UnityEngine;

namespace RussSurvivor.Runtime.Gameplay.Town.Dialogues.Data.Actions
{
  [Serializable]
  public class OverrideActorAnimation : DialogueActionBase
  {
    public string name = "Сыграть нетипичную анимацию";
    public GameObject AnimationPrefab;
  }
}