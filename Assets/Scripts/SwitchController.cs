using UnityEngine;
using System.Collections;

public class SwitchController : MonoBehaviour {
    public Transform lever;
    public GameObject indicators;
    public Camera interactingCamera;
    public float interactionDistance;
    public bool switchedOn;
    public Interaction[] interactors;

    private float originalZ;
    private bool interacted = false;

    void Start () {
        if (interactingCamera == null) {
            interactingCamera = Camera.main;
        }
        if (lever != null) {
            originalZ = lever.localEulerAngles.z;
        }
        UpdateGraphics();
    }

    void Update () {
        Vector2 camPos = interactingCamera.transform.position;
        if (PerspectiveController.CURRENT_PERSPECTIVE == PerspectiveController.PERSPECTIVE_2D) {
            camPos.y -= 1.65f;
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
        if (lever != null) {
            lever.localEulerAngles = new Vector3(lever.localEulerAngles.x, lever.localEulerAngles.y, originalZ + 45f * (switchedOn ? 1 : -1));
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
