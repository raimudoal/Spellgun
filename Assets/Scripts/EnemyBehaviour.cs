using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{

    public int health;
    [SerializeField] GameObject enemyExploision;
    private enum Status { Normal, Wet, Burnt };
    Status status = Status.Normal;
    public SpriteRenderer spriteRenderer;
    public Rigidbody2D rb;

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
            switch (collision.gameObject.GetComponent<BasicBulletBehaviour>().element)
            {
                case "Fire":
                    if (status == Status.Wet)
                    {
                        Console.WriteLine("Normal");
                        status = Status.Normal;
                    }
                    else
                    {
                        Console.WriteLine("Enemy Burnt");
                        status = Status.Burnt;
                    }
                    break;

                case "Water":
                    if (status == Status.Burnt)
                    {
                        Console.WriteLine("Normal");
                        status = Status.Normal;
                    }
                    else
                    {
                        Console.WriteLine("Enemy Wet");
                        status = Status.Wet;
                    }
                    break;

                case "Normal":
                    Console.WriteLine("Normal");
                    break;
            }

            ChangeStatus();
            Knockback(collision);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            GetHit(collision.gameObject.GetComponent<BasicBulletBehaviour>().damage);
            switch (collision.gameObject.GetComponent<BasicBulletBehaviour>().element)
            {
                case "Fire":
                    if (status == Status.Wet)
                    {
                        Console.WriteLine("Normal");
                        status = Status.Normal;
                    }
                    else
                    {
                        Console.WriteLine("Enemy Burnt");
                        status = Status.Burnt;
                    }
                    break;

                case "Water":
                    if (status == Status.Burnt)
                    {
                        Console.WriteLine("Normal");
                        status = Status.Normal;
                    }
                    else
                    {
                        Console.WriteLine("Enemy Wet");
                        status = Status.Wet;
                    }
                    break;

                case "Normal":
                    Console.WriteLine("Normal");
                    break;
            }

            ChangeStatus();
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

    private void ChangeStatus()
    {
        switch (status) 
        {
            case Status.Burnt:
                spriteRenderer.color = Color.red;
                break;
            case Status.Wet:
                spriteRenderer.color = Color.blue;
                break;
            case Status.Normal:
                spriteRenderer.color = Color.white;
                break;
        }
    }

}
