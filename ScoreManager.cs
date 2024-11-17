using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class NewBehaviourScript : MonoBehaviour
{
    [SerializeField]
    private ScoreAndLevel scoreAndLevel;
    [SerializeField]
    private TMP_InputField inputName;

    public UnityEvent<string, int> uploadScoreEvent;
    private int score;

    public void UploadScore()
    {
        if (scoreAndLevel.levelName == "Easy") {
            score = scoreAndLevel.score * 1;
        } else if (scoreAndLevel.levelName == "Medium") {
            score = (int)(scoreAndLevel.score * 1.25);
        } else if (scoreAndLevel.levelName == "Hard") {
            score = (int)(scoreAndLevel.score * 1.5);
        } else if (scoreAndLevel.levelName == "Very Hard"){
            score = (int)(scoreAndLevel.score * 1.75);
        }
        uploadScoreEvent.Invoke(inputName.text, score);
    }
}
