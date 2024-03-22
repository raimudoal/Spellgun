using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMage : EnemyBehaviour
{
    private PlayerMovement player;
    private Animator animator;
    [SerializeField] Transform shootingPosition;
    [SerializeField] Vector3[] positions;
    [SerializeField] private EnemyBulletBehaviour enemyBullet;
    [SerializeField] private DoubleProjectile doubleProjectile;
    private float attackTimer = 0.0f;
    private float speed;
    private int current = 0;
    private bool hasAnimated = false;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<PlayerMovement>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        SpeedChange();
        MoveBetweenWaypoints();
        Attack();
    }

    private void MoveBetweenWaypoints()
    {
        if (positions.Length != 0)
        {
            if (Vector2.Distance(positions[current], transform.position) < 1f)
            {
                current = Random.Range(0, positions.Length);
                if (current >= positions.Length)
                {
                    current = 0;
                }
            }

            // Calcular la posici�n intermedia hacia el waypoint actual
            Vector3 newPosition = Vector3.Slerp(transform.position, positions[current], Time.deltaTime * speed);

            // Mover hacia la posici�n intermedia
            transform.position = newPosition;
        }
    }


    private void SpeedChange()
    {
        if (health > 80)
        {
            speed = 2;
        }
        else if (health > 150)
        {
            speed = 4;
        }
        else if (health > 25)
        {
            speed = 6;
        }
        else if (health > 10)
        {
            speed = 8;
        }
        else 
        {
            speed = 12;
        }
    }

    private void Attack()
    {
        var dire = player.transform.position - transform.position;
        var angle = Mathf.Atan2(dire.y, dire.x) * Mathf.Rad2Deg;
        shootingPosition.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        attackTimer += Time.deltaTime;
        if (attackTimer > 4.5f && !hasAnimated)
        {
            animator.SetBool("Attacking", true);
            hasAnimated = true;
        }
        if (attackTimer > 5.5f)
        {
            if (Random.Range(0, 2) == 0)
            {
                EnemyBulletBehaviour projectile = Instantiate(enemyBullet, shootingPosition.position, shootingPosition.rotation);
                projectile.LaunchProjectile(new Vector2(dire.x, dire.y));
            }
            else
            {
                DoubleProjectile projectile = Instantiate(doubleProjectile, player.transform.position, player.transform.rotation);
                projectile.LaunchProjectile();
            }

            attackTimer = 0;
            hasAnimated = false;
            animator.SetBool("Attacking", false);
        }
    }
}
