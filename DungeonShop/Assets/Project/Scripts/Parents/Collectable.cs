using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : Collidable
{
    protected bool collected;

    protected override void OnCollide(Collider2D c)
    {
        if (c.CompareTag("Player")) OnCollect(c);
    }

    protected virtual void OnCollect(Collider2D c) => collected = true;
}