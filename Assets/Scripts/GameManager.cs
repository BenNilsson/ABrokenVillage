using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    public Transform houseParent;

    public List<House> houses = new List<House>();

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    private void Start()
    {
        AddHousesToList();
    }

    private void Update()
    {
        CheckHouseRepairable();
    }

    private void CheckHouseRepairable()
    {
        foreach(House house in houses)
        {
            if (!house.destroyed)
                return;
        }

        // All code execuded, assume all houses are destroyed
        foreach (House house in houses)
        {
            house.repairable = false;
        }

        // Set slime spawn rate to be extremely high
        EnemyManager.instance.spawnPercentage = 100;
        EnemyManager.instance.spawnIntervalCheck = 0.5f;
    }

    private void AddHousesToList()
    {
        for(int i = 0; i < houseParent.childCount; i++)
        {
            houses.Add(houseParent.GetChild(i).gameObject.GetComponent<House>());
        }
    }

    public void RestartGame()
    {
        Debug.Log("Pls tell dev to implement");
    }
}
