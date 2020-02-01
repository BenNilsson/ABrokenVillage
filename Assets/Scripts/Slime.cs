using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : Enemy, IDamageable
{
    public GameObject hitParticle;

    public void TakeDamage(int amount)
    {
        health -= amount;
        if (health <= 0) Destroy(gameObject);
        if (hitParticle != null)
        {
            Vector2 pos = transform.position;
            pos.x -= 0.75f * PlayerManager.instance.transform.localRotation.x;
            GameObject go = Instantiate(hitParticle, pos, PlayerManager.instance.transform.rotation);
            ParticleSystem ps = go.GetComponent<ParticleSystem>();
            if (ps != null)
                ps.Play();

            Destroy(go, 1f);
        }
    }
}
