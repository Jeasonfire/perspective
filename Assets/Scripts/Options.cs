using UnityEngine;
using System.Collections;

public class Options : MonoBehaviour {
    public static bool SSAO = true;
    public static bool MOTION_BLUR = true;

    public static Vector2 MOUSE_MAX_SENSITIVITY = new Vector2(5, 5);
    public static Vector2 MOUSE_SENSITIVITY = new Vector2(2, 2);
    public static Vector2 MOUSE_INVERT = new Vector2(1, 1);
    public static float FX_VOLUME = 0.2f;
    public static float MUSIC_VOLUME = 0.2f;
}
