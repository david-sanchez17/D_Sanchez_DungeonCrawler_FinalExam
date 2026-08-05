using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CombatLogger : MonoBehaviour
{
    [SerializeField] private CombatManager combatManager;
    [SerializeField] private TextMeshProUGUI combatLogText;

    private Queue<string> messages = new Queue<string>();
    private const int MaxMessages = 5;

    private void OnEnable()
    {
        if (combatManager !=null)
        {
            combatManager.OnCombatLog += AddLog;
        }
    }

    private void OnDisable()
    {
        if (combatManager != null)
        {
            combatManager.OnCombatLog -= AddLog;
        }
    }

    private void OnDestroy()
    {
        if (combatManager != null)
        {
            combatManager.OnCombatLog -= AddLog;
        }
    }

    private void AddLog(string message)
    {
        if (messages.Count >= MaxMessages)
        {
            messages.Clear();
        }
        messages.Enqueue(message);
        UpdateCombatLog();
    }
    
    private void UpdateCombatLog()
    {
        combatLogText.text = "";
        foreach(string message in messages)
        {
            combatLogText.text += message + "\n";
        }
    }


}
