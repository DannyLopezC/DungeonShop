using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using System;

public class Attack : Collidable
{
    private int _equipped;

    public int equipped
    {
        get { return _equipped; }
        set
        {
            _equipped = value;
            ChangeWeapon(_equipped);
        }
    }

    private Weapon _currentWeapon;
    public SpriteRenderer spriteRenderer;

    [InlineEditor]
    public List<Weapon> weapons;

    private float _cooldown = 0.5f;
    private float lastSwing;

    private Animator _animator;

    protected override void Start()
    {
        equipped = 0;
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (Time.time - lastSwing > _cooldown)
            {
                lastSwing = Time.deltaTime;
                Swing();
            }
        }
    }

    protected override void OnCollide(Collider2D c)
    {
        if (c.tag == "Enemy")
        {
            Damage dmg = new Damage(transform.position, _currentWeapon.damage, _currentWeapon.force);

            c.SendMessage("ReceiveDamage", dmg);
        }
    }

    public void Swing()
    {
        _animator.SetTrigger("Swing");
    }

    public void ChangeWeapon(int equip)
    {
        _currentWeapon = weapons.Find(w => w.id == Mathf.Clamp(equip, 0, weapons.Count - 1));

        if (_currentWeapon == null) _currentWeapon = weapons[0];

        spriteRenderer.sprite = _currentWeapon.sprite;
    }

    [Button]
    public void SetIds()
    {
        for (int i = 0; i < weapons.Count; i++) weapons[i].id = i;
    }
}
