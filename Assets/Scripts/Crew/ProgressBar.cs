using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Crew
{
    public class ProgressBar : MonoBehaviour
    {
        [SerializeField]
        private Image progressBarImage;

        public void SetProgressDone(float amount)
        {
            progressBarImage.fillAmount = amount * 0.01f;
        }
    }
}
