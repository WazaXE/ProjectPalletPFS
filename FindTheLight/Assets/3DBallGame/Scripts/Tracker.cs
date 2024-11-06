using UnityEngine;

public class Tracker : MonoBehaviour
{
    public Transform target;
    public Transform objectToRotate;
    public float rotationSmoothness = 5f;

    public GameObject[] healthPrefabs = new GameObject[4];
    private int healthPoints = 4;

    private void Update()
    {
        if (target != null)
        {
            // Track the target
            transform.LookAt(target);

            // Smooth rotation towards the objectToRotate
            if (objectToRotate != null)
            {
                Quaternion targetRotation = Quaternion.LookRotation(objectToRotate.position - transform.position);
                objectToRotate.rotation = Quaternion.Slerp(objectToRotate.rotation, targetRotation, rotationSmoothness * Time.deltaTime);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            // Reduce health and destroy corresponding health prefab
            healthPoints--;
            if (healthPoints >= 0 && healthPoints < healthPrefabs.Length && healthPrefabs[healthPoints] != null)
            {
                Destroy(healthPrefabs[healthPoints]);
            }
            if (healthPoints <= 0)
            {
                // Handle destruction of the tracked object or any other logic when health reaches zero
            }
        }
    }
}
