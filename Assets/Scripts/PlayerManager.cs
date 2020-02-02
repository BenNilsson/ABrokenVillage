using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour, IDamageable
{
    public static PlayerManager instance = null;

    public Transform interactArea;
    public Animator anim;

    public bool isAlive = true;
    public bool canTakeDamage = false;

    [SerializeField] private int maxHealth = 10;

    public int GetHealth { get { return curHealth; } }
    public int curHealth;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        anim = GetComponent<Animator>();
    }

    void Start()
    {
        curHealth = maxHealth;
        isAlive = true;
        canTakeDamage = false;
    }

    void Update()
    {
        if (!isAlive)
            gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Ignore colliders from the player
        if (collision.gameObject.tag == "Player") return;

        // Pickup Items
        Item item = collision.gameObject.GetComponent<Item>();
        collision.gameObject.GetComponent<Collider2D>().enabled = false;

        if(item != null)
        {

            if (InventoryManager.instance.AddItemToHotbar(item.id))
            {
                SoundManager.instance.PlaySound("Pickup", 0.25f);
                Destroy(item.gameObject);
                return;
            }else
            {
                collision.gameObject.GetComponent<Collider2D>().enabled = true;
            }
            
        }else
        {

        }

        collision.gameObject.GetComponent<Collider2D>().enabled = true;
    }

    public void RemoveHealth(int amount)
    {
        curHealth -= amount;
        if (curHealth <= 0) isAlive = false;
    }

    public void TakeDamage(int amount)
    {
        if (!canTakeDamage) return;
        RemoveHealth(amount);
    }
}
