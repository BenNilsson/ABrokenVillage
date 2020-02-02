using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : Enemy, IDamageable
{
    public GameObject hitParticle;
    public Transform particleSpawnPoint;
    public Animator anim;

    public void TakeDamage(int amount)
    {
        SoundManager.instance.PlaySound("PlayerHit4", 1.25f);
        if (anim != null) anim.SetTrigger("Hit");
        health -= amount;
        if (health <= 0) Destroy(gameObject);
        if (hitParticle != null)
        {
            Vector2 pos = particleSpawnPoint.position;
            GameObject go = Instantiate(hitParticle, pos, PlayerManager.instance.transform.rotation);
            ParticleSystem ps = go.GetComponent<ParticleSystem>();
            if (ps != null)
                ps.Play();

            Destroy(go, 1f);
        }
    }
}
