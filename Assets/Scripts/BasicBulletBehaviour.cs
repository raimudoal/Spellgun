using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicBulletBehaviour : MonoBehaviour
{
    [SerializeField] private float speed;
    private float destroyDelay = 2f;
    private Rigidbody2D projectileRb;
    public int damage;
    public string element;

    // Start is called before the first frame update
    private void Awake()
    {
        projectileRb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void LaunchProjectile(Vector2 direction)
    {
        projectileRb.velocity = direction * speed;
        Destroy(gameObject, destroyDelay);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }

    void OnEnable()
    {
        GameObject[] otherObjects = GameObject.FindGameObjectsWithTag("Player");

        foreach (GameObject obj in otherObjects)
        {
            Physics2D.IgnoreCollision(obj.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }
    }
}
