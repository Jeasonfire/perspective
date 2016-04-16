using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
    public CharacterController character;
    public Transform eye;
    public float movementSpeed;

    private float currentFallingSpeed = 0;

    void Start () {

	}

	void Update () {
        Vector3 movement = new Vector3();
        Vector3 rotation = new Vector3();
        switch (PerspectiveController.CURRENT_PERSPECTIVE) {
            case PerspectiveController.PERSPECTIVE_2D:
                movement.x = Input.GetAxis("Horizontal");
                break;
            case PerspectiveController.PERSPECTIVE_3D:
                movement.x = Input.GetAxis("Horizontal");
                movement.z = Input.GetAxis("Vertical");
                movement = eye.TransformDirection(movement).normalized;
                break;
        }
        if (!character.isGrounded) {
            this.currentFallingSpeed += Physics.gravity.y * Time.deltaTime;
        } else {
            this.currentFallingSpeed = 0;
        }
        character.Move((movement * this.movementSpeed + new Vector3(0, this.currentFallingSpeed, 0)) * Time.deltaTime);
        this.eye.localEulerAngles = this.eye.localEulerAngles + rotation;
    }
}
