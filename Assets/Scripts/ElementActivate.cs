using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class ElementActivate : MonoBehaviour
{
    private enum Status { Lit, Off };
    [SerializeField] string typeWanted;
    Status status = Status.Off;
    Animator animator;
    Light2D lightComp;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        lightComp = GetComponent<Light2D>();
    }

    public bool ReturnStatus(ElementActivate self)
    {
        if (status == Status.Lit)
        {
            return true;
        }
        else
            return false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            switch (collision.gameObject.GetComponent<BasicBulletBehaviour>().element)
            {
                case "Fire":
                    if (typeWanted.Equals("Fire"))
                    {
                        status = Status.Lit;
                    }
                    break;

                case "Water":
                    if (typeWanted.Equals("Water"))
                    {
                        status = Status.Lit;
                    }
                    break;
                case "Electric":
                    if (typeWanted.Equals("Electric"))
                    {
                        status = Status.Lit;
                    }
                    break;
                case "Stone":
                    if (typeWanted.Equals("Stone"))
                    {
                        status = Status.Lit;
                    }
                    break;
            }
            if (status == Status.Lit)
            {
                lightComp.intensity = 14;
            }
            else
            {
                while (lightComp.intensity > 0)
                {
                    lightComp.intensity = 0;
                }

            }
        }

    }
}
