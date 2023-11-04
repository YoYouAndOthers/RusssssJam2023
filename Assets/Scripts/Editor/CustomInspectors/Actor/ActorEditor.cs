using System;
using RussSurvivor.Runtime.Gameplay.Town.Dialogues.Data;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace RussSurvivor.Editor.CustomInspectors
{
  [CustomEditor(typeof(Actor))]
  public class ActorEditor : UnityEditor.Editor
  {
    private TemplateContainer _root;
    private Actor _data;
    private bool _callbackRegistered;

    public override VisualElement CreateInspectorGUI()
    {
      var template = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(
        "Assets/Scripts/Editor/CustomInspectors/Actor/ActorEditor.uxml");
      _root = template.CloneTree();
      _data = target as Actor;
      if (_data == null)
        return _root;

      if (_data.Id == Guid.Empty)
      {
        EditorUtility.SetDirty(_data);
        _data.Id = Guid.NewGuid();
        AssetDatabase.SaveAssets();
      }

      _root.Q<Label>("Id").text = $"Id: {_data.Id.ToString()}";

      Sprite icon = _data.Icon;
      _root.Q<IMGUIContainer>().onGUIHandler = () =>
      {
        if (icon == null)
          return;
        Rect rect = GUILayoutUtility.GetRect(80, 80, 80, 80);
        GUI.DrawTexture(rect, icon.texture);
      };
      
      if(!_callbackRegistered)
        _root.Q<PropertyField>("Icon").RegisterValueChangeCallback(
          newEvt =>
          {
            _root.Q<IMGUIContainer>().onGUIHandler = () =>
            {
              if (_data.Icon == null)
                return;
              Rect rect = GUILayoutUtility.GetRect(80, 80, 80, 80);
              GUI.DrawTexture(rect, _data.Icon.texture);
            };
          });

      return _root;
    }
  }
}