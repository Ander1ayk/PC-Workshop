using UnityEngine;

public class MenuButtons : MonoBehaviour
{
    public void NewGame()
    {
        SceneLoader.Instance.NewGame("MainGame");
    }
    public void ContinueGame()
    {
        SceneLoader.Instance.ContinueGame("MainGame");
    }
    public void BackToMenu()
    {
        SceneLoader.Instance.LoadScene("Menu");
    }
    public void LoadScene(string sceneName)
    {
        SceneLoader.Instance.LoadScene(sceneName);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
