using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Player Stats")]
    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private Transform cameraTransform;
    private CharacterController characterController;
    private float horizontalInput;
    private float verticalInput;

    private float xRotation = 0f;

    private void Start()
    {
        if (playerStats == null)
        {
            Debug.LogError("PlayerStats is not assigned in the inspector.");
        }
        characterController = GetComponent<CharacterController>();
        if (characterController == null)
        {
            Debug.LogError("CharacterController component is missing on the player.");
        }
    }
    private void Update()
    {
        if (playerStats == null || characterController == null)
            return;
        if(PlayerStateController.Instance.CurrentState != PlayerState.moving)
            return;
        MovingMouse();
        MovingPlayer();
    }
    
    private void MovingPlayer()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        Vector3 moveDirection = transform.forward * verticalInput + transform.right * horizontalInput;
        moveDirection *= playerStats.speed * Time.deltaTime;

        characterController.Move(moveDirection);
    }
    private void MovingMouse()
    {
        float mouseX = Input.GetAxis("Mouse X") * playerStats.mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * playerStats.mouseSensitivity;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        cameraTransform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        transform.Rotate(Vector3.up * mouseX);
    }
    private void OnEnable()
    {
        GameEvents.OnPlayerStateChanged += HandleState;
    }

    private void OnDisable()
    {
        GameEvents.OnPlayerStateChanged -= HandleState;
    }

    private void HandleState(PlayerState state)
    {
        if (state == PlayerState.moving)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}
