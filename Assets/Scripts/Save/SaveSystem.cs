using System.IO;
using UnityEngine;

public class SaveSystem : MonoBehaviour
{
    public static SaveSystem Instance { get; private set; }
    private string saveFilePath;

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

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if(player != null)
        {
            player.transform.position = saveData.playerPosition;
            player.transform.eulerAngles = saveData.playerRotation;
        }
    }
    public bool HasSaveData()
    {
        return File.Exists(saveFilePath);
    }
}
