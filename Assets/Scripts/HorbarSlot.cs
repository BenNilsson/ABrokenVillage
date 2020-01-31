using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorbarSlot : MonoBehaviour
{

    public Item item;
    public int amount;
    public int hotbarId;

    public Item GetItem()
    {
        return item;
    }
}
