using RussSurvivor.Runtime.Gameplay.Town.Dialogues.Models;
using TMPro;
using UnityEngine;

namespace RussSurvivor.Runtime.UI.Gameplay.Town.Dialogues
{
  public class DialoguePhraseUi : MonoBehaviour
  {
    [SerializeField] private TextMeshProUGUI _actorNameText;
    [SerializeField] private TextMeshProUGUI _dialogueText;

    public void SetActor(string actorName)
    {
      _actorNameText.text = actorName;
    }

    public void SetText(DialogueEntryModel dialogueEntryModel)
    {
      _dialogueText.text = dialogueEntryModel.Text;
    }
  }
}