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
    [SerializeField] private GameObject chainWall;
    [SerializeField] private Vector3[] chainWallPos;
    private Animator door1anim;
    private Animator door2anim;
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
        GameObject chainWall1 = Instantiate(chainWall, chainWallPos[0], Quaternion.identity);
        GameObject chainWall2 = Instantiate(chainWall, chainWallPos[1], Quaternion.identity);
        door1anim = chainWall1.GetComponent<Animator>();
        door2anim = chainWall2.GetComponent<Animator>();
    }

    void Update()
    {
        SpeedChange();
        MoveBetweenWaypoints();
        Attack();
    }

    private void OnDestroy()
    {
        door1anim.Play("chainWallOpen");
        door2anim.Play("chainWallOpen");
        Destroy(door1anim.gameObject, 2f);
        Destroy(door2anim.gameObject, 2f);
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

            // Calcular la posición intermedia hacia el waypoint actual
            Vector3 newPosition = Vector3.Slerp(transform.position, positions[current], Time.deltaTime * speed);

            // Mover hacia la posición intermedia
            transform.position = newPosition;
        }
    }


    private void SpeedChange()
    {
        if (health > 50)
        {
            speed = 1.6f;
        }
        else if (health > 35)
        {
            speed = 1.9f;
        }
        else if (health > 25)
        {
            speed = 2.3f;
        }
        else if (health > 10)
        {
            speed = 2.5f;
        }
        else 
        {
            speed = 2.8f;
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
            audioManager.PlaySFX(audioManager.enemyMageStartAttack);
            animator.SetBool("Attacking", true);
            hasAnimated = true;
        }
        if (attackTimer > 5.5f)
        {
            if (Random.Range(0, 2) == 0)
            {
                audioManager.PlaySFX(audioManager.enemyMageShoot);
                EnemyBulletBehaviour projectile = Instantiate(enemyBullet, shootingPosition.position, shootingPosition.rotation);
                projectile.LaunchProjectile(new Vector2(dire.x, dire.y));
            }
            else
            {
                audioManager.PlaySFX(audioManager.handSpawn);
                DoubleProjectile projectile = Instantiate(doubleProjectile, player.transform.position, player.transform.rotation);
                projectile.LaunchProjectile();
            }

            attackTimer = 0;
            hasAnimated = false;
            animator.SetBool("Attacking", false);
        }
    }
}
