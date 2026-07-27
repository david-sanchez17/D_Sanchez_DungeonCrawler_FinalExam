using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class LoadScreen : MonoBehaviour
{
    [Header("Menu Screens")]
    [SerializeField] private GameObject loadingScreen;
    [SerializeField] private GameObject MainMenu;

    public void LoadLevel1(string Level1ToLoad)
    {
        MainMenu.SetActive(false);
        loadingScreen.SetActive(true);

        StartCoroutine(LoadLevelAsync(Level1ToLoad));

    }

    IEnumerator LoadLevelAsync(string Level1ToLoad)
    {
        AsyncOperation loadOperation = SceneManager.LoadSceneAsync(Level1ToLoad);

        while (!loadOperation.isDone)
        {
            float progressValue = Mathf.Clamp01(loadOperation.progress / 0.9f);
            yield return null;
        }
    }
}
