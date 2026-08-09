using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

    [Header("Final Boss")]
    [SerializeField] private FinalBossAI finalboss;
    [SerializeField] private GameObject victoryPanel;

    [Header("Combat UI")]
    [SerializeField] private GameObject attackButton;
    [SerializeField] private GameObject guardButton;
    [SerializeField] private GameObject combatLog;
    [SerializeField] private GameObject playerHealthBar;

    [Header("Combatants")]
    [SerializeField] private List<PlayerCombatController> players = new List<PlayerCombatController>();
    [SerializeField] private List<IEnemyCombatant> enemies = new List<IEnemyCombatant>();


    private CombatState currentState;

    private bool waitingForTarget = false;

    public event Action<CombatState> OnTurnChanged;
    public event Action<string> OnCombatLog;


    private void Awake()
    {
        if (victoryPanel != null)
        {
            victoryPanel.SetActive(false);
        }
    }
    private void Start()
    {
        StartCoroutine(InitializeCombat());

    }


    /// <summary>
    /// Waits one frame before finding enemies. This allows spawned enemies to finish creating.
    /// Final boss is assigned
    /// </summary>
    /// 
   
    private IEnumerator InitializeCombat()
    {
        yield return null;
        players.Clear();
        enemies.Clear();
        PlayerCombatController[] playerObjects = FindObjectsByType<PlayerCombatController>();
        players.AddRange(playerObjects);

        EnemyCombatController[] normalEnemies = FindObjectsByType<EnemyCombatController>();
        foreach (EnemyCombatController enemy in normalEnemies)
        {
            enemies.Add(enemy);
        }
        FinalBossAI[] bossEnemies = FindObjectsByType<FinalBossAI>();
        foreach (FinalBossAI boss in bossEnemies)
        {
            enemies.Add(boss);
        }
        AddCombatLog("Combat Begin");
        StartPlayerTurn();
    }


    private void ChangeState(CombatState newState)
    {
        currentState = newState;
        if (OnTurnChanged != null)
        {
            OnTurnChanged(currentState);
        }
    }

    /// <summary>
    /// Handles combat log messages
    /// </summary>
    /// <param name="message"></param>
    public void AddCombatLog(string message)
    {
        if (OnCombatLog != null)
        {
            OnCombatLog(message);
        }
    }

    /// <summary>
    /// Begins the player's turn.
    /// </summary>
    private void StartPlayerTurn()
    {
        ChangeState(CombatState.PlayerTurn);
        waitingForTarget = false;

        if (players.Count > 0)
        {
            players[0].EndTurn();
        }

        AddCombatLog("Player Turn");
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
        AddCombatLog("Select an enemy to attack");
    }

    /// <summary>
    /// Handles enemy selecting after pressing the attack button
    /// </summary>
    /// <param name="selectedEnemy"></param>
    public void SelectEnemy(IEnemyCombatant selectedEnemy)
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

    /// <summary>
    /// Called by the guard button
    /// </summary>
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
        ChangeState(CombatState.EnemyTurn);
        AddCombatLog("Enemy Turn");


        foreach (IEnemyCombatant enemy in enemies.ToList())
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

    private void ShowFinalBossVictory()
    {
       if (victoryPanel != null)
        {
            victoryPanel.SetActive(true);
        }
        if (attackButton != null)
        {
            attackButton.SetActive(false);
        }
        if (guardButton != null)
        {
            guardButton.SetActive(false);
        }
        if (playerHealthBar != null)
        {
            playerHealthBar.SetActive(false);
        }
        if (combatLog != null)
        {
            combatLog.SetActive(false);
        }
        if (CombatAudioManager.Instance != null)
        {
            CombatAudioManager.Instance.StopBattleMusic();
        }
    }

    private void ReturnFromNormalBattle()
    {
        BattleTransitionManager.Instance.MarkEnemyDefeated(BattleTransitionManager.Instance.GetCurrentEnemy());
        BattleTransitionManager.Instance.StartReturningFromBattle();
        SceneManager.LoadScene(BattleTransitionManager.Instance.GetReturnScene());
    }


    /// <summary>
    /// Called when all enemies are defeated.
    /// Normal battles return through the battle transition manager
    /// final boss displays victory screen instead
    /// </summary>
    private void Victory()
    {
        ChangeState(CombatState.Victory);
        waitingForTarget = false;
        if (attackButton != null)
        {
            attackButton.SetActive(false);
        }
        if (guardButton != null)
        {
            guardButton.SetActive(false);
        }
        AddCombatLog("Victory");
        if (finalboss != null)
        {
            ShowFinalBossVictory();
            return;
        }
        ReturnFromNormalBattle();
    }

   public void ReturnToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    /// <summary>
    /// Called when all players are defeated.
    /// </summary>
    private void Defeat()
    {
        ChangeState(CombatState.Defeat);

        AddCombatLog("Defeat!");
        SceneManager.LoadSceneAsync("MainMenu");

    } 
}
