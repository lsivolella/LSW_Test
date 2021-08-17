using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClothingManager : MonoBehaviour
{
    [SerializeField] SpriteRenderer head;
    [SerializeField] SpriteRenderer body;
    [SerializeField] SpriteRenderer legs;
    [SerializeField] SpriteRenderer feet;
    [SerializeField] ClothingSO currentShirt;
    [SerializeField] ClothingSO currentPants;
    [SerializeField] ClothingSO currentShoes;

    public ClothingSO CurrentShirt { get { return currentShirt; } }
    public ClothingSO CurrentPants { get { return currentPants; } }
    public ClothingSO CurrentShoes { get { return currentShoes; } }

    private PlayerBase player;
    private GameManager gameManager;

    private void Awake()
    {
        GetComponents();

    }

    private void GetComponents()
    {
        player = GetComponent<PlayerBase>();
    }

    private void Start()
    {
        SubscribeToEvent();
        ClothingSetup();
    }

    private void SubscribeToEvent()
    {
        gameManager = GameManager.Instance;
        gameManager.onShoppingCall += CheckForNaked;
    }

    private void ClothingSetup()
    {
        body.sprite = currentShirt.Sprite;
        legs.sprite = currentPants.Sprite;
        feet.sprite = currentShoes.Sprite;
    }

    public void ChangeClothes(ClothingSO newShirt, ClothingSO newPants, ClothingSO newShoes)
    {
        currentShirt = newShirt;
        currentPants = newPants;
        currentShoes = newShoes;

        ClothingSetup();
    }

    public void CheckForNaked()
    {
        if (gameManager.ShoppingOpen) return;

        if (!player.Inventory.HasItem(currentShirt))
            currentShirt = player.Inventory.Container.Find(x => (x.ItemType == currentShirt.ItemType)
            && (x.DisplayName == "None"));
        if (!player.Inventory.HasItem(currentPants))
            currentPants = player.Inventory.Container.Find(x => (x.ItemType == currentPants.ItemType)
            && (x.DisplayName == "None"));
        if (!player.Inventory.HasItem(currentShoes))
            currentShoes = player.Inventory.Container.Find(x => (x.ItemType == currentShoes.ItemType)
            && (x.DisplayName == "None"));

        ClothingSetup();
    }

    private void OnApplicationQuit()
    {
        gameManager.onShoppingCall -= CheckForNaked;
    }
}
