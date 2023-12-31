using System;
using RussSurvivor.Runtime.Gameplay.Town.Dialogues.Data.Actions;
using UnityEngine;

namespace RussSurvivor.Runtime.Gameplay.Town.Dialogues.Data
{
  [Serializable]
  public struct DialogueEntry
  {
    public string name;
    public Actor Speaker;
    public string Text;
    [SerializeReference, SubclassSelector] public DialogueActionBase[] Actions;
    public string Id;
  }
}