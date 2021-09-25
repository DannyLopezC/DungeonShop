using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private BoxCollider2D _collider;
    private Vector3 _moveDelta;

    private void Start()
    {
        _collider = GetComponent<BoxCollider2D>();
    }

    private void FixedUpdate()
    {
        _moveDelta = Vector3.zero;

        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        //set delta
        _moveDelta = new Vector3(x, y);

        //change facing direction
        if (_moveDelta.x > 0) transform.localScale = Vector3.one;
        else if (_moveDelta.x < 0) transform.localScale = new Vector3(-1, 1, 1);

        //movement
        transform.Translate(_moveDelta * Time.deltaTime);
    }
}
