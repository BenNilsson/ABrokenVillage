using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Item : MonoBehaviour
{
    public string displayName;
    public bool interactable;
    public int stackSize = 1;
    public bool stackable;
    public virtual void Interact() { }
}
