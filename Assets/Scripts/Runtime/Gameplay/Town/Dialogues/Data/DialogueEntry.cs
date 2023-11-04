using System;
using UnityEngine;

namespace RussSurvivor.Runtime.Gameplay.Town.Dialogues.Data
{
  public class DialogueEntry : ScriptableObject
  {
    public Guid Id = Guid.NewGuid();
    public Conversation Conversation;
    public Actor Actor;
    public string Text;
  }
}