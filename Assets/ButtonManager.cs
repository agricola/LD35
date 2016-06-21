using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour {


    public void PlayButton() {
        SceneManager.LoadScene("main");
    }

    public void RestartButton() {
        SceneManager.LoadScene("start");
    }
}
