using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Systems
{
    public class SystemContent : MonoBehaviour
    {
        public void SetActive(bool setActive)
        {
            this.gameObject.SetActive(setActive);   

        }

    }
}
