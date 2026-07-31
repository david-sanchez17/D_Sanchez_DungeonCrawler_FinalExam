using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveDistance = 2f;
    [SerializeField] private float moveSpeed = 5f;

    [Header("Rotation")]
    [SerializeField] private float turnAngle = 90f;
    [SerializeField] private float turnSpeed = 360f;

    [Header("Collisions")]
    [SerializeField] private LayerMask wallLayer;
    [SerializeField] private Vector3 boxHalfExtents = new Vector3(0.4f, 0.9f, 0.4f);

    private bool isMoving = false;

    private Vector3 targetPosition;
    private Quaternion targetRotation;

    private void Start()
    {
        if (BattleTransitionManager.Instance != null && BattleTransitionManager.Instance.HasReturnPoint() && BattleTransitionManager.Instance.IsReturningFromBattle())
        {
            Vector3 returnPosition = BattleTransitionManager.Instance.GetReturnPosition();
            returnPosition.y = 1f;
            transform.position = returnPosition;
            targetPosition = transform.position;
            targetRotation = transform.rotation;
            isMoving = false;
            BattleTransitionManager.Instance.ClearReturnPoint();
        }
        else
        {
            targetPosition = transform.position;
            targetRotation = transform.rotation;
        }
    }

    private void Update()
    {
        if (!isMoving)
        {
            HandleInput();
        }
        MovePlayer();
        RotatePlayer();
    }

    private void HandleInput()
    {
        if (Input.GetButtonDown("Forward"))
        {
            TryMove(transform.forward);
        }
        else if (Input.GetButtonDown("Backward"))
        {
            TryMove(-transform.forward);
        }
        else if (Input.GetButtonDown("StrafeLeft"))
        {
            TryMove(-transform.right);
        }
        else if (Input.GetButtonDown("StrafeRight"))
        {
            TryMove(transform.right);
        }
        else if (Input.GetButtonDown("TurnLeft"))
        {
            targetRotation *= Quaternion.Euler(0f, -turnAngle, 0f);
            isMoving = true;
        }
        else if (Input.GetButtonDown("TurnRight"))
        {
            targetRotation *= Quaternion.Euler(0f, turnAngle, 0f);
            isMoving = true;
        }
    }

    private void MovePlayer()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        if (Vector3.Distance(transform.position, targetPosition) < 0.01f)
        {
            transform.position = targetPosition;
            if (Quaternion.Angle(transform.rotation, targetRotation) < 0.1f)
                isMoving = false;
        }
    }

    private void RotatePlayer()
    {
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);

        if (Quaternion.Angle(transform.rotation, targetRotation) < 0.1f)
        {
            transform.rotation = targetRotation;
            if (Vector3.Distance(transform.position, targetPosition) < 0.01f)
                isMoving = false;
        }
    }

    private void TryMove(Vector3 direction)
    {
        Vector3 destination = transform.position + direction * moveDistance;

        Collider[] hitObjects = Physics.OverlapBox(
            destination,
            boxHalfExtents,
            Quaternion.identity,
            wallLayer);


        if (hitObjects.Length == 0)
        {
            targetPosition = destination;
            isMoving = true;
        }
        else
        {
            Debug.Log("Cannot move. Wall detected.");
        }
    }
}
