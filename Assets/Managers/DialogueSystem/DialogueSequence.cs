using UnityEngine;
using System;

/// <summary>
/// Dialogue sentence configuration, used as a variable of DialogueSO
/// </summary>
[Serializable]
public class DialogueSequence
{
    [SerializeField] string conversationTitle;
    [SerializeField] string nameToDisplay;
    [TextArea(3, 10)]
    [SerializeField] string[] sentences;

    public string ConversationTitle { get { return conversationTitle; } }
    public string NameToDisplay { get { return nameToDisplay; } }
    public string[] Sentences { get { return sentences; } }
}
