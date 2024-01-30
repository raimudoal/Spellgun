using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckGround : MonoBehaviour
{
    public static bool isGrounded;


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true; // Indica que el personaje está en el suelo
        }
        else if (collision.gameObject.CompareTag("OneWayPlatform"))
        {
            isGrounded = true; // Indica que el personaje está en el suelo
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false; // Indica que el personaje ya no está en el suelo
        }
        else if (collision.gameObject.CompareTag("OneWayPlatform"))
        {
            isGrounded = false; // Indica que el personaje ya no está en el suelo
        }
    }
}