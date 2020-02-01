using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 2;

    private Rigidbody2D rb2d;
    private Animator anim;

    Vector2 movement;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        movement.x = Input.GetAxis("Horizontal");
        movement.y = Input.GetAxis("Vertical");

        if(anim != null)
        {
            if(movement != Vector2.zero)
            {
                anim.SetBool("Walking", true);
            }
            else
            {
                anim.SetBool("Walking", false);
            }
        }

        if(movement != Vector2.zero)
        {
            if (movement.x >= 0)
            {
                transform.rotation = new Quaternion(0, 0, 0, 0);
            }
            else if (movement.x <= 0.1)
            {
                transform.rotation = new Quaternion(0, 180, 0, 0);
                Camera.main.gameObject.transform.rotation = new Quaternion(0, 0, 0, 0);
            }
        }
    }

    void FixedUpdate()
    {
        rb2d.MovePosition(rb2d.position + movement * speed * Time.fixedDeltaTime);
    }
}
