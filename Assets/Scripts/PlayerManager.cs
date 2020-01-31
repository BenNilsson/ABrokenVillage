using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour, IDamageable
{
    [SerializeField] private int maxHealth = 10;

    public int GetHealth { get { return curHealth; } }
    private int curHealth;

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
