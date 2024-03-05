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
    Light2D lightComp;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        lightComp = GetComponent<Light2D>();
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
                lightComp.intensity = 14;
            }
            else
            {
                animator.SetBool("Lit", false);
                while (lightComp.intensity > 0)
                {
                    lightComp.intensity = 0;
                }
                
            }
        }

    }
}
