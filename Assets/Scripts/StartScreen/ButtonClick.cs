using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ButtonClick : MonoBehaviour
{
    [Header("Sound Settings")]
    public AudioClip clickSound;
    public float delayBeforeLoad = 0.2f;

    [Header("Scene Settings")]
    public string sceneToLoad;

    private AudioSource audioSource;
    private bool clicked = false;

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        GetComponent<Button>().onClick.AddListener(OnClick);
    }

    void OnClick()
    {
        if (clicked) return; // Prevent multiple clicks
        clicked = true;

        if (clickSound != null)
            audioSource.PlayOneShot(clickSound);

        StartCoroutine(LoadSceneAfterDelay());
    }

    System.Collections.IEnumerator LoadSceneAfterDelay()
    {
        yield return new WaitForSeconds(delayBeforeLoad);
        SceneManager.LoadScene(sceneToLoad);
    }
}
