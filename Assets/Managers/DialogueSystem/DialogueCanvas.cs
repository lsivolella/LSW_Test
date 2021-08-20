using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

/// <summary>
/// Controls dialogues operations in coordination with DialogueController. Responsible for operating
/// the dialogue canvas and also providing DialogueController with content to print.
/// </summary>
public class DialogueCanvas : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI headerText;
    [SerializeField] TextMeshProUGUI bodyText;
    [SerializeField] GameObject nextButton;
    [SerializeField] GameObject shopButton;
    [SerializeField] GameObject exitButton;
    [SerializeField] DialogueSO[] dialogue;

    public TextMeshProUGUI HeaderText { get { return headerText; } }
    public TextMeshProUGUI BodyText { get { return bodyText; } }
    public DialogueSO[] Dialogue { get { return dialogue; } }
    public Animator Animator { get; private set; }
    public int CurrentDialogueIndex { get; private set; } = 0;

    private GameManager gameManager;
    private DialogueController dialogueController;
    private SoundManager soundManager;

    public bool DialogueActive { get; private set; } = false;

    public event Action<int> onDialogueEnd; 

    private void Awake()
    {
        GetReferences();
    }

    private void GetReferences()
    {
        Animator = GetComponent<Animator>();
        soundManager = SoundManager.Instance;
        gameManager = GameManager.Instance;
        dialogueController = gameManager.DialogueController;
    }

    public void CanvasSetup()
    {
        DialogueActive = true;
        OpenDialogueCanvas();
        DisableNextButton();
        DisableShopButton();
        DisableExitButton();
    }

    public void GetNextDialogueSentence()
    {
        DisableNextButton();
        dialogueController.DisplayNextSentence();
    }

    public void OpenDialogueCanvas()
    {
        gameObject.SetActive(true);
        headerText.text = "";
        bodyText.text = "";
        Animator.Play("open");
        float animationDelay = Animator.GetCurrentAnimatorClipInfo(0)[0].clip.length;
        dialogueController.BeginDialogue(this, dialogue[CurrentDialogueIndex], animationDelay);
    }

    public void CloseDialogueCanvas()
    {
        headerText.text = "";
        bodyText.text = "";
        Animator.Play("close");
        Invoke(nameof(HideCanvas), Animator.GetCurrentAnimatorClipInfo(0)[0].clip.length);
        OnDialogueEnd(CurrentDialogueIndex);
        UpdateDialogueIndex();
    }

    public void OnDialogueEnd(int currentDialogueIndex)
    {
        if (onDialogueEnd == null) return;

        onDialogueEnd(currentDialogueIndex);
    }

    private void UpdateDialogueIndex()
    {
        if (CurrentDialogueIndex == Dialogue.Length - 1) return;

        CurrentDialogueIndex++;
    }

    public void ButtonsSetup(int remainingSentences)
    {
        if (remainingSentences > 0)
            EnableNextButton();
        else if (remainingSentences == 0)
        {
            if (CurrentDialogueIndex == 0
                || CurrentDialogueIndex == 1)
                EnableExitButton();
            else
            {
                EnableShopButton();
                EnableExitButton();
            }
        }
    }

    public void HideCanvas()
    {
        gameObject.SetActive(false);
    }

    public void EnableNextButton()
    {
        nextButton.SetActive(true);
    }

    public void DisableNextButton()
    {
        nextButton.SetActive(false);
    }

    public void EnableShopButton()
    {
        shopButton.SetActive(true);
    }

    public void DisableShopButton()
    {
        shopButton.SetActive(false);
    }

    public void EnableExitButton()
    {
        
        exitButton.SetActive(true);
    }

    public void DisableExitButton()
    {
        exitButton.SetActive(false);
    }

    public void ActionButton()
    {
        CloseDialogue();
        gameManager.MainCanvasManager.OnShoppingCall();
    }

    public void CloseDialogue()
    {
        CloseDialogueCanvas();
        DialogueActive = false;
        dialogueController.EndDialogue();
    }

    public void PlaySoundClip()
    {
        soundManager.PlayButtonsSoundEffect();
    }
}
