using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Quest Data", menuName = "ScriptableObjects/Quest Data")]
public class QuestData : ScriptableObject
{
    [Header("Basic Info")]
    public string questId;
    public string questName;
    [TextArea]
    public string questDescription;
    [Header("Progress")]
    public int currentProgress;
    public int requiredProgress;
    [Header("Needed items")]
    public List<ItemsData> requiredItems;
}
