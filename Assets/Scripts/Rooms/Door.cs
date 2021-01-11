using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Door : MonoBehaviour
{
    public bool IsOpen { get; set; }
    public bool IsLocked { get; set; }

    private Animator animator;

    [SerializeField]
    private Image slider1;
    [SerializeField]
    private Image slider2;
    [SerializeField]
    private Color unlockedColor;
    [SerializeField]
    private Color lockedColor;

    public void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void OpenDoor()
    {
        animator.SetTrigger("OpenDoor");
    }

    public void CloseDoor()
    {
        animator.SetTrigger("CloseDoor");
    }

    public void LockDoor()
    {
        CloseDoor();
        IsLocked = true;
        slider1.color = lockedColor;
        slider2.color = lockedColor;

    }

    public void UnlockDoor()
    {
        IsLocked = false;
        slider1.color = unlockedColor;
        slider2.color = unlockedColor;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!IsLocked)
            OpenDoor();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        CloseDoor();
    }
}
