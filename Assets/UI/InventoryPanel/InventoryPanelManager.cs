using System;
using TMPro;
using UnityEngine;
using static ClothingSO;

public class InventoryPanelManager : MonoBehaviour
{
    [SerializeField] protected TextMeshProUGUI currentShirtText;
    [SerializeField] protected TextMeshProUGUI currentPantsText;
    [SerializeField] protected TextMeshProUGUI currentShoesText;

    protected GameManager gameManager;
    protected SoundManager soundManager;
    protected MainCanvasManager mainCanvasManager;
    protected PlayerBase player;
    protected ClothingManager clothingManager;
    
    protected ClothingSO currentShirt;
    protected ClothingSO currentPants;
    protected ClothingSO currentShoes;

    public event Action<ClothingSO> onDisplayNewClothing;

    public virtual void Setup(MainCanvasManager mainCanvasManager)
    {
        GetReferences(mainCanvasManager);
        mainCanvasManager.onInventoryCall += GetCurrentClothing;
    }

    private void GetReferences(MainCanvasManager mainCanvasManager)
    {
        gameManager = GameManager.Instance;
        soundManager = SoundManager.Instance;
        player = gameManager.Player;
        this.clothingManager = player.GetComponent<ClothingManager>();
        this.mainCanvasManager = mainCanvasManager;
    }

    protected virtual void GetCurrentClothing()
    {
        UpdateCurrentShirt(clothingManager.CurrentShirt);
        OnDisplayNewClothing(currentShirt);
        UpdateCurrentPants(clothingManager.CurrentPants);
        OnDisplayNewClothing(currentPants);
        UpdateCurrentShoes(clothingManager.CurrentShoes);
        OnDisplayNewClothing(currentShoes);
    }

    protected virtual void UpdateCurrentShirt(ClothingSO shirt)
    {
        currentShirt = shirt;
        currentShirtText.text = currentShirt.DisplayName;
    }

    protected virtual void UpdateCurrentPants(ClothingSO pants)
    {
        currentPants = pants;
        currentPantsText.text = currentPants.DisplayName;
    }

    protected virtual void UpdateCurrentShoes(ClothingSO shoes)
    {
        currentShoes = shoes;
        currentShoesText.text = currentShoes.DisplayName;
    }

    public void GetShirt(int increment)
    {
        GetItem(currentShirt, increment);
    }

    public void GetPants(int increment)
    {
        GetItem(currentPants, increment);
    }

    public void GetShoes(int increment)
    {
        GetItem(currentShoes, increment);
    }

    protected virtual void GetItem(ClothingSO currentClothing, int nextIndex)
    {
        int lastIndex = player.Inventory.Container.Count - 1;
        int currentIndex = player.Inventory.Container.FindIndex(x => x.Equals(currentClothing));
        if (nextIndex > 0)
        {
            if (currentIndex == lastIndex)
                currentIndex = 0;
            else
                currentIndex++;
            for (int i = currentIndex; i <= lastIndex; i++)
            {
                if (player.Inventory.Container[i].ItemType == currentClothing.ItemType)
                {
                    UpdateCurrentItem(player.Inventory.Container[i]);
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
                if (player.Inventory.Container[i].ItemType == currentClothing.ItemType)
                {
                    UpdateCurrentItem(player.Inventory.Container[i]);
                    break;
                }
                if (i == 0)
                    i = lastIndex + 1;
            }
        }
    }

    protected void UpdateCurrentItem(ClothingSO currentClothing)
    {
        switch (currentClothing.ItemType)
        {
            case ClothingType.Shirt:
                UpdateCurrentShirt(currentClothing);
                OnDisplayNewClothing(currentShirt);
                break;
            case ClothingType.Pants:
                UpdateCurrentPants(currentClothing);
                OnDisplayNewClothing(currentPants);
                break;
            case ClothingType.Shoes:
                UpdateCurrentShoes(currentClothing);
                OnDisplayNewClothing(currentShoes);
                break;
        }
    }

    protected void OnDisplayNewClothing(ClothingSO newClothe)
    {
        if (onDisplayNewClothing == null) return;

        onDisplayNewClothing(newClothe);
    }

    public void UpdateClothing()
    {
        clothingManager.ChangeClothes(currentShirt, currentPants, currentShoes);
    }

    public virtual void ExitPanel()
    {
        mainCanvasManager.OnInventoryCall();
    }

    protected virtual void OnDestroy()
    {
        mainCanvasManager.onInventoryCall -= GetCurrentClothing;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void AddMoney()
    {
        player.Wallet.AddToWallet(100);
    }

    public void PlaySoundClip()
    {
        soundManager.PlayButtonsSoundEffect();
    }
}
