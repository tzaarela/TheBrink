using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    [CreateAssetMenu(fileName = "TranitionController", menuName = "TransitionController")]
    public class TransitionController : ScriptableObject
    {
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
