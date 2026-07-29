using System.Runtime.CompilerServices;
using UnityEngine;

public class EnemyCombatController : MonoBehaviour
{
    [SerializeField] private string enemyName = "Goblin";
    [SerializeField] private int maxHealth = 20;
    [SerializeField] private int attackPower = 5;
    [SerializeField] private int defense = 2;



    private int currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void Attack(PlayerCombatController player)
    {
        if (player == null)
            return;
        int damage = attackPower - player.GetDefense();

        //Always deal at least 1 damage
        damage = Mathf.Max(1, damage);
        player.TakeDamage(damage);

        Debug.Log(enemyName + " attacks " + player.GetPlayerName() + " for " + damage + " damage.");
    }

    private void OnMouseDown()
    {
        CombatManager combatManager = FindAnyObjectByType<CombatManager>();

        if (combatManager != null)
        {
            combatManager.SelectEnemy(this);
        }
    }

    public void TakeDamage(int damage)
    {
        damage -= defense;
        damage = Mathf.Max(1, damage);
        currentHealth -= damage;

        Debug.Log(enemyName + " takes " + damage + " damage.");

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Die();
        }
    }

    private void Die()
    {
        Debug.Log(enemyName + " has been defeated.");
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
