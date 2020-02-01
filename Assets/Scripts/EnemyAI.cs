using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using System;

public class EnemyAI : MonoBehaviour
{
    public Transform target;

    public float speed = 200f;
    public float nextWaypointDistance = 3f;
    public float attackMinDistance = 2f;
    public Transform attackArea;
    public float attackRange = 1f;
    public LayerMask attackMask;
    public float attackCD = 2f;
    public int dmgAmount = 1;
    private Path path;
    private int currentWaypoint = 0;
    private bool reachedEndOfPath = false;

    Seeker seeker;
    Rigidbody2D rb2d;

    private float timeSinceLastAttack;

    private void Awake()
    {
        seeker = GetComponent<Seeker>();
        rb2d = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        target = FindClosestHouse();
        InvokeRepeating("UpdatePath", 0f, 0.50f);
    }

    public Transform FindClosestHouse()
    {
        Transform closestHouse = new GameObject().transform;
        float distance = Mathf.Infinity;
        for (int i = 0; i < EnemyManager.instance.houses.Count; i++)
        {
            if (EnemyManager.instance.houses[i].GetComponent<House>().destroyed == true) continue;
            if (distance > Vector2.Distance(transform.position, EnemyManager.instance.houses[i].transform.position))
            {
                closestHouse = EnemyManager.instance.houses[i].transform;
                distance = Vector2.Distance(transform.position, EnemyManager.instance.houses[i].transform.position);
            }
        }
        if(PlayerManager.instance != null)
            if (distance == Mathf.Infinity) closestHouse = PlayerManager.instance.transform;
        return closestHouse;
    }

    void UpdatePath()
    {
        if (target == null) return;

        House house = target.gameObject.GetComponent<House>();
        if(house != null)
        {
            if(house.destroyed)
                target = FindClosestHouse();

        }else if (PlayerManager.instance != null && target == PlayerManager.instance.transform)
            target = FindClosestHouse();

        if (seeker.IsDone())
            seeker.StartPath(rb2d.position, target.position, OnPathCompleted);
    }

    private void OnPathCompleted(Path p)
    {
        if(!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    private void Update()
    {
        // Check if the slime is close enough
        if (target == null) return;

        if(Vector2.Distance(transform.position, target.position) <= attackMinDistance)
        {
            // Check if he can attack
            if(Time.time >= timeSinceLastAttack + attackCD)
            {
                timeSinceLastAttack = Time.time;
                Attack();
            }
        }


        // Fix Rotation
        if (rb2d.velocity.x >= 0.1f)
            transform.GetChild(0).transform.localScale = new Vector3(1, 1, 1);
        else if (rb2d.velocity.x <= -0.1f)
            transform.GetChild(0).transform.localScale = new Vector3(-1, 1, 1);
    }

    private void Attack()
    {
        Debug.Log(name + " Attacked");

        // Get colliders
        Collider2D[] hits = Physics2D.OverlapCircleAll(attackArea.position, attackRange, attackMask);

        // Damage houses/player
        foreach (Collider2D hit in hits)
        {
            IHarvestable harvestable = hit.gameObject.GetComponent<IHarvestable>();
            if (harvestable != null)
            {
                harvestable.TakeDamage(dmgAmount);
            }else
            {
                // Check if a player is attacked
                IDamageable damageable = hit.gameObject.GetComponent<IDamageable>();
                if(damageable != null)
                {
                    damageable.TakeDamage(dmgAmount);
                }
            }
            break;
        }
    }

    private void FixedUpdate()
    {
        if (path == null) return;

        if(currentWaypoint >= path.vectorPath.Count)
        {
            // Reached end of path
            reachedEndOfPath = true;
            return;
        }
        else
        {
            reachedEndOfPath = false;
        }

        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb2d.position).normalized;
        Vector2 force = direction * speed * Time.deltaTime;

        rb2d.AddForce(force);

        float distance = Vector2.Distance(rb2d.position, path.vectorPath[currentWaypoint]);

        if(distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }
    }

}
