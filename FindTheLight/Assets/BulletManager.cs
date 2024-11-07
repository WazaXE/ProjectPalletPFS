using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    [System.Serializable]
    public class GunSettings
    {
        public string gunType;
        public GameObject bulletPrefab;
        public float bulletSpeed;
        public float bulletSize;
        public int bulletDamage;
    }

    public List<GunSettings> gunSettingsList;
    public Transform firePoint; // Assignable fire point for bullet instantiation

    private GunSettings selectedGun;

    void Start()
    {
        // Auto-select the first gun in the list if available
        if (gunSettingsList.Count > 0)
        {
            selectedGun = gunSettingsList[0];
            Debug.Log("Auto-selected gun: " + selectedGun.gunType);
        }
        else
        {
            Debug.LogWarning("gunSettingsList is empty. Add gun settings in the Inspector.");
        }
    }

    void Update()
    {
        // Change gun type based on input (example with keys: 1 for Small, 2 for Medium, 3 for Heavy)
        if (Input.GetKeyDown(KeyCode.Alpha1)) SetGunType("Small");
        if (Input.GetKeyDown(KeyCode.Alpha2)) SetGunType("Medium");
        if (Input.GetKeyDown(KeyCode.Alpha3)) SetGunType("Heavy");

        // Fire bullet on left mouse click
        if (Input.GetMouseButtonDown(0))
        {
            FireBullet();
        }
    }

    void SetGunType(string type)
    {
        selectedGun = gunSettingsList.Find(g => g.gunType == type);
        if (selectedGun != null)
        {
            Debug.Log("Gun type set to: " + type);
        }
        else
        {
            Debug.LogWarning("Gun type not found: " + type + ". Check gunSettingsList in Inspector.");
        }
    }

    void FireBullet()
    {
        if (selectedGun == null)
        {
            Debug.LogWarning("No gun selected. Make sure gunSettingsList has entries and gun type is selected.");
            return;
        }

        if (selectedGun.bulletPrefab == null)
        {
            Debug.LogError("Bullet prefab is missing for the selected gun. Check gunSettingsList in Inspector.");
            return;
        }

        if (firePoint == null)
        {
            Debug.LogError("FirePoint not assigned. Assign a fire point in the Inspector.");
            return;
        }

        // Instantiate the bullet at the firePoint's position and rotation
        GameObject bullet = Instantiate(selectedGun.bulletPrefab, firePoint.position, firePoint.rotation);
        bullet.transform.localScale = Vector3.one * selectedGun.bulletSize;

        // Check if the bullet was instantiated successfully
        if (bullet != null)
        {
            Debug.Log("Bullet instantiated successfully.");
        }
        else
        {
            Debug.LogError("Bullet instantiation failed.");
            return;
        }

        // Add a Rigidbody component to the bullet if it doesn't already have one
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        if (rb == null) rb = bullet.AddComponent<Rigidbody>();

        // Apply forward force to propel the bullet based on the selected gun's bullet speed
        rb.AddForce(firePoint.forward * selectedGun.bulletSpeed, ForceMode.VelocityChange);

        // Optional: Store bullet damage in the bullet’s name or tag for reference
        bullet.tag = "Bullet";
        bullet.name = "Bullet_Damage_" + selectedGun.bulletDamage;

        // Destroy bullet after 5 seconds to prevent memory buildup
        Destroy(bullet, 5f);
    }
}
