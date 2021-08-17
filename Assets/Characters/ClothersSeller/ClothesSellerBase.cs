using UnityEngine;

public class ClothesSellerBase : CharacterBase
{
    // Serialized Variables
    [SerializeField] InventorySO inventory;

    // General Properties
    public InventorySO Inventory { get { return inventory; } }

    private IdleNpcState idleNpcState;

    protected override void SetCharacterStates()
    {
        idleNpcState = new IdleNpcState(this);
    }

    private void Start()
    {
        TransitionToState(idleNpcState);
    }
}
