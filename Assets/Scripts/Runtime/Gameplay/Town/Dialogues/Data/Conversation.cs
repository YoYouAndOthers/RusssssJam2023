using System;
using UnityEngine;

namespace RussSurvivor.Runtime.Gameplay.Town.Dialogues.Data
{
  [CreateAssetMenu(fileName = "Conversation", menuName = "RussSurvivor/Gameplay/Dialogues/Conversation")]
  public class Conversation : ScriptableObject
  {
    public Actor[] Actors;
    public bool IsRepeatable;
    [SerializeReference, SubclassSelector] public ConditionToStartBase[] ConditionsToStart;
    public DialogueEntry[] Entries;
    public Guid Id = Guid.NewGuid();
  }
}