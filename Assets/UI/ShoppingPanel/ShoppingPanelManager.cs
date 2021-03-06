using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static ClothingSO;

/// <summary>
/// Controls shopping operations. Allows player to see all clothers available in the shopkeer inventory,
/// as well as buy and sell clothes
/// </summary>
public class ShoppingPanelManager : InventoryPanelManager
{
    [SerializeField] Button buyShirtButton;
    [SerializeField] Button sellShirtButton;
    [SerializeField] Button buyPantsButton;
    [SerializeField] Button sellPantsButton;
    [SerializeField] Button buyShoesButton;
    [SerializeField] Button sellShoesButton;

    private ShopkeeperBase shopkeeper;

    private TextMeshProUGUI buyShirtButtonText;
    private TextMeshProUGUI sellShirtButtonText;
    private TextMeshProUGUI buyPantsButtonText;
    private TextMeshProUGUI sellPantsButtonText;
    private TextMeshProUGUI buyShoesButtonText;
    private TextMeshProUGUI sellShoesButtonText;

    private readonly StringBuilder sb = new StringBuilder();

    public override void Setup(MainCanvasManager mainCanvasManager)
    {
        gameManager = GameManager.Instance;
        soundManager = SoundManager.Instance;
        player = gameManager.Player;
        shopkeeper = gameManager.Shopkeeper;
        this.mainCanvasManager = mainCanvasManager;
        mainCanvasManager.onShoppingCall += GetCurrentClothing;
        GetButtonsComponents();
    }

    protected override void GetCurrentClothing()
    {
        UpdateCurrentShirt(shopkeeper.Inventory.Container.Find(x => x.ItemType == ClothingType.Shirt));
        OnDisplayNewClothing(currentShirt);
        UpdateCurrentPants(shopkeeper.Inventory.Container.Find(x => x.ItemType == ClothingType.Pants));
        OnDisplayNewClothing(currentPants);
        UpdateCurrentShoes(shopkeeper.Inventory.Container.Find(x => x.ItemType == ClothingType.Shoes));
        OnDisplayNewClothing(currentShoes);
    }

    private void GetButtonsComponents()
    {
        buyShirtButtonText = buyShirtButton.GetComponentInChildren<TextMeshProUGUI>();
        sellShirtButtonText = sellShirtButton.GetComponentInChildren<TextMeshProUGUI>();
        buyPantsButtonText = buyPantsButton.GetComponentInChildren<TextMeshProUGUI>();
        sellPantsButtonText = sellPantsButton.GetComponentInChildren<TextMeshProUGUI>();
        buyShoesButtonText = buyShoesButton.GetComponentInChildren<TextMeshProUGUI>();
        sellShoesButtonText = sellShoesButton.GetComponentInChildren<TextMeshProUGUI>();
    }

    protected override void UpdateCurrentShirt(ClothingSO shirt)
    {
        base.UpdateCurrentShirt(shirt);
        ButtonSetup(currentShirt);
    }

    protected override void UpdateCurrentPants(ClothingSO pants)
    {
        base.UpdateCurrentPants(pants);
        ButtonSetup(currentPants);
    }

    protected override void UpdateCurrentShoes(ClothingSO shoes)
    {
        base.UpdateCurrentShoes(shoes);
        ButtonSetup(currentShoes);
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
                sellShoesButtonText.text = sb.ToString();
                break;
        }
    }

    protected override void GetItem(ClothingSO currentClothing, int nextIndex)
    {
        int lastIndex = shopkeeper.Inventory.Container.Count - 1;
        int currentIndex = shopkeeper.Inventory.Container.FindIndex(x => x.Equals(currentClothing));
        if (nextIndex > 0)
        {
            if (currentIndex == lastIndex)
                currentIndex = 0;
            else
                currentIndex++;
            for (int i = currentIndex; i <= lastIndex; i++)
            {
                if (shopkeeper.Inventory.Container[i].ItemType == currentClothing.ItemType)
                {
                    UpdateCurrentItem(shopkeeper.Inventory.Container[i]);
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
                if (shopkeeper.Inventory.Container[i].ItemType == currentClothing.ItemType)
                {
                    UpdateCurrentItem(shopkeeper.Inventory.Container[i]);
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

    public void BuyPants()
    {
        if (!player.Wallet.RemoveFromWallet(currentPants.BuyingPrice)) return;

        player.Inventory.AddItem(currentPants);
        ButtonSetup(currentPants);
    }

    public void SellPants()
    {
        player.Wallet.AddToWallet(currentPants.SellingPrice);
        player.Inventory.RemoveItem(currentPants);
        ButtonSetup(currentPants);
    }

    public void BuyShoes()
    {
        if (!player.Wallet.RemoveFromWallet(currentShoes.BuyingPrice)) return;

        player.Inventory.AddItem(currentShoes);
        ButtonSetup(currentShoes);
    }

    public void SellShoes()
    {
        player.Wallet.AddToWallet(currentShoes.SellingPrice);
        player.Inventory.RemoveItem(currentShoes);
        ButtonSetup(currentShoes);
    }

    protected override void OnDestroy()
    {
        mainCanvasManager.onShoppingCall -= GetCurrentClothing;
    }

    public override void ExitPanel()
    {
        mainCanvasManager.OnShoppingCall();
    }
}
