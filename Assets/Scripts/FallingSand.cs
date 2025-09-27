using UnityEngine;

public class FallingSand : MonoBehaviour
{
    public Sprite sprite1;
    public Sprite sprite2;
    public float switchInterval = 0.25f; // seconds between switches

    private SpriteRenderer sr;
    private float timer;
    private bool showingSprite1 = true;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        sr.sprite = sprite1;
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= switchInterval)
        {
            sr.sprite = showingSprite1 ? sprite2 : sprite1;
            showingSprite1 = !showingSprite1;
            timer = 0f;
        }
    }
}
