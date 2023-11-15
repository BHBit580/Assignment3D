using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private int moveUnit = 2;
    [SerializeField] private float minX, maxX, minZ, maxZ;
    [SerializeField] UIVirtualJoystick virtualJoystick;
    
    private Vector3 targetPosition;

    private void Start() => targetPosition = transform.position;
    

    private void Update()
    {
        HandleKeyBoardInput();
        ClampCameraPosition(); 
        Vector3 movement = Vector3.Lerp(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        transform.position = new Vector3(movement.x, transform.position.y, movement.z);
    }

    private void HandleKeyBoardInput()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 forwardMovement = transform.forward * (verticalInput * moveUnit);
        Vector3 rightMovement = transform.right * (horizontalInput * moveUnit);

        Vector3 movement = forwardMovement + rightMovement;

        targetPosition += movement;
    }
    
    public void HandleJoystickInput(Vector2 joystickInput)
    {
        Vector3 forwardMovement = transform.forward * (joystickInput.y * moveUnit);
        Vector3 rightMovement = transform.right * (joystickInput.x * moveUnit);

        Vector3 movement = forwardMovement + rightMovement;

        targetPosition += movement;
    }
    
    private void ClampCameraPosition()
    {
        targetPosition = new Vector3(
            Mathf.Clamp(targetPosition.x, minX, maxX),
            targetPosition.y,
            Mathf.Clamp(targetPosition.z, minZ, maxZ)
        );
    }
}