using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneBoundaryHandler : MonoBehaviour
{
    [Header("Y Position Thresholds")]
    public float topThreshold = 10f;

    [Header("Scene Settings")]
    public bool useSceneBuildOrder = false;
    public string nextSceneName;

    public GameObject canvas;

    [Header("Players")]
    public GameObject player1;
    public GameObject player2;

    [Header("Rising Sand Settings")]
    public Transform sandTransform;
    public float initialSandSpeed = 0.5f;
    public float sandAcceleration = 0.05f; // Speed increase per second

    private float currentSandSpeed;
    private bool player1Exited = false;
    private bool player2Exited = false;

    void Start()
    {
        currentSandSpeed = initialSandSpeed;
    }

    void Update()
    {
        // Accelerate the sand over time
        currentSandSpeed += sandAcceleration * Time.deltaTime;

        // Move the sand upward
        if (sandTransform != null)
        {
            sandTransform.position += Vector3.up * currentSandSpeed * Time.deltaTime;
        }

        CheckPlayer(player1, ref player1Exited);
        CheckPlayer(player2, ref player2Exited);

        if (player1Exited && player2Exited)
        {
            LoadNextScene();
        }
    }

    void CheckPlayer(GameObject player, ref bool hasExited)
    {
        if (player == null) return;

        float playerY = player.transform.position.y;

        // Restart if player falls into sand
        if (IsPlayerInSand(playerY))
        {
            PauseScene();
        }

        // Exit at top of screen
        if (playerY > topThreshold && !hasExited)
        {
            hasExited = true;
            Destroy(player);
        }
    }

    bool IsPlayerInSand(float playerY)
    {
        if (sandTransform == null) return false;

        float sandTopY = sandTransform.position.y + (sandTransform.localScale.y / 2f);
        return playerY < sandTopY;
    }

    void PauseScene()
    {
        Time.timeScale = 0f;
        canvas.SetActive(true);
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
            }
            else
            {
                Debug.LogWarning("Next scene name not set.");
            }
        }
    }
}
