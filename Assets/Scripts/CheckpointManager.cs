using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{

    public GameObject[] checkPoints;
    public int lastCheckpointIndex = 0;
    public Vector3 lastCheckPoint;
    PlayerReset playerReset;

    void Start()
    {
        playerReset = FindObjectOfType<PlayerReset>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LastCheckpointActive()
    {
        for (int i = 0; i < checkPoints.Length; i++)
        {
            if (i == lastCheckpointIndex)
            {
                checkPoints[i].GetComponent<Animator>().SetBool("active", true);
                lastCheckPoint = checkPoints[i].transform.position;
                playerReset.lastCheckPointSaved = lastCheckPoint;
            }
            else
            {
                checkPoints[i].GetComponent<Animator>().SetBool("active", false);
            }
        }
    }

    public Vector3 GetLastCheckPointPosition()
    {
        return lastCheckPoint;
    }
}
