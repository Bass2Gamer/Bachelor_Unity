using System.Collections;
using UnityEngine;
using TMPro;  

public class Game : MonoBehaviour
{
    [Header("Core")]    [Header("References")]
    [SerializeField] private Transform enemiesContainer; 
    [SerializeField] private TextMeshProUGUI enemyCounterText;
    [SerializeField] private int maxEnemies = 10;    

    private const float TICK_RATE = 1f / 60f;
    private WaitForSeconds tickDelay;
    private int lastEnemyCount = -1;

    private void Awake()
    {
        Time.fixedDeltaTime = TICK_RATE;  
        tickDelay = new WaitForSeconds(TICK_RATE);
        StartCoroutine(GameLoop());
    }
    private IEnumerator GameLoop()
    {
        while (true)
        {
            Tick();
            yield return tickDelay;
        }
    }

    // Runs every tick
    private void Tick()
    {
        UpdateEnemyCounterIfChanged();

    }
    private void UpdateEnemyCounterIfChanged()
    {
        if (!enemyCounterText || !enemiesContainer) return;

        int current = enemiesContainer.childCount;
        if (current != lastEnemyCount)
        {
            enemyCounterText.text = $"{current}/{maxEnemies}";
            lastEnemyCount = current;
        }
    }
}
