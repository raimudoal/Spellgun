using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerOneWayPlatform : MonoBehaviour
{
    private GameObject currentOneWayPlatform;
    private PlatformEffector2D platformEffector;
    [SerializeField] private BoxCollider2D playerCollider;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)){
            if (currentOneWayPlatform != null)
            {
                StartCoroutine(DisableCollision());
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("OneWayPlatform"))
        {
            currentOneWayPlatform = collision.gameObject;
            platformEffector = currentOneWayPlatform.GetComponent<PlatformEffector2D>();
            Debug.Log(currentOneWayPlatform);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("OneWayPlatform"))
        {
            currentOneWayPlatform = null;
        }
    }

    private IEnumerator DisableCollision()
    {
        Debug.Log("XD");
        TilemapCollider2D platformCollider = currentOneWayPlatform.GetComponent<TilemapCollider2D>();
        platformEffector.rotationalOffset = 180;
        yield return new WaitForSeconds(0.45f);
        platformEffector.rotationalOffset = 0;
        platformEffector = null;
    }

}
