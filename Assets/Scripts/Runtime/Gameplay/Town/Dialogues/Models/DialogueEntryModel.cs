using System;
using RussSurvivor.Runtime.Gameplay.Town.Dialogues.Data;

namespace RussSurvivor.Runtime.Gameplay.Town.Dialogues.Models
{
  public class DialogueEntryModel
  {
    public string Text { get; }
    public string ActorName { get; }

    public bool IsPlayer { get; set; }
    public static DialogueEntryModel Empty => new();

    public DialogueEntryModel(DialogueEntry data)
    {
      ActorName = data.Speaker.Name;
      Text = data.Text;
      IsPlayer = data.Speaker.IsPlayer;
    }

    private DialogueEntryModel()
    {
      ActorName = string.Empty;
      Text = string.Empty;
    }

    public override bool Equals(object obj)
    {
      return obj is DialogueEntryModel other && Equals(other);
    }

    public override int GetHashCode()
    {
      return HashCode.Combine(Text, ActorName);
    }

    private bool Equals(DialogueEntryModel other)
    {
      return Text == other.Text && ActorName == other.ActorName;
    }
  }
}