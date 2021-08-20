using UnityEngine;

/// <summary>
/// Play idle animation and make the transition to the walk state
/// </summary>
public class IdlePlayerState : BaseCharacterState
{
    private PlayerBase player;

    public IdlePlayerState(PlayerBase player)
    {
        this.player = player;
    }

    public override void EnterState(BaseCharacterState previousState)
    {
        player.Rigidbody.velocity = Vector2.zero;

        if (player.LastDirection == Vector2.up)
            player.Animator.Play("idle_back");
        else if (player.LastDirection == Vector2.down)
            player.Animator.Play("idle_front");
        else if (player.LastDirection == Vector2.right)
            player.Animator.Play("idle_side");
        else if (player.LastDirection == Vector2.left)
            player.Animator.Play("idle_side");
    }

    public override void Update()
    {
        GetMovementInputs();
    }

    private void GetMovementInputs()
    {
        Vector2 movement = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        if (movement == Vector2.zero) return;

        player.TransitionToState(player.walkState);
    }
}
