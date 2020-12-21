using Assets.Scripts.InterfacePanels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    public class Debugger : MonoBehaviour
    {
        public static Debugger instance;

        public List<DebugProp> LifeSupport;
        public List<DebugProp> Reactor;
        public List<DebugProp> MainFrame;
        public List<DebugProp> Bridge;
        public List<DebugProp> Cargo;
        public List<DebugProp> Corridor;
        public List<DebugProp> Medbay;
        public List<DebugProp> MainBattery;

        public Dictionary<string, List<DebugProp>> debugProps;

        public bool isSetup;

        public void Awake()
        {
            if (instance == null)
                instance = this;
        }

        public void Start()
        {
            Reactor = new List<DebugProp>();
            LifeSupport = new List<DebugProp>();
            MainFrame = new List<DebugProp>();
            Bridge = new List<DebugProp>();
            Cargo = new List<DebugProp>();
            Corridor = new List<DebugProp>(); 
            Medbay = new List<DebugProp>();
            MainBattery = new List<DebugProp>();

            debugProps = new Dictionary<string, List<DebugProp>>();
            debugProps.Add("LifeSupportSystem", LifeSupport);
            debugProps.Add("BridgeSystem", Bridge);
            debugProps.Add("CorridorSystem", Corridor);
            debugProps.Add("MainBatterySystem", MainBattery);
            debugProps.Add("MainframeSystem", MainFrame);
            debugProps.Add("MedbaySystem", Medbay);
            debugProps.Add("ReactorSystem", Reactor);
            debugProps.Add("CargoHoldSystem", Cargo);
        }

        public void DebugPropertyValues(object obj)
        {
            Type type = obj.GetType();
            PropertyInfo[] props = type.GetProperties();
            foreach (PropertyInfo prop in props)
            {
                if (!isSetup)
                {
                    debugProps[type.Name].Add(new DebugProp(prop.Name, prop.GetValue(obj).ToString()));
                }
                else
                {
                    var debugProperties = debugProps[type.Name];

                    for (int i = 0; i < debugProperties.Count; i++)
                    {
                        var property = debugProperties[i];
                        if (property.name == prop.Name)
                        {
                            property.value = prop.GetValue(obj).ToString();
                        }
                        debugProperties[i] = property;
                    }

                    debugProps[type.Name] = debugProperties;
                }
            }
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
