using NUnit.Framework;
using UnityEngine;

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
    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
