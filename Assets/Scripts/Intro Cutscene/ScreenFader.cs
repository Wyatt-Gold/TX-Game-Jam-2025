using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScreenFader : MonoBehaviour
{
    public Image fadeImage;
    public float fadeDuration = 2f;

    void Start()
    {
        StartCoroutine(FadeFromBlack());
    }

    IEnumerator FadeFromBlack()
    {
        Color color = fadeImage.color;
        float t = 0f;

        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            color.a = Mathf.Lerp(1f, 0f, t / fadeDuration);
            fadeImage.color = color;
            yield return null;
        }

        // Make sure it's fully transparent at the end
        color.a = 0f;
        fadeImage.color = color;
    }
}
