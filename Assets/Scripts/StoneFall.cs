using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneFall : MonoBehaviour
{
    [SerializeField] GameObject stone1;
    [SerializeField] GameObject stone2;
    [SerializeField] GameObject stone3;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Player"))
        {
            Destroy(gameObject);
        }
        if (collision.tag.Equals("Ground"))
        {
            Destroy(gameObject);
        }
    }
}
