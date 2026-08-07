using UnityEngine;

public class FinalBossDoor : MonoBehaviour
{
    [SerializeField] private string bossSceneName = "FinalBossCombat";

    private void OnTriggerEnter(Collider other)
    {
       if (!other.CompareTag("Player"))
        {
            return;
        }
       if (SceneLoader.Instance != null)
        {
            SceneLoader.Instance.LoadScene(bossSceneName);
        }
       else
        {
            Debug.LogError("SceneLoader could not be found");
        }
    }
}
