using System.Collections;
using UnityEngine;

public class PCRotate : MonoBehaviour, IInteractable
{
    [Header("Player Stats")]
    [SerializeField] private PlayerStats playerStats;
    public GameObject[] detailsPC;
    public GameObject[] pcPanels;
    [SerializeField] private AudioClip completedSound;
    [SerializeField] private Collider ñollider;
    private float xRotation = 0f;
    private float yRotation = 0f;

    private Vector3 startedPosition;
    private Quaternion startRotation;

    private Coroutine destroyRoutine;
    private bool isDestroying = false;
    private bool isCompleted = false;

    private int installedCount;
    private int totalCount;
    public static PCRotate CurrentPC;
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
        CurrentPC = this;
        PlayerStateController.Instance.SetState(PlayerState.assembling);

        if (ñollider != null)
        {
            ñollider.enabled = false;
        }

        Camera cam = Camera.main;

        transform.position = cam.transform.position
                           + cam.transform.forward * 5f
                           + cam.transform.right * 0.3f
                           - cam.transform.up * 1.5f;

        transform.rotation = Quaternion.LookRotation(-cam.transform.forward);

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
        foreach (var panel in pcPanels)
        {
            panel.SetActive(false);
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
    }
    public void ExitAssembly()
    {
        if (CurrentPC != this) return;
        CurrentPC = null;
        PlayerStateController.Instance.SetState(PlayerState.moving);

        if(ñollider != null)
        {
            ñollider.enabled = true;
            Debug.Log("Collider enabled");
        }

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
        foreach (var panel in pcPanels)
        {
            panel.SetActive(true);
        }
    }
   
    private IEnumerator DestroyPCAfterDelay()
    {
        yield return new WaitForSeconds(1f);

        PlayerStateController.Instance.SetState(PlayerState.moving);
        AudioManager.Instance.PlaySFX(completedSound, 1f);

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
