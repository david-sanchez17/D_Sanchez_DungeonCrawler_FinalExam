using UnityEngine;

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
            Debug.LogWarning("Enemyspawner is missing enemy prefabs or spawn points");
            return;
        }
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            SpawnEnemy();
        }
    }

    private void SpawnEnemy()
    {
        GameObject enemyPrefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
    }



    private void Start()
    {
        SpawnEnemies();
    }
}
