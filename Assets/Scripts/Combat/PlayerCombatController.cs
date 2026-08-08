using UnityEngine;
public class PlayerCombatController : MonoBehaviour, IHealthInterface
{
    [Header("Stats")]
    [SerializeField] private string playerName = "Knight";
    [SerializeField] private int maxHealth = 30;
    [SerializeField] private int attackPower = 30;
    [SerializeField] private int defense = 3;

    [Header("Guard")]
    [SerializeField] private int guardDefenseBonus = 5;
    private int temporaryDefenseBonus = 0;
    private int guardTurnsRemaining = 0;

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
    /// <param name="enemy"></param>
    public void Attack(IEnemyCombatant enemy)
    {
        if (enemy == null)
            return;

        int damage = attackPower - enemy.GetDefense();
        damage = Mathf.Max(1, damage);
        enemy.TakeDamage(damage);

        CombatManager combatManager = FindAnyObjectByType<CombatManager>();
        //yeah i know i know im an asshole my bad
        if (combatManager != null)
        {
            combatManager.AddCombatLog(playerName + " attacks " + enemy.GetEnemyName() + " for " + damage + " damage.");
        }
        if (CombatAudioManager.Instance != null)
        {
            CombatAudioManager.Instance.PlayPlayerAttack();
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public void Guard()
    {
        temporaryDefenseBonus = guardDefenseBonus;
        guardTurnsRemaining = 2;
        CombatManager combatManager = FindAnyObjectByType<CombatManager>();
        if (combatManager != null)
        {
            combatManager.AddCombatLog(playerName + " is guarding.");
        }
        if (CombatAudioManager.Instance != null)
        {
            CombatAudioManager.Instance.PlayPlayerGuard();
        }
    }

    public void EndTurn()
    {
        if (guardTurnsRemaining > 0)
        {
            guardTurnsRemaining--;
            if (guardTurnsRemaining == 0)
            {
                temporaryDefenseBonus = 0;
                CombatManager combatManager = FindAnyObjectByType<CombatManager>();
                if (combatManager != null)
                {
                    combatManager.AddCombatLog(playerName + "'s guard has worn off.");
                }
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="damage"></param>
    public void TakeDamage(int damage)
    {
        damage -= GetDefense();

        damage = Mathf.Max(1, damage);

        currentHealth -= damage;

        CombatManager combatManager = FindAnyObjectByType<CombatManager>();
        if (combatManager != null)
        {
            combatManager.AddCombatLog(playerName + " takes " + damage + " damage.");
        }

        if (healthBar != null)
        {
            healthBar.UpdateHealthBar();
        }

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Die();
        }
        if (CombatAudioManager.Instance != null)
        {
            CombatAudioManager.Instance.PlayPlayerHit();
        }
    }


    /// <summary>
    /// Called when player dies.
    /// </summary>
    private void Die()
    {
        CombatManager combatManager = FindAnyObjectByType<CombatManager>();
        if (combatManager != null)
        {
            combatManager.AddCombatLog(playerName + " has been defeated.");
        }
        if (CombatAudioManager.Instance != null)
        {
            CombatAudioManager.Instance.PlayPlayerDeath();
        }
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
        return defense + temporaryDefenseBonus;
    }

    public string GetPlayerName()
    {
        return playerName;
    }
}
