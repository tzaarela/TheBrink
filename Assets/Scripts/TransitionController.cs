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
        private static TransitionController _instance;
        public static TransitionController Instance { 
            get
            {
                if (_instance == null)
                {
                    _instance = (TransitionController)CreateInstance(typeof(TransitionController));
                }
                
                return _instance;
            }
            private set { }
        }

        public void Awake()
        {
           
        }

        public void RunTransitionAnimation(GameScene gameScene)
        {
            switch (gameScene)
            {
                case GameScene.InMainMenu:
                    break;
                case GameScene.InMission:
                    {

                        break;
                    }
                case GameScene.InSpaceport:
                    break;
                default:
                    break;
            }
        }
    }
}
