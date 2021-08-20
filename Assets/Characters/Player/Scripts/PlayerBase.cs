using UnityEngine;

/// <summary>
/// PlayerBase inherits basic state transition and operation from CharacterBase, as well as operating
/// more complex activities for the player. It also provides easy access to components often used by
/// states
/// </summary>
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
    public IdlePlayerState idleState { get; private set; }
    public WalkPlayerState walkState { get; private set; }
    public StandbyPlayerState standbyState { get; private set; }

    // Reference Properties
    public DialogueCanvas CurrentDialogueCanvas { get; set; }
    public MessageCanvasManager CurrentMessage { get; set; }

    // Components
    public ClothingManager ClothingManager { get; private set; }
    public Rigidbody2D Rigidbody { get; private set; }
    public Animator Animator { get; private set; }

    // General Variables
    public Vector2 LastDirection { get; set; } = Vector2.down;

    public void Awake()
    {
        GetComponents();
        SetCharacterStates();
    }

    private void GetComponents()
    {
        ClothingManager = GetComponent<ClothingManager>();
        Rigidbody = GetComponent<Rigidbody2D>();
        Animator = GetComponentInChildren<Animator>();
    }

    protected override void SetCharacterStates()
    {
        idleState = new IdlePlayerState(this);
        walkState = new WalkPlayerState(this);
        standbyState = new StandbyPlayerState(this);
    }

    private void Start()
    {
        TransitionToState(walkState);
    }

    public void BeginDialogue(DialogueCanvas currentCanvas)
    {
        CurrentDialogueCanvas = currentCanvas;
        TransitionToState(standbyState);
    }

    public void BeginMessage(MessageCanvasManager currentMessage)
    {
        CurrentMessage = currentMessage;
        TransitionToState(standbyState);
    }
}
