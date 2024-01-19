using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private Transform shootPosition;
    private SpriteRenderer spriteRenderer;
    private int current = 0;
    private float WPradius = 0.01f;
    public float speed;

    private int bulletIndex = 0;

    private int bullets = 6;

    private bool canShoot = true;

    private Animator animator;

    [SerializeField] private List<GameObject> waypoints = new List<GameObject>();

    [SerializeField] private List<BasicBulletBehaviour> bulletType = new List<BasicBulletBehaviour>();

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        Random.seed = System.DateTime.Now.Millisecond;
    }

    // Update is called once per frame
    void Update()
    {
        if (canShoot && Input.GetKeyDown(KeyCode.B))
        {
            ChangeBullet();
        }
        if (!animator.GetBool("Reloading"))
        {
            var pos = Camera.main.WorldToScreenPoint(transform.position);
            var dir = Input.mousePosition - pos;
            var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if (mousePos.x > transform.position.x)
            {
                spriteRenderer.flipY = false;
            }
            else if (mousePos.x < transform.position.x)
            {
                spriteRenderer.flipY = true;
            }
        }
        
        if (Input.GetMouseButtonDown(0))
        {

            if (bullets > 0 && canShoot)
            {
                canShoot = false;
                BasicBulletBehaviour projectile = Instantiate(bulletType[bulletIndex], shootPosition.position, transform.rotation);
                projectile.LaunchProjectile(transform.right);
                animator.Play("GunShoot");
                bullets--;
                StartCoroutine(ShootDelay());
                
                //SONIDO DE DISPARO
            }
            else
            {
                //SONIDO DE ARMA SIN BALA(??)
            }

            
        }

        if (Input.GetKeyDown(KeyCode.R) && !animator.GetBool("Reloading") && bullets != 6)
        {
            canShoot = false;
            animator.SetBool("Reloading", true);
            StartCoroutine(Reload());
            bullets = 6;
        }

        MoveBetweenWaypoints();

    }

    IEnumerator ShootDelay()
    {
        yield return new WaitForSeconds(0.25f);
        canShoot = true;
    }

    IEnumerator Reload()
    {
        yield return new WaitForSeconds(1.20f);
        canShoot = true;
        animator.SetBool("Reloading", false);
    }

    private void MoveBetweenWaypoints()
    {
        if (Vector2.Distance(waypoints[current].transform.position, transform.position) < WPradius)
        {
            current = Random.Range(0, waypoints.Count);
            if (current >= waypoints.Count)
            {
                current = 0;
            }
        }

        transform.position = Vector3.MoveTowards(transform.position, waypoints[current].transform.position, Time.deltaTime * speed);
    }

    private void ChangeBullet()
    {
        if (bulletIndex == bulletType.Count -1)
        {
            bulletIndex = 0;
        }
        else
        {
            bulletIndex++;
        }
    }
}
