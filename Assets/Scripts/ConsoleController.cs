﻿using System;
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

        private Queue<IEnumerator> printQueue;

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

            printQueue = new Queue<IEnumerator>();
            StartCoroutine(PrintConsoleQueue());
        }

        public void PrintToConsole(string message, float printTime, bool shouldClear)
        {
            if (shouldClear)
                ClearConsole();

            printQueue.Enqueue(PrintTextDelayed(message, printTime));

            //StartCoroutine(PrintTextDelayed(message, printTime));
        }
        public void PrintToConsole(string message)
        {
            outputField.text = message;
        }

        public IEnumerator PrintTextDelayed(string message, float printTime)
        {
            if (outputField.text != "")
                outputField.text += "\n \n";

            var previousText = outputField.text;
            for (int i = 0; i < message.Length; i++)
            {
                outputField.text = previousText + message.Substring(0, i);
                yield return new WaitForSeconds(printTime);
            }
        }

        public void ClearConsole()
        {
            outputField.text = "";
        }

        IEnumerator PrintConsoleQueue()
        {
            while (true)
            {
                while (printQueue.Count > 0)
                {
                    yield return StartCoroutine(printQueue.Dequeue());
                }
                yield return null;
            }
        }

    }
}
