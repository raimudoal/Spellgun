using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerReset : MonoBehaviour
{
    PlayerMovement player;
    CheckpointManager checkpointManager;
    public Vector3 lastCheckPointSaved;
    bool active = false;
    public bool key1;
    public bool key2;

    public bool fire;
    public bool water;
    public bool electric = true;
    public bool stone;

    // Start is called before the first frame update
    void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("resetter");

        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }

    public bool HasKeys()
    {
        if (key1 && key2)
        {
            return true;
        }
        else
            return false;
    }

    // Update is called once per frame
    void Update()
    {
        player = FindObjectOfType<PlayerMovement>();
        checkpointManager = FindObjectOfType<CheckpointManager>();
        if (player.health == 0 && !active)
        {
            StartCoroutine(StopAndTP());
        }
        
    }

    IEnumerator StopAndTP()
    {
        active = true;
        yield return new WaitForSeconds(5.6f);
        player.transform.position = lastCheckPointSaved;
        active = false;
    }
}
