using RussSurvivor.Runtime.Gameplay.Town.Dialogues.Data.Actions;
using RussSurvivor.Runtime.Gameplay.Town.Dialogues.Data.Conditions;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace RussSurvivor.Runtime.Gameplay.Town.Dialogues.Data
{
  [CreateAssetMenu(fileName = "Conversation", menuName = "RussSurvivor/Gameplay/Dialogues/Conversation")]
  public class Conversation : ScriptableObject
  {
    public Actor[] Actors;
    public bool IsRepeatable;
    [SerializeReference, SubclassSelector] public ConditionToStartBase[] ConditionsToStart;
    public DialogueEntry[] Entries;
    [SerializeReference, SubclassSelector] public DialogueActionBase[] OnEndActions;
    public string Id;

#if UNITY_EDITOR
    public void Reset()
    {
      if (GUID.TryParse(AssetDatabase.AssetPathToGUID(AssetDatabase.GetAssetPath(this)), out GUID guid))
        Id = guid.ToString();
    }

    private void OnValidate()
    {
      Reset();
    }
#endif
  }
}