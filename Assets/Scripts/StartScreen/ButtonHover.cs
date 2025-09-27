using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ButtonHover: MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Hover Sound")]
    public AudioClip hoverSound;

    [Header("Hover Scale Settings")]
    public float hoverScale = 1.1f;
    public float scaleSpeed = 10f;

    private Vector3 originalScale;
    private Vector3 targetScale;
    private AudioSource audioSource;

    private void Start()
    {
        originalScale = transform.localScale;
        targetScale = originalScale;

        // Setup AudioSource
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.spatialBlend = 0f; // 2D sound
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        targetScale = originalScale * hoverScale;

        if (hoverSound != null)
        {
            audioSource.PlayOneShot(hoverSound);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        targetScale = originalScale;
    }

    private void Update()
    {
        // Smoothly scale toward target
        transform.localScale = Vector3.Lerp(transform.localScale, targetScale, Time.deltaTime * scaleSpeed);
    }
}
