using UnityEngine;

public class EnemieSpawner : MonoBehaviour
{
    [Header("Spawner Settings")]


    public float spawnInterval = 2f;
    public float spawnRadius = 5f;
    public float minSpawnDistance = 2f;
    public int maxEnemies = 10;

    private Transform[] spawnPoints;
    private float timer;
    private bool isSpawning = true;
    private int enemiesSpawned = 0;

    void Awake()
    {
        spawnPoints = GetComponentsInChildren<Transform>();
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
            //TODO: Check if max enemies reached
            // Spawn an enemy

            timer = spawnInterval;
        }
    }

    void SpawnEnemy()
    {

        int index = Random.Range(0, spawnPoints.Length);
        Transform spawnPoint = spawnPoints[index];

        Vector2 randomCircle = Random.insideUnitCircle.normalized * Random.Range(minSpawnDistance, spawnRadius);
        Vector3 spawnPos = spawnPoint.position + new Vector3(randomCircle.x, 0f, randomCircle.y);

        //TODO: Instantiate

        enemiesSpawned++;
    }

}