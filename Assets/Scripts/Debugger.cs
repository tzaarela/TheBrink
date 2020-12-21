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
            MainFrame = new List<DebugProp>(); ;
            Bridge = new List<DebugProp>(); ;
            Cargo = new List<DebugProp>(); ;
            Corridor = new List<DebugProp>(); ;
            Medbay = new List<DebugProp>(); ;
            MainBattery = new List<DebugProp>(); ;

            debugProps = new Dictionary<string, List<DebugProp>>();

            debugProps.Add("LifeSupportSystem", Reactor);
            debugProps.Add("BridgeSystem", LifeSupport);
            debugProps.Add("CorridorSystem", MainFrame);
            debugProps.Add("MainBatterySystem", Bridge);
            debugProps.Add("MainframeSystem", Cargo);
            debugProps.Add("MedbaySystem", Corridor);
            debugProps.Add("ReactorSystem", Medbay);
            debugProps.Add("CargoHoldSystem", MainBattery);
        }

        public void DebugPropertyValues(object obj)
        {
            Type t = obj.GetType();
            PropertyInfo[] props = t.GetProperties();
            foreach (PropertyInfo prop in props)
            {
                if (!isSetup)
                {
                    debugProps[t.Name].Add(new DebugProp(prop.Name, prop.GetValue(obj).ToString()));
                }
                else
                {
                    for (int i = 0; i < debugProps[t.Name].Count; i++)
                    {
                        var debugProp = debugProps[t.Name][i];

                        if (debugProp.name == prop.Name)
                            debugProp.value = prop.GetValue(obj).ToString();
                    }
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
