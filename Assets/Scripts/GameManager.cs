using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    [SerializeField] private GameObject deathMenu;
    [SerializeField] private GameObject inventoryMenu;
    [SerializeField] private TextMeshProUGUI timer;
    public Transform houseParent;

    private float timeSinceLevelLoaded;

    private float minutes;
    private float seconds;

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
        timeSinceLevelLoaded = Time.deltaTime;
    }

    private void Update()
    {
        UpdateTimer();

        if(CheckWin())
        {
            Debug.Log("YOU WON!!!!");
            Time.timeScale = 0;
        }

        CheckHouseRepairable();
        
        if(!PlayerManager.instance.isAlive)
        {
            // Enable some form of death menu
            deathMenu.SetActive(true);
            inventoryMenu.SetActive(false);
        }
    }

    private bool CheckWin()
    {
        if (timeSinceLevelLoaded >= 300)
        {
            return true;
        }
        else return false;
    }

    private void UpdateTimer()
    {
        timeSinceLevelLoaded += Time.deltaTime;

        minutes = Mathf.Floor(timeSinceLevelLoaded / 60);
        seconds = Mathf.RoundToInt(timeSinceLevelLoaded % 60);

        string min = "";
        string sec = "";

        if (minutes != 0)
            min = minutes.ToString();
        if (seconds < 10)
            sec = "0" + Mathf.RoundToInt(seconds).ToString();
        else sec = Mathf.RoundToInt(seconds).ToString();


        if (minutes != 0)
            timer.text = min + ":" + sec;
        else timer.text = sec;
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
