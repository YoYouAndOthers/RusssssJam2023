using System;
using UnityEngine;

namespace RussSurvivor.Runtime.Gameplay.Town.Dialogues.Data
{
  public class Conversation : ScriptableObject
  {
    public Guid Id = Guid.NewGuid();
    public Actor[] Actors;
    public DialogueEntry[] Entries;
  }
}