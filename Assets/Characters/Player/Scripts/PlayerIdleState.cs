using UnityEngine;

public class PlayerIdleState : BaseCharacterState
{
    private PlayerBase player;

    public PlayerIdleState(PlayerBase player)
    {
        this.player = player;
    }

    public override void EnterState(BaseCharacterState previousState)
    {
        player.rb.velocity = Vector2.zero;
    }

    public override void Update()
    {
        CastDialogueLinecast();
        GetMovementInputs();
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

    private void GetMovementInputs()
    {
        Vector2 movement = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        if (movement == Vector2.zero) return;

        player.TransitionToState(player.PlayerWalkState);
    }
}
