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
    /// 
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
    /// 
    /// </summary>
    /// <returns></returns>
    public bool HasReturnPoint()
    {
        return hasReturnPoint;
    }

    /// <summary>
    /// 
    /// </summary>
    public void ClearReturnPoint()
    {
        hasReturnPoint = false;
        returningFromBattle = false;
    }

    public bool IsReturningFromBattle()
    {
        return returningFromBattle;
    }

    public void StartReturningFromBattle()
    {
        returningFromBattle = true;
    }

    public string GetReturnScene()
    {
        return returnSceneName;
    }

    public Vector3 GetReturnPosition()
    {
        return returnPosition;
    }

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
