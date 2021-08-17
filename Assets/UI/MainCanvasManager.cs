using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainCanvasManager : MonoBehaviour
{
    [SerializeField] Image background;
    [SerializeField] ClothingDisplayManager clothingDisplayManager;
    [SerializeField] ClothingSelectionManager clothingSelectionManager;
    [SerializeField] ShoppingSelectionManager shoppingSelectionManager;

    public ClothingDisplayManager ClothingDisplayManager { get { return clothingDisplayManager; } }
    public ClothingSelectionManager ClothingSelectionManager { get { return clothingSelectionManager; } }
    public ShoppingSelectionManager ShoppingSelectionManager { get { return shoppingSelectionManager; } }

    private readonly List<GameObject> panels = new List<GameObject>();

    private GameManager gameManager;
    private InventoryManager inventoryManager;
    private ClothingManager clothingManager;
    private ClothesSellerInventoryManager clothesSellerInventoryManager;

    // Start is called before the first frame update
    void Awake()
    {
        CreatePanelsList();
        DeactivatePanels();
    }

    private void CreatePanelsList()
    {
        foreach (Transform child in transform)
        {
            panels.Add(child.gameObject);
        }
    }

    private void DeactivatePanels()
    {
        foreach (GameObject panel in panels)
        {
            panel.SetActive(false);
        }
    }

    private void Start()
    {
        gameManager = GameManager.Instance;
        gameManager.onInventoryCall += ManageInventoryPanels;
        gameManager.onShoppingCall += ManageShoppingPanels;

        MainCanvasManagerSetup();
        ClothingSelectionSetup();
        ClothingDisplaySetup();
        ShoppingSelectionSetup();
    }

    private void ManageInventoryPanels()
    {
        background.gameObject.SetActive(gameManager.InventoryOpen);
        ClothingSelectionManager.gameObject.SetActive(gameManager.InventoryOpen);
        ClothingDisplayManager.gameObject.SetActive(gameManager.InventoryOpen);
    }

    private void ManageShoppingPanels()
    {
        background.gameObject.SetActive(gameManager.ShoppingOpen);
        ShoppingSelectionManager.gameObject.SetActive(gameManager.ShoppingOpen);
        ClothingDisplayManager.gameObject.SetActive(gameManager.ShoppingOpen);
    }

    private void MainCanvasManagerSetup()
    {
        inventoryManager = gameManager.Player.GetComponent<InventoryManager>();
        clothingManager = gameManager.Player.GetComponent<ClothingManager>();
        clothesSellerInventoryManager = gameManager.ClotherSeller.GetComponent<ClothesSellerInventoryManager>();
    }

    private void ClothingSelectionSetup()
    {
        ClothingSelectionManager.Setup(inventoryManager, clothingManager);
    }

    private void ClothingDisplaySetup()
    {
        ClothingDisplayManager.Setup(this, clothingManager);
    }

    private void ShoppingSelectionSetup()
    {
        ShoppingSelectionManager.Setup(inventoryManager, clothingManager, clothesSellerInventoryManager);
    }

    private void OnDisable()
    {
        gameManager.onInventoryCall -= ManageInventoryPanels;
        gameManager.onShoppingCall -= ManageShoppingPanels;
    }
}

