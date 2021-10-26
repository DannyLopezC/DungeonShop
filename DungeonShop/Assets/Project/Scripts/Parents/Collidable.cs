using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collidable : MonoBehaviour
{
    protected virtual void Start()
    {
    }

    private void OnCollisionEnter2D(Collision2D c) => OnCollide(c.collider);
    private void OnTriggerEnter2D(Collider2D c) => OnCollide(c);

    private void OnCollisionExit2D(Collision2D c) => OnExitCollide(c.collider);
    private void OnTriggerExit2D(Collider2D c) => OnExitCollide(c);

    protected virtual void OnCollide(Collider2D c)
    {
    }

    protected virtual void OnExitCollide(Collider2D c)
    {
    }
}