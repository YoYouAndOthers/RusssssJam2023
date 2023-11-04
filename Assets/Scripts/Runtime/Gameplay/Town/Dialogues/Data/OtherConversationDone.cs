using System;

namespace RussSurvivor.Runtime.Gameplay.Town.Dialogues.Data
{
  [Serializable]
  public sealed class OtherConversationDone : ConditionToStartBase
  {
    public string name = "OtherConversationDone";
    public Conversation Conversation;

    public override bool IsMet()
    {
      return false;
    }
  }
}