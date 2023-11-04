using System;
using UnityEngine;

namespace RussSurvivor.Runtime.Gameplay.Common.Quests.Data
{
  [CreateAssetMenu(fileName = "QuestConfig", menuName = "RussSurvivor/Gameplay/Quests/QuestConfig")]
  public class QuestConfig : ScriptableObject
  {
    public string Name;
    [SerializeReference, SubclassSelector] public QuestDescriptionBase Description;
    public Guid Id = Guid.NewGuid();
  }
}