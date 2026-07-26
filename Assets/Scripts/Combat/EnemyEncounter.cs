using UnityEngine;
using UnityEngine.SceneManagement;
public class EnemyEncounter : MonoBehaviour
{
    private string combatSceneName = "CombatScene";
    private bool encounterStarted = false;

    private void OnTriggerEnter(Collider other)
    {
        if (encounterStarted)
            return;

        if (other.CompareTag("Player"))
        {
            encounterStarted = true;
            Debug.Log("Enemy encountered!");

            LoadCombatScene();
        }
    }

   private void LoadCombatScene()
    {
        SceneManager.LoadScene(combatSceneName);
    }
}
