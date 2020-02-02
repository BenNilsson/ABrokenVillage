using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hammer : Weapon
{
    public LayerMask hammerMask;
    public int repairAmount;

    public override void Interact()
    {
        if (!PlayerManager.instance.isAlive) return;

        base.Interact();

        // Get colliders
        Collider2D[] hits = Physics2D.OverlapCircleAll(PlayerManager.instance.interactArea.position, attackRange, hammerMask);

        // Damage trees
        foreach (Collider2D hit in hits)
        {
            IRepairable repairable = hit.gameObject.GetComponent<IRepairable>();
            if (repairable != null)
            {
                repairable.Repair(repairAmount);
            }
            break;
        }
    }
}
