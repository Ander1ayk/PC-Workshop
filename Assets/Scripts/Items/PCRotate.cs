using System.Collections;
using UnityEngine;

public class PCRotate : MonoBehaviour, IInteractable
{
    [Header("Player Stats")]
    [SerializeField] private PlayerStats playerStats;
    public GameObject[] detailsPC;
    private float xRotation = 0f;
    private float yRotation = 0f;

    private Vector3 startedPosition;
    private Quaternion startRotation;

    private Coroutine destroyRoutine;
    private bool isDestroying = false;
    private bool isCompleted = false;

    private int installedCount;
    private int totalCount;
    private void Awake()
    {
        totalCount = detailsPC.Length;
    }
    private void Start()
    {
        if (playerStats == null)
        {
            Debug.LogError("PlayerStats is not assigned in the inspector.");
        }
        startedPosition = transform.position;
        startRotation = transform.rotation;
    }
    public void Interact()
    {
        PlayerStateController.Instance.SetState(PlayerState.assembling);

        Cursor.lockState = CursorLockMode.Locked;
        foreach (var detail in detailsPC)
        {
            MeshRenderer meshRenderer = detail.GetComponent<MeshRenderer>();
            if (detail.GetComponent<PCSlot>().IsInstalled())
            {
                detail.gameObject.SetActive(true);
                meshRenderer.enabled = true;
            }
            else
            {
                detail.gameObject.SetActive(true);
                meshRenderer.enabled = false;
            }
        }
    }
    private void Update()
    {
        if (playerStats == null)
            return;
        if (PlayerStateController.Instance == null)
            return;
        if (PlayerStateController.Instance.CurrentState != PlayerState.assembling)
            return;
        if (Input.GetMouseButtonDown(1))
        {
            ExitAssembly();
        }
        MovingMouse();
    }
    private void ExitAssembly()
    {
        PlayerStateController.Instance.SetState(PlayerState.moving);

        transform.position = startedPosition;
        transform.rotation = startRotation;

        foreach (var detail in detailsPC)
        {
            MeshRenderer meshRenderer = detail.GetComponent<MeshRenderer>();
            if (detail.GetComponent<PCSlot>().IsInstalled())
            {
                detail.gameObject.SetActive(true);
                meshRenderer.enabled = true;
            }
            else
            {
                detail.gameObject.SetActive(false);
                meshRenderer.enabled = false;
            }
        }
    }
    private void MovingMouse()
    {
        float mouseX = Input.GetAxis("Mouse X") * playerStats.mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * playerStats.mouseSensitivity;

        transform.Rotate(Vector3.up, mouseX, Space.World);
        transform.Rotate(Vector3.right, -mouseY, Space.World);
    }
   
    private IEnumerator DestroyPCAfterDelay()
    {
        yield return new WaitForSeconds(5f);

        PlayerStateController.Instance.SetState(PlayerState.moving);

        Destroy(gameObject);
    }
    private void OnEnable()
    {
        GameEvents.OnSlotInstalled += OnSlot;
    }

    private void OnDisable()
    {
        GameEvents.OnSlotInstalled -= OnSlot;
    }
    private void OnSlot(PCSlot slot)
    {
        installedCount++;

        if (installedCount >= totalCount)
        {
            GameEvents.PCCompleted();
            StartCoroutine(DestroyPCAfterDelay());
        }
    }
}
