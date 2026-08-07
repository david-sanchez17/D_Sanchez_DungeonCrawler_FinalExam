using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader Instance;
    [SerializeField] private GameObject loadingScreen;
    private const float MinimumLoadTime = 1.5f;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        if (loadingScreen != null)
        {
            loadingScreen.SetActive(false);
        }
    }

    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadSceneRoutine(sceneName));
    }

    private IEnumerator LoadSceneRoutine(string sceneName)
    {
        if (loadingScreen != null)
        {
            loadingScreen.SetActive(true);
        }
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
        operation.allowSceneActivation = false;
        float timer = 0f;

        while (!operation.isDone)
        {
            timer += Time.deltaTime;
            if (operation.progress >= 0.9f)
            {
                if (timer >= MinimumLoadTime)
                {
                    operation.allowSceneActivation = true;
                }
            }
            yield return null;
        }
    }
}
