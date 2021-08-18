using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Text;
using System.Collections;

public class DialogueController : MonoBehaviour
{
    [SerializeField] float letterPrintingSpeed = 2.0f;

    private PlayerBase player;
    private ClothesSellerBase clothersSeller;

    private DialogueCanvas currentCanvas;
    private GameObject interlocutor;

    private readonly StringBuilder sb = new StringBuilder();
    private readonly Queue<string> sentences = new Queue<string>();

    public void BeginDialogue(DialogueCanvas canvas, GameObject interlocutor, DialogueSequence dialogueSequence)
    {
        currentCanvas = canvas;
        this.interlocutor = interlocutor;

        foreach (string sentence in dialogueSequence.Sentences)
        {
            sentences.Enqueue(sentence);
        }

        currentCanvas.OpenDialogueCanvas();
        Invoke(nameof(DisplayNextSentence), currentCanvas.Animator.GetCurrentAnimatorClipInfo(0)[0].clip.length);
    }

    public bool DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return false;
        }

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(PrintSentence(sentence));
        return true;
    }

    IEnumerator PrintSentence(string sentence)
    {
        currentCanvas.BodyText.text = "";
        sb.Clear();
        foreach (char letter in sentence)
        {
            sb.Append(letter);
            currentCanvas.BodyText.text = sb.ToString();
            yield return new WaitForSeconds(letterPrintingSpeed);
        }
    }
    private void EndDialogue()
    {
        currentCanvas.CloseDialogueCanvas();
        currentCanvas = null;
        interlocutor = null;
        sb.Clear();
        sentences.Clear();
    }

}
