﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    public float Speed { get; set; }
    public float Position { get; set; }
    public float Fuel { get; set; }
    public float MaxFuel { get; set; }
    public int Cash { get; set; }

        public Ship()
        {
            Speed = 100;
            Position = 0;
            Fuel = 1000;
            MaxFuel = 1000;
            Cash = 0;
        }
    }
