using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PouchUnlocker : MonoBehaviour
{
    [SerializeField] int bulletType;
    [SerializeField] Rigidbody2D rb;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag.Equals("Player"))
        {
            Gun gun = collision.GetComponentInChildren<Gun>();
            gun.UnlockBulletType(bulletType);
            Destroy(gameObject);
        }
        if(collision.tag.Equals("Ground"))
        {
            rb.bodyType = RigidbodyType2D.Static;
        }
    }
}
