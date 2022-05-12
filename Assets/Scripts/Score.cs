using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI midGameScoreText;
    [SerializeField] TextMeshProUGUI currentScoreText;
    [SerializeField] TextMeshProUGUI totalScoreText;
    [SerializeField] int currentScore=0;
    [SerializeField] int totalScore;

    public void UpdateScore(int score) {
        totalScore += score;
        currentScore = score;
    }

    public void UpdateCurrentScoreOnly(int score) {
        currentScore = score;
    }

    public void UpdateMidGameScoreText() {
        if (midGameScoreText != null) {
            midGameScoreText.text = currentScore.ToString();
        }
    }

    public void UpdateScoreText() {
        if (currentScoreText != null) {
            currentScoreText.text = "+" + currentScore.ToString();
        }
        if (totalScoreText != null) {
            totalScoreText.text = totalScore.ToString();
        }
    }
}
