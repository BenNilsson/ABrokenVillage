using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : Enemy, IDamageable
{
    

    

    public void TakeDamage(int amount)
    {
        SoundManager.instance.PlaySound("PlayerHit4", 1.25f);
        health -= amount;
        if (health <= 0) Destroy(gameObject);
    }
}
