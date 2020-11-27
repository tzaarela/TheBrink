using Assets.Scripts.InterfacePanels;
using UnityEditor;
using UnityEditor.EventSystems;

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
}
