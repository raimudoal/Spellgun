using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private BasicBulletBehaviour projectilePrefab;
    [SerializeField] private Transform shootPosition;
    private SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        var pos = Camera.main.WorldToScreenPoint(transform.position);
        var dir = Input.mousePosition - pos;
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (mousePos.x > transform.position.x)
        {
            spriteRenderer.flipY = false;
        }
        else if(mousePos.x < transform.position.x)
        {
            spriteRenderer.flipY = true;
        }

        if (Input.GetMouseButtonDown(0))
        {
            BasicBulletBehaviour projectile = Instantiate(projectilePrefab, shootPosition.position, transform.rotation);
            projectile.LaunchProjectile(transform.right);
        }
    }
}
