using System;
using Unity.VisualScripting;

public static class GameEvents
{
    //Player State
    public static event Action<PlayerState> OnPlayerStateChanged;
    public static void PlayerStateChanged(PlayerState newState)
    {
        OnPlayerStateChanged?.Invoke(newState);
    }
    // Count the number of times the player has made a mistake
    public static event Action OnMadeMistake;

    public static void MadeMistake()
    {
        OnMadeMistake?.Invoke();
    }
    // Inventory
    public static event Action OnInventoryChanged;
    public static event Action<ItemsData, int> OnItemAdded;
    public static void InventoryChanged()
    {
        OnInventoryChanged?.Invoke();
    }
    public static void ItemAdded(ItemsData item, int amount)
    {
        OnItemAdded?.Invoke(item, amount);
    }
    // Quests
    public static event Action <string, int> OnQuestUpdated;
    public static event Action<QuestData> OnQuestStarted;
    public static event Action<QuestData> OnQuestCompleted;
    public static void QuestUpdated(string questId, int progress)
    {
        OnQuestUpdated?.Invoke(questId, progress);
    }
    public static void QuestStarted(QuestData questData)
    {
        OnQuestStarted?.Invoke(questData);
    }
    public static void QuestCompleted(QuestData questData)
    {
        OnQuestCompleted?.Invoke(questData);
    }
    // NPC
    public static event Action<bool> OnNPCInteraction;
    public static void NPCInteraction(bool isInteracting)
    {
        OnNPCInteraction?.Invoke(isInteracting);
    }
    // PC
    public static event Action<PCSlot> OnSlotInstalled;
    public static void SlotInstalled(PCSlot slot)
    {
        OnSlotInstalled?.Invoke(slot);
    }
    public static event Action OnPCCompleted;

    public static void PCCompleted()
    {
        OnPCCompleted?.Invoke();
    }
    // Game
    public static event Action OnGameCompleted;
    public static void GameCompleted()
    {
        OnGameCompleted?.Invoke();
    }
    // Mouse Sensitivity
    public static event Action<float> OnMouseSensitivityChanged;

    public static void MouseSensitivityChanged(float value)
    {
        OnMouseSensitivityChanged?.Invoke(value);
    }
    // Items collected from delivery box
    public static event Action<ItemsData> OnItemCollectedFromBox;
    public static void ItemCollectedFromBox(ItemsData item)
    {
        OnItemCollectedFromBox?.Invoke(item);
    }
}
