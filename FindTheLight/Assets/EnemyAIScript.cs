using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAIScript : MonoBehaviour
{
    public Transform player; // Reference to the player's transform
    public float speed = 5f; // Speed at which the enemy moves towards the player

    private void Update()
    {
        // Move towards the player
        MoveTowardsPlayer();
    }

    void MoveTowardsPlayer()
    {
        // Check if the player transform is assigned
        if (player != null)
        {
            // Calculate the direction to the player
            Vector3 direction = (player.position - transform.position).normalized;

            // Move the enemy towards the player
            transform.position += direction * speed * Time.deltaTime;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the collided object is the player
        if (other.CompareTag("Player"))
        {
            // Destroy the enemy object
            Destroy(gameObject);
        }
    }
}
