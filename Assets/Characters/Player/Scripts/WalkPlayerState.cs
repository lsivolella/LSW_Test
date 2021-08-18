using UnityEngine;

public class WalkPlayerState : BaseCharacterState
{
    private PlayerBase player;

    private Vector2 movement;

    public WalkPlayerState(PlayerBase player)
    {
        this.player = player;
    }

    public override void Update()
    {
        GetMovementInputs();
        PlayAnimator();
        player.CastDialogueLinecast();
    }

    private void GetMovementInputs()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        movement = movement.normalized;

        if (movement == Vector2.zero)
            player.TransitionToState(player.idleState);
    }

    private void PlayAnimator()
    {
        if (movement.y > 0)
        {
            player.Animator.Play("walk_back");
            player.ClothingManager.ClothingBackSetup();
            player.LastDirection = Vector2.up;
        }
        else if (movement.y < 0)
        {
            player.Animator.Play("walk_front");
            player.ClothingManager.ClothingFrontSetup();
            player.LastDirection = Vector2.down;

        }
        else if (movement.x > 0)
        {
            player.Animator.Play("walk_side");
            player.ClothingManager.ClothingRightSetup();
            player.LastDirection = Vector2.right;

        }
        else if (movement.x < 0)
        {
            player.Animator.Play("walk_side");
            player.ClothingManager.ClothingLeftSetup();
            player.LastDirection = Vector2.left;

        }
    }

    public override void FixedUpdate()
    {
        MovePlayer();
    }

    private void MovePlayer()
    {
        player.Rigidbody.velocity = movement * player.ConfigSO.MoveSpeed * Time.fixedDeltaTime;
    }
}
