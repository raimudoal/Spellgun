using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowEnemyBullet : MonoBehaviour
{
    [SerializeField] private float speed;
    public int damage;
    public GameObject particleOnDeath;
    AudioManager audioManager;
    bool active = false;
    PlayerMovement player;
    // Start is called before the first frame update
    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("audio").GetComponent<AudioManager>();
        player = FindObjectOfType<PlayerMovement>();
        StartCoroutine(firstWait());
        Destroy(gameObject, 10);
    }
    private void Update()
    {
        if (active)
        {
            var dire = player.transform.position - transform.position;
            var angle = Mathf.Atan2(dire.y, dire.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(player.transform.position.x, player.transform.position.y, 0), Time.deltaTime * Random.Range(3,8));
        }
    }

    IEnumerator firstWait()
    {
        yield return new WaitForSeconds(1.3f);
        active = true;
        yield return null;
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
