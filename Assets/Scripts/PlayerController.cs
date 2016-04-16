using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
    public CharacterController character;
    public Transform eye;
    public float movementSpeed;
    public float jumpForce;

    private float currentFallingSpeed = 0;

    void Start () {
	}

	void Update () {
        Vector3 movement = new Vector3();
        Vector3 rotation = eye.localEulerAngles;
        switch (PerspectiveController.CURRENT_PERSPECTIVE) {
            case PerspectiveController.PERSPECTIVE_2D:
                movement.x = Input.GetAxis("Horizontal");
                break;
            case PerspectiveController.PERSPECTIVE_3D:
                movement.x = Input.GetAxis("Horizontal");
                movement.z = Input.GetAxis("Vertical");
                movement = eye.TransformDirection(movement);
                movement.y = 0;
                movement.Normalize();

                rotation.y += Input.GetAxis("Mouse X") * Options.MOUSE_SENSITIVITY.x;
                rotation.x -= Input.GetAxis("Mouse Y") * Options.MOUSE_SENSITIVITY.y;
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
        character.Move((movement * movementSpeed + new Vector3(0, currentFallingSpeed, 0)) * Time.deltaTime);
        eye.localEulerAngles = rotation;
    }
}
