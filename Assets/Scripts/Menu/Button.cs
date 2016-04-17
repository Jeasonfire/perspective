using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public enum OptionType {
    MouseX, MouseY, FXVol, MusicVol
}

public class Button : MonoBehaviour {
    public enum ButtonType {
        SceneLoader, MouseInvert
    }

    public Camera interactingCamera;
    public ButtonType buttonType;
    public string loadableScene;

    public OptionType optionType;
    public TextMesh mouseInvertText;

    private bool clicked = false;

    void Start () {
        if (interactingCamera == null) {
            interactingCamera = Camera.main;
        }
    }

    void Update () {
        RaycastHit hit;
        Physics.Raycast(interactingCamera.ScreenPointToRay(Input.mousePosition), out hit);
        if (hit.collider != null && hit.collider.gameObject == gameObject) {
            // TODO: Add some effect to show that you're hovering over the button
            if (Input.GetMouseButton(0) && !clicked) {
                switch (buttonType) {
                    case ButtonType.SceneLoader:
                        if (!loadableScene.Equals("")) {
                            SceneManager.LoadScene(loadableScene);
                        }
                        break;
                    case ButtonType.MouseInvert:
                        if (mouseInvertText != null) {
                            float value = 0;
                            switch (optionType) {
                                case OptionType.MouseX:
                                    Options.MOUSE_INVERT.x *= -1;
                                    value = Options.MOUSE_INVERT.x;
                                    break;
                                case OptionType.MouseY:
                                    Options.MOUSE_INVERT.y *= -1;
                                    value = Options.MOUSE_INVERT.y;
                                    break;
                            }
                            if (value < 0) {
                                mouseInvertText.color = new Color(mouseInvertText.color.r, mouseInvertText.color.g, mouseInvertText.color.b, 1.0f);
                            } else {
                                mouseInvertText.color = new Color(mouseInvertText.color.r, mouseInvertText.color.g, mouseInvertText.color.b, 0.25f);
                            }
                        }
                        break;
                }
                clicked = true;
            }
        }
        if (!Input.GetMouseButton(0)) {
            clicked = false;
        }
    }
}
