using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class LostChangeManager : MonoBehaviour
{
    [SerializeField] int minimumReward = 1;
    [SerializeField] int maximumReward = 20;
    [SerializeField] float minimumRespawn = 30f;
    [SerializeField] float maximumRespawn = 120f;

    private Timer respawnTimer;
    private PlayerBase player;
    private StringBuilder stringBuilder = new StringBuilder();

    private SpriteRenderer spriteRenderer;
    private PopupManager popupManager;
    private MessageCanvasManager messageCanvas;

    private void Awake()
    {
        GetComponents();
        InstantiateTimer();
    }

    private void InstantiateTimer()
    {
        stringBuilder.Append(gameObject.name);
        stringBuilder.Append("_respawn_timer");

        var respawnCooldown = Random.Range(minimumRespawn, maximumRespawn);
        respawnTimer = new Timer(
            respawnCooldown, () =>
            {
                Respawn();
            })
            .SetName(stringBuilder.ToString())
            .RunOnce();
    }

    private void GetComponents()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        popupManager = GetComponentInChildren<PopupManager>();
        messageCanvas = GetComponentInChildren<MessageCanvasManager>();
    }

    private void Start()
    {
        spriteRenderer.enabled = false;
        messageCanvas.DisableCanvas();
    }

    private void Respawn()
    {
        popupManager.ActivatePopup();
    }

    private void OnMouseDown()
    {
        if (!popupManager.TriggerActive) return;

        if (!popupManager.PopupActive) return;

        if (player == null)
            player = popupManager.Player;

        popupManager.PlayFadeAnimation();
        popupManager.DeactivatePopup();
        respawnTimer.Start();

        int reward = RewardPlayer();
        string[] message = MessageSetup(reward);
        CallMessageCanvas(message);
    }

    private int RewardPlayer()
    {
        int reward = Random.Range(minimumReward, maximumReward);

        player.Wallet.AddToWallet(reward);
        return reward;
    }

    private string[] MessageSetup(int reward)
    {
        stringBuilder.Clear();
        stringBuilder.Append("You found $");
        stringBuilder.Append(reward.ToString());
        stringBuilder.Append("!");

        string[] message = new string[1];
        message[0] = stringBuilder.ToString();
        return message;
    }

    private void CallMessageCanvas(string[] text)
    {
        messageCanvas.EnableCanvas();
        messageCanvas.BeginMessage(text);
    }
}
