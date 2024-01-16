using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{

    public GameObject[] checkPoints;
    public int lastCheckpointIndex = 0;

    void Start()
    {
        
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
            }
            else
            {
                checkPoints[i].GetComponent<Animator>().SetBool("active", false);
            }
        }
    }
}
