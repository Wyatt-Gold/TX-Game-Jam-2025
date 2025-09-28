using UnityEngine;
using UnityEngine.Rendering.Universal; // Required if using Light2D

public class ButtonPress : MonoBehaviour
{
    public Transform button;
    public float moveDistance;
    public float moveDuration;
    public Light2D torchLight;
    public ButtonPuzzleManager manager;

    private bool isPressed = false;
    private Vector3 originalPosition;

    void Start()
    {
        if (button != null)
            originalPosition = button.localPosition;

        if (torchLight != null)
            torchLight.enabled = false; // Light off at start
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isPressed)
        {
            isPressed = true;

            float direction = GetPlayerHitDirection(other);
            Vector3 targetPosition = originalPosition + new Vector3(moveDistance * direction, 0f, 0f);
            StartCoroutine(MoveButtonSmoothly(button, targetPosition, moveDuration));
            manager.ButtonPressed();

            LightTorch();
        }
    }

    private float GetPlayerHitDirection(Collider2D player)
    {
        return player.transform.position.x < transform.position.x ? 1f : -1f;
    }

    private System.Collections.IEnumerator MoveButtonSmoothly(Transform obj, Vector3 destination, float duration)
    {
        float elapsed = 0f;
        Vector3 startPos = obj.localPosition;

        while (elapsed < duration)
        {
            obj.localPosition = Vector3.Lerp(startPos, destination, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        obj.localPosition = destination;
    }

    private void LightTorch()
    {
        if (torchLight != null)
        {
            torchLight.enabled = true;
            Debug.Log("Torch Lit");
        }
    }
}
