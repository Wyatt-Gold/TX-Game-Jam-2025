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

    [Header("Fade Settings")]
    public Image fadeOverlay;
    public float fadeToBlackDuration = 1.0f;  // Slow dim
    public float fadeFromBlackDuration = 0.3f; // Fast undim (in next scene)

    private AudioSource audioSource;
    private bool clicked = false;

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        GetComponent<Button>().onClick.AddListener(OnClick);

        if (fadeOverlay != null)
        {
            Color c = fadeOverlay.color;
            c.a = 0;
            fadeOverlay.color = c;
        }
    }

    void OnClick()
    {
        if (clicked) return; // Prevent multiple clicks
        clicked = true;

        if (clickSound != null)
            audioSource.PlayOneShot(clickSound);

        StartCoroutine(FadeAndLoadScene());
    }

    System.Collections.IEnumerator FadeAndLoadScene()
    {
        // Slowly fade to black
        yield return StartCoroutine(FadeOverlay(0f, 1f, fadeToBlackDuration));

        // Optional wait (in case you want sound delay)
        yield return new WaitForSeconds(delayBeforeLoad);

        SceneManager.LoadScene(sceneToLoad);
    }

    System.Collections.IEnumerator FadeOverlay(float fromAlpha, float toAlpha, float duration)
    {
        if (fadeOverlay == null) yield break;

        float timer = 0f;
        Color c = fadeOverlay.color;

        while (timer < duration)
        {
            float t = timer / duration;
            c.a = Mathf.Lerp(fromAlpha, toAlpha, t);
            fadeOverlay.color = c;
            timer += Time.deltaTime;
            yield return null;
        }

        c.a = toAlpha;
        fadeOverlay.color = c;
    }
}
