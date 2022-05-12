using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textMeshPro;
    [SerializeField] float timer;
    [SerializeField] float delay;

    private void Start() {
        StartCoroutine(CountdownTimer());
        OnGameEnd += DebugGameEnd;
    }

    public void SetTimer(float duration) {
        timer = duration;
    }

    void DebugGameEnd() {
        Debug.Log("Game Ends");
    }

    IEnumerator CountdownTimer() {
        yield return new WaitForSeconds(delay);
        timer -= delay;
        if (textMeshPro != null) {
            textMeshPro.text = (int)timer + "s";
        }
        if (timer <= 0) {
            OnGameEnd();
        }
        StartCoroutine(CountdownTimer());
    }

    public delegate void GameEnd();
    public event GameEnd OnGameEnd;
}
