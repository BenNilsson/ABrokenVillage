using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Harvest : MonoBehaviour, IHarvestable
{
    public int health = 3;
    public Vector2 amountToDrop = new Vector2(1,3);
    public GameObject itemToDropPrefab;
    public Vector2 respawnTime = new Vector2(5, 15);
    public List<Sprite> sprites = new List<Sprite>();
    public SpriteRenderer spriteRenderer;
    public bool harvestable;
    public string soundName;
    public ParticleSystem hitparticle;

    private Sprite randomSprite;
    private int initialHealth;

    private float timeElapsed;
    private float randomRespawnTime;

    private ParticleSystem ps;

    void Awake()
    {
        ps = transform.GetChild(0).gameObject.GetComponent<ParticleSystem>();
    }

    void Start()
    {
        initialHealth = health;
        randomRespawnTime = Random.Range(respawnTime.x, respawnTime.y);
        randomSprite = sprites[Random.Range(1, sprites.Count)];
        spriteRenderer.sprite = randomSprite;
    }

    public void TakeDamage(int amount)
    {
        if(harvestable)
        {
            health -= amount;

            if (soundName != "")
            {
            SoundManager.instance.PlaySound(soundName, 0.25f);          
            }

            if (hitparticle != null)
                hitparticle.Play();

            if (health <= 0)
                HarvestItem();
        }
    }

    void Update()
    {
        if(Time.time >= timeElapsed + randomRespawnTime && !harvestable)
        {
            timeElapsed = Time.time;
            harvestable = true;
            if (ps != null) ps.Play();
            GetComponent<Collider2D>().enabled = true;
            spriteRenderer.sprite = randomSprite;
            health = initialHealth;
        }
    }

    public void HarvestItem()
    {
        DropItem();
        harvestable = false;
        GetComponent<Collider2D>().enabled = false;
        if (ps != null) ps.Stop();
        spriteRenderer.sprite = sprites[0];
        timeElapsed = Time.time;
    }

    public void DropItem()
    {
        // Get random drop range
        int dropAmount = Random.Range(Mathf.FloorToInt(amountToDrop.x), Mathf.FloorToInt(amountToDrop.y));
        for(int i = 0; i < dropAmount; i++)
        {
            Vector2 pos = transform.position;
            
            ItemDataBase.instance.SpawnItem(itemToDropPrefab.GetComponent<Item>().id, pos);
        }
    }
}
