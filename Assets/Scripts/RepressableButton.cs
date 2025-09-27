using UnityEngine;
using UnityEngine.Rendering.Universal;

public class RepressableButton : MonoBehaviour
{

    public Transform button;
    public float moveDistance;
    public float moveDuration;
    public GameObject target;
    // ID saves the specific action which the button calls for.
    // 1: Left rotation
    // 2: Right rotation
    // 3: Toggle [1]
    // 4: Toggle [1], [2], [3]
    // 5: Toggle [1], [3]
    public byte ID = 1;

    private bool isPressed = false;
    private Vector3 originalPosition;

    void Start()
    {
        if (button != null)
            originalPosition = button.localPosition;
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isPressed)
        {
            isPressed = true;

            float direction = GetPlayerHitDirection(other);
            Vector3 targetPosition = originalPosition + new Vector3(0f, moveDistance * direction, 0f);
            StartCoroutine(ButtonPressCycle(button, targetPosition, moveDuration));

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

        while (elapsed < duration)
        {
            obj.localPosition = Vector3.Lerp(startPos, destination, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        obj.localPosition = destination;

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

    }

    private void MessageTarget()
    {
        //Debug.Log("Sending message");
        target.GetComponent<TorchPuzzle>().RecieveCommand(ID);
    }
}
