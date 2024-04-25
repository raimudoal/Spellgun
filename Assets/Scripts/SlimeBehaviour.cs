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
    ParticleSystem ownParticleSystem;
    [SerializeField]ParticleSystem outerParticleSystem;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<PlayerMovement>();
        animator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
        circleCollider = GetComponent<CircleCollider2D>();
        ownParticleSystem = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    [System.Obsolete]
    void Update()
    {
        if (Mathf.Abs(player.transform.position.x - transform.position.x) < 1 && Mathf.Abs(player.transform.position.z - transform.position.z) < 1 && transform.position.y > player.transform.position.y)
        {
            active = true;
        }


        if (active && !once)
        {
            audioManager.PlaySFX(audioManager.slimeFall);
            ownParticleSystem.enableEmission = false;
            rb.bodyType = RigidbodyType2D.Dynamic;
            circleCollider.offset = new Vector2(-0.007697105f, 0);
            animator.Play("slimeFall");
            boxCollider.enabled = true;
            once = true;
        }
    }

    [System.Obsolete]
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (active) 
        {
            if (collision.tag.Equals("Ground"))
            {
                audioManager.PlaySFX(audioManager.slimeGround);
                outerParticleSystem.enableEmission = true;
                active = false;
                circleCollider.enabled = false;
                rb.bodyType = RigidbodyType2D.Static;
                animator.Play("slimeSplash");
            }
        }
    }
}
