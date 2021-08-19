using UnityEngine;
using TMPro;
using UnityEngine.UI;

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

    private GameManager gameManager;
    private DialogueController dialogueController;

    private string defaultNextText;
    public bool DialogueActive { get; private set; }

    private void Awake()
    {
        Animator = GetComponent<Animator>();
    }

    private void Start()
    {
        gameManager = GameManager.Instance;
        dialogueController = gameManager.DialogueController;
        gameObject.SetActive(false);
        DialogueActive = false;
    }

    public void DialogueSetup()
    {
        DialogueActive = true;
        OpenDialogueCanvas();
        DisableNextButton();
        DisableOptionsButtons();
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
        dialogueController.BeginDialogue(this, dialogue[0], animationDelay);
    }

    public void CloseDialogueCanvas()
    {
        headerText.text = "";
        bodyText.text = "";
        Animator.Play("close");
        Invoke(nameof(ControlClosing), Animator.GetCurrentAnimatorClipInfo(0)[0].clip.length);
    }

    private void ControlClosing()
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

    public void EnableOptionsButtons()
    {
        shopButton.SetActive(true);
        exitButton.SetActive(true);
    }

    public void DisableOptionsButtons()
    {
        shopButton.SetActive(false);
        exitButton.SetActive(false);
    }

    public void ActionButton()
    {
        gameManager.MainCanvasManager.OnShoppingCall();
    }

    public void CloseDialogue()
    {
        DialogueActive = false;
        dialogueController.EndDialogue();
        CloseDialogueCanvas();
    }
}
