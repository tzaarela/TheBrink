using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Tweening.Animations
{
    public class RunSequence : MonoBehaviour
    {
        [SerializeField]
        private TextSequence textSequence;

        public void Start()
        {
            Execute();
        }

        public void Execute()
        {
            textSequence.ExecuteSequence();
        }
    }
}
