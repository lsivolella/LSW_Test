using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// BaseCharacterState is the foundation of characters states. It provides a set of pre-defined methods
/// that handle state activity and state transitions
/// </summary>
public abstract class BaseCharacterState
{
    public virtual bool ForceInterruption => false;
    public virtual bool CanBeInterrupted => true;
    public virtual bool CanEnterState() => true;
    public virtual bool CanExitState() => CanBeInterrupted;
    public virtual void EnterState(BaseCharacterState previousState) { }
    public virtual void ExitState() { }
    public virtual void TriggerAnimationEvent(string eventName) { }
    public virtual void Update() { }
    public virtual void FixedUpdate() { }
    public virtual void OnCollisionEnter(Collision2D collision) { }
    public virtual void OnCollisionStay(Collision2D collision) { }
    public virtual void OnTriggerEnter(Collider2D collision) { }
    public virtual void OnTriggerStay(Collider2D collision) { }
    public virtual void OnTriggerExit(Collider2D collision) { }
}
