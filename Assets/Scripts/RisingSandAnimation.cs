using UnityEngine;

public class RisingSandAnimation : MonoBehaviour
{
    public Sprite[] sprites;              // Array to hold 3 sprites
    public float cycleDelay = 0.5f;       // Delay between each sprite change in seconds

    private SpriteRenderer spriteRenderer;
    private int currentIndex = 0;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (sprites.Length < 3)
        {
            Debug.LogWarning("Please assign 3 sprites to the SpriteCycler.");
            enabled = false;
            return;
        }

        InvokeRepeating(nameof(CycleSprite), 0f, cycleDelay);
    }

    void CycleSprite()
    {
        currentIndex = (currentIndex + 1) % sprites.Length;
        spriteRenderer.sprite = sprites[currentIndex];
    }
}
