using Assets.Scripts.Controls;
using Assets.Scripts.InterfacePanels;
using Assets.Scripts.Rooms;
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

    [CustomEditor(typeof(ReactorControls))]
    public class ReactorControlsTriggerEditor : EventTriggerEditor
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
    
    [CustomEditor(typeof(RepairContent))]
    public class RepairContentEditor : EventTriggerEditor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            base.OnInspectorGUI();
        }
    }
    
    [CustomEditor(typeof(EmployeeContent))]
    public class EmployeeContentEditor : EventTriggerEditor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            base.OnInspectorGUI();
        }
    }
    
    [CustomEditor(typeof(VolumeSlider))]
    public class VolumeSliderEditor : EventTriggerEditor
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

            ArrayGUI(ref _showSprites, "_sprites", "Button Sprites");
            ArrayGUI(ref _showColors, "_colors", "Button Colors");

            serializedObject.ApplyModifiedProperties();
            
            // DrawDefaultInspector();
            
            base.OnInspectorGUI();
        }

        private void ArrayGUI(ref bool foldout, string propertyName, string displayName)
        {
            foldout = EditorGUILayout.Foldout(foldout, displayName);
            if (!foldout)
                return;
            
            EditorGUI.indentLevel++;

            SerializedProperty array = serializedObject.FindProperty(propertyName);

            for (int i = 0; i < array.arraySize; i++)
            {
                EditorGUILayout.PropertyField(array.GetArrayElementAtIndex(i), new GUIContent(_spriteNames[i]), true);
            }
            
            EditorGUI.indentLevel--;
        }
    }
}
