using UnityEngine;

public class IdleAndWalkState : BaseCharacterState
{
    private PlayerBase player;

    private Vector2 movement;

    public IdleAndWalkState(PlayerBase player)
    {
        this.player = player;
    }

    public override void Update()
    {
        GetMovementInputs();
    }

    private void GetMovementInputs()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        movement = movement.normalized;
    }

    public override void FixedUpdate()
    {
        MovePlayer();
    }

    private void MovePlayer()
    {
        player.rb.velocity = movement * player.ConfigSO.MoveSpeed * Time.fixedDeltaTime;
    }
}
