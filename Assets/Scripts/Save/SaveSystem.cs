using System.IO;
using UnityEngine;

public class SaveSystem : MonoBehaviour
{
    public static SaveSystem Instance { get; private set; }
    private string saveFilePath;
    private bool isGameCompleted;
    private void Awake()
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
        saveFilePath = Path.Combine(Application.persistentDataPath, "savefile.json");
    }
    public void SaveGame()
    {
        SaveData saveData = new SaveData();

        saveData.currentLevel = NPCManager.Instance.GetCurrentLevel();
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            saveData.playerPosition = player.transform.position;
            saveData.playerRotation = player.transform.eulerAngles;
        }
        saveData.inventorySlots = Inventory.Instance.slots;
        saveData.isGameCompleted = isGameCompleted;
        string json = JsonUtility.ToJson(saveData);
        saveFilePath = Path.Combine(Application.persistentDataPath, "savefile.json");
        File.WriteAllText(saveFilePath, json);
    }
    public void LoadGame()
    {
        if(!File.Exists(saveFilePath))
        {
            Debug.LogWarning("No save file found!");
            return;
        }
        string json = File.ReadAllText(saveFilePath);
        SaveData saveData = JsonUtility.FromJson<SaveData>(json);

        NPCManager.Instance.SetLevel(saveData.currentLevel);
        Inventory.Instance.SetInventoryFromSave(saveData.inventorySlots);
        isGameCompleted = saveData.isGameCompleted;
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if(player != null)
        {
            player.transform.position = saveData.playerPosition;
            player.transform.eulerAngles = saveData.playerRotation;
        }
    }
    public void DeleteSaveData()
    {
        if (File.Exists(saveFilePath))
        {
            File.Delete(saveFilePath);
            Debug.Log("Save data deleted.");
        }
        else
        {
            Debug.LogWarning("No save file to delete!");
        }
    }
    public bool HasSaveData()
    {
        return File.Exists(saveFilePath);
    }
    private void OnEnable()
    {
        GameEvents.OnGameCompleted += MarkGameCompleted;
    }

    private void OnDisable()
    {
        GameEvents.OnGameCompleted -= MarkGameCompleted;
    }

    private void MarkGameCompleted()
    {
        isGameCompleted = true;
        SaveGame();
    }
    public bool IsGameCompleted()
    {
        if (!File.Exists(saveFilePath))
            return false;

        string json = File.ReadAllText(saveFilePath);
        SaveData saveData = JsonUtility.FromJson<SaveData>(json);

        return saveData.isGameCompleted;
    }
    public void ResetGameCompleted()
    {
        isGameCompleted = false;
    }
}
