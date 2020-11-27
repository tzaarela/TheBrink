using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using TMPro;

namespace Assets.Scripts.Systems
{
    public class SystemTabController : MonoBehaviour
    {
        List<SystemTab> systemTabs;

        [SerializeField]
        TextMeshProUGUI systemWindowText;

        public void Start()
        {
            systemTabs = gameObject.GetComponentsInChildren<SystemTab>().ToList();
        }

        public void DeselectActive()
        {
            var tab = systemTabs.FirstOrDefault(x => x.IsSelected);

            if (tab != null)
                tab.IsSelected = false;
        }

        public void Select(SystemTab systemTab)
        {
            systemTab.IsSelected = true;

            //Display System Window
            systemWindowText.text = systemTab.name;
        }
    }
}
