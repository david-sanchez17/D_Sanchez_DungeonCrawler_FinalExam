using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
   [Header("Panels")]
   [SerializeField] private GameObject pauseMenu;
   [SerializeField] private GameObject settingsPanel;

    public bool isPaused;
    private bool settingsOpen;

   private void Start()
    {
        isPaused = false;
        settingsOpen = false;
        pauseMenu.SetActive(false);
        settingsPanel.SetActive(false);
        
        Time.timeScale = 1f;
       
    }

    void Update()
    {
        if(Input.GetButtonDown("Pause"))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
        
    }
    
    public void PauseGame()
    {
        isPaused = true;
        pauseMenu.SetActive(true);
        settingsPanel.SetActive(false);
        Time.timeScale = 0f;
        
    }

    public void ResumeGame()
    {
        isPaused = false;
        settingsOpen = false;
        pauseMenu.SetActive(false);
        settingsPanel.SetActive(false);
        Time.timeScale = 1f;
        Debug.Log("Resume");
    }

    public void OpenSettings()
    {
        settingsOpen = true;
        pauseMenu.SetActive(false);
        settingsPanel.SetActive(true);
        Debug.Log("Open settings");
    }

    public void CloseSettings()
    {
        settingsOpen = false;
        settingsPanel.SetActive(false);
        pauseMenu.SetActive(true);
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public bool IsPaused()
    {
        return isPaused;
    }
}

