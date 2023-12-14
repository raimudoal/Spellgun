using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class RoomFade : MonoBehaviour
{
    public Tilemap secretArea;
    float alphaValue;
    public float disappearRate = 1;

    bool playerEntered = false;

    public bool toggleWall = false;

    private void Update()
    {
        if (playerEntered)
        {
            alphaValue -= Time.deltaTime * disappearRate;

            if (alphaValue <= 0)
            {
                alphaValue = 0;
            }

            secretArea.color = new Color(secretArea.color.r, secretArea.color.g, secretArea.color.b, alphaValue);
        }
        else
        {
            alphaValue += Time.deltaTime * disappearRate;

            if (alphaValue >= 1)
            {
                alphaValue = 1;
            }

            secretArea.color = new Color(secretArea.color.r, secretArea.color.g, secretArea.color.b, alphaValue);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerEntered = true;

        }
    }


    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && toggleWall)
        {
            playerEntered = false;
        }
    }
}
