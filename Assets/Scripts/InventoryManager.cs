using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] private int slotAmount;
    [SerializeField] private Transform hotbar;
    [SerializeField] private GameObject slotPrefab;

    private void Start()
    {
        AddInventorySlots(slotAmount);
    }


    private void AddInventorySlots(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            GameObject obj = Instantiate(slotPrefab, new Vector2(0,0), Quaternion.identity, hotbar);
        }
    }
}
