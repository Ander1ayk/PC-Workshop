using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DisplaySettingsUI : MonoBehaviour
{
    [Header("Display Settings")]
    [SerializeField] private TMP_Dropdown resolutionDropdown;
    [SerializeField] private Toggle fullscreenToggle;
    [SerializeField] private TMP_Dropdown fpsLimitDropdown;
    [SerializeField] private Toggle showFPSToggle;

    private Resolution[] resolutions;

    private readonly int[] fpsOptions =
        {60, 120, 144, 165, 240, -1};

    private void Start()
    {
        SetupResolutions();
        SetupFPSLimit();

        bool isFullScreen = PlayerPrefs.GetInt("Fullscreen", Screen.fullScreen ? 1: 0) == 1;
        fullscreenToggle.isOn = isFullScreen;
        Screen.fullScreen = isFullScreen;

        bool showFPS = PlayerPrefs.GetInt("ShowFPS", 0) == 1;
        showFPSToggle.isOn = showFPS;

        fullscreenToggle.onValueChanged.AddListener(SetFullscreen);
        showFPSToggle.onValueChanged.AddListener(SetShowFPS);
        resolutionDropdown.onValueChanged.AddListener(SetResolution);
        fpsLimitDropdown.onValueChanged.AddListener(SetFPSLimit);
    }
    private void SetupResolutions()
    {
        resolutions = Screen.resolutions;

        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();
        int currentResolutionIndex = 0;

        int saveWidth = PlayerPrefs.GetInt("ResolutionWidth", Screen.currentResolution.width);
        int saveHeight = PlayerPrefs.GetInt("ResolutionHeight", Screen.currentResolution.height);

        for(int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if(resolutions[i].width == saveWidth && resolutions[i].height == saveHeight)
            {
                currentResolutionIndex = i;
            }
        }
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }
    private void SetupFPSLimit()
    {
        fpsLimitDropdown.ClearOptions();

        List<string> options = new List<string>
        {
            "60 FPS",
            "120 FPS",
            "144 FPS",
            "165 FPS",
            "240 FPS",
            "Unlimited"
        };

        fpsLimitDropdown.AddOptions(options);

        int savedFPS = PlayerPrefs.GetInt("FPSLimit", 120);
        int index = 1;
        for (int i = 0; i< fpsOptions.Length; i++)
        {
            if (fpsOptions[i] == savedFPS)
            {
                index = i;
                break;
            }
        }
        fpsLimitDropdown.value = index;
        fpsLimitDropdown.RefreshShownValue();

        Application.targetFrameRate = savedFPS;
    }
    public void SetResolution(int index)
    {
        Resolution resolution = resolutions[index];

        Screen.SetResolution(
            resolution.width,
            resolution.height,
            Screen.fullScreen
        );

        PlayerPrefs.SetInt("ResolutionWidth", resolution.width);
        PlayerPrefs.SetInt("ResolutionHeight", resolution.height);
        PlayerPrefs.Save();
    }
    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;

        PlayerPrefs.SetInt("Fullscreen", isFullscreen ? 1 : 0);
        PlayerPrefs.Save();
    }
    public void SetFPSLimit(int index)
    {
        int fpsLimit = fpsOptions[index];

        Application.targetFrameRate = fpsLimit;

        PlayerPrefs.SetInt("FPSLimit", fpsLimit);
        PlayerPrefs.Save();
    }
    public void SetShowFPS(bool show)
    {
        PlayerPrefs.SetInt("ShowFPS", show ? 1 : 0);
        PlayerPrefs.Save();
    }
}
