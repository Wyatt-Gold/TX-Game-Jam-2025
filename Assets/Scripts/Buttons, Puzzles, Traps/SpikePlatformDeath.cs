using UnityEngine;
using UnityEngine.SceneManagement;

public class SpikeResetTrigger : MonoBehaviour
{
    public string playerTag = "Player"; // Tag to identify the player

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(playerTag))
        {
            // Reload the current scene
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
