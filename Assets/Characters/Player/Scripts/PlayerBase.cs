using UnityEngine;

public class PlayerBase : CharacterBase
{
    // Serialized Variables
    [SerializeField] PlayerSO configSO;
    [SerializeField] InventorySO inventory;
    [SerializeField] WalletSO wallet;

    // General Properties
    public PlayerSO ConfigSO { get { return configSO; } }
    public InventorySO Inventory { get { return inventory; } }
    public WalletSO Wallet { get { return wallet; } }

    // Base States
    public PlayerIdleState PlayerIdleState { get; private set; }
    public PlayerWalkState PlayerWalkState { get; private set; }
    public PlayerConversationState PlayerConversationState { get; private set; }

    // Reference Properties
    public DialogueCanvas CurrentDialogueCanvas { get; set; }

    // Components
    public Rigidbody2D rb { get; private set; }

    public void Awake()
    {
        GetComponents();
        SetCharacterStates();
    }

    private void GetComponents()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    protected override void SetCharacterStates()
    {
        PlayerIdleState = new PlayerIdleState(this);
        PlayerWalkState = new PlayerWalkState(this);
        PlayerConversationState = new PlayerConversationState(this);
    }

    private void Start()
    {
        TransitionToState(PlayerWalkState);
    }
}
