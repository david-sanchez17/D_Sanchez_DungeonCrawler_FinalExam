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
    private void UpdatePatrol()
    {
        if (CanSeePlayer())
        {
            currentState = EnemyState.Chase;
            return;
        }

        if (!agent.pathPending)
        {
            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                patrolTimer += Time.deltaTime;

                if (patrolTimer >= patrolWaitTime)
                {
                    patrolTimer = 0f;
                    ChoosePatrolPoint();
                }
            }
        }
    }

    private void UpdateChase()
    {
        if (CanSeePlayer())
        {
            lastKnownPlayerPosition = player.position;
            agent.SetDestination(lastKnownPlayerPosition);
        }
        else
        {
            currentState = EnemyState.Search;
            searchTimer = searchTime;
            agent.SetDestination(lastKnownPlayerPosition);
        }
    }
    private void UpdateSearch()
    {
        if (CanSeePlayer())
        {
            currentState = EnemyState.Chase;
            return;
        }

        if (!agent.pathPending)
        {
            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                searchTimer -= Time.deltaTime;
                if (searchTimer <= 0f)
                {
                    currentState = EnemyState.Patrol;
                    ChoosePatrolPoint();
                }
            }
        }
    }

    private bool CanSeePlayer()
    {
        if (player == null)
        {
            return false;
        }
        Vector3 origin = transform.position;
        origin.y += eyeHeight;
        Vector3 target = player.position;
        target.y += eyeHeight;
        Vector3 direction = target - origin;

        if (direction.magnitude > sightDistance)
        {
            return false;
        }
        RaycastHit hit;
        if (Physics.Raycast(origin, direction.normalized, out hit, sightDistance, visionLayers))
        {
            if (hit.transform == player)
            {
                return true;
            }
        }
        return false;
    }

    private void ChoosePatrolPoint()
    {
        for (int i = 0; i <20; i++)
        {
            Vector2 random = Random.insideUnitCircle * patrolRadius;
            Vector3 point = patrolCenter;
            point.x += random.x;
            point.z += random.y;
            NavMeshHit hit;
            if (NavMesh.SamplePosition(point, out hit, 1.5f, NavMesh.AllAreas))
            {
                agent.SetDestination(hit.position);
                return;
            }
        }
        agent.SetDestination(patrolCenter);
    }
 
}
