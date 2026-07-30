using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    private enum EnemyState
    {
        Patrol,
        Chase,
        Search
    }
    [Header("References")]
    [SerializeField] private Transform player;

    [Header("Movement")]
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private float patrolRadius = 6;
    [SerializeField] private float patrolWaitTime = 2f;

    [SerializeField] private float sightDistance = 10f;
    [SerializeField] private float eyeHeight = 1.0f;
    [SerializeField] private LayerMask visionLayers;

    [Header("Search")]
    [SerializeField] private float searchTime = 2f;

    private EnemyState currentState;
    private Vector3 patrolCenter;
    private Vector3 lastKnownPlayerPosition;
    private float patrolTimer;
    private float searchTimer;

    private void Start()
    {
        if (agent == null)
        {
            agent = GetComponent<NavMeshAgent>();
        }
        patrolCenter = transform.position;
        currentState = EnemyState.Patrol;

        ChoosePatrolPoint();
    }
    private void Update()
    {
        switch (currentState)
        {
            case EnemyState.Patrol:
                UpdatePatrol();
                break;

            case EnemyState.Chase:
                UpdateChase();
                break;

            case EnemyState.Search:
                UpdateSearch();
                break;
        }
    }
 
}
