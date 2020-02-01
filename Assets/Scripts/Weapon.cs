﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Item
{
    public LayerMask enemies;
    public float attackRange = 1f;
    public int dmgAmount;

    public override void Interact()
    {
        // Get enemies
        Collider2D[] hits = Physics2D.OverlapCircleAll(PlayerManager.instance.interactArea.position, attackRange, enemies);

        // Damage enemies
        foreach(Collider2D enemy in hits)
        {
            IDamageable damageable = enemy.gameObject.GetComponent<IDamageable>();
            if(damageable != null)
            {
                damageable.TakeDamage(dmgAmount);
            }
        }
    }
}
