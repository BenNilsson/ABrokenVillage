using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : Weapon
{
    public LayerMask swordMask;

    public override void Interact()
    {
        if (!PlayerManager.instance.isAlive) return;

        base.Interact();

        SoundManager.instance.PlaySound("SwordSwing", 0.25f);

        // Get colliders
        Collider2D[] hits = Physics2D.OverlapCircleAll(PlayerManager.instance.interactArea.position, attackRange, swordMask);

        // Damage trees
        foreach (Collider2D hit in hits)
        {
            IHarvestable harvestable = hit.gameObject.GetComponent<IHarvestable>();
            if (harvestable != null)
            {
                harvestable.TakeDamage(dmgAmount);
            }
            break;
        }
    }
}
