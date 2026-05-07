using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }
    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    public void NewGame(string scene)
    {
        SaveSystem.Instance.DeleteSaveData();
        SceneManager.LoadScene(scene);
    }
    public void LoadScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }
    public void ContinueGame(string scene)
    {
        Debug.Log("ContinueGame вызван");
        StartCoroutine(LoadSceneAndData(scene));
    }
    public void ExitGame()
    {
        Application.Quit();
    }
    private IEnumerator LoadSceneAndData(string scene)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(scene);
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
        yield return new WaitForSeconds(0.1f);
        Debug.Log("—цена загружена, вызываю LoadGame");
        SaveSystem.Instance.LoadGame();
    }
}
