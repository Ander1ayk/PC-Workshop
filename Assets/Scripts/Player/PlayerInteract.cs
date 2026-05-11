using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    [Header("Player Stats")]
    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private LayerMask interactableLayer;
    [SerializeField] private TMPro.TextMeshProUGUI pressToInteract;
    [SerializeField] private GameObject pausePanel;
    private void Start()
    {
        if (playerStats == null)
        {
            Debug.LogError("PlayerStats is not assigned in the inspector.");
        }
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.T) && PlayerStateController.Instance.GetCurrentState() == PlayerState.moving)
        {
            TogglePause();
        }

        if (playerStats == null)
            return;
        if (PlayerStateController.Instance.CurrentState == PlayerState.ui) return;

        Ray ray = new Ray(cameraTransform.position, cameraTransform.forward);
        RaycastHit hit;

        if (!Physics.Raycast(ray, out hit, playerStats.interactionRange, interactableLayer))
        {
            pressToInteract.gameObject.SetActive(false);
            return;
        }

        bool canShowInteract = false;
        if (PlayerStateController.Instance.CurrentState == PlayerState.moving)
        {
            if (hit.collider.TryGetComponent(out IInteractable interactable))
            {
                canShowInteract = true;
                if (Input.GetKeyDown(KeyCode.E))
                {
                    interactable.Interact();
                    Debug.Log("Interacted with: " + hit.collider.name);
                }
            }
        }
        else if (PlayerStateController.Instance.CurrentState == PlayerState.assembling)
        {

            if (hit.collider.TryGetComponent(out IPCInteractable pcInteractable))
            {
                canShowInteract = true;
                if (Input.GetKeyDown(KeyCode.E))
                {
                    pcInteractable.InteractWithPC();
                    Debug.Log("Interacted with: " + hit.collider.name);
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
        Gizmos.DrawRay(cameraTransform.position, cameraTransform.forward * playerStats.interactionRange);
    }
    private void TogglePause()
    {
        pausePanel.SetActive(!pausePanel);
        Time.timeScale = pausePanel.activeSelf ? 0 : 1;
        Cursor.lockState = pausePanel.activeSelf ? CursorLockMode.None : CursorLockMode.Locked;
    }
}
