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

    public ClothingDisplayManager Setup(MainCanvasManager mainCanvasManager, ClothingManager clothingManager)
    {
        this.mainCanvasManager = mainCanvasManager;
        mainCanvasManager.ClothingSelectionManager.onDisplayNewClothing += UpdateShirt;

        body.sprite = clothingManager.CurrentShirt.Sprite;
        legs.sprite = clothingManager.CurrentPants.Sprite;
        feet.sprite = clothingManager.CurrentShoes.Sprite;

        return this;
    }

    private void UpdateShirt(ClothingSO newClothing)
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
}
