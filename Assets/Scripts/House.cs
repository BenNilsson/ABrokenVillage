using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class House : MonoBehaviour, IRepairable, IDamageable
{
    public int health;
    private int maxHealth;

    public List<Sprite> houseStages = new List<Sprite>();

    private SpriteRenderer spriteRenderer;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        maxHealth = health;
    }

    public void Repair(int amount)
    {
        health += amount;
        Mathf.Clamp(health, 0, maxHealth);
    }

    public void TakeDamage(int amount)
    {
        health -= amount;

        int healthPercentage = health / 20;
        if(healthPercentage >= 0.75)
        {
            // House is above 75% health
            spriteRenderer.sprite = houseStages[0];
        }
        else if (healthPercentage >= 0.50)
        {
            // House is above 50% health
            spriteRenderer.sprite = houseStages[1];
        }
        else if (healthPercentage >= 0.25)
        {
            // House is above 25% health
            spriteRenderer.sprite = houseStages[2];
        }
        else
        {
            // House is below 25% health
            spriteRenderer.sprite = houseStages[3];
        }

        if (health <= 0)
            Destroy(gameObject);
    }
}
