using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    [Header("Player Stats")]
    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private LayerMask interactableLayer;
    private IInteractable currentInteractable;
    private void Start()
    {
        if (playerStats == null)
        {
            Debug.LogError("PlayerStats is not assigned in the inspector.");
        }
    }
    private void Update()
    {
        if (playerStats == null)
            return;
        if (PlayerStateController.Instance.CurrentState != PlayerState.moving) return;

        Ray ray =  new Ray(cameraTransform.position, cameraTransform.forward);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit, playerStats.interactionRange, interactableLayer))
        {
            if(hit.collider.TryGetComponent(out IInteractable interactable))
            {
                currentInteractable = interactable;
                if (Input.GetKeyDown(KeyCode.E))
                {
                    interactable.Interact();
                    Debug.Log("Interacted with: " + hit.collider.name);
                }
            }
            else
            {
                currentInteractable = null;
            }
        }
        else
        {
            currentInteractable = null;
        }
    }
}
