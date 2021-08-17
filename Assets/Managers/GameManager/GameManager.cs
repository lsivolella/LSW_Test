using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public PlayerBase Player { get; private set; }
    public ClothesSellerBase ClotherSeller { get; private set; }

    public event Action onInventoryCall;
    public event Action onShoppingCall;
    public bool InventoryOpen { get; private set; }
    public bool ShoppingOpen { get; private set; }

    public void Awake()
    {
        InventoryOpen = false;

        SingletonSetup();
        FindCharacters();
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

    private void FindCharacters()
    {
        Player = FindObjectOfType<PlayerBase>();
        ClotherSeller = FindObjectOfType<ClothesSellerBase>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !ShoppingOpen)
            OnInventoryCall();
        if (Input.GetKeyDown(KeyCode.P) && !InventoryOpen)
            OnShoppingCall();
    }

    private void OnInventoryCall()
    {
        if (onInventoryCall == null) return;

        InventoryOpen = !InventoryOpen;
        onInventoryCall();
    }

    private void OnShoppingCall()
    {
        if (onShoppingCall == null) return;

        ShoppingOpen = !ShoppingOpen;
        onShoppingCall();
    }
}
