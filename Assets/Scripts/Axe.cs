﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Axe : Weapon
{
    public LayerMask treeMask;

    public override void Interact()
    {
        base.Interact();

        // Get colliders
        Collider2D[] hits = Physics2D.OverlapCircleAll(PlayerManager.instance.interactArea.position, attackRange, treeMask);

        // Damage trees
        foreach (Collider2D hit in hits)
        {
            IHarvestable harvestable = hit.gameObject.GetComponent<IHarvestable>();
            if (harvestable != null)
            {
                harvestable.TakeDamage(1);
            }
            break;
        }
    }
}
