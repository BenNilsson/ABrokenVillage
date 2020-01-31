using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Axe : Weapon
{
    public override void Interact()
    {
        base.Interact();
        Debug.Log("Attack with axe");
    }
}
