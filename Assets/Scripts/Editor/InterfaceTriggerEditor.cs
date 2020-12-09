using Assets.Scripts.InterfacePanels;
using Assets.Scripts.Systems;
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
        private string[] _spriteNames = new string[5] {"Normal", "Highlight", "Pressed", "Selected", "Disabled"};

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            
            ArrayGUI(serializedObject.FindProperty("_sprites"));
            
            serializedObject.ApplyModifiedProperties();
        }

        private void ArrayGUI(SerializedProperty array)
        {
            EditorGUILayout.LabelField("Button Sprites", new GUILayoutOption[0]);
            EditorGUI.indentLevel++;
            
            for (int i = 0; i < array.arraySize; i++)
            {
                EditorGUILayout.PropertyField(array.GetArrayElementAtIndex(i), new GUIContent(_spriteNames[i]), true);
            }
            
            EditorGUI.indentLevel--;
        }
    }
}
