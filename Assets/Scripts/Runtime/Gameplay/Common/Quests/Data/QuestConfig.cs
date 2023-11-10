using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace RussSurvivor.Runtime.Gameplay.Common.Quests.Data
{
  [CreateAssetMenu(fileName = "QuestConfig", menuName = "RussSurvivor/Gameplay/Quests/QuestConfig")]
  public class QuestConfig : ScriptableObject
  {
    public string Name;
    [SerializeReference, SubclassSelector] public QuestDescriptionBase Description;
    public string Id;

#if UNITY_EDITOR
    public void Reset()
    {
      if(GUID.TryParse(AssetDatabase.AssetPathToGUID(AssetDatabase.GetAssetPath(this)), out GUID guid))
        Id = guid.ToString();
    }

    private void OnValidate()
    {
      if (string.IsNullOrEmpty(Id))
        Reset();
    }
#endif
  }
} 