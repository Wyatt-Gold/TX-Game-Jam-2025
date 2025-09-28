using UnityEngine;
using UnityEngine.Rendering.Universal;

public class RepressableButton : MonoBehaviour
{
    public Transform button;
    public float moveDistance;
    public float moveDuration;
    public GameObject target;

    [Header("Button Sprites")]
    public SpriteRenderer buttonSpriteRenderer; // Drag your SpriteRenderer here
    public Sprite unpressedSprite;
    public Sprite pressedSprite;

    public byte ID = 1;

    private bool isPressed = false;
    private Vector3 originalPosition;
    private AudioSource torchAudioSource;

    void Start()
    {
        if (button != null)
            originalPosition = button.localPosition;

        // Set to unpressed sprite at start
        if (buttonSpriteRenderer != null && unpressedSprite != null)
            buttonSpriteRenderer.sprite = unpressedSprite;

        torchAudioSource = GetComponentInParent<AudioSource>();

        if (torchAudioSource != null)
            torchAudioSource.playOnAwake = false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isPressed)
        {
            isPressed = true;

            float direction = GetPlayerHitDirection(other);
            Vector3 targetPosition = originalPosition + new Vector3(0f, moveDistance * direction, 0f);
            StartCoroutine(ButtonPressCycle(button, targetPosition, moveDuration));

            // Change to pressed sprite
            if (buttonSpriteRenderer != null && pressedSprite != null)
                buttonSpriteRenderer.sprite = pressedSprite;

            MessageTarget();
        }
    }

    private float GetPlayerHitDirection(Collider2D player)
    {
        return player.transform.position.y < transform.position.y ? 1f : -1f;
    }

    private System.Collections.IEnumerator ButtonPressCycle(Transform obj, Vector3 destination, float duration)
    {
        float elapsed = 0f;
        Vector3 startPos = obj.localPosition;

        if (torchAudioSource != null)
            torchAudioSource.Play();

        while (elapsed < duration)
        {
            obj.localPosition = Vector3.Lerp(startPos, destination, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        obj.localPosition = destination;

        // Pause for realism, optional:
        yield return new WaitForSeconds(0.1f);

        elapsed = 0f;
        startPos = obj.localPosition;
        destination = originalPosition;

        while (elapsed < duration)
        {
            obj.localPosition = Vector3.Lerp(startPos, destination, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        obj.localPosition = originalPosition;
        isPressed = false;

        // Change back to unpressed sprite
        if (buttonSpriteRenderer != null && unpressedSprite != null)
            buttonSpriteRenderer.sprite = unpressedSprite;
    }

    private void MessageTarget()
    {
        if (target != null)
            target.GetComponent<TorchPuzzle>().RecieveCommand(ID);
    }
}
