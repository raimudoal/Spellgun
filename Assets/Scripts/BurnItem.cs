using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurnItem : MonoBehaviour
{
    [SerializeField] Rigidbody2D heldItem;
    Animator animator;
    [SerializeField] string animatorText;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Bullet"))
        {
            BasicBulletBehaviour bullet = collision.gameObject.GetComponent<BasicBulletBehaviour>();
            if (bullet.element == "Fire")
            {
                Destroy(this.gameObject, 3);
                if (heldItem != null)
                {
                    heldItem.bodyType = RigidbodyType2D.Dynamic;
                }
                animator.Play(animatorText);
            }
        }
    }
}
