using UnityEngine;
using UnityEngine.SceneManagement;

public class SpikeResetTrigger : MonoBehaviour
{
    public string playerTag = "Player"; // Tag to identify the player

    private GameObject canvas;

    void Start()
    {
        canvas = GameObject.Find("Restart Screen");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(playerTag)) // Player died pause screen
        {
            Time.timeScale = 0f;
            canvas.SetActive(true);
        }
    }
}
