using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public static QuestManager Instance { get; private set; }
    private QuestData activeQuest;
    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void StartQuest(QuestData questData)
    {
        activeQuest = questData;
        activeQuest.currentProgress = 0;
        GameEvents.QuestStarted(questData);
    }
    public void UpdateQuestProgress(string questId, int progress)
    {
        if (activeQuest == null) return;

        foreach(var item in activeQuest.requiredItems)
        {
            if(item.itemId == questId)
            {
                activeQuest.currentProgress += progress;

                GameEvents.QuestUpdated(questId, activeQuest.currentProgress);

                if (activeQuest.currentProgress >= activeQuest.requiredProgress)
                {
                    CompleteQuest(activeQuest);
                }
                return;
            }
        }
    }
    public void CompleteQuest(QuestData questData)
    {
        GameEvents.QuestCompleted(questData);
    }
    private void OnEnable()
    {
        GameEvents.OnQuestStarted += HandleQuestStarted;
        GameEvents.OnQuestUpdated += HandleQuestUpdated;
        GameEvents.OnQuestCompleted += HandleQuestCompleted;
    }
    private void OnDisable()
    {
        GameEvents.OnQuestStarted -= HandleQuestStarted;
        GameEvents.OnQuestUpdated -= HandleQuestUpdated;
        GameEvents.OnQuestCompleted -= HandleQuestCompleted;
    }
    private void HandleQuestStarted(QuestData questData)
    {
        Debug.Log($"Quest Started: {questData.questName}");
    }
    private void HandleQuestUpdated(string questId, int progress)
    {
        Debug.Log($"Quest Updated: {questId}, Progress: {progress}");
    }

    private void HandleQuestCompleted(QuestData questData)
    {
        Debug.Log($"Quest Completed: {questData.questName}");
    }
}
