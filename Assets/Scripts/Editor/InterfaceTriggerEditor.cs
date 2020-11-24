using Assets.Scripts.InterfacePanels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            Debug.Log("onInspector");
            DrawDefaultInspector();

            base.OnInspectorGUI();
        }
    }
}
