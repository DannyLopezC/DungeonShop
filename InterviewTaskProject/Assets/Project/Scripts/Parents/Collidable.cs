using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collidable : MonoBehaviour
{
    protected virtual void Start() { }

    protected virtual void OnCollisionEnter2D(Collision2D c) => OnCollide(c.collider);

    protected virtual void OnCollide(Collider2D c) { }
}
