using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader Instance { get; private set; }

    [Header("Loading Screen")]
    [SerializeField] private GameObject loadingScreen;
    [SerializeField] private Image progressBar;

    private bool loadSaveAfterScene;
    public static bool ShouldLoadGameAfterScene;
    private void Awake()
    {
        if (Instance !=null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        if(loadingScreen != null)
            loadingScreen.SetActive(false);
    }
    public void NewGame(string sceneName)
    {
        Time.timeScale = 1f;
        SaveSystem.Instance.DeleteSaveData();
        SaveSystem.Instance.ResetGameCompleted();
        loadSaveAfterScene = false;
        ShouldLoadGameAfterScene = false;
        StartCoroutine(LoadSceneAsync(sceneName));
    }
    public void ContinueGame(string sceneName)
    {
        if (!SaveSystem.Instance.HasSaveData())
            return;
        if (SaveSystem.Instance.IsGameCompleted())
        {
            Debug.Log("Game completed. Continue disabled.");
            return;
        }
        Time.timeScale = 1f;
        loadSaveAfterScene = true; 
        ShouldLoadGameAfterScene = true;
        StartCoroutine(LoadSceneAsync(sceneName));
    }
    public void LoadScene(string sceneName)
    {
        Time.timeScale = 1f;
        loadSaveAfterScene = false;
        StartCoroutine(LoadSceneAsync(sceneName));
    }
    private IEnumerator LoadSceneAsync(string sceneName)
    {
        if(loadingScreen != null)
            loadingScreen.SetActive(true);
        if(progressBar != null)
            progressBar.fillAmount = 0f;
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);

        while(!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);

            if(progressBar != null)
                progressBar.fillAmount = progress;
            yield return null;
        }
        if (loadingScreen != null)
            loadingScreen.SetActive(false);
    }
    public void ExitGame()
    {
        Application.Quit();
    }
}
