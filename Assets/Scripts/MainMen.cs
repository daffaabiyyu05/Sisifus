using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMen : MonoBehaviour
{
    Button thisButton;

    private void Start() {
        thisButton = GetComponent<Button>();
        thisButton.onClick.AddListener(() => GoToMain());
    }

    void GoToMain() {
        SendMessageUpwards("MainMenu");
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Q)) {
            SendMessageUpwards("MainMenu");
        }
    }
}
