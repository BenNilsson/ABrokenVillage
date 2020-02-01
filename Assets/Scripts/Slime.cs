﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : Enemy, IDamageable
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(int amount)
    {
        Debug.Log("OUCH");
        health -= amount;
        if (health <= 0) Destroy(gameObject);
    }
}
