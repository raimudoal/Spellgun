using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] EnemyBehaviour[] enemies;
    [SerializeField] Vector3[] enemy_positions;
    private bool active = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !active)   
        {
            active = true;
            SpawnEnemies();
        }
    }

    void SpawnEnemies()
    {
        for(int i = 0; i < enemies.Length; i++)
        {
            Instantiate(enemies[i], enemy_positions[i], transform.rotation);
        }
    }
}
