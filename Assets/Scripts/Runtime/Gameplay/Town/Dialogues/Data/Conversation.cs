using System;
using RussSurvivor.Runtime.Gameplay.Town.Dialogues.Data.Actions;
using RussSurvivor.Runtime.Gameplay.Town.Dialogues.Data.Conditions;
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
    [SerializeReference, SubclassSelector] public DialogueActionBase[] OnEndActions;
    public Guid Id = Guid.NewGuid();
  }
}