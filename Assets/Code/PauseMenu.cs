using System;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public static event Action<float> OnSensitivityChanged;

    [SerializeField] GameObject pauseMenuUI;
    [SerializeField] Slider volumeSlider;
    [SerializeField] Slider sensitivitySlider;

    bool isPaused;


    void Start()
    {
        volumeSlider.value = PlayerPrefs.GetFloat("volume", 1f);
        sensitivitySlider.value = PlayerPrefs.GetFloat("sensitivity", 100f);

        volumeSlider.onValueChanged.AddListener(SetVolume);
        sensitivitySlider.onValueChanged.AddListener(SetSensitivity);

        pauseMenuUI.SetActive(false);
        ApplySettings();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
                Resume();
            else
                Pause();
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    void SetVolume(float value)
    {
        AudioListener.volume = value;
        PlayerPrefs.SetFloat("volume", value);
    }

    void SetSensitivity(float value)
    {
        PlayerPrefs.SetFloat("sensitivity", value);
        OnSensitivityChanged?.Invoke(value);
    }

    void ApplySettings() => AudioListener.volume = volumeSlider.value;
}
