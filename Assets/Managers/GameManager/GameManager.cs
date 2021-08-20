using UnityEngine;

/// <summary>
/// Provides other classes with quick reference to PlayerBase, MainCanvasManager, Shopkeeper
/// and DialogueController. Also responsible for pausing the game during menus operations
/// </summary>
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public MainCanvasManager MainCanvasManager { get; private set; }
    public DialogueController DialogueController { get; private set; }
    public PlayerBase Player { get; private set; }
    public ShopkeeperBase Shopkeeper { get; private set; }
    public bool GamePaused { get; private set; } = false;

    public void Awake()
    {
        SingletonSetup();
        FindObjects();
    }

    private void SingletonSetup()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }

    private void FindObjects()
    {
        MainCanvasManager = FindObjectOfType<MainCanvasManager>();
        DialogueController = FindObjectOfType<DialogueController>();
        Player = FindObjectOfType<PlayerBase>();
        Shopkeeper = FindObjectOfType<ShopkeeperBase>();
    }

    private void Start()
    {
        MainCanvasManager.onIntroCall += PauseGame;
        MainCanvasManager.onInventoryCall += PauseGame;
        MainCanvasManager.onShoppingCall += PauseGame;
    }

    private void PauseGame()
    {
        GamePaused = !GamePaused;

        if (GamePaused)
            Time.timeScale = 0f;
        else
            Time.timeScale = 1f;
    }

    private void OnDestroy()
    {
        MainCanvasManager.onIntroCall += PauseGame;
        MainCanvasManager.onInventoryCall -= PauseGame;
        MainCanvasManager.onShoppingCall -= PauseGame;
    }
}
