using System;
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
    public Guid Id;
  }
}