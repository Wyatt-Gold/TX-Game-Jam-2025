using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEditor.Experimental.GraphView.GraphView;

public class RoomTwoBoundary : MonoBehaviour
{
    Collider2D stairs;
    
    [Header("Scene Settings")]
    public bool useSceneBuildOrder = false;
    public string nextSceneName;

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

    public GameObject canvas;

    void Start()
    {
        stairs = gameObject.GetComponent<Collider2D>();
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

        if (IsPlayerInSand(player1.transform.localPosition.y) || IsPlayerInSand(player2.transform.localPosition.y))
        {
            if (Time.timeScale > 0) Instantiate(canvas);
            Time.timeScale = 0f;
        }

        if (player1Exited && player2Exited)
        {
            LoadNextScene();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == player1)
        {
            player1Exited = true;
        }
        else if (collision.gameObject == player2)
        {
            player2Exited = true; 
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == player1)
        {
            player1Exited = false;
        }
        else if (collision.gameObject == player2)
        {
            player2Exited = false;
        }
    }

    bool IsPlayerInSand(float playerY)
    {
        if (sandTransform == null) return false;

        float sandTopY = sandTransform.position.y + (sandTransform.localScale.y / 2f);
        return playerY < sandTopY;
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
            }
            else
            {
                Debug.LogWarning("Next scene name not set.");
            }
        }
    }
}
