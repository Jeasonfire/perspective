using UnityEngine;
using System.Collections;

public class TutorialController : MonoBehaviour {
    public enum TutorialChapter {
        Misc, Movement, Jumping, Perspective, Mouselook, Finish, Switch
    }

    public TextMesh text;
    public TutorialChapter chapter;
    public float fadeTime;
    public TutorialController previousTutorial;

    private float fadeInTime = -1;
    private float doneTime = -1;

    void Start () {
        if (previousTutorial != null && !previousTutorial.IsDone()) {
            text.color = new Color(text.color.r, text.color.g, text.color.b, 0);
        }
    }

	void Update () {
        if (previousTutorial != null && fadeInTime < 0) {
            if (previousTutorial.IsDone()) {
                fadeInTime = Time.fixedTime + fadeTime;
            }
            return;
        }

        if ((fadeInTime - Time.fixedTime) / fadeTime > 0) {
            text.color = new Color(text.color.r, text.color.g, text.color.b, 1.0f - (fadeInTime - Time.fixedTime) / fadeTime);
        } else if (IsDone() && doneTime < Time.fixedTime) {
            Destroy(gameObject);
            return;
        } else if (IsDone()) {
            text.color = new Color(text.color.r, text.color.g, text.color.b, (doneTime - Time.fixedTime) / fadeTime);
        } else {
            switch (chapter) {
                case TutorialChapter.Movement:
                    if (Input.GetAxis("Horizontal") != 0) {
                        Done();
                    }
                    break;
                case TutorialChapter.Jumping:
                    if (Input.GetButton("Jump")) {
                        Done();
                    }
                    break;
                case TutorialChapter.Perspective:
                    if (Input.GetButton("Change Perspective")) {
                        Done();
                    }
                    break;
                case TutorialChapter.Mouselook:
                    if (Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0) {
                        Done();
                    }
                    break;
                case TutorialChapter.Finish:
                    if ((GameObject.Find("Level Advancer Door").GetComponent<Door>().IsOpen())) {
                        Done();
                    }
                    break;
                case TutorialChapter.Switch:
                    if ((GameObject.Find("Tutorial Door").GetComponent<Door>().IsOpen())) {
                        Done();
                    }
                    break;
            }
        }
	}

    void Done () {
        doneTime = Time.fixedTime + fadeTime;
    }

    bool IsDone() {
        return doneTime >= 0;
    }

    void OnTriggerEnter(Collider other) {
        Done();
    }
}
