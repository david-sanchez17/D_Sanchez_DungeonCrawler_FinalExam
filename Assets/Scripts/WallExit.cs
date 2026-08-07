using UnityEngine;

public class WallExit : MonoBehaviour
{
    [SerializeField] private string nextSceneName = "Level2";
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
        {
            return;
        }
        if (SceneLoader.Instance != null)
        {
            SceneLoader.Instance.LoadScene(nextSceneName);
        }
    }
}
