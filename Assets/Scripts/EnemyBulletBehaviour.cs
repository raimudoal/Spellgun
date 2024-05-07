using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletBehaviour : MonoBehaviour
{
    [SerializeField] private float speed;
    private float destroyDelay = 3f;
    private Rigidbody2D projectileRb;
    public int damage;
    public GameObject particleOnDeath;
    AudioManager audioManager;
    // Start is called before the first frame update
    private void Awake()
    {
        projectileRb = GetComponent<Rigidbody2D>();
        audioManager = GameObject.FindGameObjectWithTag("audio").GetComponent<AudioManager>();
    }
    public void LaunchProjectile(Vector2 direction)
    {
        direction.Normalize();
        projectileRb.velocity = direction * speed;
        Destroy(gameObject, destroyDelay);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        audioManager.PlaySFX(audioManager.enemyProjectileHit);
        Destroy(gameObject);
        if (particleOnDeath)
        {
            GameObject explosion = Instantiate(particleOnDeath, transform.position, transform.rotation);
            Destroy(explosion, 1);
        }
    }

    void OnEnable()
    {
        GameObject[] otherObjects = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject obj in otherObjects)
        {
            Physics2D.IgnoreCollision(obj.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }
    }
}