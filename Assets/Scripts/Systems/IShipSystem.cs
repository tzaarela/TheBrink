﻿using Assets.Scripts;
using Assets.Scripts.Rooms;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IShipSystem 
{
    Room SystemRoom { get; set; }

    SystemType SystemType { get; set; }
    PowerState PowerState  { get; set; }

    bool IsDepressurised { get; set; }

    float AirLevel { get; set; }

    float CurrentEnergy { get; set; }

    void Run();

    void Update();

    void Upkeep();

    void Reboot();

    void RunDiagnostic();
}
