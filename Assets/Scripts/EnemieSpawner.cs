using UnityEngine;

public class EnemieSpawner : MonoBehaviour
{
    [Header("Spawner Settings")]
    public GameObject enemyPrefab;
    public float spawnInterval = 2f;
    public Transform enemiesContainer;
    public float spawnRadius = 5f;
    public float minSpawnDistance = 2f;
    public int maxEnemies = 10;

    private Transform[] spawnPoints;
    private float timer;
    private bool isSpawning = true;
    private int currentEnemyCount = 0;

    void Awake()
    {
        // Get all child transforms except self as spawn points
        spawnPoints = GetComponentsInChildren<Transform>();
        // Remove self from the array
        spawnPoints = System.Array.FindAll(spawnPoints, t => t != this.transform);
    }

    void Start()
    {
        timer = spawnInterval;
    }

    void Update()
    {
        if (!isSpawning) return;

        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            if (currentEnemyCount < maxEnemies)
            {
                SpawnEnemy();
            }
            timer = spawnInterval;
        }
    }

    void SpawnEnemy()
    {
        if (enemyPrefab == null || spawnPoints.Length == 0)
            return;

        int index = Random.Range(0, spawnPoints.Length);
        Transform spawnPoint = spawnPoints[index];

        Vector2 randomCircle = Random.insideUnitCircle.normalized * Random.Range(minSpawnDistance, spawnRadius);
        Vector3 spawnPos = spawnPoint.position + new Vector3(randomCircle.x, 0f, randomCircle.y);

        GameObject enemy = Instantiate(enemyPrefab, spawnPos, spawnPoint.rotation);
        if (enemiesContainer != null)
        {
            enemy.transform.SetParent(enemiesContainer);
        }

        currentEnemyCount++;

    }

    private void HandleEnemyDespawned()
    {
        currentEnemyCount = Mathf.Max(0, currentEnemyCount - 1);
    }

    public void StartSpawning()
    {
        isSpawning = true;
    }

    public void StopSpawning()
    {
        isSpawning = false;
    }

    public void SetSpawnInterval(float interval)
    {
        spawnInterval = interval;
        timer = interval;
    }

    public void ResetSpawner()
    {
        timer = spawnInterval;
    }
}