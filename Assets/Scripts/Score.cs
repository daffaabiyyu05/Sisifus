using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textMeshPro;

    public void UpdateScore(int score) {
        textMeshPro.text = score.ToString().PadLeft(8, '0');
    }
}
