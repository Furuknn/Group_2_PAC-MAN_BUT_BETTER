using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (!PlayerPrefs.HasKey("pacmanVersion"))
        {
            PlayerPrefs.SetString("pacmanVersion","");
        }
        
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

    private void UpdateButtonState()
    {
        if (Screen.fullScreen)
        {
            Debug.Log("Fullscreen mode is ON");
        }
        else
        {
            Debug.Log("Fullscreen mode is OFF");
        }
    }
}
