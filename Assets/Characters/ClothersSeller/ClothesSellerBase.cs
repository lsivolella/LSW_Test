using UnityEngine;

public class ClothesSellerBase : CharacterBase
{
    // Serialized Variables
    [SerializeField] InventorySO inventory;

    // General Properties
    public InventorySO Inventory { get { return inventory; } }
    public DialogueCanvas DialogueCanvas { get; private set; }

    // Cached States
    private IdleNpcState idleNpcState;

    protected override void OnAwake()
    {
        DialogueCanvas = GetComponentInChildren<DialogueCanvas>();
    }

    protected override void SetCharacterStates()
    {
        idleNpcState = new IdleNpcState(this);
    }

    protected override void OnStart()
    {
        TransitionToState(idleNpcState);
    }
}
