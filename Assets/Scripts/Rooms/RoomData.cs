using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Rooms
{
    [CreateAssetMenu(fileName = "RoomData", menuName = "RoomData")]
    public class RoomData : ScriptableObject
    {
        public float health;
        public RoomType roomType;
    }
}
