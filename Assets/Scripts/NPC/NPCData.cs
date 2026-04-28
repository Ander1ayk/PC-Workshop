using UnityEngine;
[CreateAssetMenu(fileName = "New NPC Data", menuName = "ScriptableObjects/NPC Data")]
public class NPCData : ScriptableObject
{
    [Header("NPC Data")]
    public string npcName;
    public int npcLevel;
    public float npcSpeed;
    public QuestData npcQuest;
}
