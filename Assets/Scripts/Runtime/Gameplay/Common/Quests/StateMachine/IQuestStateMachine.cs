using UniRx;

namespace RussSurvivor.Runtime.Gameplay.Common.Quests.StateMachine
{
  public interface IQuestStateMachine
  {
    IReactiveProperty<QuestState> CurrentState { get; }
    void StartNewQuest(string questId);
    void CompleteCurrentQuest();
    void NextState<T>() where T : QuestState;
  }
}