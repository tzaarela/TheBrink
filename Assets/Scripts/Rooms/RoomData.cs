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
        public SystemType systemType;
        public float health;
        private const float MaxHealth = 100f;
        [Header("DEBUG")]
        [SerializeField] private int _defaultHealth = 50;
        [SerializeField] private bool _beginWithDefault;

        private void OnEnable()
        {
            if (_beginWithDefault)
                Reset();
        }

        public void Reset()
        {
            health = _defaultHealth;
        }

        public void SetFullHealth()
        {
            health = MaxHealth;
        }
    }
}
