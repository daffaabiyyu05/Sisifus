using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartGame : MonoBehaviour
{
    Button startButton;
    void Start()
    {
        startButton = GetComponent<Button>();
        startButton.onClick.AddListener(() => SendMessageUpwards("StartGame"));
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Q)) {
            SendMessageUpwards("StartGame");
        }
    }
}
