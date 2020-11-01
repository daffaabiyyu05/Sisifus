using System.Collections.Generic;
using UnityEngine;

public class TileGenerator : MonoBehaviour
{
    [SerializeField] GameObject tileObject;
    [SerializeField] Vector2 startPoint;
    [SerializeField] Vector2 stepPoint;
    [SerializeField] Vector2 targetPoint;
    [SerializeField] float priorTargetY;
    public int tileOnScreen;

    public List<GameObject> GenerateTiles() {
        List<GameObject> gameObjects = new List<GameObject>();
        int i;
        RectTransform createdTile;
        for (i = 0; i < tileOnScreen; i++) {
            createdTile = Instantiate(tileObject, transform).GetComponent<RectTransform>();
            createdTile.localPosition = startPoint + stepPoint * i;
            if (i == 1) {
                createdTile.localPosition = targetPoint;
            }
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

    public void UpdateTileTarget(GameObject tileObject) {
        RectTransform tileTransform = tileObject.GetComponent<RectTransform>();
        tileTransform.localPosition = targetPoint;
    }

    public void ResetY(GameObject tileObject) {
        RectTransform tileTransform = tileObject.GetComponent<RectTransform>();
        tileTransform.localPosition = new Vector2(tileTransform.localPosition.x, priorTargetY);
    }
}
