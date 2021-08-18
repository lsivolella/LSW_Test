using UnityEngine;

public class PlayerConversationState : BaseCharacterState
{
    private PlayerBase player;

    public PlayerConversationState(PlayerBase player)
    {
        this.player = player;
    }

    public override void EnterState(BaseCharacterState previousState)
    {
        player.rb.velocity = Vector2.zero;
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
        player.TransitionToState(player.PlayerIdleState);
    }
}
