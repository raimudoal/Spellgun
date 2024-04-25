using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleProjectile : MonoBehaviour
{
    [SerializeField] GameObject leftProjectile;
    [SerializeField] GameObject rightProjectile;
    private float destroyDelay = 3f;
    float speed = 20;
    public GameObject particleOnDeath;
    AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("audio").GetComponent<AudioManager>();
    }

    public void LaunchProjectile()
    {
        StartCoroutine(LaunchDelay());
    }

    IEnumerator LaunchDelay()
    {
        yield return new WaitForSeconds(0.5f);
        audioManager.PlaySFX(audioManager.handMove);
        leftProjectile.GetComponent<Rigidbody2D>().velocity = Vector2.right * speed;
        rightProjectile.GetComponent<Rigidbody2D>().velocity = Vector2.left * speed;
        Destroy(gameObject, destroyDelay);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
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
            Physics2D.IgnoreCollision(obj.GetComponent<Collider2D>(), leftProjectile.GetComponent<Collider2D>());
            Physics2D.IgnoreCollision(obj.GetComponent<Collider2D>(), rightProjectile.GetComponent<Collider2D>());
        }
    }
}
