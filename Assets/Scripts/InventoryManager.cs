using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance = null;

    public int curSelectedSlot;
    public Item curItem;

    [SerializeField] private int slotAmount;
    [SerializeField] private Transform hotbar;
    [SerializeField] private GameObject slotPrefab;

    public List<HotbarSlot> hotbarSlots = new List<HotbarSlot>();

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    private void Start()
    {
        AddInventorySlots(slotAmount);

        AddItemToHotbar(2);
        AddItemToHotbar(0);
        AddItemToHotbar(3);

        curSelectedSlot = 1;
        SelectInventorySlot(1);
    }

    private void Update()
    {
        // Interact with item
        if(Input.GetMouseButton(0))
        {
            if (curItem != null)
            {
                if(curItem.interactable)
                {
                    if(Time.time >= curItem.timeSinceLastInteract + curItem.interactCd)
                    {
                        curItem.Interact();
                        curItem.timeSinceLastInteract = Time.time;
                    }
                }
            }
        }

        // Drop Item
        if(Input.GetKeyDown(KeyCode.Q))
        {
            DropSelectedItem();
        }

        // Select inventory option
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            SelectInventorySlot(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SelectInventorySlot(2);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SelectInventorySlot(3);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            SelectInventorySlot(4);
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            SelectInventorySlot(5);
        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            SelectInventorySlot(6);
        }
        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            SelectInventorySlot(7);
        }
        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            SelectInventorySlot(8);
        }
        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            SelectInventorySlot(9);
        }
    }

    private void DropSelectedItem()
    {
        HotbarSlot slot = hotbarSlots[curSelectedSlot - 1];
        Item i = slot.GetItem();

        if(i != null)
        {
            int itemAmount = slot.amount;
            for (int j = 0; j < itemAmount; j++)
            {
                Vector2 pos = PlayerManager.instance.gameObject.transform.position;
                pos.x += 1.5f;
                ItemDataBase.instance.SpawnItem(slot.item.id, pos);
            }

            slot.gameObject.transform.GetChild(0).GetComponent<Image>().enabled = false;
            slot.amount = 0;
            if (slot.item.stackable) slot.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = "";
            slot.item = null;
        }
    }

    private void SelectInventorySlot(int number)
    {
        hotbarSlots[curSelectedSlot - 1].outline.enabled = false;
        curSelectedSlot = number;
        hotbarSlots[curSelectedSlot - 1].outline.enabled = true;
        if (hotbarSlots[curSelectedSlot - 1].item != null)
        {
            curItem = hotbarSlots[curSelectedSlot - 1].item;
            if(curItem.interactable)
            {
                curItem.timeSinceLastInteract = Time.time;
            }
        }
        else curItem = null;
    }

    private void AddInventorySlots(int amount)
    {
        GameObject obj = null;
        for (int i = 0; i < amount; i++)
        {
            obj = Instantiate(slotPrefab, new Vector2(0,0), Quaternion.identity, hotbar);
            obj.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = (i + 1).ToString();
            hotbarSlots.Add(obj.GetComponent<HotbarSlot>());
        } 
    }

    public bool AddItemToHotbar(int id)
    {
        Item item = ItemDataBase.instance.GetItemFromList(id);
        if (item == null) return false;

        // Check if there are any free slots
        foreach(HotbarSlot slot in hotbarSlots)
        {
            if(slot.item != null)
            {
                // Slot contains item. Check if it contains the same item
                if (slot.item == item)
                {
                    // Check if it can be stacked
                    if (slot.item.stackable)
                    {
                        // Check if it is lower than the current stack size, if so add another item
                        if (slot.amount < slot.item.stackSize)
                        {
                            slot.amount++;
                            slot.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = slot.amount.ToString();
                            return true;
                        }
                    }
                    else
                    {
                        slot.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = "";
                    }
                }
            }
            else
            {
                // Free slot, add item, then return
                slot.item = item;
                slot.amount++;
                slot.gameObject.transform.GetChild(0).GetComponent<Image>().enabled = true;
                slot.gameObject.transform.GetChild(0).GetComponent<Image>().sprite = item.imgSprite;
                if(slot.item.stackable) slot.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = slot.amount.ToString();
                return true;
            }
        }
        return false;
    }

    private HotbarSlot GetHotbarSlot(int number)
    {
        if (number > slotAmount || number < 1)
        {
            Debug.Log("Slot Range is out of bounds");
            return null;
        }
        else
        {
            return hotbarSlots[number];
        }
    }

}
