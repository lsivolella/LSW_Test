using UnityEngine;
using UnityEngine.UI;
using static ClothingSO;

public class ClothingDisplayManager : MonoBehaviour
{
    [SerializeField] Image head;
    [SerializeField] Image body;
    [SerializeField] Image legs;
    [SerializeField] Image feet;

    private MainCanvasManager mainCanvasManager;

    public void Setup(MainCanvasManager mainCanvasManager)
    {
        this.mainCanvasManager = mainCanvasManager;
        mainCanvasManager.ClothingSelectionManager.onDisplayNewClothing += UpdateClothing;
        mainCanvasManager.ShoppingSelectionManager.onDisplayNewClothing += UpdateClothing;
    }

    private void UpdateClothing(ClothingSO newClothing)
    {
        switch(newClothing.ItemType)
        {
            case ClothingType.Shirt:
                body.sprite = newClothing.Sprite;
                break;
            case ClothingType.Pants:
                legs.sprite = newClothing.Sprite;
                break;
            case ClothingType.Shoes:
                feet.sprite = newClothing.Sprite;
                break;
        }     
    }

    private void OnApplicationQuit()
    {
        mainCanvasManager.ClothingSelectionManager.onDisplayNewClothing -= UpdateClothing;
        mainCanvasManager.ShoppingSelectionManager.onDisplayNewClothing -= UpdateClothing;
    }
}
