using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private BasicBulletBehaviour projectilePrefab;
    [SerializeField] private Transform shootPosition;
    private SpriteRenderer spriteRenderer;
    private int current = 0;
    private float WPradius = 0.1f;
    public float speed;

    [SerializeField] private List<GameObject> waypoints = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        Random.seed = System.DateTime.Now.Millisecond;
    }

    // Update is called once per frame
    void Update()
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
        else if(mousePos.x < transform.position.x)
        {
            spriteRenderer.flipY = true;
        }

        if (Input.GetMouseButtonDown(0))
        {
            BasicBulletBehaviour projectile = Instantiate(projectilePrefab, shootPosition.position, transform.rotation);
            projectile.LaunchProjectile(transform.right);
        }

        MoveBetweenWaypoints();

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
}
