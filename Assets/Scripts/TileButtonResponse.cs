using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TileButtonResponse : MonoBehaviour
{
    [SerializeField] TileGenerator tileGenerator;
    [SerializeField] Button redButton;
    [SerializeField] Button greenButton;
    [SerializeField] Button blueButton;
    List<GameObject> tileObjects;
    TileColor currentColor;
    int position = 1;
    int maxPosition;
    Score score;
    [SerializeField] SimpleAnimation simpleAnimation;
    [SerializeField] SimpleRotator simpleRotator;
    [SerializeField] AudioSource audioSource;
    Timer timer;

    private void Start() {
        tileGenerator = GetComponent<TileGenerator>();
        score = GetComponent<Score>();
        timer = GetComponent<Timer>();
        if (tileGenerator != null) {
            tileObjects = tileGenerator.GenerateTiles();
            StartCoroutine(FirstTileClear());
            maxPosition = tileGenerator.tileOnScreen;
        }
        if (redButton != null) {
            redButton.onClick.AddListener(() => RedButton());
        }
        if (greenButton != null) {
            greenButton.onClick.AddListener(() => GreenButton());
        }
        if (blueButton != null) {
            blueButton.onClick.AddListener(() => BlueButton());
        }
        if (audioSource != null && timer != null) {
            timer.SetTimer(audioSource.clip.length);
            timer.OnGameEnd += EndGame;
        }
    }

    IEnumerator FirstTileClear() {
        yield return new WaitForFixedUpdate();
        tileObjects[0].GetComponent<TileData>().ClearTile();
        currentColor = tileObjects[1].GetComponent<TileData>().GetColor();
    }

    void Correct() {
        //Destroy(tileObjects[0]);
        //tileObjects.RemoveAt(0);
        simpleAnimation.RunAnimation();
        simpleRotator.RunRotator();
        tileGenerator.UpdateTilesUp(tileObjects);
        if (position >= maxPosition - (tileGenerator.tileOnScreen - 1)) {
            maxPosition++;
            tileObjects.Add(tileGenerator.GenerateNewTile());
            if (score != null) {
                score.UpdateScore(maxPosition - tileGenerator.tileOnScreen);
            }
        }
        position++;
        currentColor = tileObjects[position].GetComponent<TileData>().GetColor();
        tileGenerator.UpdateTileTarget(tileObjects[position]);
        Debug.Log("Step up, next is: " + currentColor + " " + position);
    }

    void Wrong() {
        if (position > 1) {
            tileGenerator.ResetY(tileObjects[position]);
            position--;
            tileGenerator.UpdateTilesDown(tileObjects);
            currentColor = tileObjects[position].GetComponent<TileData>().GetColor();
            tileGenerator.UpdateTileTarget(tileObjects[position]);
            simpleAnimation.StopAnimation();
            simpleRotator.StopRotator();
            Debug.Log("Step down, current is: " + currentColor + " " + position);
        }
    }

    void RedButton() {
        if (currentColor != TileColor.Red) {
            Wrong();
        }
        else {
            Correct();
        }
    }

    void GreenButton() {
        if (currentColor != TileColor.Green) {
            Wrong();
        }
        else {
            Correct();
        }
    }

    void BlueButton() {
        if (currentColor != TileColor.Blue) {
            Wrong();
        }
        else {
            Correct();
        }
    }

    void EndGame() {
        Time.timeScale = 0f;
        if (redButton != null) {
            redButton.onClick.RemoveAllListeners();
        }
        if (greenButton != null) {
            greenButton.onClick.RemoveAllListeners();
        }
        if (blueButton != null) {
            blueButton.onClick.RemoveAllListeners();
        }
    }
}
