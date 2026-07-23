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

    private bool isMoving = false;

    private Vector3 targetPosition;
    private Quaternion targetRotation;

    private void Start()
    {
        targetPosition = transform.position;
        targetRotation = transform.rotation;
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
            targetPosition += transform.forward * moveDistance;
            isMoving = true;
        }
        else if (Input.GetButtonDown("Backward"))
        {
            targetPosition -= transform.forward * moveDistance;
            isMoving = true;
        }
        else if (Input.GetButtonDown("StrafeLeft"))
        {
            targetPosition -= transform.right * moveDistance;
            isMoving = true;
        }
        else if (Input.GetButtonDown("StrafeRight"))
        {
            targetPosition += transform.right * moveDistance;
            isMoving = true;
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
}
