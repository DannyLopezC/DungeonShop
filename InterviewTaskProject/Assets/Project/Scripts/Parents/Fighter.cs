using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighter : MonoBehaviour
{
    //Public fields

    public int life = 10;
    public int maxHitpoint = 10;
    public float pushRecoverySpeed = 0.2f;

    //Inmunity

    protected float inmuneTime = 1f;
    protected float lastInmune;

    //push
    protected Vector3 pushDirection;

    protected virtual void ReceiveDamage(Damage dmg)
    {
        if (Time.time - lastInmune > inmuneTime)
        {
            lastInmune = Time.time;

            life -= dmg.damageAmount;

            pushDirection = (transform.position - dmg.origin).normalized * dmg.pushForce;

            GameManager.instance.ShowText($"{dmg.damageAmount} damage", 35, Color.white, transform.position, Vector3.up * Random.Range(30, 50), 2f);

            if (life <= 0)
            {
                life = 0;
                Death();
            }
        }
    }

    protected virtual void Death()
    {
        gameObject.SetActive(false);
        GameManager.instance.ShowText("Defeated", 50, Color.black, transform.position, Vector3.zero, 3f);
    }
}
