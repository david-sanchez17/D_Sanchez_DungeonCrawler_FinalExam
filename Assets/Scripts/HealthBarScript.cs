using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;

public class HealthBarScript : MonoBehaviour
{
    [SerializeField] private Slider healthBarSlider;
    [SerializeField] private TextMeshProUGUI healthBarValueText;

    private IHealthInterface healthTarget;

    public void Initialize(IHealthInterface target)
    {
        healthTarget = target;
        UpdateHealthBar();
    }

    public void UpdateHealthBar()
    {
        if (healthTarget == null)
        {
            return;
        }

        healthBarSlider.maxValue = healthTarget.GetMaxHealth();
        healthBarSlider.value = healthTarget.GetHealth();
        healthBarValueText.text = healthTarget.GetHealth().ToString() + "/" + healthTarget.GetMaxHealth().ToString();
    }
}
