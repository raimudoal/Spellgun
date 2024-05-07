using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMageKing : EnemyBehaviour
{
    private PlayerMovement player;
    private Animator animator;
    [SerializeField] Transform shootingPosition;
    [SerializeField] Vector3[] positions;
    [SerializeField] private EnemyBulletBehaviour enemyBullet;
    [SerializeField] private DoubleProjectile doubleProjectile;

    [SerializeField] private StoneFall stoneFall;
    [SerializeField] private EnemyBulletBehaviour enemyBulletStone;

    [SerializeField] private EnemyBulletBehaviour enemyBulletWater;
    [SerializeField] private FollowEnemyBullet followBullet;

    private float attackTimer = 0.0f;
    private float speed;
    private int current = 0;
    private bool hasAnimated = false;

    [SerializeField] GameObject crown;

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

    private void OnDestroy()
    {
        if (health <= 0)
            Instantiate(crown, transform.position, Quaternion.identity);
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
            speed = 1.5f;
        }
        else if (health > 65)
        {
            speed = 1.8f;
        }
        else if (health > 45)
        {
            speed = 2.3f;
        }
        else if (health > 20)
        {
            speed = 2.5f;
        }
        else
        {
            speed = 3f;
        }
    }

    private void Attack()
    {
        var dire = player.transform.position - transform.position;
        var angle = Mathf.Atan2(dire.y, dire.x) * Mathf.Rad2Deg;
        shootingPosition.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        attackTimer += Time.deltaTime;
        int attackIndex = Random.Range(0, 6);
        if (attackTimer > 4.5f && !hasAnimated)
        {
            audioManager.PlaySFX(audioManager.enemyMageStartAttack);
            if (attackIndex == 0 || attackIndex == 1)
            {
                animator.SetBool("AttackingFire", true);
            }
            else if (attackIndex == 2 || attackIndex == 3)
            {
                animator.SetBool("AttackingStone", true);
            }
            else if (attackIndex == 4 || attackIndex == 5)
            {
                animator.SetBool("AttackingWater", true);
            }
            hasAnimated = true;
        }
        if (attackTimer > 5.5f)
        {
            

            if (attackIndex == 0)
            {
                audioManager.PlaySFX(audioManager.enemyMageShoot);
                EnemyBulletBehaviour projectile = Instantiate(enemyBullet, shootingPosition.position, shootingPosition.rotation);
                projectile.LaunchProjectile(new Vector2(dire.x, dire.y));
            }
            else if(attackIndex == 1)
            {
                audioManager.PlaySFX(audioManager.handSpawn);
                DoubleProjectile projectile = Instantiate(doubleProjectile, player.transform.position, player.transform.rotation);
                projectile.LaunchProjectile();
            }
            else if(attackIndex == 2)
            {
                audioManager.PlaySFX(audioManager.handSpawn);
                StoneFall stones = Instantiate(stoneFall, new Vector3(Random.Range(28, 65), Random.Range(-1, -5), 0), Quaternion.identity);
                StoneFall stones1 = Instantiate(stoneFall, new Vector3(Random.Range(28, 65), Random.Range(-1, -5), 0), Quaternion.identity);
                StoneFall stones2 = Instantiate(stoneFall, new Vector3(Random.Range(28, 65), Random.Range(-1, -5), 0), Quaternion.identity);
                StoneFall stones3 = Instantiate(stoneFall, new Vector3(Random.Range(28, 65), Random.Range(-1, -5), 0), Quaternion.identity);
                StoneFall stones4 = Instantiate(stoneFall, new Vector3(Random.Range(28, 65), Random.Range(-1, -5), 0), Quaternion.identity);
            }
            else if (attackIndex == 3)
            {
                audioManager.PlaySFX(audioManager.enemyMageShoot);
                EnemyBulletBehaviour projectile = Instantiate(enemyBulletStone, shootingPosition.position, shootingPosition.rotation);
                projectile.LaunchProjectile(new Vector2(dire.x, dire.y));
            }
            else if (attackIndex == 4)
            {
                audioManager.PlaySFX(audioManager.enemyMageShoot);
                EnemyBulletBehaviour projectile = Instantiate(enemyBulletWater, shootingPosition.position, shootingPosition.rotation);
                projectile.LaunchProjectile(new Vector2(dire.x, dire.y));
            }
            else if (attackIndex == 5)
            {
                FollowEnemyBullet bullet1 = Instantiate(followBullet, new Vector3(Random.Range(43, 52), Random.Range(-4, -9), 0), Quaternion.identity);
                FollowEnemyBullet bullet2 = Instantiate(followBullet, new Vector3(Random.Range(43, 52), Random.Range(-4, -9), 0), Quaternion.identity);
                FollowEnemyBullet bullet3 = Instantiate(followBullet, new Vector3(Random.Range(43, 52), Random.Range(-4, -9), 0), Quaternion.identity);
                FollowEnemyBullet bullet4 = Instantiate(followBullet, new Vector3(Random.Range(43, 52), Random.Range(-4, -9), 0), Quaternion.identity);
                FollowEnemyBullet bullet5 = Instantiate(followBullet, new Vector3(Random.Range(43, 52), Random.Range(-4, -9), 0), Quaternion.identity);
                FollowEnemyBullet bullet6 = Instantiate(followBullet, new Vector3(Random.Range(43, 52), Random.Range(-4, -9), 0), Quaternion.identity);
                FollowEnemyBullet bullet7 = Instantiate(followBullet, new Vector3(Random.Range(43, 52), Random.Range(-4, -9), 0), Quaternion.identity);
                FollowEnemyBullet bullet8 = Instantiate(followBullet, new Vector3(Random.Range(43, 52), Random.Range(-4, -9), 0), Quaternion.identity);
            }

            attackTimer = 0;
            hasAnimated = false;
            animator.SetBool("AttackingFire", false);
            animator.SetBool("AttackingWater", false);
            animator.SetBool("AttackingStone", false);
        }
    }
}
