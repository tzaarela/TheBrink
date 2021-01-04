using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Controls
{
    interface IControls
    {
        IShipSystem shipSystem { get; set; }
        void UpdateUI();
        void Init();
    }
}
