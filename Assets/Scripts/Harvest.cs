using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Harvest : MonoBehaviour, IHarvestable
{
    public int health = 3;
    public Vector2 amountToDrop = new Vector2(1,3);
    public GameObject itemToDropPrefab;

    public void TakeDamage(int amount)
    {
        health -= amount;
        if (health <= 0)
            HarvestItem();
    }

    public void HarvestItem()
    {
        Debug.Log("Harvested item " + gameObject.name);
        DropItem();
        Destroy(gameObject);
    }

    public void DropItem()
    {
        // Get random drop range
        int dropAmount = Random.Range(Mathf.FloorToInt(amountToDrop.x), Mathf.FloorToInt(amountToDrop.y));
        for(int i = 0; i < dropAmount; i++)
        {
            Vector2 pos = transform.position;
            pos += Random.insideUnitCircle;
            
            ItemDataBase.instance.SpawnItem(itemToDropPrefab.GetComponent<Item>().id, pos);
        }
    }
}
