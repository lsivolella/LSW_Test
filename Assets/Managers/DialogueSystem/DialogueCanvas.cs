using UnityEngine;
using TMPro;

public class DialogueCanvas : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI headerText;
    [SerializeField] TextMeshProUGUI bodyText;
    [SerializeField] TextMeshProUGUI nextText;
    [SerializeField] DialogueSequence[] conversation;

    public TextMeshProUGUI HeaderText { get { return headerText; } }
    public TextMeshProUGUI BodyText { get { return bodyText; } }
    public DialogueSequence[] Conversation { get { return conversation; } }
    public Animator Animator { get; private set; }

    private GameManager gameManager;
    private DialogueController dialogueController;

    private bool canvasOpened = false;
    private bool canvasClosed = true;
    private string defaultNextText;

    public bool CanvasOpened { get { return canvasOpened; } }

    private void Awake()
    {
        Animator = GetComponent<Animator>();
        
        defaultNextText = nextText.text;
    }

    private void Start()
    {
        gameManager = GameManager.Instance;
        dialogueController = gameManager.DialogueController;
        gameObject.SetActive(false);
    }

    public void DialogueSetup()
    {
        dialogueController.BeginDialogue(this, this.gameObject, conversation[0]);
    }

    public bool GetNextDialogueSentence()
    {
        return dialogueController.DisplayNextSentence();
    }

    public void OpenDialogueCanvas()
    {
        gameObject.SetActive(true);
        headerText.text = "";
        bodyText.text = "";
        nextText.text = "";
        canvasOpened = false;
        canvasClosed = false;
        Animator.Play("open");
        Invoke(nameof(ControlOpening), Animator.GetCurrentAnimatorClipInfo(0)[0].clip.length);
    }

    private void ControlOpening()
    {
        canvasOpened = true;
        nextText.text = defaultNextText;
    }

    public void CloseDialogueCanvas()
    {
        headerText.text = "";
        bodyText.text = "";
        nextText.text = "";
        Animator.Play("close");
        Invoke(nameof(ControlClosing), Animator.GetCurrentAnimatorClipInfo(0)[0].clip.length);
    }

    private void ControlClosing()
    {
        canvasOpened = false;
        canvasClosed = true;
        gameObject.SetActive(false);
    }
}
