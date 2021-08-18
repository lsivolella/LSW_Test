using UnityEngine;

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
        GetNextDialogueSentence();
    }

    private void GetNextDialogueSentence()
    {
        if (!Input.GetKeyDown(KeyCode.Space)) return;

        if (player.CurrentDialogueCanvas.GetNextDialogueSentence()) return;
        
        player.CurrentDialogueCanvas = null;
        player.TransitionToState(player.idleState);
    }
}
