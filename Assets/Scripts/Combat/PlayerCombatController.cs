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



    void Update()
    {
        
    }
}
