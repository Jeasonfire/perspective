using UnityEngine;
using System.Collections;

public class SwitchController : MonoBehaviour {
    public Animator animator;
    public GameObject indicators;
    public Camera interactingCamera;
    public float interactionDistance;
    public bool switchedOn;
    public Interaction[] interactors;
    
    private bool interacted = false;
    private bool animatedOn;

    void Start () {
        if (interactingCamera == null) {
            interactingCamera = Camera.main;
        }
        animatedOn = !switchedOn;
        float originalSpeed = 0;
        if (animator != null) {
            originalSpeed = animator.speed;
            animator.speed = 1000 * originalSpeed;
        }
        UpdateGraphics();
        if (animator != null) {
            animator.speed = originalSpeed;
        }
    }

    void Update () {
        Vector2 camPos = interactingCamera.transform.position;
        if (PerspectiveController.CURRENT_PERSPECTIVE == PerspectiveController.PERSPECTIVE_2D) {
            camPos.y -= GameObject.Find("2D Camera Transform").transform.localPosition.y;
        }
        Vector2 ctrlPos = transform.position;
        float distance = (camPos - ctrlPos).magnitude;
        if (distance <= interactionDistance) {
            if (!interacted && Input.GetButton("Interact") && CheckHover()) {
                interacted = true;
                switchedOn = !switchedOn;
                UpdateGraphics();
                foreach (Interaction interactor in interactors) {
                    interactor.Interact(switchedOn);
                }
            }
        }
        if (!Input.GetButton("Interact")) {
            interacted = false;
        }
    }

    void UpdateGraphics () {
        if (indicators != null) {
            foreach (MeshRenderer renderer in indicators.GetComponentsInChildren<MeshRenderer>()) {
                renderer.enabled = switchedOn;
            }
        }
        if (animator != null && animatedOn != switchedOn) {
            animatedOn = switchedOn;
            if (animatedOn) {
                animator.Play("Open");
            } else {
                animator.Play("Close");
            }
        }
    }

    bool CheckHover () {
        RaycastHit hit;
        if (PerspectiveController.CURRENT_PERSPECTIVE == PerspectiveController.PERSPECTIVE_2D) {
            Physics.Raycast(interactingCamera.ScreenPointToRay(Input.mousePosition), out hit);
            if (hit.collider != null) {
                return hit.collider.gameObject == gameObject;
            }
        } else {
            Physics.Raycast(interactingCamera.transform.position, interactingCamera.transform.forward, out hit);
            if (hit.collider != null) {
                return hit.collider.gameObject == gameObject;
            }
        }
        return false;
    }
}
