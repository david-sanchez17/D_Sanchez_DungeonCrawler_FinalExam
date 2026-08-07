using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject settings;

    private void Start()
    {
        mainMenu.SetActive(true);
        settings.SetActive(false);
    }


    public void PlayGame()
    {
        SceneManager.LoadSceneAsync("Level1");
    }

    public void OpenSettings()
    {
        mainMenu.SetActive(false);
        settings.SetActive(true);
    }

    public void CloseSettings()
    {
        settings.SetActive(false);
        mainMenu.SetActive(true);
    }


    public void QuitGame()
    {
        Application.Quit();
    }
}
