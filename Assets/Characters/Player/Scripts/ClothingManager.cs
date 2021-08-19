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
        ClothingFrontSetup();
    }

    private void SubscribeToEvent()
    {
        gameManager = GameManager.Instance;
        gameManager.MainCanvasManager.onShoppingCall += CheckForNaked;
    }

    private void GetClothingSetup()
    {
        if (player.LastDirection == Vector2.up)
            ClothingBackSetup();
        else if (player.LastDirection == Vector2.down)
            ClothingFrontSetup();
        else if (player.LastDirection == Vector2.right)
            ClothingRightSetup();
        else if (player.LastDirection == Vector2.left)
            ClothingLeftSetup();
    }

    public void ClothingFrontSetup()
    {
        player.transform.localScale = new Vector2(1, 1);
        body.sprite = currentShirt.FrontSprite;
        legs.sprite = currentPants.FrontSprite;
        feet.sprite = currentShoes.FrontSprite;
    }

    public void ClothingBackSetup()
    {
        player.transform.localScale = new Vector2(1, 1);
        body.sprite = currentShirt.BackSprite;
        legs.sprite = currentPants.BackSprite;
        feet.sprite = currentShoes.BackSprite;
    }

    public void ClothingLeftSetup()
    {
        player.transform.localScale = new Vector2(1, 1);
        body.sprite = currentShirt.SideSprite;
        legs.sprite = currentPants.SideSprite;
        feet.sprite = currentShoes.SideSprite;
    }

    public void ClothingRightSetup()
    {
        player.transform.localScale = new Vector2(-1, 1);
        body.sprite = currentShirt.SideSprite;
        legs.sprite = currentPants.SideSprite;
        feet.sprite = currentShoes.SideSprite;
    }

    public void ChangeClothes(ClothingSO newShirt, ClothingSO newPants, ClothingSO newShoes)
    {
        currentShirt = newShirt;
        currentPants = newPants;
        currentShoes = newShoes;

        GetClothingSetup();
    }

    public void CheckForNaked()
    {
        if (gameManager.MainCanvasManager.ShoppingOpen) return;

        if (!player.Inventory.HasItem(currentShirt))
            currentShirt = player.Inventory.Container.Find(x => (x.ItemType == currentShirt.ItemType)
            && (x.DisplayName == "None"));
        if (!player.Inventory.HasItem(currentPants))
            currentPants = player.Inventory.Container.Find(x => (x.ItemType == currentPants.ItemType)
            && (x.DisplayName == "None"));
        if (!player.Inventory.HasItem(currentShoes))
            currentShoes = player.Inventory.Container.Find(x => (x.ItemType == currentShoes.ItemType)
            && (x.DisplayName == "None"));

        GetClothingSetup();

    }

    private void OnApplicationQuit()
    {
        gameManager.MainCanvasManager.onShoppingCall -= CheckForNaked;
    }
}
