using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMageStone : EnemyBehaviour
{
    private PlayerMovement player;
    private Animator animator;
    [SerializeField] Transform shootingPosition;
    [SerializeField] Vector3[] positions;
    [SerializeField] private EnemyBulletBehaviour enemyBullet;
    [SerializeField] private StoneFall stoneFall;
    [SerializeField] private GameObject chainWall;
    [SerializeField] private Vector3 chainWallPos;
    private Animator door1anim;
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
        GameObject chainWall1 = Instantiate(chainWall, chainWallPos, Quaternion.identity);
        door1anim = chainWall1.GetComponent<Animator>();
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
        Destroy(door1anim.gameObject, 2f);
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
        if (health > 80)
        {
            speed = 0.2f;
        }
        else if (health > 150)
        {
            speed = 0.4f;
        }
        else if (health > 25)
        {
            speed = 0.5f;
        }
        else if (health > 10)
        {
            speed = 0.7f;
        }
        else 
        {
            speed = 0.8f;
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
                StoneFall stones = Instantiate(stoneFall, new Vector3(Random.Range(236,268), Random.Range(-28,-37), 0), Quaternion.identity);
                StoneFall stones1 = Instantiate(stoneFall, new Vector3(Random.Range(236, 268), Random.Range(-28, -37), 0), Quaternion.identity);
                StoneFall stones2 = Instantiate(stoneFall, new Vector3(Random.Range(236, 268), Random.Range(-28, -37), 0), Quaternion.identity);
                StoneFall stones3 = Instantiate(stoneFall, new Vector3(Random.Range(236, 268), Random.Range(-28, -37), 0), Quaternion.identity);

            }

            attackTimer = 0;
            hasAnimated = false;
            animator.SetBool("Attacking", false);
        }
    }
}
