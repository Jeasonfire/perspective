using UnityEngine;
using System.Collections;

public class Door : Interaction {
    public Animator animator;

    public override void Interact (bool switchedOn) {
        if (switchedOn) {
            Open();
        } else {
            Close();
        }
    }

    public void Open () {
        animator.Play("DoorOpen");
    }

    public void Close() {
        animator.Play("DoorClose");
    }
}
