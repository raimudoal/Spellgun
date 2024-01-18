using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    private Animator animator;
    public bool active;
    public int index;
    CheckpointManager controlCheckpoints;

    CheckPoint[] allCheckpoints;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        controlCheckpoints = FindObjectOfType<CheckpointManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            controlCheckpoints.lastCheckpointIndex = index;
            controlCheckpoints.LastCheckpointActive();
        }
        
    }
}
