using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class EnemyCombatController : MonoBehaviour, IHealthInterface, IEnemyCombatant
{
    [SerializeField] private string enemyName = "Goblin";
    [SerializeField] private int maxHealth = 20;
    [SerializeField] private int attackPower = 5;
    [SerializeField] private int defense = 2;

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

    /// <summary>
    /// 
    /// </summary>
    /// <param name="player"></param>

    public void Attack(PlayerCombatController player)
    {
        if (player == null)
            return;
        int damage = attackPower - player.GetDefense();

        //Always deal at least 1 damage
        damage = Mathf.Max(1, damage);
        player.TakeDamage(damage);

        CombatManager combatManager = FindAnyObjectByType<CombatManager>();
        if (combatManager != null)
        {
            combatManager.AddCombatLog(enemyName + " attacks " + player.GetPlayerName() + " for " + damage + " damage.");
        }
        if (CombatAudioManager.Instance != null)
        {
            CombatAudioManager.Instance.PlayEnemyAttack();
        }
    }

    /// <summary>
    /// 
    /// </summary>
    private void OnMouseDown()
    {
        CombatManager combatManager = FindAnyObjectByType<CombatManager>();

        if (combatManager != null)
        {
            combatManager.SelectEnemy(this);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="damage"></param>
    public void TakeDamage(int damage)
    {
        damage -= defense;
        damage = Mathf.Max(1, damage);

        currentHealth -= damage;
        currentHealth = Mathf.Max(0, currentHealth);

        CombatManager combatManager = FindAnyObjectByType<CombatManager>();
        if (combatManager != null)
        {
            combatManager.AddCombatLog(enemyName + " takes " + damage + " damage.");
        }
        if (CombatAudioManager.Instance != null)
        {
            CombatAudioManager.Instance.PlayEnemyHit();
        }

        if (currentHealth < 0)
        {
            currentHealth = 0;
        }

        if (healthBar != null)
        {
            healthBar.UpdateHealthBar();
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    /// <summary>
    /// Called when an enemy dies.
    /// </summary>
    private void Die()
    {
        CombatManager combatManager = FindAnyObjectByType<CombatManager>();
        combatManager.AddCombatLog(enemyName + " has been defeated.");

        if (CombatAudioManager.Instance != null)
        {
            CombatAudioManager.Instance.PlayEnemyDeath();
        }

        if (healthBar != null)
        {
            Destroy(healthBar.gameObject);
        }

        Destroy(gameObject);
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

    public string GetEnemyName()
    {
        return enemyName;
    }
}
