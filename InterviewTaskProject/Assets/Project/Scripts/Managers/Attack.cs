using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : Collidable
{
    public class Weapon
    {
        public int id;
        public int damage;
        public float force;

        public Sprite sprite;
    }

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

    private float _cooldown = 0.5f;
    private float lastSwing;

    protected override void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        equipped = 0;
    }

    private void Update()
    {

    }

    protected override void OnCollide(Collider2D c)
    {

    }

    public void ChangeWeapon(int equip) => _spriteRenderer.sprite = weapons.Find(w => w.id == equip).sprite;


}
