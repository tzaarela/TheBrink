using Assets.Scripts.InterfacePanels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    public class Debugger : MonoBehaviour
    {
        public static Debugger instance;

        public List<DebugProp> debugProperties;

        public void Awake()
        {
            if (instance == null)
                instance = this;
        }

        public void Start()
        {
            debugProperties = new List<DebugProp>();
        }

        public void Add(string name, string value)
        {
            debugProperties.Add(new DebugProp(name, value));
        }
    }

    [Serializable]
    public struct DebugProp
    {
        public string name;
        public string value;

        public DebugProp(string name, string value)
        {
            this.name = name;
            this.value = value;
        }
    }
}
