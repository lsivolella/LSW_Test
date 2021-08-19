using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainCanvasManager : MonoBehaviour
{
    [SerializeField] Image background;
    [SerializeField] DisplayPanelManager displayPanelManager;
    [SerializeField] InventoryPanelManager inventoryPanelManager;
    [SerializeField] ShoppingPanelManager shoppingPanelManager;
    [SerializeField] GameObject inventoryHUD;
    [SerializeField] GameObject moneyHUD;

    public DisplayPanelManager DisplayPanelManager { get { return displayPanelManager; } }
    public InventoryPanelManager InventoryPanelManager { get { return inventoryPanelManager; } }
    public ShoppingPanelManager ShoppingPanelManager { get { return shoppingPanelManager; } }
    public GameObject InventoryHUD { get { return inventoryHUD; } }
    public GameObject MoneyHUD { get { return moneyHUD; } }

    private readonly List<GameObject> UIElements = new List<GameObject>();

    public bool InventoryOpen { get; private set; } = false;
    public bool ShoppingOpen { get; private set; } = false;

    public event Action onInventoryCall;
    public event Action onShoppingCall;

    void Awake()
    {
        CreateUIElementsList();

        ManagePanels(InventoryOpen);
    }

    private void CreateUIElementsList()
    {
        foreach (Transform child in transform)
        {
            UIElements.Add(child.gameObject);
        }
    }

    private void Start()
    {
        ClothingSelectionManagerSetup();
        ShoppingSelectionManagerSetup();
        ClothingDisplaySetup();
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

    public void OnInventoryCall()
    {
        if (onInventoryCall == null) return;

        InventoryOpen = !InventoryOpen;
        ManagePanels(InventoryOpen);
        onInventoryCall();
    }

    public void OnShoppingCall()
    {
        if (onShoppingCall == null) return;

        ShoppingOpen = !ShoppingOpen;
        ManagePanels(ShoppingOpen);
        onShoppingCall();
    }

    private void ManagePanels(bool activation)
    {
        background.gameObject.SetActive(activation);
        InventoryPanelManager.gameObject.SetActive(InventoryOpen);
        DisplayPanelManager.gameObject.SetActive(activation);
        ShoppingPanelManager.gameObject.SetActive(ShoppingOpen);
        inventoryHUD.SetActive(!activation);
        moneyHUD.SetActive(!activation);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P) && !InventoryOpen)
            OnShoppingCall();
    }
}

