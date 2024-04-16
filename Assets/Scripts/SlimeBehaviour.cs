using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeBehaviour : EnemyBehaviour
{
    Animator animator;
    PlayerMovement player;
    bool active = false;
    bool once = false;
    BoxCollider2D boxCollider;
    CircleCollider2D circleCollider;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<PlayerMovement>();
        animator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
        circleCollider = GetComponent<CircleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Abs(player.transform.position.x - transform.position.x) < 1 && Mathf.Abs(player.transform.position.z - transform.position.z) < 1 && transform.position.y > player.transform.position.y)
        {
            active = true;
        }


        if (active && !once)
        {
            rb.bodyType = RigidbodyType2D.Dynamic;
            circleCollider.offset = new Vector2(-0.007697105f, 0);
            animator.Play("slimeFall");
            boxCollider.enabled = true;
            once = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (active) 
        {
            if (collision.tag.Equals("Ground"))
            {
                active = false;
                circleCollider.enabled = false;
                rb.bodyType = RigidbodyType2D.Static;
                animator.Play("slimeSplash");
            }
        }
    }
}
