using System.Collections.Generic;
using UnityEngine;

public class NPCManager : MonoBehaviour
{
    public static NPCManager Instance { get; private set; }
    [Header("NPC Manager Components")]
    [SerializeField] private List<NPCSpawnData> npcPrefabs;
    [SerializeField] private Transform startPoint;
    [SerializeField] private Transform endPoint;

    private int index = 0;
    private NPC currentNPC;
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        npcPrefabs.Sort((a, b) => a.npcData.npcLevel.CompareTo(b.npcData.npcLevel));
        if (SaveSystem.Instance.HasSaveData())
        {
            SaveSystem.Instance.LoadGame();
        }
        SpawnNPC();
    }

    private void SpawnNPC()
    {
        if (index >= npcPrefabs.Count)
        {
            Debug.Log(" GAME COMPLETED");
            GameEvents.GameCompleted();
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
        SaveSystem.Instance.SaveGame();
        SpawnNPC();
    }
    public void SetLevel(int currentLevel)
    {
        index = currentLevel;
    }
    public int GetCurrentLevel() => index;
}
