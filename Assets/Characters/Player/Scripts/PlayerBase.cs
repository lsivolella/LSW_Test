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
    public IdlePlayerState idleState { get; private set; }
    public WalkPlayerState walkState { get; private set; }
    public StandbyPlayerState standbyState { get; private set; }

    // Reference Properties
    public DialogueCanvas CurrentDialogueCanvas { get; set; }

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

    public void CastDialogueLinecast()
    {
        if (!Input.GetKeyDown(KeyCode.Space)) return;

        Vector2 playerPosition = transform.position;
        float linecastDistance = 5f;
        var linecast = Physics2D.Linecast(playerPosition,
            playerPosition + (LastDirection * linecastDistance), LayerMask.GetMask("NPC"));

        if (linecast.collider == null) return;

        CurrentDialogueCanvas = linecast.collider.GetComponent<ClothesSellerBase>()
            .DialogueCanvas;
        CurrentDialogueCanvas.DialogueSetup();
        TransitionToState(standbyState);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, (Vector2)transform.position + (LastDirection * 5));
    }
}
