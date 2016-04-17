using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
    public PerspectiveController perspective;
    public CharacterController character;
    public AudioSource music;
    public Transform eye;
    public float movementSpeed;
    public float jumpForce;
    public float direction = 0;

    private float currentFallingSpeed = 0;

    void Start () {
        music.volume = Options.MUSIC_VOLUME;
    }

    void Update () {
        music.volume = Options.MUSIC_VOLUME;

        /* Get & apply perspective input */
        if (Input.GetButton("Change Perspective") && PerspectiveController.CURRENT_PERSPECTIVE != PerspectiveController.TRANSITION) {
            perspective.ChangeState();
        }
        
        /* Get movement inputs */
        Vector3 movement = new Vector3();
        Vector3 rotation = eye.localEulerAngles;
        switch (PerspectiveController.CURRENT_PERSPECTIVE) {
            case PerspectiveController.PERSPECTIVE_2D:
                movement.x = Input.GetAxis("Horizontal");
                this.direction = Mathf.Clamp(this.direction + movement.x * Time.deltaTime, -1, 1);
                break;
            case PerspectiveController.PERSPECTIVE_3D:
                movement.x = Input.GetAxis("Horizontal");
                movement.z = Input.GetAxis("Vertical");
                movement = eye.TransformDirection(movement);
                movement.y = 0;
                movement.Normalize();

                rotation.y += Input.GetAxis("Mouse X") * Options.MOUSE_SENSITIVITY.x * Options.MOUSE_INVERT.x;
                rotation.x -= Input.GetAxis("Mouse Y") * Options.MOUSE_SENSITIVITY.y * Options.MOUSE_INVERT.y;
                if (rotation.x > 180) {
                    rotation.x -= 360;
                }
                rotation.x = Mathf.Clamp(rotation.x, -90, 90);
                break;
        }
        if (!character.isGrounded) {
            currentFallingSpeed += Physics.gravity.y * Time.deltaTime;
        } else {
            currentFallingSpeed = 0;
            if (Input.GetButton("Jump")) {
                currentFallingSpeed = jumpForce;
            }
        }

        /* Apply movement inputs */
        character.Move((movement * movementSpeed + new Vector3(0, currentFallingSpeed, 0)) * Time.deltaTime);
        eye.localEulerAngles = rotation;
    }
}
