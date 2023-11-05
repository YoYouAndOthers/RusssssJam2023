using RussSurvivor.Runtime.Gameplay.Common.Cinema;
using RussSurvivor.Runtime.Gameplay.Common.Quests;
using RussSurvivor.Runtime.Gameplay.Common.Quests.Data;
using RussSurvivor.Runtime.Gameplay.Common.Quests.StateMachine;
using RussSurvivor.Runtime.Infrastructure.Installers;
using RussSurvivor.Runtime.Infrastructure.Scenes;
using UnityEngine.SceneManagement;

namespace RussSurvivor.Runtime.Gameplay.Common.Transitions
{
  public class GameplayTransitionService : IGameplayTransitionService
  {
    private readonly ISceneLoader _sceneLoader;
    private readonly ICurtain _curtain;
    private readonly CameraFollower _cameraFollower;
    private readonly IQuestStateMachine _questStateMachine;

    public SceneEntrance.SceneName CurrentScene { get; set; }
    private readonly IQuestRegistry _questRegistry;

    public GameplayTransitionService(
      ISceneLoader sceneLoader,
      ICurtain curtain,
      CameraFollower cameraFollower,
      IQuestRegistry questRegistry,
      IQuestStateMachine questStateMachine)
    {
      _sceneLoader = sceneLoader;
      _curtain = curtain;
      _cameraFollower = cameraFollower;
      _questRegistry = questRegistry;
      _questStateMachine = questStateMachine;
    }

    public async void GoThroughGates()
    {
      _curtain.Show();
      _cameraFollower.Disable();
      switch (CurrentScene)
      {
        case SceneEntrance.SceneName.Battle:
          await _sceneLoader.UnloadSceneAsync(SceneEntrance.SceneName.Battle);
          await _sceneLoader.LoadSceneAsync(SceneEntrance.SceneName.Town, LoadSceneMode.Additive);
          ChangeStateToTownOne();
          break;
        case SceneEntrance.SceneName.Town:
          await _sceneLoader.UnloadSceneAsync(SceneEntrance.SceneName.Town);
          await _sceneLoader.LoadSceneAsync(SceneEntrance.SceneName.Battle, LoadSceneMode.Additive);
          ChangeStateToBattleOne();
          break;
      }
    }

    private void ChangeStateToBattleOne()
    {
      QuestDescriptionBase questDescription =
        _questRegistry.GetQuestConfig(_questStateMachine.CurrentState.Value.QuestId).Description;
      switch (questDescription)
      {
        case TalkToNpcQuestDescription:
          _questStateMachine.NextState<ReturnToTownQuestState>();
          return;
        case CollectItemsQuestDescription:
        {
          if (_questStateMachine.CurrentState.Value is TalkToNpcQuestState)
            _questStateMachine.NextState<ReturnToTownQuestState>();
          return;
        }
      }
    }

    private void ChangeStateToTownOne()
    {
      QuestDescriptionBase questDescription =
        _questRegistry.GetQuestConfig(_questStateMachine.CurrentState.Value.QuestId).Description;
      switch (questDescription)
      {
        case TalkToNpcQuestDescription:
          _questStateMachine.NextState<TalkToNpcQuestState>();
          return;
        case CollectItemsQuestDescription:
        {
          switch (_questStateMachine.CurrentState.Value)
          {
            case ReturnToTownQuestState:
              _questStateMachine.NextState<TalkToNpcQuestState>();
              return;
          }

          return;
        }
      }
    }
  }
}