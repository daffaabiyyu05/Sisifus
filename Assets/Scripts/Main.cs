using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject gameplay;
    GameObject activeScene;

    private void Start() {
        activeScene = Instantiate(mainMenu, this.transform);
    }

    void StartGame() {
        Destroy(activeScene);
        activeScene = Instantiate(gameplay, this.transform);
    }
}
