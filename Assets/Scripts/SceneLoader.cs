using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader Instance;
    [SerializeField] private GameObject loadingScreen;
    private const float MinimumLoadTime = 1.5f;
    private const float SceneReadyProgress = 0.9f;
    private const float InitialTimerValue = 0f;

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
        float timer = InitialTimerValue;
        //Ryan pointed out some magic numbers, got rid of them and added constants
        while (!operation.isDone)
        {
            timer += Time.deltaTime;
            if (operation.progress >= SceneReadyProgress)
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
