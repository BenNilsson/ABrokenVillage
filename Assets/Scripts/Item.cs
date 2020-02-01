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
    public int id;
    public float interactCd = 1f;
    public float timeSinceLastInteract;
    public Sprite imgSprite;

    private void Start()
    {
        timeSinceLastInteract = Time.time;
        gameObject.name = displayName;
    }

    public virtual void Interact()
    {
        if(PlayerManager.instance != null)
        {
            if(PlayerManager.instance.anim != null)
            {
                PlayerManager.instance.anim.SetTrigger("Swing");
            }
        }
    }

    public void Pickup()
    {
        InventoryManager.instance.AddItemToHotbar(id);
        Destroy(gameObject);
    }
}
