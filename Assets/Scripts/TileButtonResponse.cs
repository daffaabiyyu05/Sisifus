using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    [SerializeField] GameObject PopupObject;
    [SerializeField] Parallax parallax;
    [SerializeField] Image popupSprite;
    [SerializeField] AudioClip[] storyParts;
    [SerializeField] Sprite[] storySprites;
    [SerializeField] int currentStage = 0;
    [SerializeField] TextMeshProUGUI continueTextButton;
    [SerializeField] AudioSource soundSource;
    [SerializeField] AudioClip[] rightWrongClip;

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
            timer.OnGameEnd += EndStage;
        }
        if (PopupObject != null) {
            PopupObject.SetActive(false);
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
        soundSource.clip = rightWrongClip[0];
        soundSource.Play();
        simpleAnimation.RunAnimation();
        simpleRotator.RunRotator();
        parallax.RunAnimation();
        tileGenerator.UpdateTilesUp(tileObjects);
        if (position >= maxPosition - (tileGenerator.tileOnScreen - 1)) {
            maxPosition++;
            tileObjects.Add(tileGenerator.GenerateNewTile());
            score.UpdateCurrentScoreOnly(maxPosition - tileGenerator.tileOnScreen);
            score.UpdateMidGameScoreText();
        }
        position++;
        currentColor = tileObjects[position].GetComponent<TileData>().GetColor();
        tileGenerator.UpdateTileTarget(tileObjects[position]);
        Debug.Log("Step up, next is: " + currentColor + " " + position);
    }

    void Wrong() {
        if (position > 1) {
            tileGenerator.ResetY(tileObjects[position]);
            soundSource.clip = rightWrongClip[1];
            soundSource.Play();
            position--;
            tileGenerator.UpdateTilesDown(tileObjects);
            currentColor = tileObjects[position].GetComponent<TileData>().GetColor();
            tileGenerator.UpdateTileTarget(tileObjects[position]);
            simpleAnimation.StopAnimation();
            simpleRotator.StopRotator();
            parallax.StopAnimation();
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

    void EndStage() {
        score.UpdateScore(maxPosition - tileGenerator.tileOnScreen);
        score.UpdateScoreText();
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
        ShowPopup();
    }

    void ShowPopup() {
        if (PopupObject != null) {
            PopupObject.SetActive(true);
            if (currentStage >= storyParts.Length - 1 && continueTextButton != null) {
                continueTextButton.text = "SELESAI";
            }
        }
    }

    public void ResetGame() {
        Time.timeScale = 1f;
        currentStage++;
        if (currentStage >= storyParts.Length) {
            SendMessageUpwards("MainMenu");
            return;
        }
        foreach (GameObject tile in tileObjects) {
            Destroy(tile);
        }
        if (tileGenerator != null) {
            tileObjects = tileGenerator.GenerateTiles();
            StartCoroutine(FirstTileClear());
            position = 1;
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
            audioSource.Stop();
            audioSource.clip = storyParts[currentStage];
            timer.SetTimer(audioSource.clip.length);
            audioSource.Play();
        }
        if (PopupObject != null) {
            popupSprite.sprite = storySprites[currentStage];
            PopupObject.SetActive(false);
        }
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Q)) {
            RedButton();
        }
        if (Input.GetKeyDown(KeyCode.W)) {
            GreenButton();
        }
        if (Input.GetKeyDown(KeyCode.E)) {
            BlueButton();
        }
    }
}
