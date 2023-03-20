using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private float spawnRadius = 10f;
    [SerializeField] private float minFollowDistance = 5f;
    [SerializeField] private float maxFollowDistance = 10f;
    [SerializeField] private float minMoveSpeed = 2f;
    [SerializeField] private float maxMoveSpeed = 5f;
    [SerializeField] private float noOfEnemies = 20;

    private void Start()
    {
        SpawnEnemies();
    }

    private void SpawnEnemies()
    {
        // Spawn 10 enemies
        for (int i = 0; i < noOfEnemies; i++)
        {
            // Generate a random position within the spawn radius
            Vector3 spawnPos = transform.position + Random.insideUnitSphere * spawnRadius;
            spawnPos.y = 2f;

            // Instantiate the enemy at the random position
            GameObject newEnemy = Instantiate(enemyPrefab, spawnPos, Quaternion.identity);

            // Set the enemy's follow distance and move speed to random values within the specified ranges
            EnemyAI enemyAI = newEnemy.GetComponent<EnemyAI>();
            enemyAI.followDistance = Random.Range(minFollowDistance, maxFollowDistance);
            enemyAI.moveSpeed = Random.Range(minMoveSpeed, maxMoveSpeed);
        }
    }
}
