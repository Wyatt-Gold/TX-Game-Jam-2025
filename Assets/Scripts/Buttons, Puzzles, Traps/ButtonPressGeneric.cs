using UnityEngine;
using UnityEngine.Rendering.Universal;

public class ButtonPressGeneric : MonoBehaviour
{
    public Transform button;
    public float moveDistance;
    public float moveDuration;
    //public Light2D torchLight;

    private bool isPressed = false;
    public bool actionReady = false;
    private Vector3 originalPosition;

    void Awake()
    {
        if (button != null)
            originalPosition = button.localPosition;

        actionReady = false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isPressed)
        {
            isPressed = true;
            actionReady = true;

            float direction = GetPlayerHitDirection(other);
            Vector3 targetPosition = originalPosition + new Vector3(moveDistance * direction, 0f, 0f);
            StartCoroutine(MoveButtonSmoothly(button, targetPosition, moveDuration));

            
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
}
