using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;


public class CombatManager : MonoBehaviour
{

    public enum CombatState
    {
        PlayerTurn,
        EnemyTurn,
        Victory,
        Defeat
    }


    [Header("Combatants")]
    [SerializeField] private List<PlayerCombatController> players = new List<PlayerCombatController>();
    [SerializeField] private List<EnemyCombatController> enemies = new List<EnemyCombatController>();


    private CombatState currentState;

    private bool waitingForTarget = false;


    private void Start()
    {
        StartCoroutine(InitializeCombat());

    }


    /// <summary>
    /// Waits one frame before finding enemies. This allows spawned enemies to finish creating.
    /// </summary>
    private IEnumerator InitializeCombat()
    {
        yield return null;
        players.Clear();
        enemies.Clear();

        players.AddRange(FindObjectsByType<PlayerCombatController>());
        enemies.AddRange(FindObjectsByType<EnemyCombatController>());

        Debug.Log("Players: " + players.Count);
        Debug.Log("Enemies: " + enemies.Count);


        currentState = CombatState.PlayerTurn;
        Debug.Log("Combat Begin");
        StartPlayerTurn();
    }

    /// <summary>
    /// Begins the player's turn.
    /// </summary>
    private void StartPlayerTurn()
    {
        currentState = CombatState.PlayerTurn;
        waitingForTarget = false;

        if (players.Count > 0)
        {
            players[0].EndTurn();
        }

        Debug.Log("Player Turn");
    }

    /// <summary>
    /// Called by the attack button.
    /// </summary>
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
        waitingForTarget = true;
        Debug.Log("Select an enemy to attack");
    }

    public void SelectEnemy(EnemyCombatController selectedEnemy)
    {
        if (!waitingForTarget)
            return;
        if (selectedEnemy == null)
            return;
        if (!selectedEnemy.IsAlive())
            return;

        waitingForTarget = false;


        players[0].Attack(selectedEnemy);
        RemoveDeadEnemies();

        if (enemies.Count == 0)
        {
            Victory();
            return;
        }
        StartEnemyTurn();
    }

    public bool IsSelectingTarget()
    {
        return waitingForTarget;
    }


    public void PlayerGuard()
    {
        if (currentState != CombatState.PlayerTurn)
        {
            return;
        }
        players[0].Guard();
        StartEnemyTurn();
    }

    /// <summary>
    /// Starts the enemy turn.
    /// </summary>
    private void StartEnemyTurn()
    {
        StartCoroutine(EnemyTurnRoutine());
    }

    /// <summary>
    /// Handles enemy attacks one at a time.
    /// </summary>
    private IEnumerator EnemyTurnRoutine()
    {
        currentState = CombatState.EnemyTurn;


        Debug.Log("Enemy Turn");


        foreach (EnemyCombatController enemy in enemies.ToList())
        {
            if (enemy != null && enemy.IsAlive())
            {
                enemy.Attack(players[0]);

                yield return new WaitForSeconds(1f);
            }
        }


        RemoveDeadPlayers();


        if (players.Count == 0)
        {
            Defeat();
            yield break;
        }
        yield return new WaitForSeconds(1f);


        StartPlayerTurn();
    }

    /// <summary>
    /// Removes enemies that have died.
    /// </summary>
    private void RemoveDeadEnemies()
    {
        enemies.RemoveAll(enemy => enemy == null || !enemy.IsAlive());
    }

    /// <summary>
    /// Removes players that have died.
    /// </summary>
    private void RemoveDeadPlayers()
    {
        players.RemoveAll(player => player == null || !player.IsAlive());
    }

    /// <summary>
    /// Called when all enemies are defeated.
    /// </summary>
    private void Victory()
    {
        currentState = CombatState.Victory;

        Debug.Log("Victory!");
       
    }

    /// <summary>
    /// Called when all players are defeated.
    /// </summary>
    private void Defeat()
    {
        currentState = CombatState.Defeat;

        Debug.Log("Defeat!");
        SceneManager.LoadSceneAsync("MainMenu");

    }
}
