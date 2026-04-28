using UnityEngine;
using UnityEngine.UI;

public class QuestUI : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private GameObject questPanel;
    [SerializeField] private TMPro.TextMeshProUGUI questTitleText;
    [SerializeField] private TMPro.TextMeshProUGUI questDescriptionText;
    [SerializeField] private Image questProgressBar;
    private QuestData currentQuest;
    private void OnEnable()
    {
        GameEvents.OnQuestStarted += ShowQuest;
        GameEvents.OnQuestUpdated += UpdateQuestProgress;
        GameEvents.OnQuestCompleted += HandleQuestCompleted;
    }
    private void OnDisable()
    {
        GameEvents.OnQuestStarted -= ShowQuest;
        GameEvents.OnQuestUpdated -= UpdateQuestProgress;
        GameEvents.OnQuestCompleted -= HandleQuestCompleted;
    }
    private void ShowQuest(QuestData quest)
    {
        currentQuest = quest;

        questPanel.SetActive(true);

        questTitleText.text = quest.questName;
        questDescriptionText.text = quest.questDescription;

        questProgressBar.fillAmount = (float)quest.currentProgress / quest.requiredProgress;
    }
    private void UpdateQuestProgress(string questId, int progress)
    {
        if (currentQuest == null) return;
        if (currentQuest.questId != questId) return;

        questProgressBar.fillAmount = (float)progress / currentQuest.requiredProgress;
    }
    private void HandleQuestCompleted(QuestData quest)
    {
        if (currentQuest == null) return;
        if (currentQuest.questId != quest.questId) return;
        questPanel.SetActive(false);
        currentQuest = null;
    }
}
