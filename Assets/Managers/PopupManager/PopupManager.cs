using UnityEngine;

public class PopupManager : MonoBehaviour
{
    private Animator animator;

    /// <summary>
    /// True if popup is to show when player is inside collider trigger.
    /// </summary>
    public bool PopupActive { get; private set; } = true;

    /// <summary>
    /// True if player is inside collider trigger. False if player is outside collider trigger
    /// </summary>
    public bool TriggerActive { get; private set; } = false;
    public PlayerBase Player { get; private set; }

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

    public void ActivatePopup()
    {
        PopupActive = true;
    }

    public void DeactivatePopup()
    {
        PopupActive = false;
    }

    public void PlayEmergeAnimation()
    {
        animator.Play("emerge");
    }

    public void PlayBounceAnimation()
    {
        animator.Play("bounce");
    }

    public void PlayFadeAnimation()
    {
        animator.Play("fade");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;

        if (!PopupActive) return;

        PlayEmergeAnimation();
        TriggerActive = true;

        if (Player != null) return;

        Player = collision.GetComponent<PlayerBase>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;

        if (!PopupActive) return;

        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 0.99f) return;

        PlayBounceAnimation();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;
        
        if (!PopupActive) return;

        PlayFadeAnimation();
        TriggerActive = false;
    }
}
