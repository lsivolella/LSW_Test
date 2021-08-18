using UnityEngine;
using System;

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
