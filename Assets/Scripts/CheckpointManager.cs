using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{

    public CheckPoint[] checkPoints;
    public int lastCheckpointIndex = 0;
    private Vector3 lastCheckPoint;

    void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("controller");

        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);

        GetAllCheckPoints();
    }

    private void GetAllCheckPoints()
    {
        checkPoints = null;
        checkPoints = FindObjectsOfType<CheckPoint>();
    }

    public void LastCheckpointActive()
    {
        for (int i = 0; i < checkPoints.Length; i++)
        {
            if (i == lastCheckpointIndex)
            {
                checkPoints[i].GetComponent<Animator>().SetBool("active", true);
                lastCheckPoint = checkPoints[i].transform.position;
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
