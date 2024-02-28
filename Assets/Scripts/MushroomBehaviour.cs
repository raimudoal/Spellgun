using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomBehaviour : EnemyBehaviour
{
    private float speed = 2.0f;
    private float jumpForce = 5.0f;
    private bool following = false;
    private bool attacking = false;
    private PlayerMovement player;
    private Vector3 playerPos;
    private float followTimer = 0.0f;
    private Animator animator;
    // Start is called before the first frame update
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
        
        if (!attacking)
        {
            animator.SetBool("isRunning", false);
            if (player.transform.position.x > this.transform.position.x)
            {
                
                this.transform.localScale = new Vector3(-1, 1, 1);
            }
            else
            {
                this.transform.localScale = Vector3.one;
            }

            if (!following)
            {
                transform.position = Vector3.MoveTowards(transform.position, Vector3.left, Time.deltaTime * speed);
                followTimer = 0.0f;
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(player.transform.position.x, 0, 0), Time.deltaTime * 3.5f);
                followTimer += Time.deltaTime;
                Debug.Log(followTimer);
            }

            if (followTimer > 4.0f)
            {
                followTimer = 0.0f;
                if (transform.localScale == new Vector3(-1, 1, 1))
                {
                    playerPos = player.transform.position + new Vector3(20, 0, 0);
                }
                else if (transform.localScale == new Vector3(1,1,1))
                {
                    playerPos = player.transform.position - new Vector3(20, 0, 0);
                }
                attacking = true;
                Debug.Log("START ATTACK");
            }

            if (!following)
            {
                if (transform.position.x < 83)
                {
                    speed = -2.0f;
                }
                if (transform.position.x > 100)
                {
                    speed = 2.0f;
                }
            }


            if (Vector3.Distance(new Vector3(player.transform.position.x, 0, 0), new Vector3(transform.position.x, 0, 0)) < 10)
            {
                following = true;

            }
            else
            {
                following = false;
            }
        }
        else
        {
            animator.SetBool("isRunning", true);
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(playerPos.x, 0, 0), Time.deltaTime * 10f);
            if ((new Vector3(playerPos.x ,0,0) - new Vector3(transform.position.x,0,0)).sqrMagnitude < 0.5f)
            {
                Debug.Log("STOPP ATTACK");
                attacking = false;
            }
        }
    }
        

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }
}
