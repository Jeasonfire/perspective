using UnityEngine;
using System.Collections;

public class Door : Interaction {
    public Animator animator;

    private bool open = false;

    public override void Interact (bool switchedOn) {
        if (switchedOn) {
            Open();
        } else {
            Close();
        }
    }

    public void Open () {
        animator.Play("DoorOpen");
        open = true;
    }

    public void Close() {
        animator.Play("DoorClose");
        open = false;
    }

    public bool IsOpen () {
        return open;
    }
}
