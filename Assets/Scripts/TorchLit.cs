using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class TorchLit : MonoBehaviour
{
    private enum Status { Lit, Off };
    Status status = Status.Off;
    Animator animator;
    Light2D light;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        light = GetComponent<Light2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            switch (collision.gameObject.GetComponent<BasicBulletBehaviour>().element)
            {
                case "Fire":
                    if (status == Status.Off)
                    {
                        status = Status.Lit;
                    }
                    break;

                case "Water":
                    if (status == Status.Lit)
                    {
                        status = Status.Off;
                    }
                    break;
            }
            if (status == Status.Lit)
            {
                animator.SetBool("Lit", true);
                light.intensity = 14;
            }
            else
            {
                animator.SetBool("Lit", false);
                while (light.intensity > 0)
                {
                    light.intensity = 0;
                }
                
            }
        }

    }
}
