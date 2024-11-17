using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LoadNewScene : MonoBehaviour
{
    public TMP_Text highScoreText;
    public string sceneName;

    void Start() {
        // int highScore = Get From Scriptable Object
        // highScoreText.text = $"Max Waves Survived: {highScore}";
    }

    public void StartScene() {
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }
}
