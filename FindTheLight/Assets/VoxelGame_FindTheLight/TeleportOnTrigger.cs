using UnityEngine;

public class TeleportOnTrigger : MonoBehaviour
{
    public Transform teleportTarget; // The target location for teleportation

    // This method is called when the player enters a trigger collider
    private void OnTriggerEnter(Collider other)
    {
        // Check if the collider has the tag "Enemy"
        if (other.CompareTag("Enemy"))
        {
            // Teleport the player to the teleportTarget position
            if (teleportTarget != null)
            {
                transform.position = teleportTarget.position;
                Debug.Log("Player teleported to " + teleportTarget.position);
            }
            else
            {
                Debug.LogWarning("Teleport target not set!");
            }
        }
    }
}
