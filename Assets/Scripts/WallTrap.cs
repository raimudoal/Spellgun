using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallTrap : MonoBehaviour
{
    [SerializeField]GameObject spikePrefab;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Player")) 
        {
            StartCoroutine(Activate());
        }
    }


    private IEnumerator Activate()
    {
        GameObject spikes = Instantiate(spikePrefab, transform.position, transform.rotation);

        yield return new WaitForSeconds(3f);
        Destroy(spikes);
    }
}
