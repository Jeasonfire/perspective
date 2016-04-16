using UnityEngine;
using System.Collections;

public class PerspectiveController : MonoBehaviour {
    public const int PERSPECTIVE_2D = 0;
    public const int PERSPECTIVE_3D = 1;
    public const int INITIAL_PERSPECTIVE = PERSPECTIVE_2D;
    public static int CURRENT_PERSPECTIVE = INITIAL_PERSPECTIVE;

    public PlayerController player;
    public Camera perspectiveCamera;
    public Transform fpsTransform;
    public Transform platformerTransform;

    private bool lastOrtho = false;

    void Start () {
        perspectiveCamera.orthographic = lastOrtho = INITIAL_PERSPECTIVE == PERSPECTIVE_2D;
        UpdateCamera();
    }

    void Update () {
        if (lastOrtho != perspectiveCamera.orthographic) {
            UpdateCamera();
        }
        lastOrtho = perspectiveCamera.orthographic;
    }

    void UpdateCamera () {
        if (perspectiveCamera.orthographic) {
            perspectiveCamera.transform.position = platformerTransform.position;
            perspectiveCamera.transform.localEulerAngles = new Vector3(0, 0, 0);
        } else {
            perspectiveCamera.transform.position = fpsTransform.position;
            Vector3 newRotation = perspectiveCamera.transform.localEulerAngles;
            newRotation.y += player.character.velocity.x < 0 ? -90 : 90;
            perspectiveCamera.transform.localEulerAngles = newRotation;
        }
    }

	public void ChangeState () {
        perspectiveCamera.orthographic = !perspectiveCamera.orthographic;
    }
}
