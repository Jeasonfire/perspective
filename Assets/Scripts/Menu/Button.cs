using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public enum OptionType {
    MouseX, MouseY, FXVol, MusicVol
}

public class Button : MonoBehaviour {
    public enum ButtonType {
        SceneLoader, MouseInvert, SSAO, MotionBlur
    }

    public Camera interactingCamera;
    public ButtonType buttonType;
    public string loadableScene;

    public OptionType optionType;
    public TextMesh textMesh;

    private bool clicked = false;

    void Start () {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        if (interactingCamera == null) {
            interactingCamera = Camera.main;
        }
        UpdateButtonAlpha();
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
                        if (textMesh != null) {
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
                                textMesh.color = new Color(textMesh.color.r, textMesh.color.g, textMesh.color.b, 1.0f);
                            } else {
                                textMesh.color = new Color(textMesh.color.r, textMesh.color.g, textMesh.color.b, 0.25f);
                            }
                        }
                        break;
                    case ButtonType.MotionBlur:
                        Options.MOTION_BLUR = !Options.MOTION_BLUR;
                        UpdateButtonAlpha();
                        break;
                    case ButtonType.SSAO:
                        Options.SSAO = !Options.SSAO;
                        UpdateButtonAlpha();
                        break;
                    default:
                        break;
                }
                clicked = true;
            }
        }
        if (!Input.GetMouseButton(0)) {
            clicked = false;
        }
    }

    void UpdateButtonAlpha () {
        switch (buttonType) {
            case ButtonType.SSAO:
                if (Options.SSAO) {
                    textMesh.color = new Color(textMesh.color.r, textMesh.color.g, textMesh.color.b, 1.0f);
                } else {
                    textMesh.color = new Color(textMesh.color.r, textMesh.color.g, textMesh.color.b, 0.25f);
                }
                break;
            case ButtonType.MotionBlur:
                if (Options.MOTION_BLUR) {
                    textMesh.color = new Color(textMesh.color.r, textMesh.color.g, textMesh.color.b, 1.0f);
                } else {
                    textMesh.color = new Color(textMesh.color.r, textMesh.color.g, textMesh.color.b, 0.25f);
                }
                break;
        }
    }
}
