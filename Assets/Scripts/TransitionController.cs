using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    public class TransitionController : ScriptableObject
    {
        public static TransitionController Instance;

        public void Awake()
        {
            if(Instance == null)
            {
                Instance = this;
            }
            else if (Instance != this)
            {
                Destroy(this);
            }
        }


        public void RunTransitionAnimation(object animation)
        {
            
        }
    }
}
