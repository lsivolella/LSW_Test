using UnityEngine;

public class PlayerBase : CharacterBase
{
    // Serialized Variables
    [SerializeField] PlayerSO configSO;

    // General Properties
    public PlayerSO ConfigSO { get { return configSO; } }

    // Base States
    public IdleAndWalkState IdleAndWalkState { get; private set; }

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
        IdleAndWalkState = new IdleAndWalkState(this);
    }

    private void Start()
    {
        TransitionToState(IdleAndWalkState);
    }
}