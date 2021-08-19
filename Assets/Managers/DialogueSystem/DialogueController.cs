using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System.Collections;

public class DialogueController : MonoBehaviour
{
    [SerializeField] float letterPrintingDelay = 0.5f;

    private DialogueCanvas currentCanvas;

    private readonly StringBuilder sb = new StringBuilder();
    private readonly Queue<string> sentences = new Queue<string>();

    public void BeginDialogue(DialogueCanvas canvas, DialogueSO dialogueSequence, float animationDelay)
    {
        currentCanvas = canvas;

        foreach (string sentence in dialogueSequence.Sentences)
        {
            sentences.Enqueue(sentence);
        }

        currentCanvas.HeaderText.text = dialogueSequence.NameToDisplay;
        Invoke(nameof(DisplayNextSentence), animationDelay);
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
        }

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(PrintSentence(sentence));
    }

    IEnumerator PrintSentence(string sentence)
    {
        currentCanvas.BodyText.text = "";
        sb.Clear();
        foreach (char letter in sentence)
        {
            sb.Append(letter);
            currentCanvas.BodyText.text = sb.ToString();
            yield return new WaitForSeconds(letterPrintingDelay);
        }

        currentCanvas.ButtonsSetup(sentences.Count);
    }
    public void EndDialogue()
    {
        currentCanvas = null;
        sb.Clear();
        sentences.Clear();
    }

}
