using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour, IDamageable
{
    public static PlayerManager instance = null;

    public Transform interactArea;

    [SerializeField] private int maxHealth = 10;

    public int GetHealth { get { return curHealth; } }
    private int curHealth;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    void Start()
    {
        curHealth = maxHealth;
    }

    public void RemoveHealth(int amount)
    {
        curHealth -= amount;
        if (curHealth <= 0) Destroy(gameObject);
    }

    public void TakeDamage(int amount)
    {
        RemoveHealth(amount);
    }
}
