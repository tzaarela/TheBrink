using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door
{
    public bool IsOpen { get; set; }
    public bool IsLocked { get; set; }

    private Transform _doorPosition;

    [SerializeField]
    private GameObject _door;

    public Door(Transform doorPosition)
    {
        this._doorPosition = doorPosition;

        IsOpen = false;
        IsLocked = false;
    }

    public void OpenDoor()
    {

    }

    public void CloseDoor()
    {

    }

    public void LockDoor()
    {

    }

    public void UnlockDoor()
    {

    }
}
