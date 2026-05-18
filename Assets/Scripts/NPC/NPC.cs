using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour, IInteractable
{
    [Header("NPC Components")]
    [SerializeField] private NPCData npcData;
    [SerializeField] private Animator npcAnimator;
    [SerializeField] private GameObject startPoint;
    [SerializeField] private GameObject endPoint;

    [SerializeField] private AudioClip npcVoice;

    private Transform currentTarget;
    private bool isWaiting = false;
    private bool isCompletedQuest = false;
    private bool currentIsWalking = false;
    private bool isLeaving = false;
    private bool canLeave = false;

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
            AudioManager.Instance.PlaySFX(npcVoice, 1f, transform.position);
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
        if (isCompletedQuest)
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
        GameEvents.OnPCCompleted += HandlePCDone;
    }
    private void OnDisable()
    {
        GameEvents.OnPCCompleted -= HandlePCDone;
    }
    IEnumerator WaitAndLeave()
    {
        isLeaving = true;

        yield return new WaitForSeconds(5f);

        isCompletedQuest = true;
        isWaiting = false;

        npcAnimator.SetTrigger("IsWalk");
        currentTarget = startPoint.transform;
    }
    private void HandlePCDone()
    {
        if (isLeaving) return;

        StartCoroutine(WaitAndLeave());
    }
    public void Setup(NPCManager manager, Transform start, Transform end)
    {
        this.manager = manager;
        startPoint = start.gameObject;
        endPoint = end.gameObject;

        currentTarget = endPoint.transform;
    }
}
