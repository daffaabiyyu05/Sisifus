using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject gameplay;
    [SerializeField] GameObject credits;
    GameObject activeScene;

    private void Start() {
        activeScene = Instantiate(mainMenu, this.transform);
    }

    void StartGame() {
        Destroy(activeScene);
        activeScene = Instantiate(gameplay, this.transform);
    }

    void MainMenu() {
        Destroy(activeScene);
        activeScene = Instantiate(mainMenu, this.transform);
    }

    void Credits() {
        Destroy(activeScene);
        activeScene = Instantiate(credits, this.transform);
    }
}
