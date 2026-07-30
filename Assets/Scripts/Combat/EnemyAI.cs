using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{

    //movement script of infinite fucking evil
    [Header("References")]
    [SerializeField] private Transform player;

    [Header("Movement")]
    [SerializeField] private float moveDistance = 2f;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float moveDelay = 0.5f;

    [Header("Rotation")]
    [SerializeField] private float turnSpeed = 360f;


    [SerializeField] private float positionTolerance = 0.01f;
    [SerializeField] private float rotationTolerance = 0.1f;

    [SerializeField] private float forwardAngle = 0f;
    [SerializeField] private float rightAngle = 90f;
    [SerializeField] private float backwardAngle = 180f;
    [SerializeField] private float leftAngle = 270f;

    private NavMeshAgent agent;
    private Vector3 targetPosition;
    private Quaternion targetRotation;
    private bool isMoving;
    private float moveTimer;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updatePosition = false;
        agent.updateRotation = false;

        targetPosition = transform.position;
        targetRotation = transform.rotation;
        moveTimer = moveDelay;

    }
    private void Update()
    {
        if (isMoving)
        {
            MoveEnemy();
            RotateEnemy();
        }
        else
        {
            moveTimer += Time.deltaTime;
            if (moveTimer >= moveDelay)
            {
                moveTimer = 0f;
                ChooseNextMove();
            }
        }
    }

    private void ChooseNextMove()
    {
        if (player == null)
        {
            return;
        }
        agent.SetDestination(player.position);

        if (agent.path.corners.Length < 2)
        {
            return;
        }
        Vector3 nextPoint = agent.path.corners[1];
        Vector3 direction = nextPoint - transform.position;
        direction.y = 0f;

        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.z))
        {
            if (direction.x > 0f)
            {
                direction = Vector3.right;
            }
            else
            {
                direction = Vector3.left;
            }
        }
        else
        {
            if (direction.z > 0f)
            {
                direction = Vector3.forward;
            }
            else
            {
                direction = Vector3.back;
            }
        }
        targetPosition = transform.position + (direction * moveDistance);
        targetPosition = SnapToGrid(targetPosition);
        targetRotation = Quaternion.LookRotation(direction);
        isMoving = true;
       
    }
    private Vector3 SnapToGrid (Vector3 position)
    {
        float x = Mathf.Round(position.x / moveDistance) * moveDistance;
        float y = position.y;
        float z = Mathf.Round(position.z / moveDistance) * moveDistance;
        return new Vector3(x, y, z);
    }

    private void MoveEnemy()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, targetPosition) <= positionTolerance)
        {
            transform.position = targetPosition;
            agent.Warp(transform.position);
            if (Quaternion.Angle(transform.rotation, targetRotation) <= rotationTolerance)
            {
                isMoving = false;
            }
        }
    }

    private void RotateEnemy()
    {
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);

        if (Quaternion.Angle(transform.rotation, targetRotation) <= rotationTolerance)
        {
            transform.rotation = targetRotation;

            if (Vector3.Distance(transform.position, targetPosition) <= positionTolerance)
            {
                isMoving = false;
            }
        }
    }
}
