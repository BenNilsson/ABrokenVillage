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
    [SerializeField] private GameObject wonMenu;
    [SerializeField] private GameObject pauseMenu;
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

    private void Start()
    {
        AddHousesToList();
        timeSinceLevelLoaded = Time.deltaTime;
        wonMenu.SetActive(false);
        Time.timeScale = 1;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        UpdateTimer();

        if(CheckWin())
        {
            Time.timeScale = 0;
            inventoryMenu.SetActive(false);
            timer.text = "";
            if (wonMenu.activeSelf == false) wonMenu.SetActive(true);
            Cursor.visible = true;
        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame();
        }

        CheckHouseRepairable();
        
        if(!PlayerManager.instance.isAlive)
        {
            // Enable some form of death menu
            deathMenu.SetActive(true);
            inventoryMenu.SetActive(false);
        }
    }

    public void PauseGame()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
        Time.timeScale = 0;
        pauseMenu.SetActive(true);
        deathMenu.SetActive(false);
        inventoryMenu.SetActive(false);
    }

    public void ResumeGame()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Time.timeScale = 1;
        inventoryMenu.SetActive(true);
        UpdateTimer();
        pauseMenu.SetActive(false);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    private bool CheckWin()
    {
        if (timeSinceLevelLoaded >= 300)
        {
            return true;
        }
        else return false;
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
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
