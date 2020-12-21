using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Controls
{
    interface BaseControl
    {
        float AirLevel { get; set; }
        float EnergyLevel { get; set; }

    }
}
