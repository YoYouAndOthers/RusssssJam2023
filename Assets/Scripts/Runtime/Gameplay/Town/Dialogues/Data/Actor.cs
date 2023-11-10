using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace RussSurvivor.Runtime.Gameplay.Town.Dialogues.Data
{
  [CreateAssetMenu(fileName = "Actor", menuName = "RussSurvivor/Gameplay/Dialogues/Actor")]
  public class Actor : ScriptableObject
  {
    public string Name;
    public bool IsPlayer;
    public bool IsTrader;
    public Sprite Icon;
    public string Description;
    public GameObject DefaultAnimation;
    public string Id;

#if UNITY_EDITOR
    public void Reset()
    {
      if (GUID.TryParse(AssetDatabase.AssetPathToGUID(AssetDatabase.GetAssetPath(this)), out GUID guid))
        Id = guid.ToString();
    }

    private void OnValidate() =>
      Reset();
#endif
  }
}