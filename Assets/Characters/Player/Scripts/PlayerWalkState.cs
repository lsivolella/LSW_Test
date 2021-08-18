using UnityEngine;

public class PlayerWalkState : BaseCharacterState
{
    private PlayerBase player;

    private Vector2 movement;

    public PlayerWalkState(PlayerBase player)
    {
        this.player = player;
    }

    public override void Update()
    {
        GetMovementInputs();
        CastDialogueLinecast();
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

    private void CastDialogueLinecast()
    {
        if (!Input.GetKeyDown(KeyCode.Space)) return;
        Debug.Log("Raycast");
        Vector2 playerPosition = player.transform.position;
        float linecastDistance = 5f;
        var linecast = Physics2D.Linecast(playerPosition,
            playerPosition + (Vector2.up * linecastDistance), LayerMask.GetMask("NPC"));

        if (linecast.collider == null) return;

        player.CurrentDialogueCanvas = linecast.collider.GetComponent<ClothesSellerBase>()
            .DialogueCanvas;
        player.CurrentDialogueCanvas.DialogueSetup();
        player.TransitionToState(player.PlayerConversationState);
    }
}
