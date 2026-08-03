using TMPro;
using UnityEngine;

public class CombatLogger : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private CombatManager combatManager;
    [SerializeField] private TextMeshProUGUI combatLogText;
    private string logText = "";

    private void OnEnable()
    {
        if (combatManager != null)
        {
            combatManager.OnTurnChanged += LogTurnChange;
        }
    }

    private void OnDisable()
    {
        if (combatManager != null)
        {
            combatManager.OnTurnChanged -= LogTurnChange;
        }
    }

    private void OnDestroy()
    {
        if (combatManager !=null)
        {
            combatManager.OnTurnChanged -= LogTurnChange
        }
    }
    void Start()
    {
        
    }
    void Update()
    {
        
    }
}
