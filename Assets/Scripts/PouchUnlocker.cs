using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PouchUnlocker : MonoBehaviour
{
    [SerializeField] int bulletType;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag.Equals("Player"))
        {
            Gun gun = collision.GetComponentInChildren<Gun>();
            gun.UnlockBulletType(bulletType);
        }
    }
}
