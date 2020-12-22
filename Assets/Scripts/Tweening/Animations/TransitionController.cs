using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

[Serializable]
public struct TransitionLayer
{
    public string name;
    public TextSequence textSequence;
}

namespace Assets.Scripts.Tweening.Animations
{
    public class TransitionController : MonoBehaviour
    {
        public Canvas mainMenuCanvas;
        public Canvas loginCanvas;
        public List<Canvas> canvases;

        public List<TransitionLayer> transitionLayers;
        public Queue<TextSequence> transitionQueue;

        private static TransitionController _instance;
        public static TransitionController Instance { get => _instance; set => _instance = value; }

        public void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
            }
            else if (_instance != this)
                Destroy(gameObject);
        }

        public void Start()
        {
            transitionQueue = new Queue<TextSequence>();
        }

        public void PlayLogin(Action onTransitionComplete)
        {
            mainMenuCanvas.gameObject.SetActive(false);
            loginCanvas.gameObject.SetActive(true);
            var loginTransitions = transitionLayers.Where(x => x.name == "Login").ToList();
            loginTransitions.ForEach(x => AddToQueue(x.textSequence, onTransitionComplete));
        }

        public void PlayOutpostReached(Action onTransitionComplete)
        {
            loginCanvas.gameObject.SetActive(true);
            var loginTransitions = transitionLayers.Where(x => x.name == "OutpostReached").ToList();
            loginTransitions.ForEach(x => AddToQueue(x.textSequence, onTransitionComplete));
        }

        public void AddToQueue(TextSequence textSequence, Action onTransitionComplete)
        {
            transitionQueue.Enqueue(textSequence);
            textSequence.onComplete += () => HandleOnSequenceComplete(onTransitionComplete);
            if (!transitionQueue.Peek().isExecuted)
                transitionQueue.Peek().ExecuteSequence();
        }

        private void HandleOnSequenceComplete(Action onTransitionComplete)
        {
            var textSequence = transitionQueue.Dequeue();
            Destroy(textSequence.gameObject);
            if (transitionQueue.Count > 0)
                transitionQueue.Peek().ExecuteSequence();
            else
                onTransitionComplete.Invoke();
        }
    }
}
