using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Enemy : MonoBehaviour, IDamageable
{
    public int health;

    public virtual void Attack() { }

    public void TakeDamage(int amount)
    {
        Debug.Log("OUCH");
        health -= amount;
        if (health <= 0) Destroy(gameObject);
    }
}
