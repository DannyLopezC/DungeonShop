using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public TMP_Text nameText;
    public TMP_Text dialogueText;
    private List<string> _sentences;

    private void Start()
    {
        _sentences = new List<string>();
    }

    public void StartDialogue(Dialogue dialogue)
    {
        nameText.text = dialogue.name;

        _sentences.Clear();
        GameManager.instance.inDialogue = true;

        foreach (string s in dialogue.sentences) _sentences.Add(s);

        DisplaySentence();
    }

    public void DisplaySentence()
    {
        string s = _sentences[0];

        dialogueText.text = s;
    }

    public void EndDialogue()
    {
        GameManager.instance.inDialogue = false;
    }
}
