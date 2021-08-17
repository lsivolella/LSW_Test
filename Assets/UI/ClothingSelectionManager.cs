using System;
using TMPro;
using UnityEngine;

public class ClothingSelectionManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI currentShirtText;
    [SerializeField] TextMeshProUGUI currentPantsText;
    [SerializeField] TextMeshProUGUI currentShoesText;

    private GameManager gameManager;
    private InventoryManager inventoryManager;
    private ClothingManager clothingManager;
    
    private ClothingSO currentShirt;
    private int currentShirtIndex;

    private ClothingSO currentPants;
    private int currentPantsIndex;

    private ClothingSO currentShoes;
    private int currentShoesIndex;

    public event Action<ClothingSO> onDisplayNewClothing;

    private void Start()
    {
        gameManager = GameManager.Instance;
        gameManager.onInventoryCall += UpdateCurrentClothes;
    }

    public void Setup(InventoryManager inventoryManager, ClothingManager clothingManager)
    {
        this.inventoryManager = inventoryManager;
        this.clothingManager = clothingManager;
        UpdateCurrentClothes();
    }

    private void UpdateCurrentClothes()
    {
        GetCurrentClothing();
        GetClothingIndex();
    }

    private void GetCurrentClothing()
    {
        currentShirt = clothingManager.CurrentShirt;
        currentShirtText.text = currentShirt.DisplayName;

        currentPants = clothingManager.CurrentPants;
        currentPantsText.text = currentPants.DisplayName;

        currentShoes = clothingManager.CurrentShoes;
        currentShoesText.text = currentShoes.DisplayName;
    }

    private void GetClothingIndex()
    {
        currentShirtIndex = inventoryManager.Shirts.FindIndex(x => x.Equals(currentShirt));
        currentPantsIndex = inventoryManager.Pants.FindIndex(x => x.Equals(currentPants));
        currentShoesIndex = inventoryManager.Shoes.FindIndex(x => x.Equals(currentShoes));
    }

    public void GetNextShirt(int nextIndex)
    {
        int lastIndex = inventoryManager.Shirts.Count - 1;

        if (currentShirtIndex == lastIndex && nextIndex > 0)
            currentShirtIndex = 0;
        else if (currentShirtIndex == 0 && nextIndex < 0)
            currentShirtIndex = lastIndex;
        else
            currentShirtIndex += nextIndex;

        currentShirt = inventoryManager.Shirts[currentShirtIndex];
        currentShirtText.text = inventoryManager.Shirts[currentShirtIndex].DisplayName;

        OnDisplayNewClothing(currentShirt);
    }

    public void GetNextPants(int nextIndex)
    {
        int lastIndex = inventoryManager.Pants.Count - 1;

        if (currentPantsIndex == lastIndex && nextIndex > 0)
            currentPantsIndex = 0;
        else if (currentPantsIndex == 0 && nextIndex < 0)
            currentPantsIndex = lastIndex;
        else
            currentPantsIndex += nextIndex;

        currentPants = inventoryManager.Pants[currentPantsIndex];
        currentPantsText.text = inventoryManager.Pants[currentPantsIndex].DisplayName;

        OnDisplayNewClothing(currentPants);
    }

    public void GetNextShoes(int nextIndex)
    {
        int lastIndex = inventoryManager.Shoes.Count - 1;

        if (currentShoesIndex == lastIndex && nextIndex > 0)
            currentShoesIndex = 0;
        else if (currentShoesIndex == 0 && nextIndex < 0)
            currentShoesIndex = lastIndex;
        else
            currentShoesIndex += nextIndex;

        currentShoes = inventoryManager.Shoes[currentShoesIndex];
        currentShoesText.text = inventoryManager.Shoes[currentShoesIndex].DisplayName;

        OnDisplayNewClothing(currentShoes);
    }

    private void OnDisplayNewClothing(ClothingSO newClothe)
    {
        if (onDisplayNewClothing == null) return;

        onDisplayNewClothing(newClothe);
    }

    private void OnDestroy()
    {
        gameManager.onInventoryCall -= UpdateCurrentClothes;
    }

    public void UpdateClothing()
    {
        clothingManager.ChangeClothes(currentShirt, currentPants, currentShoes);
    }
}
