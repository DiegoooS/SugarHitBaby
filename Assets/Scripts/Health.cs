using SugarHitBaby;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] int maxHealth = 10;

    private int currentHealth;

    private void Start() => SetCurrentHealth(maxHealth);

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (gameObject.GetComponent<Zombie>() != null)
        {
            gameObject.GetComponent<SpriteRenderer>().color = Color.grey;
        }
        
        if (currentHealth <= 0) Die();

        if (gameObject.TryGetComponent<Player>(out Player player))
        {
            player.SetHealthUI(maxHealth, currentHealth);
        }
    }

    private void Die()
    {
        Destroy(gameObject);

        if (gameObject.GetComponent<Zombie>() != null)
            SetZombieCounter();

        if (gameObject.GetComponent<Player>() != null)
            SceneManager.Instance.SetEndGameLevel();
    }

    private void SetZombieCounter()
    {
        if (GameManager.Instance == null) return;

        GameManager.Instance.AddZombieKillCounter();
    }

    private void SetCurrentHealth(int health) => currentHealth = health;
}
