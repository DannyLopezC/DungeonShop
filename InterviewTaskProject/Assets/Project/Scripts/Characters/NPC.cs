using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : Collidable
{
    public Dialogue firstDialogue;
    public Dialogue goodbyeDialogue;

    protected override void OnCollide(Collider2D c)
    {
        if (c.tag == "Player")
        {
            TriggerDialogue();
        }
    }

    public void TriggerDialogue()
    {
        GameManager.instance.dialogueManager.StartDialogue(GameManager.instance.firstDialogue);
    }
}
