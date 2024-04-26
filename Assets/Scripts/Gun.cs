using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Gun : MonoBehaviour
{
    [SerializeField] private Transform shootPosition;
    private SpriteRenderer spriteRenderer;
    private int current = 0;
    private float WPradius = 0.01f;
    public float speed;
    [SerializeField] private Transform UIBullet;
    [SerializeField] private bool fire = false, water = false, electric = true, stone = false;
    [SerializeField] private Image[] bulletsUI;
    [SerializeField] private Sprite[] bulletsImage;

    PlayerReset playerReset;

    int target = 0;
    float r;

    private int bulletIndex = 0;

    public int bullets = 6;

    private bool canShoot = true;

    private Animator animator;

    [SerializeField] private List<GameObject> waypoints = new List<GameObject>();

    [SerializeField] private List<BasicBulletBehaviour> bulletType = new List<BasicBulletBehaviour>();

    private void Awake()
    {
        playerReset = GameObject.FindGameObjectWithTag("resetter").GetComponent<PlayerReset>();

    }

    // Start is called before the first frame update
    [System.Obsolete]
    void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        Random.seed = System.DateTime.Now.Millisecond;
        ChangeBullet();
        ChangeBullet();
        ChangeBullet();
        fire = playerReset.fire;
        water = playerReset.water;
        stone = playerReset.stone;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale != 0.0f)
        {
            if(fire)
                playerReset.fire = true;

            if (water)
                playerReset.water = true;

            if (stone)
                playerReset.stone = true;

            float Angle = Mathf.SmoothDampAngle(UIBullet.eulerAngles.z, target, ref r, 0.1f);
            UIBullet.rotation = Quaternion.Euler(0, 0, Angle);

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

            if (Input.GetMouseButtonDown(0) && !animator.GetBool("Reloading"))
            {

                if (bullets > 0 && canShoot)
                {
                    canShoot = false;
                    BasicBulletBehaviour projectile = Instantiate(bulletType[bulletIndex], shootPosition.position, transform.rotation);
                    projectile.LaunchProjectile(transform.right);
                    animator.Play("GunShoot");
                    bullets--;
                    StartCoroutine(ShootDelay());
                    BulletsUpdateUI();
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
        for (int i = 0; i < bulletsUI.Length; i++)
        {
            bulletsUI[i].sprite = bulletsImage[0];
        }
    }

    private void BulletsUpdateUI()
    {
        int num = bulletsUI.Length - bullets  ;
        Debug.Log(num);

        for (int i = 0; i < (num); i++) 
        {
            bulletsUI[i].sprite = bulletsImage[1];
        }
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
            if(!fire)
            bulletIndex = 2;
            else
                bulletIndex = 0;
        }
        else
        {
            bulletIndex++;
            if (bulletIndex == 0 && !fire)
            {
                ChangeBullet();
            }
            else if (bulletIndex == 1 && !water)
            {
                ChangeBullet();
            }
            else if (bulletIndex == 2 && !electric)
            {
                ChangeBullet();
            }
            else if (bulletIndex == 3 && !stone)
            {
                ChangeBullet();
            }
        }
        if (!fire && !stone && !water) {
            target = 180;
        }else
        target = target - 90;
    }

    public void UnlockBulletType(int type)
    {
        switch (type)
        {
            case 0:
                fire = true;
                break;
            case 1:
                water = true;
                break;
            case 2:
                electric = true;
                break;
            case 3:
                stone = true;
                break;
            default:
                break;

        }
    }
}
