using System;
using RussSurvivor.Runtime.Gameplay.Battle.Weapons;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace RussSurvivor.Editor.CustomInspectors
{
  [CustomEditor(typeof(WeaponConfig))]
  public class WeaponConfigEditor : UnityEditor.Editor
  {
    private TemplateContainer _root;
    private WeaponConfig _data;

    public override VisualElement CreateInspectorGUI()
    {
      var template = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(
        "Assets/Scripts/Editor/CustomInspectors/WeaponConfig/WeaponConfigEditor.uxml");
      _root = template.CloneTree();
      _data = target as WeaponConfig;

      if (_data == null)
        return _root;
      if (_data.Id == Guid.Empty)
      {
        EditorUtility.SetDirty(_data);
        _data.Id = Guid.NewGuid();
        AssetDatabase.SaveAssets();
      }

      _root.Q<Label>("Id").text = _data.Id.ToString();

      _root.Q<PropertyField>("Damage").RegisterValueChangeCallback(
        newEvt =>
        {
          ApplyDamageTypeFields();
        });
      _root.Q<PropertyField>("Damage").schedule.Execute(ApplyDamageTypeFields);

      _root.Q<PropertyField>("DamageDirectionType").RegisterValueChangeCallback(
        newEvt =>
        {
          ApplyDamageDirection();
        });
      _root.Q<PropertyField>("DamageDirectionType").schedule.Execute(ApplyDamageDirection);
      _root.Q<LayerMaskField>("DamagableLayers").value = _data.DamagableLayers;
      _root.Q<LayerMaskField>("DamagableLayers").RegisterValueChangedCallback(
        newEvt =>
        {
          EditorUtility.SetDirty(_data);
          _data.DamagableLayers = newEvt.newValue;
          AssetDatabase.SaveAssets();
        });
      
      return _root;
    }

    private void ApplyDamageDirection()
    {
      if (_data.DamageDirectionType is
          DamageDirectionType.AoEByMovement or
          DamageDirectionType.AoEOnClosest or
          DamageDirectionType.AoEOnRandom)
      {
        _root.Q<PropertyField>("BaseSize").style.display = DisplayStyle.Flex;
      }
      else
      {
        _root.Q<PropertyField>("BaseSize").style.display = DisplayStyle.None;
      }

      if (_data.DamageDirectionType is
          DamageDirectionType.ByMovement or
          DamageDirectionType.RandomDirection or
          DamageDirectionType.ClosestToUser)
      {
        _root.Q<PropertyField>("Reach").style.display = DisplayStyle.Flex;
      }
      else
      {
        _root.Q<PropertyField>("Reach").style.display = DisplayStyle.None;
      }
    }

    private void ApplyDamageTypeFields()
    {
      if (_data.Damage.DamageApplyType is DamageApplyType.OverTimePercents or DamageApplyType.OverTimeValue)
      {
        _root.Q<PropertyField>("DamageOverTimeRate").style.display = DisplayStyle.Flex;
        _root.Q<PropertyField>("DamageOverTimeDuration").style.display = DisplayStyle.Flex;
      }
      else
      {
        if (_root.Q<PropertyField>("DamageOverTimeRate") == null)
          return;
        _root.Q<PropertyField>("DamageOverTimeRate").style.display = DisplayStyle.None;
        _root.Q<PropertyField>("DamageOverTimeDuration").style.display = DisplayStyle.None;
      }
    }
  }
}