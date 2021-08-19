using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public MainCanvasManager MainCanvasManager { get; private set; }
    public DialogueController DialogueController { get; private set; }
    public PlayerBase Player { get; private set; }
    public ShopkeeperBase Shopkeeper { get; private set; }


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
}
