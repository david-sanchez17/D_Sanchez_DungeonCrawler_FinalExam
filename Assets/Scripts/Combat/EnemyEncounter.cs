using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AI;
public class EnemyEncounter : MonoBehaviour
{
    [SerializeField] private Transform target;

    private NavMeshAgent agent;
    private Vector3 targetPosition;
    private Quaternion targetRotation;



    private string combatSceneName = "CombatScene";
    private bool encounterStarted = false;

    void Awake()
    {
        if(target == null)
        {
            GameObject player = GameObject.FindWithTag("Player");
            if(player != null)
            {
                target = player.transform;
            }
        }
        agent = GetComponent<NavMeshAgent>();
        agent.SetDestination(target.position);
    }



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
