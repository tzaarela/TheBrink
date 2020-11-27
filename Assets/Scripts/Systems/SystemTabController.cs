using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Systems
{
    public class SystemTabController : MonoBehaviour
    {
        List<SystemTab> systemTabs;

        public void Start()
        {
            systemTabs = gameObject.GetComponentsInChildren<SystemTab>().ToList();
        }

        public void Deselect()
        {
            var tab = systemTabs.FirstOrDefault(x => x.IsSelected);

            if (tab != null)
                tab.IsSelected = false;
        }
    }
}
