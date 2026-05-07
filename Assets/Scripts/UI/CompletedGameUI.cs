using TMPro;
using UnityEngine;

public class CompletedGameUI : MonoBehaviour
{
    [SerializeField] private GameObject completedGamePanel;
    [SerializeField] private TextMeshProUGUI mistakesCount;
    private void Start()
    {
        completedGamePanel.SetActive(false);
    }
    public void ShowCompletedGameUI()
    {
        PlayerStateController.Instance.SetState(PlayerState.ui);
        completedGamePanel.SetActive(true);
        mistakesCount.text = "Mistakes Made: " + PlayerStateController.Instance.GetMistakesCount();
    }
    private void OnEnable()
    {
        GameEvents.OnGameCompleted += ShowCompletedGameUI;
    }
    private void OnDisable()
    {
        GameEvents.OnGameCompleted -= ShowCompletedGameUI;
    }
}
