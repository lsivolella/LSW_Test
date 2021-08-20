using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Coordinates menus operations. Also provides Action events to other classes allowing
/// for coordinated action during menus operations
/// </summary>
public class MainCanvasManager : MonoBehaviour
{
    [SerializeField] Image background;
    [SerializeField] IntroScreenManager introScreenManager;
    [SerializeField] DisplayPanelManager displayPanelManager;
    [SerializeField] InventoryPanelManager inventoryPanelManager;
    [SerializeField] ShoppingPanelManager shoppingPanelManager;
    [SerializeField] GameObject inventoryHUD;
    [SerializeField] GameObject moneyHUD;

    public IntroScreenManager IntroScreenManager { get { return introScreenManager; } }
    public DisplayPanelManager DisplayPanelManager { get { return displayPanelManager; } }
    public InventoryPanelManager InventoryPanelManager { get { return inventoryPanelManager; } }
    public ShoppingPanelManager ShoppingPanelManager { get { return shoppingPanelManager; } }
    public GameObject InventoryHUD { get { return inventoryHUD; } }
    public GameObject MoneyHUD { get { return moneyHUD; } }
    

    private readonly List<GameObject> UIElements = new List<GameObject>();

    public bool InventoryOpen { get; private set; } = false;
    public bool ShoppingOpen { get; private set; } = false;

    public event Action onIntroCall;
    public event Action onInventoryCall;
    public event Action onShoppingCall;

    void Awake()
    {
        CreateUIElementsList();
        DisableAllUI();
    }

    private void CreateUIElementsList()
    {
        foreach (Transform child in transform)
        {
            UIElements.Add(child.gameObject);
        }
    }

    private void DisableAllUI()
    {
        UIElements.ForEach(x => x.SetActive(false));
    }

    private void Start()
    {
        ClothingSelectionManagerSetup();
        ShoppingSelectionManagerSetup();
        ClothingDisplaySetup();
        EnableIntroScreen();
    }

    private void EnableIntroScreen()
    {
        IntroScreenManager.gameObject.SetActive(true);
        OnIntroCall();
    }

    public void DisableIntroScreen()
    {
        OnIntroCall();
        IntroScreenManager.gameObject.SetActive(false);
        ManageGamePanels(InventoryOpen);
    }

    private void ClothingSelectionManagerSetup()
    {
        InventoryPanelManager.Setup(this);
    }

    private void ShoppingSelectionManagerSetup()
    {
        ShoppingPanelManager.Setup(this);
    }

    private void ClothingDisplaySetup()
    {
        DisplayPanelManager.Setup(this);
    }

    public void OnIntroCall()
    {
        if (onIntroCall == null) return;

        onIntroCall();
    }

    public void OnInventoryCall()
    {
        if (onInventoryCall == null) return;

        InventoryOpen = !InventoryOpen;
        ManageGamePanels(InventoryOpen);
        onInventoryCall();
    }

    public void OnShoppingCall()
    {
        if (onShoppingCall == null) return;

        ShoppingOpen = !ShoppingOpen;
        ManageGamePanels(ShoppingOpen);
        onShoppingCall();
    }

    private void ManageGamePanels(bool activation)
    {
        background.gameObject.SetActive(activation);
        InventoryPanelManager.gameObject.SetActive(InventoryOpen);
        DisplayPanelManager.gameObject.SetActive(activation);
        ShoppingPanelManager.gameObject.SetActive(ShoppingOpen);
        inventoryHUD.SetActive(!activation);
        moneyHUD.SetActive(!activation);
    }
}

