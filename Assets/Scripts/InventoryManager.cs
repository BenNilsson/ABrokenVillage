using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance = null;

    public int curSelectedSlot;

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
        curSelectedSlot = 1;
        SelectInventorySlot(1);
    }

    private void Update()
    {
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

    private void SelectInventorySlot(int number)
    {
        hotbarSlots[curSelectedSlot - 1].outline.enabled = false;
        curSelectedSlot = number;
        hotbarSlots[curSelectedSlot - 1].outline.enabled = true;
        
    }

    private void AddInventorySlots(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            GameObject obj = Instantiate(slotPrefab, new Vector2(0,0), Quaternion.identity, hotbar);
            hotbarSlots.Add(obj.GetComponent<HotbarSlot>());
        }
    }

    public void AddItemToHotbar(Item item)
    {
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
                            break;
                        }
                    }
                }
            }
            else
            {
                // Free slot, add item, then return
                slot.item = item;
                slot.amount++;
                break;
            }
        }
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
