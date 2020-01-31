using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ItemDataBase : MonoBehaviour
{
    public static ItemDataBase instance = null;

    public List<GameObject> itemObjs = new List<GameObject>();
    public List<Item> items = new List<Item>();

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    private void Start()
    {
        foreach(GameObject obj in itemObjs)
        {
            Item item = obj.GetComponent<Item>();
            items.Add(item);
        }

        SpawnItem(0, new Vector3(2, 0, 0));
    }

    public Item GetItemFromList(int id)
    {
        Item item = items.Where(obj => obj.id == id).SingleOrDefault();
        if (item != null)
            return item;
        else return null;
    }

    public void SpawnItem(int id, Vector3 position)
    {
        Item i = items.Where(obj => obj.id == id).SingleOrDefault();
        if (i != null)
        {
            GameObject itemObj = itemObjs.Where(obj => obj.GetComponent<Item>().displayName == i.displayName).SingleOrDefault();
            Instantiate(itemObj, position, Quaternion.identity);
        }
    }
}
