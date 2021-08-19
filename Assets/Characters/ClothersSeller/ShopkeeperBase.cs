using UnityEngine;
using static ClothingSO;

public class ShopkeeperBase : CharacterBase
{
    // Serialized Variables
    [SerializeField] InventorySO inventory;

    // General Properties
    public InventorySO Inventory { get { return inventory; } }
    public DialogueCanvas DialogueCanvas { get; private set; }
    public PopupManager PopupManager { get; private set; }

    // Cached States
    private IdleNpcState idleNpcState;

    private PlayerBase player;

    protected override void OnAwake()
    {
        DialogueCanvas = GetComponentInChildren<DialogueCanvas>();
        DialogueCanvas.onDialogueEnd += GiftPlayer;

        PopupManager = GetComponentInChildren<PopupManager>();
    }

    protected override void SetCharacterStates()
    {
        idleNpcState = new IdleNpcState(this);
    }

    protected override void OnStart()
    {
        TransitionToState(idleNpcState);
        DialogueCanvas.HideCanvas();
    }

    private void OnMouseDown()
    {
        if (!PopupManager.TriggerActive) return;

        if (DialogueCanvas.DialogueActive) return;

        if (player == null)
            player = PopupManager.Player;

        PopupManager.PlayFadeAnimation();
        player.BeginDialogue(DialogueCanvas);
        DialogueCanvas.CanvasSetup();
    }

    public void GiftPlayer(int currentDialogueIndex)
    {
        if (currentDialogueIndex != 0) return;

        GiftClothing(ClothingType.Shirt);
        GiftClothing(ClothingType.Pants);
        GiftClothing(ClothingType.Shoes);
    }

    private void GiftClothing(ClothingType clothingType)
    {
        int random;

        do
            random = GenerateRandom(0, Inventory.Container.Count);
        while (Inventory.Container[random].ItemType != clothingType);

        player.Inventory.AddItem(Inventory.Container[random]);
    }

    private int GenerateRandom(int min, int max)
    {
        return(Random.Range(min, max));
    }
}
