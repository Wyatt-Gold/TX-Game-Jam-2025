using UnityEngine;

public class MoveUponCommand : MonoBehaviour
{

    public Vector2 movement;
    public float duration = 3f;
    private Vector2 originalPosition;
    public ButtonPressGeneric button;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (button.actionReady)
        {
            button.actionReady = false;
            StartCoroutine(Move());
        }
    }

    System.Collections.IEnumerator Move()
    {
        float elapsed = 0f;
        Vector2 startPos = transform.localPosition;
        Vector2 destination = startPos + movement;

        while (elapsed < duration)
        {
            transform.localPosition = Vector3.Lerp(startPos, destination, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = destination;
    }
}
