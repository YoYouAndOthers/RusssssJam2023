using System;

namespace RussSurvivor.Runtime.Gameplay.Town.Dialogues.Data.Conditions
{
  [Serializable]
  public sealed class OtherConversationDone : ConditionToStartBase
  {
    public string name = "OtherConversationDone";
    public Conversation Conversation;
  }
}