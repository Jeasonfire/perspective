using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelAdvancer : MonoBehaviour {
    public string nextLevelName;

    void OnTriggerEnter(Collider other) {
        SceneManager.LoadScene(nextLevelName);
    }
}
