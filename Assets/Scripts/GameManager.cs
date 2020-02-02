using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    [SerializeField] private GameObject deathMenu;
    [SerializeField] private GameObject inventoryMenu;
    public Transform houseParent;

    public List<House> houses = new List<House>();

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    private void OnEnable()
    {
        AddHousesToList();
    }

    private void Update()
    {
        CheckHouseRepairable();
        
        if(!PlayerManager.instance.isAlive)
        {
            // Enable some form of death menu
            deathMenu.SetActive(true);
            inventoryMenu.SetActive(false);
        }
    }

    private void CheckHouseRepairable()
    {
        foreach(House house in houses)
        {
            if (!house.destroyed)
                return;
        }

        // End The Game
        EndGame();
    }

    private void EndGame()
    {
        // All code execuded, assume all houses are destroyed
        foreach (House house in houses)
        {
            house.repairable = false;
        }

        // Set slime spawn rate to be extremely high
        EnemyManager.instance.spawnPercentage = 100;
        EnemyManager.instance.spawnIntervalCheck = 0.5f;
        PlayerManager.instance.canTakeDamage = true;
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
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }
}
