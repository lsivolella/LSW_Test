using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System.Text;
using System.Collections;

/// <summary>
/// Displays simple messages to the player. It is a fusion of DialogueCanvas and DialogueController 
/// classes, but it only takes simple messages.
/// NOTE: improve on MessageCanvasManager to replace DialogueCanvas and DialogueController
/// </summary>
public class MessageCanvasManager : MonoBehaviour
{
    [SerializeField] float letterPrintingDeley = 0.02f;
    [SerializeField] TextMeshProUGUI bodyText;
    [SerializeField] GameObject nextButton;
    [SerializeField] GameObject exitButton;

    public TextMeshProUGUI BodyText { get { return bodyText; } }
    public Animator Animator { get; private set; }

    private readonly Queue<string> sentences = new Queue<string>();
    private readonly StringBuilder stringBuilder = new StringBuilder();

    public bool MessageActive { get; private set; } = false;

    private SoundManager soundManager;

    private void Awake()
    {
        Animator = GetComponent<Animator>();
        soundManager = SoundManager.Instance;
    }

    public void BeginMessage(string[] messages)
    {
        MessageActive = true;
        CanvasInitialSetup();
        foreach (string message in messages)
            sentences.Enqueue(message);

        var animationDelay = GetAnimationDelay();
        Invoke(nameof(DisplayNextSentence), animationDelay);
    }

    private void CanvasInitialSetup()
    {
        OpenDialogueCanvas();
        ClearBodyText();
        DisableNextButton();
        DisableExitButton();
    }

    private float GetAnimationDelay()
    {
        return Animator.GetCurrentAnimatorClipInfo(0)[0].clip.length;
    }

    private void ClearBodyText()
    {
        bodyText.text = "";
    }

    public void DisplayNextSentence()
    {
        DisableNextButton();
        StopAllCoroutines();
        string sentence = sentences.Dequeue();
        StartCoroutine(PrintSentence(sentence));
    }

    IEnumerator PrintSentence(string sentence)
    {
        ClearBodyText();
        stringBuilder.Clear();

        foreach (char letter in sentence)
        {
            stringBuilder.Append(letter);
            bodyText.text = stringBuilder.ToString();
            yield return new WaitForSeconds(letterPrintingDeley);
        }
        ButtonsSetup();
    }

    private void ButtonsSetup()
    {
        if (sentences.Count > 0)
            EnableNextButton();
        else if (sentences.Count == 0)
            EnableExitButton();
    }

    public void FinishMessage()
    {
        MessageActive = false;
        ClearBodyText();
        stringBuilder.Clear();
        sentences.Clear();
        CloseDialogueCanvas();
        Invoke(nameof(DisableCanvas), GetAnimationDelay());
    }

    public void EnableCanvas()
    {
        gameObject.SetActive(true);
    }

    private void OpenDialogueCanvas()
    {
        Animator.Play("open");
    }

    private void CloseDialogueCanvas()
    {
        Animator.Play("close");
    }

    public void DisableCanvas()
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

    public void EnableExitButton()
    {

        exitButton.SetActive(true);
    }

    public void DisableExitButton()
    {
        exitButton.SetActive(false);
    }

    public void PlaySoundClip()
    {
        soundManager.PlayButtonsSoundEffect();
    }
}
