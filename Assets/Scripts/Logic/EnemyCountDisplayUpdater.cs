using UnityEngine;
using TMPro;

public class EnemyCountDisplayUpdater : MonoBehaviour
{
    public Transform enemiesContainer;
    public TextMeshProUGUI enemyCounterText;
    public EnemieSpawner enemieSpawner; // Reference to the spawner
    public float checkInterval = 1f;

    private float timer;
    private int lastEnemyCount = -1;

    void Start()
    {
        timer = checkInterval;
        // Try to auto-assign if not set
        if (enemieSpawner == null && enemiesContainer != null)
            enemieSpawner = enemiesContainer.GetComponentInParent<EnemieSpawner>();
    }

    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            UpdateEnemyCounterIfChanged();
            timer = checkInterval;
        }
    }

    private void UpdateEnemyCounterIfChanged()
    {
        if (enemyCounterText == null || enemiesContainer == null || enemieSpawner == null) return;

        int currentCount = enemiesContainer.childCount;
        int maxEnemies = enemieSpawner.maxEnemies;

        if (currentCount != lastEnemyCount)
        {
            enemyCounterText.text = $"{currentCount}/{maxEnemies}";
            lastEnemyCount = currentCount;
        }
    }
}