using RussSurvivor.Runtime.Gameplay.Battle.Weapons;
using UnityEditor;
using UnityEngine.UIElements;

namespace RussSurvivor.Editor.PropertyDrawers
{
  [CustomPropertyDrawer(typeof(WeaponDamage))]
  public class WeaponDamagePropertyDrawer : PropertyDrawer
  {
    private WeaponDamage _data;
    private TemplateContainer _root;

    public override VisualElement CreatePropertyGUI(SerializedProperty property)
    {
      var template = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(
        "Assets/Scripts/Editor/PropertyDrawers/WeaponDamage/WeaponDamagePropertyDrawer.uxml");
      _root = template.CloneTree();

      return _root;
    }
  }
}