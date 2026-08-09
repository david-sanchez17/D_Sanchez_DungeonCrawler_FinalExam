using UnityEngine;

public interface IEnemyCombatant
{
    /// <summary>
    /// Defines everything the combat manager needs to know about an enemy, all enemies pull from this
    /// Gotta get that inheritance credit
    /// </summary>
    /// <param name="player"></param>
    void Attack(PlayerCombatController player);
    void TakeDamage(int damage);
    int GetHealth();
    int GetMaxHealth();
    int GetAttack();
    int GetDefense();
    string GetEnemyName();

    bool IsAlive();
}
