using UnityEngine;
using System.Collections;

public class MusicPlayer : MonoBehaviour {
    private static float MUSIC_TIME = 0;

    public AudioSource audioSource;

    void Start () {
        audioSource.volume = Options.MUSIC_VOLUME;
        audioSource.time = MUSIC_TIME;
    }

	void Update () {
        audioSource.volume = Options.MUSIC_VOLUME;
        MUSIC_TIME = audioSource.time;
    }
}
