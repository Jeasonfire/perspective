using UnityEngine;
using System.Collections;

public class Slider : MonoBehaviour {
    public Camera interactingCamera;
    public OptionType optionType;
    public TextMesh valueText;
    public Transform sliderStart;
    public Transform sliderEnd;
    public Transform sliderHandle;

    private float startX;
    private float length;
    private float topY;
    private float botY;
    private bool interacting = false;

    void Start () {
        if (interactingCamera == null) {
            interactingCamera = Camera.main;
        }
        startX = sliderStart.position.x;
        length = sliderEnd.position.x - sliderStart.position.x;
        topY = sliderStart.position.y;
        botY = sliderEnd.position.y;
        float value = 1;
        switch (optionType) {
            case OptionType.FXVol:
                value = Options.FX_VOLUME;
                valueText.text = (int) (Options.FX_VOLUME * 100f) + "%";
                break;
            case OptionType.MusicVol:
                value = Options.MUSIC_VOLUME;
                valueText.text = (int) (Options.MUSIC_VOLUME * 100f) + "%";
                break;
            case OptionType.MouseX:
                value = Options.MOUSE_SENSITIVITY.x / Options.MOUSE_MAX_SENSITIVITY.x;
                valueText.text = (((int) (Options.MOUSE_SENSITIVITY.x * 100f)) / 100f).ToString();
                break;
            case OptionType.MouseY:
                value = Options.MOUSE_SENSITIVITY.y / Options.MOUSE_MAX_SENSITIVITY.y;
                valueText.text = (((int) (Options.MOUSE_SENSITIVITY.y * 100f)) / 100f).ToString();
                break;
        }
        sliderHandle.position = new Vector3(startX + length * value, sliderHandle.position.y, sliderHandle.position.z);
    }

    void Update () {
        RaycastHit hit;
        Physics.Raycast(interactingCamera.ScreenPointToRay(Input.mousePosition), out hit);
        if (Input.GetMouseButton(0) && hit.point.x >= startX && hit.point.x <= startX + length && hit.point.y < topY && hit.point.y > botY) {
            interacting = true;
        } else if (!Input.GetMouseButton(0)) {
            interacting = false;
        }
        if (interacting) {
            float mouseX = Mathf.Clamp(hit.point.x - startX, 0, length);
            sliderHandle.position = new Vector3(startX + mouseX, sliderHandle.position.y, sliderHandle.position.z);
            float value = mouseX / length;
            switch (optionType) {
                case OptionType.FXVol:
                    Options.FX_VOLUME = value;
                    valueText.text = (int) (Options.FX_VOLUME * 100f) + "%";
                    break;
                case OptionType.MusicVol:
                    Options.MUSIC_VOLUME = value;
                    valueText.text = (int) (Options.MUSIC_VOLUME * 100f) + "%";
                    break;
                case OptionType.MouseX:
                    Options.MOUSE_SENSITIVITY.x = value * Options.MOUSE_MAX_SENSITIVITY.x;
                    valueText.text = (((int) (Options.MOUSE_SENSITIVITY.x * 100f)) / 100f).ToString();
                    break;
                case OptionType.MouseY:
                    Options.MOUSE_SENSITIVITY.y = value * Options.MOUSE_MAX_SENSITIVITY.y;
                    valueText.text = (((int) (Options.MOUSE_SENSITIVITY.y * 100f)) / 100f).ToString();
                    break;
            }
        }
    }
}
