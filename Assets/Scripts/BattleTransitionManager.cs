using UnityEngine;

public class BattleTransitionManager : MonoBehaviour
{
    public static BattleTransitionManager Instance;
    private Vector3 returnPosition;
    private string returnSceneName;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SaveReturnPoint(string sceneName, Vector3 playerPosition)
    {
        returnSceneName = sceneName;
        returnPosition = playerPosition;
    }

    public string GetReturnScene()
    {
        return returnSceneName;
    }

    public Vector3 GetReturnPosition()
    {
        return returnPosition;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
