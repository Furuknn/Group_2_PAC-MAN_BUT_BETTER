using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{

    [SerializeField] private GameObject mainPanel, optionsPanel;
    [SerializeField] private TextMeshProUGUI fullscreenButtonText;
    [SerializeField] private Slider volumeSlider;
    [SerializeField] private AudioSource menuMusicSource;

    void Start()
    {
        if (!PlayerPrefs.HasKey("pacmanVersion"))
        {
            PlayerPrefs.SetString("pacmanVersion","");
        }
        if (!PlayerPrefs.HasKey("volumeValue"))
        {
            PlayerPrefs.SetInt("volumeValue",100);
        }

        GameObject objectToDestroy1 = GameObject.Find("GameManager");
        GameObject objectToDestroy2 = GameObject.Find("AudioManager");

        if (objectToDestroy1 != null)
            Destroy(objectToDestroy1);
        if(objectToDestroy2!=null)
            Destroy(objectToDestroy2);
        else
            Debug.Log("Object to destroy not found!");
        
        volumeSlider.value = PlayerPrefs.GetInt("volumeValue");
        //menuMusicSource = FindObjectOfType<AudioSource>();
        menuMusicSource.volume = (float)PlayerPrefs.GetInt("volumeValue") / 100.0f;

        UpdateButtonState();
    }


    public void ClassicButtonClick()
    {
        PlayerPrefs.SetString("pacmanVersion", "classic");
        SceneManager.LoadScene("PacMan");
    }

    public void BetterButtonClick()
    {
        PlayerPrefs.SetString("pacmanVersion", "better");
        SceneManager.LoadScene("PacMan");
    }

    public void ToggleFullscreen()
    {
        Screen.fullScreen = !Screen.fullScreen;

        UpdateButtonState();
    }

    public void OptionsToggle()
    {
        
        mainPanel.SetActive(!mainPanel.activeSelf);
        optionsPanel.SetActive(!optionsPanel.activeSelf);
    }

    public void VolumeSlider()
    {
        PlayerPrefs.SetInt("volumeValue", (int)volumeSlider.value);

        //AudioSource menuMusicSource=FindObjectOfType<AudioSource>();
        menuMusicSource.volume = (float)PlayerPrefs.GetInt("volumeValue") / 100.0f;
    }

    private void UpdateButtonState()
    {
        if (Screen.fullScreen)
        {
            Debug.Log("Fullscreen mode is ON");
            fullscreenButtonText.text = "ON";
            fullscreenButtonText.color = Color.green;
        }
        else
        {
            Debug.Log("Fullscreen mode is OFF");
            fullscreenButtonText.text = "OFF";
            fullscreenButtonText.color = Color.red;
        }
    }
}
