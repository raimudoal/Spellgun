using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatBehaviour : EnemyBehaviour
{
    Animator animator;
    PlayerMovement player;
    bool active = false;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<PlayerMovement>();
        animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(new Vector3(player.transform.position.x, player.transform.position.y, 0), new Vector3(transform.position.x, transform.position.y, 0)) < 8 && !active)
        {
            active = true;
        }

        if(active)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(player.transform.position.x, player.transform.position.y, 0), Time.deltaTime * 4f);
            animator.Play("batMove");
        }
    }
}
