using UnityEngine;

public class SkeletonBehaviour : EnemyBehaviour
{
    private PlayerMovement player;
    private Animator animator;
    private bool canAttack;
    private float attackTimer = 0.0f;
    [SerializeField] private EnemyBulletBehaviour enemyBullet;
    [SerializeField] Transform shootingPosition;
    [SerializeField] int dir;
    private bool hasAnimated = false;
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
            attackTimer += Time.deltaTime;
            if(attackTimer > 4.5f && !hasAnimated)
            {
                animator.Play("SkeletonThrow");
                hasAnimated = true;
            }
            if (attackTimer > 5)
            {
                audioManager.PlaySFX(audioManager.skeletonThrow);
                EnemyBulletBehaviour projectile = Instantiate(enemyBullet, shootingPosition.position, transform.rotation);
                projectile.LaunchProjectile(new Vector2(dir, 0));
                attackTimer = 0;
                hasAnimated = false;
            }
        }
    }
}
