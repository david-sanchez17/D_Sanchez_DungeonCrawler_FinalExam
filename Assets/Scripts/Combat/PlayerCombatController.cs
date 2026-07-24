using UnityEngine;
public class PlayerCombatController : MonoBehaviour
{

    [SerializeField] private string playerName = "Knight";
    [SerializeField] private int maxHealth = 30;
    [SerializeField] private int attackPower = 8;
    [SerializeField] private int defense = 3;

    private int currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;
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
