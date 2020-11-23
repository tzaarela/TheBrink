using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    public float CurrentSpeed { get; set; }
    public float CurrentPosition { get; set; }
    public float CurrentFuel { get; set; }
    public float MaxFuel { get; set; }
    public int CurrentCash { get; set; }

        public Ship()
        {
            CurrentSpeed = 100;
            CurrentPosition = 0;
            CurrentFuel = 1000;
            MaxFuel = 1000;
            CurrentCash = 0;
        }
    }
