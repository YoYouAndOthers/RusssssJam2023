using System;
using System.Collections.Generic;
using System.Linq;
using RussSurvivor.Runtime.Gameplay.Town.Dialogues;
using RussSurvivor.Runtime.Gameplay.Town.Dialogues.Data;
using RussSurvivor.Runtime.Gameplay.Town.Dialogues.Models;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace RussSurvivor.Runtime.UI.Gameplay.Town.Dialogues
{
  public class DialogueEntryPresenter : MonoBehaviour, IInitializable, IDisposable
  {
    private readonly List<IDisposable> _disposables = new();

    [SerializeField] private DialoguePhraseUi _playerDialoguePhraseUi;
    [SerializeField] private DialoguePhraseUi _npcDialoguePhraseUi;
    [SerializeField] private Button _nextPhraseButton;
    [SerializeField] private Button _finishConversationButton;
    [SerializeField] private Image _playerIconImage;
    [SerializeField] private Image _npcIconImage;

    private IDialogueSystem _dialogueSystem;

    [Inject]
    private void Construct(IDialogueSystem dialogueSystem)
    {
      _dialogueSystem = dialogueSystem;
    }

    public void Dispose()
    {
      foreach (IDisposable disposable in _disposables)
        disposable.Dispose();
      _disposables.Clear();
    }

    public void Initialize()
    {
      _nextPhraseButton
        .OnClickAsObservable()
        .Subscribe(k => _dialogueSystem.NextPhrase())
        .AddTo(_disposables);
      _finishConversationButton
        .OnClickAsObservable()
        .Subscribe(k =>
        {
          if (!_dialogueSystem.HasNextDialogueEntry.Value)
            _dialogueSystem.FinishConversation();
          else
            Debug.Log("Cancel conversation");
        })
        .AddTo(_disposables);

      _dialogueSystem.IsConversationActive
        .ObserveEveryValueChanged(k => k.Value)
        .Subscribe(ShowDialogue)
        .AddTo(_disposables);
      _dialogueSystem.HasNextDialogueEntry
        .ObserveEveryValueChanged(k => k.Value)
        .Subscribe(k => _nextPhraseButton.gameObject.SetActive(k))
        .AddTo(_disposables);
      _dialogueSystem.CurrentDialogueEntry
        .ObserveEveryValueChanged(k => k.Value)
        .Subscribe(SetDialogueEntryView)
        .AddTo(_disposables);
    }

    private void SetDialogueEntryView(DialogueEntryModel dialogueEntryModel)
    {
      _playerDialoguePhraseUi.gameObject.SetActive(dialogueEntryModel.IsPlayer);
      _npcDialoguePhraseUi.gameObject.SetActive(!dialogueEntryModel.IsPlayer);

      SetPhraseUi(dialogueEntryModel, dialogueEntryModel.IsPlayer ? _playerDialoguePhraseUi : _npcDialoguePhraseUi);
    }

    private static void SetPhraseUi(DialogueEntryModel dialogueEntryModel, DialoguePhraseUi playerDialoguePhraseUi)
    {
      playerDialoguePhraseUi.SetText(dialogueEntryModel);
    }

    private void ShowDialogue(bool isConversationActive)
    {
      gameObject.SetActive(isConversationActive);
      if (!isConversationActive)
        return;
      Actor playerActor = _dialogueSystem.CurrentConversation.Actors.FirstOrDefault(k => k.IsPlayer);
      Actor npcActor = _dialogueSystem.CurrentConversation.Actors.FirstOrDefault(k => !k.IsPlayer);

      if (playerActor == null || npcActor == null)
        return;

      _playerIconImage.sprite = playerActor.Icon;
      _npcIconImage.sprite = npcActor.Icon;
      _playerDialoguePhraseUi.SetActor(playerActor.Name);
      _npcDialoguePhraseUi.SetActor(npcActor.Name);
    }
  }
}