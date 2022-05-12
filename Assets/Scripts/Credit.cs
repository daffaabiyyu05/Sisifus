using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Credit : MonoBehaviour
{
    Button thisButton;

    // Start is called before the first frame update
    void Start()
    {
        thisButton = GetComponent<Button>();
        thisButton.onClick.AddListener(() => RunCredits());
    }

    // Update is called once per frame
    void RunCredits()
    {
        SendMessageUpwards("Credits");
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.W)) {
            SendMessageUpwards("Credits");
        }
    }
}
