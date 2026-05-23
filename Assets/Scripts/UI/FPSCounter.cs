using UnityEngine;

public class FPSCounter : MonoBehaviour
{
    [SerializeField] private TMPro.TextMeshProUGUI fpsText;
    private float deltaTime;
    private void Start()
    {
        bool showFPS = PlayerPrefs.GetInt("ShowFPS", 0) == 1;
        fpsText.gameObject.SetActive(showFPS);
    }
    private void Update()
    {
        bool showFPS = PlayerPrefs.GetInt("ShowFPS", 0) == 1;

        if (!showFPS)
        {
            if (fpsText.gameObject.activeSelf)
                fpsText.gameObject.SetActive(false);

            return;
        }
        if(!fpsText.gameObject.activeSelf)
            fpsText.gameObject.SetActive(true);

        deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;

        float fps = 1f / deltaTime;

        fpsText.text = "FPS: " + Mathf.CeilToInt(fps);
    }
}
