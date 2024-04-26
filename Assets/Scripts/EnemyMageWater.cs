using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMageWater : EnemyBehaviour
{
    private PlayerMovement player;
    private Animator animator;
    [SerializeField] Transform shootingPosition;
    [SerializeField] Vector3[] positions;
    [SerializeField] private EnemyBulletBehaviour enemyBullet;
    [SerializeField] private FollowEnemyBullet followBullet;
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
        GameObject chainWall1 = Instantiate(chainWall, chainWallPos, Quaternion.Euler(0,0,90));
        door1anim = chainWall1.GetComponent<Animator>();
        
    }

    void Update()
    {
        Attack();
    }

    private void OnDestroy()
    {
        door1anim.Play("chainWallOpen");
        Destroy(door1anim.gameObject, 2f);
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
                FollowEnemyBullet bullet1 = Instantiate(followBullet, new Vector3(Random.Range(66,77), Random.Range(125,130), 0), Quaternion.identity);
                FollowEnemyBullet bullet2 = Instantiate(followBullet, new Vector3(Random.Range(66,77), Random.Range(125,130), 0), Quaternion.identity);
                FollowEnemyBullet bullet3 = Instantiate(followBullet, new Vector3(Random.Range(66,77), Random.Range(125,130), 0), Quaternion.identity);
                FollowEnemyBullet bullet4 = Instantiate(followBullet, new Vector3(Random.Range(66,77), Random.Range(125,130), 0), Quaternion.identity);
                FollowEnemyBullet bullet5 = Instantiate(followBullet, new Vector3(Random.Range(66, 77), Random.Range(125, 130), 0), Quaternion.identity);
                FollowEnemyBullet bullet6 = Instantiate(followBullet, new Vector3(Random.Range(66, 77), Random.Range(125, 130), 0), Quaternion.identity);
                FollowEnemyBullet bullet7 = Instantiate(followBullet, new Vector3(Random.Range(66, 77), Random.Range(125, 130), 0), Quaternion.identity);
                FollowEnemyBullet bullet8 = Instantiate(followBullet, new Vector3(Random.Range(66, 77), Random.Range(125, 130), 0), Quaternion.identity);
            }

            attackTimer = 0;
            hasAnimated = false;
            animator.SetBool("Attacking", false);
        }
    }
}
