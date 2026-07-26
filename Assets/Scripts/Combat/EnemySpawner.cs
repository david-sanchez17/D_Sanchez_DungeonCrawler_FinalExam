using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour
{
    [Header("Enemy settings")]
    [SerializeField] private GameObject[] enemyPrefabs;

    [Header("Spawn Locations")]
    [SerializeField] private Transform[] spawnPoints;

    [Header("Spawning")]
    [SerializeField] private int enemiesToSpawn = 3;

    


    /// <summary>
    /// Spawns specified number of enemies
    /// </summary>
    public void SpawnEnemies()
    {
        if (enemyPrefabs.Length == 0 || spawnPoints.Length == 0)
        {
            //patch ts out later
            Debug.LogWarning("Enemyspawner is missing enemy prefabs or spawn points");
            return;
        }

        //list of available spawn points
        List<Transform> availableSpawnPoints = new List<Transform>(spawnPoints);

        for (int i = 0; i < enemiesToSpawn; i++)
        {
            SpawnEnemy(availableSpawnPoints);
        }
    }

    private void SpawnEnemy(List<Transform> availableSpawnPoints)
    {
        GameObject enemyPrefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];
        int randomIndex = Random.Range(0, availableSpawnPoints.Count);
        Transform spawnPoint = availableSpawnPoints[randomIndex];

        availableSpawnPoints.RemoveAt(randomIndex);

        Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
    }
    private void Start()
    {
        SpawnEnemies();
    }
}
