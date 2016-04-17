using UnityEngine;
using System.Collections;

public class PerspectiveController : MonoBehaviour {
    public const int PERSPECTIVE_2D = 0;
    public const int PERSPECTIVE_3D = 1;
    public const int TRANSITION = 2;
    public static int CURRENT_PERSPECTIVE = PERSPECTIVE_2D;

    public PlayerController player;
    public Camera perspectiveCamera;
    public Transform fpsTransform;
    public Transform platformerTransform;
    public MeshRenderer blinder;
    public float transitionLength;
   
    private float transitionStartTime = 0;

    void Start () {
        perspectiveCamera.orthographic = true;
        blinder.material.color = new Color(blinder.material.color.r, blinder.material.color.g, blinder.material.color.b, 0);
        transitionStartTime = Time.fixedTime - transitionLength;
        UpdateCamera();
    }

    void Update () {
        float transitionTime = (Time.fixedTime - transitionStartTime) / transitionLength;
        if (transitionTime < 1.0) {
            blinder.material.color = new Color(blinder.material.color.r, blinder.material.color.g, blinder.material.color.b, transitionTime);
        } else if (CURRENT_PERSPECTIVE == TRANSITION) {
            perspectiveCamera.orthographic = !perspectiveCamera.orthographic;
            UpdateCamera();
            blinder.material.color = new Color(blinder.material.color.r, blinder.material.color.g, blinder.material.color.b, 0);
        }
    }

    void UpdateCamera () {
        if (perspectiveCamera.orthographic) {
            CURRENT_PERSPECTIVE = PERSPECTIVE_2D;
            perspectiveCamera.transform.position = platformerTransform.position;
            perspectiveCamera.transform.eulerAngles = platformerTransform.eulerAngles;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        } else {
            CURRENT_PERSPECTIVE = PERSPECTIVE_3D;
            perspectiveCamera.transform.position = fpsTransform.position;
            perspectiveCamera.transform.eulerAngles = fpsTransform.eulerAngles;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    public void ChangeState () {
        CURRENT_PERSPECTIVE = TRANSITION;
        transitionStartTime = Time.fixedTime;
    }
}
