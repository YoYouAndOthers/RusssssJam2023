using System;
using UnityEngine;

namespace RussSurvivor.Runtime.Gameplay.Common.Quests.Data
{
  [CreateAssetMenu(fileName = "QuestConfig", menuName = "RussSurvivor/Gameplay/Quests/QuestConfig")]
  public class QuestConfig : ScriptableObject
  {
    public Guid Id = Guid.NewGuid();
    public string Name;
    [SerializeReference, SubclassSelector] public QuestDescriptionBase Description;
  }
}