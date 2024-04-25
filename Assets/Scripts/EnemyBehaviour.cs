using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{

    public int health;
    [SerializeField] GameObject enemyExploision;
    public SpriteRenderer spriteRenderer;
    public Rigidbody2D rb;
    public AudioManager audioManager;
    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("audio").GetComponent<AudioManager>();
    }

    private void GetHit(int damage)
    {
        health = health - damage;
        audioManager.PlaySFX(audioManager.enemyHurt);
        if (health <= 0)
        {
            audioManager.PlaySFX(audioManager.enemyDeath);
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
            Knockback(collision);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            GetHit(collision.gameObject.GetComponent<BasicBulletBehaviour>().damage);
            Knockback(collision);
        }
    }

    private void Knockback(Collider2D collider)
    {
        var direction = (collider.transform.position - transform.position).normalized;
        rb.AddForce(-direction * 10);
    }

    private void Knockback(Collision2D collision)
    {
        var direction = (collision.transform.position - transform.position).normalized;
        rb.AddForce(-direction * 10);
    }

}
