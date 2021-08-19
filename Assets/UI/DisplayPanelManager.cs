using UnityEngine;
using UnityEngine.UI;
using static ClothingSO;

public class DisplayPanelManager : MonoBehaviour
{
    [SerializeField] Image head;
    [SerializeField] Image body;
    [SerializeField] Image legs;
    [SerializeField] Image feet;

    private MainCanvasManager mainCanvasManager;

    public void Setup(MainCanvasManager mainCanvasManager)
    {
        this.mainCanvasManager = mainCanvasManager;
        mainCanvasManager.InventoryPanelManager.onDisplayNewClothing += UpdateClothing;
        mainCanvasManager.ShoppingPanelManager.onDisplayNewClothing += UpdateClothing;
    }

    private void UpdateClothing(ClothingSO newClothing)
    {
        switch(newClothing.ItemType)
        {
            case ClothingType.Shirt:
                body.sprite = newClothing.FrontSprite;
                break;
            case ClothingType.Pants:
                legs.sprite = newClothing.FrontSprite;
                break;
            case ClothingType.Shoes:
                feet.sprite = newClothing.FrontSprite;
                break;
        }     
    }

    private void OnDestroy()
    {
        mainCanvasManager.InventoryPanelManager.onDisplayNewClothing -= UpdateClothing;
        mainCanvasManager.ShoppingPanelManager.onDisplayNewClothing -= UpdateClothing;
    }
}
