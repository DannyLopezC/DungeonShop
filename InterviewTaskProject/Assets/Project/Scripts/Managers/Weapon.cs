using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[System.Serializable, CreateAssetMenu(fileName = "Weapon_", menuName = "Clothes/Weapons")]
public class Weapon : ScriptableObject
{
    public int id;
    public int damage;
    public float force;

    public Sprite sprite;

    [Button]
    public void SetNames()
    {
        if (sprite != null) this.name = sprite.name;
        else Debug.Log($"No sprite in weapon {id}");
    }
}
