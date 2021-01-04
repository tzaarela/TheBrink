using Assets.Scripts.InterfacePanels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Controls
{
    public class LifeSupportControls : UITrigger, IControls
    {
        public IShipSystem shipSystem { get; set; }

        public void Init()
        {
            //throw new NotImplementedException();
        }

        public void UpdateUI()
        {
            //throw new NotImplementedException();
        }
    }
}
