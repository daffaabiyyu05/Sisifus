using UnityEngine;
using UnityEngine.UI;

public class TileData : MonoBehaviour
{
    [SerializeField] TileColor tileColor;
    [SerializeField] Image tileImage;
    [SerializeField] Sprite[] tileSprites;

    private void Start() {
        tileColor = (TileColor) Random.Range(0, 3);
        TileColoring();
    }

    void TileColoring() {
        tileImage = GetComponent<Image>();
        switch (tileColor) {
            case TileColor.Red: tileImage.color = Color.white; tileImage.sprite = tileSprites[0]; break;
            case TileColor.Green: tileImage.color = Color.white; tileImage.sprite = tileSprites[1]; break;
            case TileColor.Blue: tileImage.color = Color.white; tileImage.sprite = tileSprites[2]; break;
            default: tileImage.color = Color.white; break;
        }
    }
    
    public void ClearTile() {
        tileColor = TileColor.Unused;
        TileColoring();
    }

    public TileColor GetColor() {
        return tileColor;
    }
}
