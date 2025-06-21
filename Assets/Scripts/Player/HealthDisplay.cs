using UnityEngine;
using TMPro;

public class HealthDisplay : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

[SerializeField] public HealthSystem healthSystem;
[SerializeField] private TextMeshProUGUI healthText;  // Assign in Inspector

    void Update()
    {
        healthText.text = "Health: " + healthSystem.health.ToString();
    }
}
