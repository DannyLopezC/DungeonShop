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
            _equipped = Mathf.Clamp(value, 0, weapons.Count - 1);
            ChangeWeapon(_equipped);
        }
    }

    public Weapon currentWeapon;
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
            Damage dmg = new Damage(transform.position, currentWeapon.damage, currentWeapon.force);

            c.SendMessage("ReceiveDamage", dmg);
        }
    }

    public void Swing()
    {
        _animator.SetTrigger("Swing");
    }

    public void ChangeWeapon(int equip)
    {
        currentWeapon = weapons.Find(w => w.id == equip);

        if (currentWeapon == null) currentWeapon = weapons[0];

        spriteRenderer.sprite = currentWeapon.sprite;
    }

    public void ChangeWeaponV2(int id)
    {
        currentWeapon = weapons[id];
        spriteRenderer.sprite = currentWeapon.sprite;
    }
}
