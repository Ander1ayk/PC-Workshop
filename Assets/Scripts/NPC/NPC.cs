using UnityEngine;

public class NPC : MonoBehaviour, IInteractable
{
    [Header("NPC Components")]
    [SerializeField] private NPCData npcData;
    [SerializeField] private Animator npcAnimator;
    [SerializeField] private GameObject startPoint;
    [SerializeField] private GameObject endPoint;

    private Transform currentTarget;
    private bool isWaiting = false;
    private bool isCompletedQuest = false;
    private bool currentIsWalking = false;

    private bool questTaken = false;
    private NPCManager manager;

    public NPCData dataNPC => npcData;
    private void Start()
    {
        if (npcData == null)
        {
            Debug.LogError("NPC Data is not assigned for " + gameObject.name);
            return;
        }
        if (npcAnimator == null)
        {
            Debug.LogError("NPC Animator is not assigned for " + gameObject.name);
            return;
        }
        if (startPoint == null || endPoint == null)
        {
            Debug.LogError("Start Point or End Point is not assigned for " + gameObject.name);
            return;
        }
        currentTarget = endPoint.transform;
    }

    public void Interact()
    {
        if (isWaiting && !questTaken)
        {
            questTaken = true;
            // Show quest details or dialogue here
            Debug.Log("Interacting with " + npcData.npcName);
            QuestManager.Instance.StartQuest(npcData.npcQuest);
        }
    }

    private void Update()
    {
        if (currentTarget == null) return;

        MoveToTarget();

        if (Vector3.Distance(transform.position, endPoint.transform.position) < 0.1f && !isWaiting)
        {
            transform.position = endPoint.transform.position;

            isWaiting = true;
            npcAnimator.SetTrigger("IsWaiting");
        }
        if ((Vector3.Distance(transform.position, startPoint.transform.position) < 0.1f) && isCompletedQuest)
        {
            manager.NPCFinished();
            Destroy(gameObject);
        }
    }
    private void MoveToTarget()
    {
        Vector3 direction = (currentTarget.position - transform.position).normalized;

        transform.position += direction * npcData.npcSpeed * Time.deltaTime;

        transform.LookAt(currentTarget);
    }
    private void OnEnable()
    {
        GameEvents.OnQuestCompleted += HandleQuestCompleted;
    }
    private void OnDisable()
    {
        GameEvents.OnQuestCompleted -= HandleQuestCompleted;
    }
    private void HandleQuestCompleted(QuestData questData)
    {
        if (questData == npcData.npcQuest)
        {
            isCompletedQuest = true;
            isWaiting = false;

            npcAnimator.SetTrigger("IsWalk");
            currentTarget = startPoint.transform;
        }
    }
    public void Setup(NPCManager manager, Transform start, Transform end)
    {
        this.manager = manager;
        startPoint = start.gameObject;
        endPoint = end.gameObject;

        currentTarget = endPoint.transform;
    }
}
