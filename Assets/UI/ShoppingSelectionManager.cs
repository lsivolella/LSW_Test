using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static ClothingSO;

public class ShoppingSelectionManager : ClothingSelectionManager
{
    [SerializeField] Button buyShirtButton;
    [SerializeField] Button sellShirtButton;
    [SerializeField] Button buyPantsButton;
    [SerializeField] Button sellPantsButton;
    [SerializeField] Button buyShoesButton;
    [SerializeField] Button sellShoesButton;

    private ClothesSellerBase clothesSeller;

    private TextMeshProUGUI buyShirtButtonText;
    private TextMeshProUGUI sellShirtButtonText;
    private TextMeshProUGUI buyPantsButtonText;
    private TextMeshProUGUI sellPantsButtonText;
    private TextMeshProUGUI buyShoesButtonText;
    private TextMeshProUGUI sellShoestButtonText;

    private readonly StringBuilder sb = new StringBuilder();

    public override void Setup()
    {
        gameManager = GameManager.Instance;
        gameManager.onShoppingCall += GetCurrentClothing;
        player = gameManager.Player;
        clothesSeller = gameManager.ClothesSeller;
        GetButtonsComponents();
    }

    protected override void GetCurrentClothing()
    {
        UpdateCurrentShirt(clothesSeller.Inventory.Container.Find(x => x.ItemType == ClothingType.Shirt));
        OnDisplayNewClothing(currentShirt);
        UpdateCurrentPants(clothesSeller.Inventory.Container.Find(x => x.ItemType == ClothingType.Pants));
        OnDisplayNewClothing(currentPants);
        UpdateCurrentShoes(clothesSeller.Inventory.Container.Find(x => x.ItemType == ClothingType.Shoes));
        OnDisplayNewClothing(currentShoes);
    }

    private void GetButtonsComponents()
    {
        buyShirtButtonText = buyShirtButton.GetComponentInChildren<TextMeshProUGUI>();
        sellShirtButtonText = sellShirtButton.GetComponentInChildren<TextMeshProUGUI>();
    }

    protected override void UpdateCurrentShirt(ClothingSO shirt)
    {
        base.UpdateCurrentShirt(shirt);
        ButtonSetup(currentShirt);
    }

    protected override void UpdateCurrentPants(ClothingSO pants)
    {
        base.UpdateCurrentPants(pants);
    }

    protected override void UpdateCurrentShoes(ClothingSO shoes)
    {
        base.UpdateCurrentShoes(shoes);
    }

    private void ButtonSetup(ClothingSO currentClothing)
    {
        sb.Clear();
        switch (currentClothing.ItemType)
        {
            case ClothingType.Shirt:
                sb.Append("Buy $");
                sb.Append(currentClothing.BuyingPrice);
                buyShirtButton.interactable = !player.Inventory.HasItem(currentClothing);
                buyShirtButtonText.text = sb.ToString();
                sb.Clear();
                sb.Append("Sell $");
                sb.Append(currentClothing.SellingPrice);
                sellShirtButton.interactable = player.Inventory.HasItem(currentClothing);
                sellShirtButtonText.text = sb.ToString();
                break;
            case ClothingType.Pants:
                sb.Append("Buy $");
                sb.Append(currentClothing.BuyingPrice);
                buyPantsButton.interactable = !player.Inventory.HasItem(currentClothing);
                buyPantsButtonText.text = sb.ToString();
                sb.Clear();
                sb.Append("Sell $");
                sb.Append(currentClothing.SellingPrice);
                sellPantsButton.interactable = player.Inventory.HasItem(currentClothing);
                sellPantsButtonText.text = sb.ToString();
                break;
            case ClothingType.Shoes:
                sb.Append("Buy $");
                sb.Append(currentClothing.BuyingPrice);
                buyShoesButton.interactable = !player.Inventory.HasItem(currentClothing);
                buyShoesButtonText.text = sb.ToString();
                sb.Clear();
                sb.Append("Sell $");
                sb.Append(currentClothing.SellingPrice);
                sellShoesButton.interactable = player.Inventory.HasItem(currentClothing);
                sellShoestButtonText.text = sb.ToString();
                break;
        }
    }

    protected override void GetItem(ClothingSO currentClothing, int nextIndex)
    {
        int lastIndex = clothesSeller.Inventory.Container.Count - 1;
        int currentIndex = clothesSeller.Inventory.Container.FindIndex(x => x.Equals(currentClothing));
        if (nextIndex > 0)
        {
            if (currentIndex == lastIndex)
                currentIndex = 0;
            else
                currentIndex++;
            for (int i = currentIndex; i <= lastIndex; i++)
            {
                if (clothesSeller.Inventory.Container[i].ItemType == currentClothing.ItemType)
                {
                    UpdateCurrentItem(clothesSeller.Inventory.Container[i]);
                    break;
                }
                if (i == lastIndex)
                    i = -1;
            }
        }
        else if (nextIndex < 0)
        {
            if (currentIndex == 0)
                currentIndex = lastIndex;
            else
                currentIndex--;
            for (int i = currentIndex; i >= 0; i--)
            {
                if (clothesSeller.Inventory.Container[i].ItemType == currentClothing.ItemType)
                {
                    UpdateCurrentItem(clothesSeller.Inventory.Container[i]);
                    break;
                }
                if (i == 0)
                    i = lastIndex + 1;
            }
        }
    }

    public void BuyShirt()
    {
        if (!player.Wallet.RemoveFromWallet(currentShirt.BuyingPrice)) return;

        player.Inventory.AddItem(currentShirt);
        ButtonSetup(currentShirt);
    }

    public void SellShirt()
    {
        player.Wallet.AddToWallet(currentShirt.SellingPrice);
        player.Inventory.RemoveItem(currentShirt);
        ButtonSetup(currentShirt);
    }
}
