using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using TMPro;
using System.Collections;

namespace Assets.Scripts
{
    public class ConsoleController : MonoBehaviour
    {
        public static ConsoleController instance;

        [SerializeField]
        private TextMeshProUGUI outputField;

        public void Start()
        {
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(this);
            }
        }

        public void PrintToConsole(string message, float printTime)
        {
            StartCoroutine(PrintTextDelayed(message, printTime));
        }
        public void PrintToConsole(string message)
        {
            outputField.text = message;
        }

        public IEnumerator PrintTextDelayed(string message, float printTime)
        {
            for (int i = 0; i < message.Length; i++)
            {
                outputField.text = message.Substring(0, i);
                yield return new WaitForSeconds(printTime);
            }
        }

    }
}
