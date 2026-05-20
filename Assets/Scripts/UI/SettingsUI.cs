using UnityEngine;
using UnityEngine.UI;

public class SettingsUI : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private Slider mouseSensitivitySlider;

    private const string MouseSensitivityKey = "MouseSensitivity";

    private void Start()
    {
        float savedSensitivity = PlayerPrefs.GetFloat(
            MouseSensitivityKey,
            playerStats.mouseSensitivity
        );

        mouseSensitivitySlider.value = savedSensitivity;

        mouseSensitivitySlider.onValueChanged.AddListener(SetMouseSensitivity);
    }

    private void SetMouseSensitivity(float value)
    {
        PlayerPrefs.SetFloat(MouseSensitivityKey, value);
        PlayerPrefs.Save();
        
        GameEvents.MouseSensitivityChanged(value);
    }
}
