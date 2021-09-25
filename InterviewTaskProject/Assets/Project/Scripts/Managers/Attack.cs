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


    private SpriteRenderer _spriteRenderer;

    public List<Weapon> weapons;

    [Button]
    public void SetNames()
    {
        for (int i = 0; i < weapons.Count; i++)
        {
            if (weapons[i].sprite != null) weapons[i].name = weapons[i].sprite.name;
            else Debug.Log($"No sprite in weapon {weapons[i]}");
        }
    }

    private float _cooldown = 0.5f;
    private float lastSwing;

    protected override void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        equipped = 0;
    }

    private void Update()
    {
        if (Time.time - lastSwing > _cooldown)
        {
            lastSwing = Time.deltaTime;
            Swing();
        }
    }

    private void Swing()
    {
    }

    protected override void OnCollide(Collider2D c)
    {

    }

    public void ChangeWeapon(int equip) =>
        _spriteRenderer.sprite = weapons.Find(w => w.id == Mathf.Clamp(equip, 0, weapons.Count - 1)).sprite;

    [Button]
    public void SetIds()
    {
        for (int i = 0; i < weapons.Count; i++) weapons[i].id = i;
    }
}
