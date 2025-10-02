using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemySpawnData
{
    public string enemyName;
    public GameObject enemyPrefab;
    public Transform spawnPoint;

    [HideInInspector] public GameObject currentInstance;

    // dane startowe
    [HideInInspector] public Vector3 initialPosition;
    [HideInInspector] public Quaternion initialRotation;
}

public class DungeonManager : MonoBehaviour
{
    [Header("Dungeon Info")]
    public string dungeonName;

    [Header("Enemy Spawns")]
    public List<EnemySpawnData> enemies = new List<EnemySpawnData>();

    private void Start()
    {
        // zapisz początkowe dane wszystkich przeciwników
        CaptureInitialEnemyData();

        // od razu spawnuj na starcie
        SpawnAllEnemies();
    }

    /// <summary>
    /// Zapisuje pozycję/rotację przeciwników na początku gry
    /// </summary>
    private void CaptureInitialEnemyData()
    {
        foreach (var enemy in enemies)
        {
            if (enemy.spawnPoint != null)
            {
                enemy.initialPosition = enemy.spawnPoint.position;
                enemy.initialRotation = enemy.spawnPoint.rotation;
            }
        }
    }

    /// <summary>
    /// Spawn wszystkich przeciwników w ich początkowych lokacjach
    /// </summary>
    public void SpawnAllEnemies()
    {
        foreach (var enemy in enemies)
        {
            if (enemy.enemyPrefab == null) continue;

            // usuń stare instancje
            if (enemy.currentInstance != null)
                Destroy(enemy.currentInstance);

            // spawn w zapisanej pozycji startowej
            enemy.currentInstance = Instantiate(enemy.enemyPrefab,
                                                enemy.initialPosition,
                                                enemy.initialRotation);
        }
    }

    /// <summary>
    /// Respawn przy ognisku/checkpoincie
    /// </summary>
    public void RespawnEnemies()
    {
        SpawnAllEnemies();
        Debug.Log($"Enemies respawned in dungeon: {dungeonName}");
    }
}
