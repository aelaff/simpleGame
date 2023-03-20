using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float followDistance = 5f;

    private Transform player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        // Calculate the distance between the enemy and the player
        float distance = Vector3.Distance(transform.position, player.position);

        // If the player is within the follow distance, move towards them
        if (distance <= followDistance)
        {
            // Calculate the direction to move towards the player
            Vector3 direction = (player.position - transform.position).normalized;

            // Move the enemy towards the player
            transform.position += direction * moveSpeed * Time.deltaTime;

            // Rotate the enemy to face the player
            transform.LookAt(player);
        }
    }
}
