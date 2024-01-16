using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    private Animator animator;
    private bool active;

    CheckPoint[] allCheckpoints;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (allCheckpoints != null)
        {
            Array.Clear(allCheckpoints, 0, allCheckpoints.Length);
        }
        
        allCheckpoints = FindObjectsOfType<CheckPoint>();
        foreach (CheckPoint checkpoint in allCheckpoints)
        {
            checkpoint.active = false;
        }

        active = true;
        if (collision.CompareTag("Player"))
        {
            animator.SetBool("active", active);
        }
    }
}
