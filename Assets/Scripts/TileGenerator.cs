using System.Collections.Generic;
using UnityEngine;

public class TileGenerator : MonoBehaviour
{
    [SerializeField] GameObject tileObject;
    [SerializeField] Vector2 startPoint;
    [SerializeField] Vector2 stepPoint;
    public int tileOnScreen;

    public List<GameObject> GenerateTiles() {
        List<GameObject> gameObjects = new List<GameObject>();
        int i;
        RectTransform createdTile;
        for (i = 0; i < tileOnScreen; i++) {
            createdTile = Instantiate(tileObject, transform).GetComponent<RectTransform>();
            createdTile.localPosition = startPoint + stepPoint * i;
            gameObjects.Add(createdTile.gameObject);
        }
        return gameObjects;
    }

    public GameObject GenerateNewTile() {
        RectTransform createdTile;
        createdTile = Instantiate(tileObject, transform).GetComponent<RectTransform>();
        createdTile.localPosition = startPoint + stepPoint * (tileOnScreen-1);
        return createdTile.gameObject;
    }

    public void UpdateTilesUp(List<GameObject> tileObjects) {
        foreach (GameObject tile in tileObjects) {
            RectTransform tileTransform = tile.GetComponent<RectTransform>();
            Vector2 localPosCast = tileTransform.localPosition;
            tileTransform.localPosition = localPosCast - stepPoint;
        }
    }

    public void UpdateTilesDown(List<GameObject> tileObjects) {
        foreach (GameObject tile in tileObjects) {
            RectTransform tileTransform = tile.GetComponent<RectTransform>();
            Vector2 localPosCast = tileTransform.localPosition;
            tileTransform.localPosition = localPosCast + stepPoint;
        }
    }
}
