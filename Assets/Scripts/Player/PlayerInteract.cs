using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInteract : MonoBehaviour
{
    [Header("Player Stats")]
    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private LayerMask interactableLayer;
    [SerializeField] private TMPro.TextMeshProUGUI pressToInteract;
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject guidePanel;
    [Header("Show Collected Items")]
    [SerializeField] private GameObject showCollectedItems;
    [SerializeField] private TMPro.TextMeshProUGUI collectedItemsText;
    [SerializeField] private Image collectedItemsImage;
    private bool isPaused = false;
    private bool isGuideOpen = false;

    private float interactionDistance;
    private Coroutine showCollectedRoutine;
    private void Start()
    {
        if (playerStats == null)
        {
            Debug.LogError("PlayerStats is not assigned in the inspector.");
        }
        if (showCollectedItems == null) return;
        showCollectedItems.SetActive(false);
        
        interactionDistance = playerStats.interactionRange;
        isPaused = false;
        isGuideOpen = false;
        PlayerStateController.Instance.SetState(PlayerState.moving);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
            Debug.Log("Pause have to be");
        }
        if(Input.GetKeyDown(KeyCode.H))
        {
            ToggleGuide();
            Debug.Log("Guide have to be");
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            ToggleInventory();
            Debug.Log("Inventory have to be");
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
    public void TogglePause()
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
    private void ToggleGuide()
    {
        isGuideOpen = !isGuideOpen;

        guidePanel.SetActive(isGuideOpen);
        PlayerStateController.Instance.SetState(
            isGuideOpen ? PlayerState.guide : PlayerState.moving
        );
    }
    private void OnEnable()
    {
        GameEvents.OnItemCollectedFromBox += ShowCollectedItem;
    }
    private void OnDisable()
    {
        GameEvents.OnItemCollectedFromBox -= ShowCollectedItem;
    }
    private void ShowCollectedItem(ItemsData items)
    {
        if(showCollectedItems == null || collectedItemsText == null || collectedItemsImage == null)
        {
            Debug.LogError("UI elements for showing collected items are not assigned.");
            return;
        }
        collectedItemsText.text = items.itemName; 
        collectedItemsImage.sprite = items.itemIcon;

        if(showCollectedRoutine != null)
        {
            StopCoroutine(showCollectedRoutine);
        }

        showCollectedRoutine = StartCoroutine(ShowCollectedItemsTemporarily());
    }
    private IEnumerator ShowCollectedItemsTemporarily()
    {
        showCollectedItems.SetActive(true);

        yield return new WaitForSeconds(2f);

        showCollectedItems.SetActive(false);
        showCollectedRoutine = null;
    }
    private void ToggleInventory()
    {
        if(PlayerStateController.Instance.CurrentState == PlayerState.assembling)
        {
            return;
        }
        if (PlayerStateController.Instance.CurrentState == PlayerState.lookInventory)
        {
            PlayerStateController.Instance.SetState(PlayerState.moving);
            UIInventory.Instance.Hide();
        }
        else
        {
            PlayerStateController.Instance.SetState(PlayerState.lookInventory);
            UIInventory.Instance.Show();
        }
    }
}
