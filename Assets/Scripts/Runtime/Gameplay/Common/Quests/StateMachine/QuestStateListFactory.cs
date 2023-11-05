using System;
using System.Collections.Generic;
using RussSurvivor.Runtime.Gameplay.Common.Quests.Data;
using RussSurvivor.Runtime.Gameplay.Town.Characters;

namespace RussSurvivor.Runtime.Gameplay.Common.Quests.StateMachine
{
  public class QuestStateListFactory : IQuestStateListFactory
  {
    private readonly IActorRegistry _actorRegistry;

    public QuestStateListFactory(IActorRegistry actorRegistry) =>
      _actorRegistry = actorRegistry;

    public List<QuestState> Create(Guid questId, QuestDescriptionBase description)
    {
      var states = new List<QuestState>();
      if (description is CollectItemsQuestDescription collectingQuest)
      {
        states.Add(new CollectingQuestState(questId, collectingQuest));
        states.Add(new TalkToNpcQuestState(questId, collectingQuest.BringTo.Id, _actorRegistry));
        states.Add(new ReturnToTownQuestState(questId));
        return states;
      }

      if (description is TalkToNpcQuestDescription talkingQuest)
      {
        states.Add(new TalkToNpcQuestState(questId, talkingQuest.NpcToTalkTo.Id, _actorRegistry));
        states.Add(new ReturnToTownQuestState(questId));
        return states;
      }
      
      if (description is DestructionQuestDescription destructionQuest)
      {
        states.Add(new GoOutsideQuestState(questId));
        states.Add(new DestroyNpcQuestState(questId, destructionQuest.HostileObject));
        states.Add(new ReturnToTownQuestState(questId));
        states.Add(new TalkToNpcQuestState(questId, destructionQuest.QuestRewardGiver.Id, _actorRegistry));
        return states;
      }

      return states;
    }
  }
}