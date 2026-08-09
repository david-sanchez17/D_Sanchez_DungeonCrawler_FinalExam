using System.Collections.Generic;
using UnityEngine;

public class BattleTransitionManager : MonoBehaviour
{
    public static BattleTransitionManager Instance;
    private Vector3 returnPosition;
    private string returnSceneName;

    private bool hasReturnPoint = false;
    private bool returningFromBattle = false;

    private int currentEnemyID;

    private HashSet<int> defeatedEnemies = new HashSet<int>();

    /// <summary>
    /// Creates a singleton instance of the battletransitionmanager
    /// Prevents duplicate instances and keeps object between scenes
    /// </summary>
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    /// <summary>
    /// Saves players current scene and position before entering combat
    /// Is used to return the player back to the correct location after battle ends
    /// </summary>
    /// <param name="sceneName"></param>
    /// <param name="playerPosition"></param>
    public void SaveReturnPoint(string sceneName, Vector3 playerPosition)
    {
        returnSceneName = sceneName;
        returnPosition = playerPosition;
        hasReturnPoint = true;
        returningFromBattle = false;
    }

    /// <summary>
    /// checks whether a valid return point is saved
    /// </summary>
    /// <returns></returns>
    public bool HasReturnPoint()
    {
        return hasReturnPoint;
    }

    /// <summary>
    /// Clears the saved return point and resets the return from battle state
    /// </summary>
    public void ClearReturnPoint()
    {
        hasReturnPoint = false;
        returningFromBattle = false;
    }

    /// <summary>
    /// Checks whether player is currently returning to overworld
    /// </summary>
    /// <returns></returns>
    public bool IsReturningFromBattle()
    {
        return returningFromBattle;
    }

    /// <summary>
    /// Marks the player has started returning from combat scene
    /// </summary>
    public void StartReturningFromBattle()
    {
        returningFromBattle = true;
    }

    /// <summary>
    /// Gets name of the scene the player should return to 
    /// </summary>
    /// <returns></returns>
    public string GetReturnScene()
    {
        return returnSceneName;
    }

    /// <summary>
    /// Gets position where player should be placed after returning from combat
    /// </summary>
    /// <returns></returns>
    public Vector3 GetReturnPosition()
    {
        return returnPosition;
    }

    /// <summary>
    /// Gets ID of the enemy currently being encountered
    /// ID stuff was stuff i learned from my boss when working on our card game. Not sure if thats relavent but i figured i'd clarify
    /// </summary>
    /// <param name="enemyID"></param>
   public void SetCurrentEnemy(int enemyID)
    {
        currentEnemyID = enemyID;
    }

    public int GetCurrentEnemy()
    {
        return currentEnemyID;
    }

    public void MarkEnemyDefeated(int enemyID)
    {
        defeatedEnemies.Add(enemyID);
    }

    public bool IsEnemyDefeated(int enemyID)
    {
        return defeatedEnemies.Contains(enemyID);
    }
}
