using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace Assets.Scripts
{
    [CustomEditor(typeof(Debugger))]
    [CanEditMultipleObjects]
    public class LookAtPointEditor : Editor
    {
        List<SerializedProperty> properties;

        void OnEnable()
        {
            properties = new List<SerializedProperty>();
            properties.Add(serializedObject.FindProperty("LifeSupport"));
            properties.Add(serializedObject.FindProperty("Reactor"));
            properties.Add(serializedObject.FindProperty("Medbay"));
            properties.Add(serializedObject.FindProperty("MainFrame"));
            properties.Add(serializedObject.FindProperty("MainBattery"));
            properties.Add(serializedObject.FindProperty("Bridge"));
        }

        public override void OnInspectorGUI()
        {
            GUIStyle style = new GUIStyle();
            style.fontStyle = FontStyle.Bold;
            style.richText = true;

            serializedObject.Update();
            for (int i = 0; i < properties.Count; i++)
            {
                EditorGUILayout.LabelField(properties[i].displayName, style);
                for (int j = 0; j < properties[i].arraySize; j++)
                {
                    var name = properties[i].GetArrayElementAtIndex(j).FindPropertyRelative("name");
                    var value = properties[i].GetArrayElementAtIndex(j).FindPropertyRelative("value");
                    EditorGUILayout.LabelField($"{name.stringValue}: {value.stringValue}");

                }

                EditorGUILayout.Separator();

            }
            serializedObject.ApplyModifiedProperties();

        }
    }
}
