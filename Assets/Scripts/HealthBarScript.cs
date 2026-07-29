using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;

public class HealthBarScript : MonoBehaviour
{
    [SerializeField] private Slider healthBarSlider;
    [SerializeField] private TextMeshProUGUI healthBarValueText;

    [Header("Enemy")]
    [SerializeField] private EnemyCombatController enemy;

    public void Initialize(EnemyCombatController enemyController)
    {
        enemy = enemyController;
        UpdateHealthBar();
    }

    public void UpdateHealthBar()
    {
        if (enemy == null)
        {
            return;
        }

        healthBarSlider.maxValue = enemy.GetMaxHealth();
        healthBarSlider.value = enemy.GetHealth();
        healthBarValueText.text = enemy.GetHealth().ToString() + "/" + enemy.GetMaxHealth().ToString();
    }
}
