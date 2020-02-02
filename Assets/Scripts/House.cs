using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class House : MonoBehaviour, IRepairable, IDamageable
{
    public int curHealth;
    public int maxHealth;
    public bool destroyed;
    public bool repairable;
    public GameObject hitParticle;
    public Transform particleSpawnPoint;
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
        repairable = true;
    }

    public void Repair(int amount)
    {
        if (!repairable) return;
        if (curHealth != maxHealth)
        {

            // Check if we have 1 log in inventory
            if (InventoryManager.instance.ContainsItem(1, 1))
            {
                // Check if we have 2 grass in inventory
                if (InventoryManager.instance.ContainsItem(4, 2))
                {
                    if (InventoryManager.instance.RemoveItemFromHotbar(1, 1))
                    {
                        if (InventoryManager.instance.RemoveItemFromHotbar(4, 2))
                        {
                            curHealth += amount;
                            destroyed = false;
                            SoundManager.instance.PlaySound("RepairHouse", 0.1f);
                            if (curHealth > maxHealth) curHealth = maxHealth;
                            FindTexture();
                            destroyed = false;
                        }
                        else
                        {
                            Debug.Log("Error: Could not remove grass from inventory");
                        }
                    }
                    else
                    {
                        Debug.Log("Error: Could not remove logs from inventory");
                    }
                }
                else
                {
                    Debug.Log("Not enough grass");
                }
            }
            else
            {
                Debug.Log("Not enough wood");
            }
        }
    }

    public void TakeDamage(int amount)
    {
        if (!PlayerManager.instance.isAlive) return;

        curHealth -= amount;

        SoundManager.instance.PlaySound("HouseHit", 0.25f);

        if (curHealth <= 0)
            curHealth = 0;

        if (curHealth == 0 && !destroyed)
        {
            SoundManager.instance.PlaySound("HouseDestroy", 0.25f);
            destroyed = true;
        }

        if (hitParticle != null)
        {
            GameObject go = Instantiate(hitParticle, particleSpawnPoint.position, Quaternion.identity);
            ParticleSystem ps = go.GetComponent<ParticleSystem>();
            if (ps != null)
                ps.Play();

            Destroy(go, 1f);
        }

        FindTexture();

    }

    private void FindTexture()
    {
        if (HealthPercentage >= 1)
        {
            // House is above 75% health
            spriteRenderer.sprite = houseStages[0];
        }
        else if (HealthPercentage > 0.75)
        {
            // House is above 50% health
            spriteRenderer.sprite = houseStages[1];
        }
        else if (HealthPercentage > 0)
        {
            // House is above 25% health
            spriteRenderer.sprite = houseStages[2];
        }
        else if (HealthPercentage <= 0)
        {
            // House is below 25% health
            spriteRenderer.sprite = houseStages[3];
        }
    }
}
