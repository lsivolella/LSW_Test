using UnityEngine;

/// <summary>
/// CharacterBase is the base of complex character behaviour. It has a set of methods that
/// handle state transition, as well as provides MonoBehaviour methods (Start, Update, Collision, etc)
/// to CharacterBaseState, which is not a MonoBehaviour object.
/// </summary>
public abstract class CharacterBase : MonoBehaviour
{
    public BaseCharacterState CurrentState { get; private set; }

    public BaseCharacterState BaseState { get; private set; }

    protected virtual void OnAwake() { }
    protected virtual void OnStart() { }
    protected virtual void OnEnableCall() { }
    protected virtual void OnUpdate() { }
    protected virtual void OnFixedUpdate() { }
    protected virtual void PosOnCollisionEnter2D(Collision2D collision) { }
    protected virtual void PosOnTriggerEnter(Collider2D colission) { }
    protected virtual void PosOnTriggerStay(Collider2D collision) { }
    protected virtual void PosOnTriggerExit(Collider2D collision) { }
    protected virtual void SetBaseCharacterState(BaseCharacterState state)
    {
        BaseState = state;
    }
    protected abstract void SetCharacterStates();

    private void Awake()
    {
        OnAwake();
        SetCharacterStates();
    }

    private void Start()
    {
        OnStart();
    }

    private void OnEnable()
    {
        OnEnableCall();
    }

    private void Update()
    {
        CurrentState?.Update();
        OnUpdate();
    }

    private void FixedUpdate()
    {
        CurrentState?.FixedUpdate();
        OnFixedUpdate();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        CurrentState?.OnCollisionEnter(collision);
        PosOnCollisionEnter2D(collision);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        CurrentState?.OnTriggerEnter(collision);
        PosOnTriggerEnter(collision);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        CurrentState.OnTriggerStay(collision);
        PosOnTriggerStay(collision);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        CurrentState?.OnTriggerExit(collision);
        PosOnTriggerExit(collision);
    }

    public void TriggerAnimationEvent(string name)
    {
        CurrentState?.TriggerAnimationEvent(name);
    }

    public virtual void TransitionToState(BaseCharacterState newState)
    {
        if (!CanTransitionState(newState)) return;

        if (!newState.CanEnterState()) return;

        var previousState = CurrentState;
        CurrentState = newState;

        previousState?.ExitState();
        CurrentState.EnterState(previousState);
    }

    private bool CanTransitionState(BaseCharacterState newState)
    {
        if (CurrentState == null)
            return true;

        if (CurrentState.CanExitState())
            return true;

        if (CurrentState.CanBeInterrupted && newState.ForceInterruption)
            return true;

        return false;
    }
}
