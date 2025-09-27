using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneBoundaryHandler : MonoBehaviour
{
    [Header("Y Position Thresholds")]
    public float bottomThreshold = -10f;
    public float topThreshold = 10f;

    [Header("Scene Settings")]
    public bool useSceneBuildOrder = false;
    public string nextSceneName;

    [Header("Assign Players Manually")]
    public GameObject player1;
    public GameObject player2;

    private bool player1Exited = false;
    private bool player2Exited = false;

    void Update()
    {
        if (player1 != null)
        {
            if (player1.transform.position.y < bottomThreshold)
            {
                RestartScene();
            }
            else if (player1.transform.position.y > topThreshold && !player1Exited)
            {
                player1Exited = true;
                Destroy(player1);
            }
        }

        if (player2 != null)
        {
            if (player2.transform.position.y < bottomThreshold)
            {
                RestartScene();
            }
            else if (player2.transform.position.y > topThreshold && !player2Exited)
            {
                player2Exited = true;
                Destroy(player2);
            }
        }

        if (player1Exited && player2Exited)
        {
            LoadNextScene();
        }
    }

    void RestartScene()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }

    void LoadNextScene()
    {
        if (useSceneBuildOrder)
        {
            int currentIndex = SceneManager.GetActiveScene().buildIndex;
            int nextIndex = currentIndex + 1;

            if (nextIndex < SceneManager.sceneCountInBuildSettings)
            {
                SceneManager.LoadScene(nextIndex);
            }
            else
            {
                Debug.LogWarning("No next scene in Build Settings.");
            }
        }
        else
        {
            if (!string.IsNullOrEmpty(nextSceneName))
            {
                SceneManager.LoadScene(nextSceneName);
                Debug.LogWarning("Loading Next Scene");
            }
            else
            {
                Debug.LogWarning("Next scene name not set.");
            }
        }
    }
}
