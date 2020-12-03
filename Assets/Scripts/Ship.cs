using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship
{
    public float Speed { get; set; }
    public float Position { get; set; }
    public float Fuel { get; set; }
    public float MaxFuel { get; set; }
    public float Capacitor { get; set; }
    public float MaxCapacitor { get; set; }
    public int Cash { get; set; }

    public Ship()
    {
        Speed = 2;
        Position = 0;
        Fuel = 1000;
        MaxFuel = 1000;
        Cash = 0;
        MaxCapacitor = 1000;
        Capacitor = MaxCapacitor / 2;
    }
}
