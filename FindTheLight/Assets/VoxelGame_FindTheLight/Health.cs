using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    public int health;
    public int numOfHearts;
    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;
    public string gameOverScene;

    private bool canTakeDamage = true;
    private float damageCooldown = 5f;
    private float damageCooldownTimer;

    private void Start()
    {
        damageCooldownTimer = damageCooldown;
        UpdateHeartsDisplay();
        Debug.Log("Health script initialized.");
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("OnTriggerEnter called with " + other.tag);
        if (canTakeDamage && other.CompareTag("Enemy"))
        {
            Debug.Log("Collided with Enemy. Taking damage.");
            TakeDamage(1);
        }
        else if (other.CompareTag("HealthObj"))
        {
            Debug.Log("Collided with HealthObj. Adding health.");
            AddHealth(1);
            Destroy(other.gameObject); // Destroy the health object
        }
    }

    private void TakeDamage(int damage)
    {
        if (canTakeDamage)
        {
            health -= damage;
            Debug.Log("Took damage. Health now: " + health);
            UpdateHeartsDisplay();

            if (health <= 0)
            {
                gameObject.SetActive(false); // Hide the player GameObject
                Debug.Log("Health is zero or less. Loading game over scene: " + gameOverScene);
                SceneManager.LoadScene(gameOverScene);
            }

            canTakeDamage = false;
            damageCooldownTimer = damageCooldown;
        }
    }

    private void AddHealth(int amount)
    {
        if (health < numOfHearts)
        {
            health += amount;
            Debug.Log("Added health. Health now: " + health);
            UpdateHeartsDisplay();
        }
    }

    private void UpdateHeartsDisplay()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < health)
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }

            if (i < numOfHearts)
            {
                hearts[i].color = Color.white;
            }
            else
            {
                hearts[i].color = Color.clear;
            }
        }
        Debug.Log("Hearts display updated.");
    }

    private void Update()
    {
        HandleDamageCooldown();
    }

    private void HandleDamageCooldown()
    {
        if (!canTakeDamage)
        {
            damageCooldownTimer -= Time.deltaTime;

            if (damageCooldownTimer <= 0f)
            {
                canTakeDamage = true;
                Debug.Log("Damage cooldown expired. Can take damage again.");
            }
        }
    }
}