using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : Enemy, IDamageable
{
    

    

    public void TakeDamage(int amount)
    {
        Debug.Log("OUCH");
        health -= amount;
        if (health <= 0) Destroy(gameObject);
    }
}
