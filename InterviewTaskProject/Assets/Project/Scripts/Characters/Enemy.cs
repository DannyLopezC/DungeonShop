using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : Fighter
{
    public float triggerLength = 1;
    public float chaseLength = 5;

    public int damage;
    public float force;

    private bool _chasing;
    private bool _goingHome;
    private bool _inHome;
    private Transform _player;

    public Transform homeTransform;
    private Vector3 _startingPos;

    private Vector3 _moveDelta;

    protected override void Start()
    {
        base.Start();

        _player = GameManager.instance.player.transform;
        _startingPos = homeTransform.position;
    }

    private void FixedUpdate()
    {
        if (_chasing) Movement(_player.position);
        else if (_goingHome) Movement(_startingPos);

        _chasing = Vector3.Distance(_player.position, _startingPos) < chaseLength &&
            Vector3.Distance(_player.position, _startingPos) < triggerLength &&
            GameManager.instance.player.isActiveAndEnabled;

        _goingHome = !_chasing && !_inHome;
    }

    private void Update()
    {
        //GetComponentInChildren<Image>().fillAmount = life / maxLife;
    }

    private void OnTriggerEnter2D(Collider2D c)
    {
        DoDamage(c);

        if (_goingHome) HomeReached(c);
    }

    private void OnCollisionEnter2D(Collision2D c)
    {
        DoDamage(c.collider);

        if (_goingHome) HomeReached(c.collider);
    }

    private void OnTriggerExit2D(Collider2D c)
    {
        ExitHome(c);
    }

    private void OnCollisionExit2D(Collision2D c)
    {
        ExitHome(c.collider);
    }

    private void HomeReached(Collider2D c)
    {
        if (c.tag == "Home") _inHome = true;
    }

    private void ExitHome(Collider2D c)
    {
        if (c.tag == "Home") _inHome = false;
    }
    private void DoDamage(Collider2D c)
    {
        if (c.tag == "Player")
        {
            Damage dmg = new Damage(transform.position, damage, force);

            c.SendMessage("ReceiveDamage", dmg);
        }
    }

    private void Movement(Vector3 objective)
    {
        float x = objective.x > transform.position.x ? 1 : -1;
        float y = objective.y > transform.position.y ? 1 : -1;

        _moveDelta = new Vector3(x, y);

        //push
        _moveDelta += pushDirection;
        pushDirection = Vector3.Lerp(pushDirection, Vector3.zero, pushRecoverySpeed);

        if (_moveDelta.x < 0) transform.localScale = Vector3.one;
        else if (_moveDelta.x > 0) transform.localScale = new Vector3(-1, 1, 1);

        transform.Translate((_moveDelta * Time.deltaTime) / 2);
    }
}
