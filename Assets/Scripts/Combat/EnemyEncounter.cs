using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AI;
public class EnemyEncounter : MonoBehaviour
{
    [Header("Enemy Settings")]
    [SerializeField] private int enemyID;
    [SerializeField] private string combatSceneName = "CombatScene";


    [Header("Target")]
    [SerializeField] private Transform target;

    private NavMeshAgent agent;
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
    }

    private void Start()
    {
        if (BattleTransitionManager.Instance != null)
        {
            if (BattleTransitionManager.Instance.IsEnemyDefeated(enemyID))
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (encounterStarted)
        {
            return;
        }
        if (!other.CompareTag("Player"))
        {
            return;
        }
        encounterStarted = true;
        Vector3 returnPosition = other.transform.position;
        returnPosition.y = 1f;
        returnPosition -= other.transform.forward * 1f;
        BattleTransitionManager.Instance.SaveReturnPoint(SceneManager.GetActiveScene().name, returnPosition);

        BattleTransitionManager.Instance.SetCurrentEnemy(enemyID);
        Debug.Log("enemy encountered! ID: " + enemyID);
        LoadCombatScene();
    }

   private void LoadCombatScene()
    {
        SceneManager.LoadScene(combatSceneName);
    }
}
