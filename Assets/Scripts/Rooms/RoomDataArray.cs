using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Rooms
{
    [CreateAssetMenu(fileName = "RoomDataArray", menuName = "RoomDataArray")]
    public class RoomDataArray : ScriptableObject
    {
        public List<RoomData> roomsData;
    }
}
