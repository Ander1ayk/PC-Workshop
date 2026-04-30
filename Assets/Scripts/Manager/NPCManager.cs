using System.Collections.Generic;
using UnityEngine;

public class NPCManager : MonoBehaviour
{
    [Header("NPC Manager Components")]
    [SerializeField] private List<NPCSpawnData> npcPrefabs;
    [SerializeField] private Transform startPoint;
    [SerializeField] private Transform endPoint;

    private int index = 0;
    private NPC currentNPC;

    private void Start()
    {
        npcPrefabs.Sort((a, b) => a.npcData.npcLevel.CompareTo(b.npcData.npcLevel));
        SpawnNPC();
    }

    private void SpawnNPC()
    {
        if (index >= npcPrefabs.Count)
        {
            Debug.Log(" GAME COMPLETED");
            return;
        }
        GameObject npcObj = Instantiate(
        npcPrefabs[index].npcPrefab,
        startPoint.position,
        Quaternion.identity
    );
        currentNPC = npcObj.GetComponent<NPC>();
        currentNPC.Setup(this, startPoint, endPoint);
    }

    public void NPCFinished()
    {
        index++;
        SpawnNPC();
    }
}
