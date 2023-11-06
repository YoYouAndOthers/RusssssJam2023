using System;
using System.Collections.Generic;
using System.Linq;
using RussSurvivor.Runtime.Gameplay.Town.Dialogues;
using RussSurvivor.Runtime.Gameplay.Town.Dialogues.Data;
using RussSurvivor.Runtime.Gameplay.Town.Dialogues.Data.Actions;
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
    private IInstantiator _instantiator;

    [Inject]
    private void Construct(IInstantiator instantiator, IDialogueSystem dialogueSystem)
    {
      _instantiator = instantiator;
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
            _dialogueSystem.CancelConversation();
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
      _dialogueSystem.NpcActor
        .ObserveEveryValueChanged(k => k.Value)
        .Subscribe(ChangeNpcAnimation)
        .AddTo(_disposables);
      _dialogueSystem.PlayerActor
        .ObserveEveryValueChanged(k => k.Value)
        .Subscribe(ChangePlayerAnimation)
        .AddTo(_disposables);
    }

    private void ChangeNpcAnimation(ActorModel actorModel)
    {
      Debug.Log($"ChangeNpcAnimation {actorModel.AnimationPrefab}");
      if (actorModel.AnimationPrefab != null)
      {
        CleanAnimationPrefabs();
        GameObject animationGo =
          _instantiator.InstantiatePrefab(actorModel.AnimationPrefab, _npcIconImage.transform);
      }
    }

    private void ChangePlayerAnimation(ActorModel actorModel)
    {
      Debug.Log($"ChangePlayerAnimation {actorModel.AnimationPrefab}");
      if (actorModel.AnimationPrefab != null)
      {
        CleanAnimationPrefabs();
        GameObject animationGo =
          _instantiator.InstantiatePrefab(actorModel.AnimationPrefab, _playerIconImage.transform);
      }
    }

    private void CleanAnimationPrefabs()
    {
      foreach (Transform child in _playerIconImage.transform)
        Destroy(child.gameObject);
      foreach (Transform child in _npcIconImage.transform)
        Destroy(child.gameObject);
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