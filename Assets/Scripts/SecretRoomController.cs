using UnityEngine;

public class SecretRoomController : MonoBehaviour
{
    public GameObject coveringLayer;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Player entered the secret room, hide the covering layer
            coveringLayer.SetActive(false);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Player exited the secret room, show the covering layer
            coveringLayer.SetActive(true);
        }
    }
}
