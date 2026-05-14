using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    [Header("Player Stats")]
    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private LayerMask interactableLayer;
    [SerializeField] private TMPro.TextMeshProUGUI pressToInteract;
    [SerializeField] private GameObject pausePanel;
    private bool isPaused = false;

    private float interactionDistance;
    private void Start()
    {
        if (playerStats == null)
        {
            Debug.LogError("PlayerStats is not assigned in the inspector.");
        }
        interactionDistance = playerStats.interactionRange;
        isPaused = false;
        PlayerStateController.Instance.SetState(PlayerState.moving);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            TogglePause();
            Debug.Log("Pause have to be");
        }
        if (Input.GetKey(KeyCode.LeftControl))
        {
            interactionDistance = playerStats.interactionRange * 2f;
        }
        else
        {
            interactionDistance = playerStats.interactionRange;
        }
        if (playerStats == null)
            return;
        if (PlayerStateController.Instance.CurrentState == PlayerState.ui) return;

        bool canShowInteract = false;
        RaycastHit hit;

        if (PlayerStateController.Instance.CurrentState == PlayerState.moving)
        {
            Ray ray = new Ray(cameraTransform.position, cameraTransform.forward);

            if (!Physics.Raycast(ray, out hit, interactionDistance, interactableLayer))
            {
                pressToInteract.gameObject.SetActive(false);
                return;
            }

            if (hit.collider.TryGetComponent(out IInteractable interactable))
            {
                canShowInteract = true;

                if (Input.GetKeyDown(KeyCode.E))
                {
                    interactable.Interact();
                }
            }
        }
        else if (PlayerStateController.Instance.CurrentState == PlayerState.assembling)
        {
            Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            
            if (!Physics.Raycast(mouseRay, out hit, interactionDistance, interactableLayer))
            {
                pressToInteract.gameObject.SetActive(false);
                return;
            }
            Debug.Log("Hit: " + hit.collider.name);
            if (hit.collider.TryGetComponent(out IPCInteractable pcInteractable))
            {
                canShowInteract = true;

                if (Input.GetKeyDown(KeyCode.E))
                {
                    pcInteractable.InteractWithPC();
                }
            }
        }
        pressToInteract.gameObject.SetActive(canShowInteract);
    }
    private void OnDrawGizmos()
    {
        if (cameraTransform == null)
            return;
        Gizmos.color = Color.green;
        Gizmos.DrawRay(cameraTransform.position, cameraTransform.forward * interactionDistance);
    }
    private void TogglePause()
    {
        isPaused = !isPaused;

        pausePanel.SetActive(isPaused);

        PlayerStateController.Instance.SetState(
            isPaused ? PlayerState.pause : PlayerState.moving
        );

        Time.timeScale = isPaused ? 0f : 1f;

        Cursor.lockState = isPaused
            ? CursorLockMode.None
            : CursorLockMode.Locked;

        Cursor.visible = isPaused;
    }
}
