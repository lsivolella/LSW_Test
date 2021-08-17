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

        ClothingDisplaySetup();
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

    private void ClothingDisplaySetup()
    {
        ClothingDisplayManager.Setup(this);
    }

    private void OnDisable()
    {
        gameManager.onInventoryCall -= ManageInventoryPanels;
        gameManager.onShoppingCall -= ManageShoppingPanels;
    }
}

