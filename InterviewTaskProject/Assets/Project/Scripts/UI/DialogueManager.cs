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
    public GameObject exitButton, shopButton;

    public bool buttonsOff
    {
        get { return _buttonsOff; }
        set
        {
            if (!value)
            {
                exitButton.SetActive(!value);
                shopButton.SetActive(!value);
            }
            else
            {
                exitButton.SetActive(!value);
                shopButton.SetActive(!value);
            }

            _buttonsOff = value;
        }
    }

    private bool _buttonsOff;

    public GameManager gm;

    public Animator animator;

    private void Start()
    {
        _sentences = new List<string>();
        gm = GameManager.instance;
    }

    private void Update()
    {

    }

    public void StartDialogue(Dialogue dialogue)
    {
        nameText.text = dialogue.name;

        _sentences.Clear();
        gm.inDialogue = true;

        foreach (string s in dialogue.sentences) _sentences.Add(s);

        animator.SetTrigger("show");
        DisplaySentence();
    }

    public void DisplaySentence()
    {
        string s = _sentences[Random.Range(0, _sentences.Count - 1)];

        StopAllCoroutines();
        StartCoroutine(Type(s));
    }

    IEnumerator Type(string s, bool goodbye = false)
    {
        buttonsOff = goodbye;
        gm.inDialogue = true;
        dialogueText.text = "";
        foreach (char l in s.ToCharArray())
        {
            dialogueText.text += l;
            yield return new WaitForSeconds(0.05f);

        }

        if (goodbye)
        {
            yield return new WaitForSeconds(1.5f);
            animator.SetTrigger("hide");
            gm.inDialogue = false;
            _buttonsOff = false;
        }
    }

    public void EndDialogue(bool accept)
    {
        if (accept)
        {
            animator.SetTrigger("hide");
            gm.uIManager.OnShop(true);
            gm.inDialogue = false;
        }
        else
        {
            if (!gm.inDialogue) animator.SetTrigger("show");
            string s = gm.goodbyeDialogue.sentences[Random.Range(0, gm.goodbyeDialogue.sentences.Count - 1)];
            StopAllCoroutines();
            StartCoroutine(Type(s, true));
        }
    }
}
