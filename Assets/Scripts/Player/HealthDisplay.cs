using UnityEngine;
using TMPro;

public class HealthDisplay : MonoBehaviour
{
    public HealthSystem playerHealth;
    public TextMeshProUGUI healthText;

    void Update()
    {
        healthText.text = "Health: " + playerHealth.CurrentHealth.ToString("F0");
    }
}
