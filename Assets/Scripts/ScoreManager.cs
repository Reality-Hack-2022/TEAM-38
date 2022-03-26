using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public int Score = 0;
    public TMPro.TextMeshProUGUI ScoreText; 

    public void Start()
    {
        ScoreText.text = Score.ToString();
    }

    public void UpdateScore(int updateValue = 1)
    {
        Score += updateValue;
        ScoreText.text = Score.ToString();
    }

    public void ResetScore()
    {
        Score = 0;
        ScoreText.text = Score.ToString();
    }
}
