using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class EyeTurret : EnemyBehaviour
{
    private PlayerMovement player;
    private Animator animator;
    private bool canAttack;
    private float attackTimer = 0.0f;
    [SerializeField] private EnemyBulletBehaviour enemyBullet;
    [SerializeField] Transform shootingPosition;
    [SerializeField] int dir;
    private bool hasAnimated = false;

    private Light2D light2d;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<PlayerMovement>();
        animator = GetComponent<Animator>();
        light2d = GetComponent<Light2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(new Vector3(player.transform.position.x, 0, 0), new Vector3(transform.position.x, 0, 0)) < 10)
        {
            canAttack = true;
        }
        else
        {
            canAttack = false;
        }

        if (canAttack)
        {
            light2d.intensity = 0.4f;
            animator.SetBool("Unactive", false);
            var dire = player.transform.position - transform.position;
            var angle = Mathf.Atan2(dire.y, dire.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            attackTimer += Time.deltaTime;
            if (attackTimer > 4.5f && !hasAnimated)
            {
                animator.SetBool("Attacking", true);
                hasAnimated = true;
            }
            if (attackTimer > 5.5f)
            {
                audioManager.PlaySFX(audioManager.pepAttack);
                EnemyBulletBehaviour projectile = Instantiate(enemyBullet, shootingPosition.position, transform.rotation);
                projectile.LaunchProjectile(new Vector2(dire.x, dire.y));
                attackTimer = 0;
                hasAnimated = false;
                animator.SetBool("Attacking", false);
            }
        }
        else
        {
            animator.SetBool("Unactive", true);
            animator.SetBool("Attacking", false);
            light2d.intensity = 0.1f;
        }
    }
}