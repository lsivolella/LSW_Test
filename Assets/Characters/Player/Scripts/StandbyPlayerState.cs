using UnityEngine;

/// <summary>
/// StandbyPlayerState locks player in a immobile state while dialogues and mesages take place
/// </summary>
public class StandbyPlayerState : BaseCharacterState
{
    private PlayerBase player;

    public StandbyPlayerState(PlayerBase player)
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
        FinishDialogue();
        FinishMessage();
    }

    private void FinishDialogue()
    {
        if (player.CurrentDialogueCanvas == null) return;

        if (player.CurrentDialogueCanvas.DialogueActive) return;
        
        player.CurrentDialogueCanvas = null;
        player.TransitionToState(player.idleState);
    }

    private void FinishMessage()
    {
        if (player.CurrentMessage == null) return;

        if (player.CurrentMessage.MessageActive) return;

        player.CurrentMessage = null;
        player.TransitionToState(player.idleState);
    }
}
