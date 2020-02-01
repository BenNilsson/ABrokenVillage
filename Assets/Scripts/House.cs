using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class House : MonoBehaviour, IRepairable, IDamageable
{
    public int curHealth;
    public int maxHealth;
    private bool destroyed;
    public float HealthPercentage { get { return (float)curHealth / (float)maxHealth; } }

    public List<Sprite> houseStages = new List<Sprite>();

    private SpriteRenderer spriteRenderer;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        FindTexture();
    }

    public void Repair(int amount)
    {
        if (curHealth != maxHealth)
        {
        curHealth += amount;
        SoundManager.instance.PlaySound("RepairHouse", 0.1f);
        }
        if (curHealth > maxHealth) curHealth = maxHealth;
        destroyed = false;
        FindTexture();
    }

    public void TakeDamage(int amount)
    {

        curHealth -= amount;

        SoundManager.instance.PlaySound("HouseHit", 0.25f);

        if (curHealth <= 0)
            FindTexture();
        if (curHealth == 0 && !destroyed)
        {
            SoundManager.instance.PlaySound("HouseDestroy", 0.25f);
            destroyed = true;
        }

    }

    private void FindTexture()
    {
        if (HealthPercentage > 0.75)
        {
            // House is above 75% health
            spriteRenderer.sprite = houseStages[0];
        }
        else if (HealthPercentage > 0.50)
        {
            // House is above 50% health
            spriteRenderer.sprite = houseStages[1];
        }
        else if (HealthPercentage > 0.25)
        {
            // House is above 25% health
            spriteRenderer.sprite = houseStages[2];
        }
        else
        {
            // House is below 25% health
            spriteRenderer.sprite = houseStages[3];
        }
    }
}
