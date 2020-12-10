using Assets.Scripts.InterfacePanels;
using Assets.Scripts.Systems;
using Unity.Collections;
using UnityEditor;
using UnityEditor.EventSystems;
using UnityEngine;

namespace Assets.Scripts.Editor
{
    [CustomEditor(typeof(ContextMenuItem))]
    public class InterfaceTriggerEditor : EventTriggerEditor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            base.OnInspectorGUI();
        }
    }

    [CustomEditor(typeof(Room))]
    public class RoomTriggerEditor : EventTriggerEditor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            base.OnInspectorGUI();
        }
    }

    [CustomEditor(typeof(CrewMember))]
    public class CrewTriggerEditor : EventTriggerEditor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            base.OnInspectorGUI();
        }
    }

    [CustomEditor(typeof(CrewPanelObject))]
    public class CrewPanelTriggerEditor : EventTriggerEditor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            base.OnInspectorGUI();
        }
    }

    [CustomEditor(typeof(SystemTab))]
    public class SystemTabTriggerEditor : EventTriggerEditor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            base.OnInspectorGUI();
        }
    }
    
    [CustomEditor(typeof(SpaceportTabObject))]
    public class SpaceportTabObjectEditor : EventTriggerEditor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            base.OnInspectorGUI();
        }
    }
    
    [CustomEditor(typeof(UIButton))]
    public class UIButtonEditor : EventTriggerEditor
    {
        private SerializedProperty _spriteArray;
        private SerializedProperty _colorArray;
        private string[] _spriteNames = new string[5] {"Normal", "Highlight", "Pressed", "Selected", "Disabled"};
        private bool _showSprites;
        private bool _showColors;

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            
            _showSprites = EditorGUILayout.Foldout(_showSprites, "Button Sprites");
            if (_showSprites)
                ArrayGUI(serializedObject.FindProperty("_sprites"));

            _showColors = EditorGUILayout.Foldout(_showColors, "Button Colors");
            if (_showColors)
                ArrayGUI(serializedObject.FindProperty("_colors"));
            
            serializedObject.ApplyModifiedProperties();
        }

        private void ArrayGUI(SerializedProperty array)
        {
            EditorGUI.indentLevel++;
            
            // Debug.Log($"{array.name}.arraySize: {array.arraySize}");
            
            for (int i = 0; i < array.arraySize; i++)
            {
                EditorGUILayout.PropertyField(array.GetArrayElementAtIndex(i), new GUIContent(_spriteNames[i]), true);
            }
            
            EditorGUI.indentLevel--;
        }
    }
}
