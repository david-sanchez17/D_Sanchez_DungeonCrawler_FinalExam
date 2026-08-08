using UnityEngine;

public interface IEnemyCombatant
{
    void Attack(PlayerCombatController player);
    void TakeDamage(int damage);
    int GetHealth();
    int GetMaxHealth();
    int GetAttack();
    int GetDefense();
    string GetEnemyName();

    bool IsAlive();
}
