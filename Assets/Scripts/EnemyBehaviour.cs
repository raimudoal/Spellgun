using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{

    public int health;
    [SerializeField] GameObject enemyExploision;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void GetHit(int damage)
    {
        health = health - damage;
        if (health <= 0)
        {
            Destroy(gameObject);
            GameObject explosion = Instantiate(enemyExploision, transform.position, transform.rotation);
            Destroy(explosion, 1);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            GetHit(collision.gameObject.GetComponent<BasicBulletBehaviour>().damage);
        }
    }

}
