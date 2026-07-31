using UnityEngine;
public class PlayerCombatController : MonoBehaviour, IHealthInterface
{

    [SerializeField] private string playerName = "Knight";
    [SerializeField] private int maxHealth = 30;
    [SerializeField] private int attackPower = 30;
    [SerializeField] private int defense = 3;

    [Header("UI")]
    [SerializeField] private HealthBarScript healthBar;

    private int currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;
        if (healthBar != null)
        {
            healthBar.Initialize(this);
        }
       
    }

    public void Attack(EnemyCombatController enemy)
    {
        if (enemy == null)
            return;

        int damage = attackPower - enemy.GetDefense();
        damage = Mathf.Max(1, damage);
        enemy.TakeDamage(damage);
        Debug.Log(playerName + " attacks " + enemy.GetEnemyName() + " for " + damage + " damage.");
    }

    public void TakeDamage(int damage)
    {
        damage -= defense;
        damage = Mathf.Max(1, damage);

        currentHealth -= damage;

        Debug.Log(playerName + " takes " + damage + " damage.");

        if (healthBar != null)
        {
            healthBar.UpdateHealthBar();
        }

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Die();
        }
    }



    private void Die()
    {
        Debug.Log(playerName + " has been defeated.");
    }

    public bool IsAlive()
    {
        return currentHealth > 0;
    }

    public int GetHealth()
    {
        return currentHealth;
    }

    public int GetMaxHealth()
    {
        return maxHealth;
    }

    public int GetAttack()
    {
        return attackPower;
    }

    public int GetDefense()
    {
        return defense;
    }

    public string GetPlayerName()
    {
        return playerName;
    }
}
