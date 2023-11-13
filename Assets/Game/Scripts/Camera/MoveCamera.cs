using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private int moveUnit = 2;
    private Vector3 targetPosition;

    private void Start()
    {
        targetPosition = transform.position;
    }

    private void Update()
    {
        HandleInput();
        transform.position = Vector3.Lerp(transform.position, targetPosition, moveSpeed * Time.deltaTime);
    }

    private void HandleInput()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 forwardMovement = transform.forward * (verticalInput * moveUnit);
        Vector3 rightMovement = transform.right * (horizontalInput * moveUnit);

        // Calculate the total movement vector
        Vector3 movement = forwardMovement + rightMovement;

        // Update the target position based on the calculated movement
        targetPosition += movement;
    }
}