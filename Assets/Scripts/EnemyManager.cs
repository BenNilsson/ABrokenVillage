using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager instance = null;

    public List<GameObject> enemiesToSpawn = new List<GameObject>();
    public List<Transform> spawnPoints = new List<Transform>();
    public List<GameObject> houses = new List<GameObject>();

    public float spawnPercentage = 20;
    public float spawnIntervalCheck = 5f;

    private float timeSinceLastCheck;
    private float timeWhenLevelLoaded;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    private void Start()
    {
        timeWhenLevelLoaded = Time.time;
    }

    private void Update()
    {
        if(PlayerManager.instance.isAlive)
        {
            // Ignore anything until 10 seconds has elapsed
            if (Time.time >= timeWhenLevelLoaded + 10)
            {
                if (Time.time > timeSinceLastCheck + spawnIntervalCheck)
                {
                    timeSinceLastCheck = Time.time;
                    if (CheckForMobSpawn())
                    {
                        // Check if spawn points are set up
                        if (spawnPoints.Count != 0)
                        {
                            // Get random spawn point
                            int spawnPoint = Random.Range(0, spawnPoints.Count);
                            // Get random enemy
                            int enemy = Random.Range(0, enemiesToSpawn.Count);

                            // Spawn enemy
                            SpawnEnemy(enemy, spawnPoints[spawnPoint].position);

                        }
                    }
                }
            }
        }
    }

    public bool CheckForMobSpawn()
    {
        // Roll a random number between 0-100
        float rand = Random.Range(0, 100);

        // Check if the number is higher than the current spawn chance
        if(rand <= spawnPercentage)
        {
            // Can spawn a monster
            return true;
        }else
        {
            return false;
        }
    }

    public void SpawnEnemy(int type, Vector3 position)
    {
        // TODO
        // Object Pooling

        GameObject obj = Instantiate(enemiesToSpawn[type], position, Quaternion.identity);
    }
}
