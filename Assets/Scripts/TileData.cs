using UnityEngine;
using UnityEngine.UI;

public class TileData : MonoBehaviour
{
    [SerializeField] TileColor tileColor;
    //Sprite tileSprite;
    [SerializeField] Image tileImage;

    private void Start() {
        tileColor = (TileColor) Random.Range(0, 3);
        TileColoring();
    }

    void TileColoring() {
        tileImage = GetComponent<Image>();
        switch (tileColor) {
            case TileColor.Red: tileImage.color = Color.red; break;
            case TileColor.Green: tileImage.color = Color.green; break;
            case TileColor.Blue: tileImage.color = Color.blue; break;
            default: tileImage.color = Color.black; break;
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
