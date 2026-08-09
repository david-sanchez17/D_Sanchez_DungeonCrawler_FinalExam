using UnityEngine;

public class FinalBossAI : MonoBehaviour, IHealthInterface, IEnemyCombatant
{
    [SerializeField] private string bossName = "The Forsaken King";

    [Header("Boss stat")]
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private int attackPower = 15;
    [SerializeField] private int defense = 5;

    [Header("Special Attacks")]
    [SerializeField] private int heavyAttackPower = 25;
    [SerializeField] private int undeadWrathPower = 30;

    [SerializeField] private HealthBarScript healthBar;

    private int currentHealth;
    private const int MinimumDamage = 1;

    private void Start()
    {
        currentHealth = maxHealth;

        if (healthBar != null)
        {
            healthBar.Initialize(this);
        }
    }

    /// <summary>
    /// Performs the boss main attack
    /// Player defense reduces amount of damage taken
    /// </summary>
    /// <param name="player"></param>
    public void Attack(PlayerCombatController player)
    {
        if (player == null)
        {
            return;
        }
        int damage = attackPower - player.GetDefense();
        damage = Mathf.Max(MinimumDamage, damage);
        player.TakeDamage(damage);
        CombatManager combatManager = FindAnyObjectByType<CombatManager>();

        if (combatManager != null)
        {
            combatManager.AddCombatLog(bossName + " attacks " + player.GetPlayerName() + " for " + damage + " damage ");
        }
        if (CombatAudioManager.Instance != null)
        {
            CombatAudioManager.Instance.PlayEnemyAttack();
        }
    }

    /// <summary>
    /// Performs heavy attack
    /// Deals more damage than base attack
    /// </summary>
    /// <param name="player"></param>
    public void HeavyAttack(PlayerCombatController player)
    {
        if (player == null)
        {
            return;
        }
            int damage = heavyAttackPower - player.GetDefense();
            damage = Mathf.Max(MinimumDamage, damage);
            player.TakeDamage(damage);
            CombatManager combatManager = FindAnyObjectByType<CombatManager>();

            if (combatManager != null)
            {
                combatManager.AddCombatLog(bossName + " unleashes a devastating attack for " + damage + " damage! ");
            }
        if (CombatAudioManager.Instance != null)
        {
            CombatAudioManager.Instance.PlayEnemyAttack();
        }
    }

    /// <summary>
    /// Performs special attack
    /// </summary>
    /// <param name="player"></param>
    public void UndeadWrath(PlayerCombatController player)
    {
        if (player == null)
        {
            return;
        }
        int damage = undeadWrathPower - player.GetDefense();
        damage = Mathf.Max(MinimumDamage, damage);

        player.TakeDamage(damage);
        CombatManager combatManager = FindAnyObjectByType<CombatManager>();

        if (combatManager != null)
        {
            combatManager.AddCombatLog(bossName + "uses Undead Wrath and deals" + damage + " devastating damage!");
        }
        if (CombatAudioManager.Instance != null)
        {
            CombatAudioManager.Instance.PlayEnemyAttack();
        }
    }

    /// <summary>
    /// Applies incoming damage to the boss after calculating defense
    /// updates health bar and defeats boss when health reaches zero
    /// </summary>
    /// <param name="damage"></param>
    public void TakeDamage (int damage)
    {
        damage -= defense;
        damage = Mathf.Max(MinimumDamage, damage);
        currentHealth -= damage;
        currentHealth = Mathf.Max(0, currentHealth);
        CombatManager combatManager = FindAnyObjectByType<CombatManager>();

        if (combatManager != null)
        {
            CombatAudioManager.Instance.PlayEnemyHit();
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
    /// Removes health bar, destroys boss, haldes everything that happens when final boss dies
    /// </summary>
    private void Die()
    {
        CombatManager combatManager = FindAnyObjectByType<CombatManager>();
        if (combatManager != null)
        {
            combatManager.AddCombatLog(bossName + " has been defeated!");
        }
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

    /// <summary>
    /// Allows player to select the final boss by clicking on it
    /// </summary>
    private void OnMouseDown()
    {
        CombatManager combatManager = FindAnyObjectByType<CombatManager>();
        if (combatManager != null)
        {
            combatManager.SelectEnemy(this);
        }
    }

    public bool isAlive()
    {
        return currentHealth > 0;
    }

    public bool IsAlive()
    {
        return isAlive();
    }

    public int GetAttack()
    {
        return attackPower;
    }

    public int GetHealth()
    {
        return currentHealth;
    }

    public int GetMaxHealth()
    {
        return maxHealth;
    }

    public int GetDefense()
    {
        return defense;
    }

    public string GetEnemyName()
    {
        return bossName;
    }

    public int GetHeavyAttackPower()
    {
        return heavyAttackPower;
    }

    public int GetUndeadWrathPower()
    {
        return undeadWrathPower;
    }
}
