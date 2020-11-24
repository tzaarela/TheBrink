﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts
{
    public class SceneManager : MonoBehaviour
    {
        public static SceneManager Instance { get; set; }

        public Scene CurrentScene { get; set; }

        public List<Scene> Scenes { get; set; }

        public void Start()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(this);

            Scenes = new List<Scene>();
        }

        public void ChangeScene(string name)
        {
            var scene = Scenes.First(x => x.name == name);
        }
    }
}
