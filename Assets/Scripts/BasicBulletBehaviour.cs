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
    public GameObject particleOnDeath;
    private Gun gun;
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
        gun = FindObjectOfType<Gun>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (element.Equals("Stone") && gun.bullets == 4 || element.Equals("Stone") && gun.bullets == 5)
        {
            Explode();
        }
        Destroy(gameObject);
        if (particleOnDeath)
        {
            GameObject explosion = Instantiate(particleOnDeath, transform.position, transform.rotation);
            Destroy(explosion, 1);
        }
    }

    void OnEnable()
    {
        GameObject[] otherObjects = GameObject.FindGameObjectsWithTag("Player");

        foreach (GameObject obj in otherObjects)
        {
            Physics2D.IgnoreCollision(obj.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }
    }

    private void Explode()
    {
        var pos = transform.position;
        Collider2D[] nearColliders = Physics2D.OverlapCircleAll(pos, 5);
        foreach (var col in nearColliders)
        {
            if (col.GetComponent<Rigidbody2D>() && col.gameObject.CompareTag("Player"))
            {
                Vector3 direction = col.transform.position - transform.position;
                float forceFalloff = 1 - (direction.magnitude / 3);
                col.GetComponent<Rigidbody2D>().AddForce(direction.normalized * (forceFalloff <= 0 ? 0 : 250) * forceFalloff);
            }
        }
    }

    void AddExplosionForce2D(Collider2D col, Vector3 explosionOrigin, float explosionForce, float explosionRadius)
    {
        Vector3 direction = transform.position - explosionOrigin;
        float forceFalloff = 1 - (direction.magnitude / explosionRadius);
        col.GetComponent<Rigidbody2D>().AddForce(direction.normalized * (forceFalloff <= 0 ? 0 : explosionForce) * forceFalloff);
    }
}
