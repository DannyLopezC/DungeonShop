using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeFountain : Collidable
{
    private bool _healing;
    public float healAmount;

    protected override void OnCollide(Collider2D c)
    {
        if (c.tag == "Player") _healing = true;
    }

    protected override void OnExitCollide(Collider2D c)
    {
        if (c.tag == "Player") _healing = false;
    }

    private void Update()
    {
        if (_healing) GameManager.instance.player.life += healAmount;
    }
}
