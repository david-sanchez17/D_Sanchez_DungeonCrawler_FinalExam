using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class CombatManager : MonoBehaviour
{
    public enum CombatState
    {
        PlayerTurn,
        EnemyTurn,
        Victory, 
        Defeat,
    }

    [SerializeField] private List<PlayerCombatController> players = new List<PlayerCombatController>();
    [SerializeField] private List<EnemyCombatController> enemies = new List<EnemyCombatController>();

    private CombatState currentState;


    private void Start()
    {
        currentState = CombatState.PlayerTurn;
        Debug.Log("Combat Begin");
        StartPlayerTurn();
    }

    private void StartPlayerTurn()
    {
        currentState = CombatState.PlayerTurn;
        Debug.Log("Player turn");
    }

    public void PlayerAttack()
    {
        if (currentState != CombatState.PlayerTurn)
            return;
        RemoveDeadEnemies();

        if (enemies.Count == 0)
        {
            Victory();
            return;
        }

        players[0].Attack(enemies[0]);
        RemoveDeadEnemies();

        if (enemies.Count == 0)
        {
            Victory();
            return;
        }

        StartEnemyTurn();
    }

    private void StartEnemyTurn()
    {
        currentState = CombatState.EnemyTurn;
        Debug.Log("EnemyTurn");

        foreach (EnemyCombatController enemy in enemies)
        {
            if (enemy.IsAlive())
            {
                enemy.Attack(players[0]);
            }
        }

        RemoveDeadPlayers();

        if (players.Count == 0)
        {
            Defeat();
            return;
        }
        StartPlayerTurn();
    }

    private void RemoveDeadEnemies()
    {
        enemies.RemoveAll(EnemyCombatController => !EnemyCombatController.IsAlive());
    }

    private void RemoveDeadPlayers()
    {
        players.RemoveAll(players => !players.IsAlive());
    }

    private void Victory()
    {
        currentState = CombatState.Victory;
        Debug.Log("Victory");
    }

    private void Defeat()
    {
        currentState = CombatState.Defeat;
        Debug.Log("Defeat");
    }
}
