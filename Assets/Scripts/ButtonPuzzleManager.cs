using System.Collections;
using UnityEngine;

public class ButtonPuzzleManager : MonoBehaviour
{
    [Header("Buttons")]
    public int totalButtons = 3;
    private int buttonsPressed = 0;

    [Header("Objects to Move")]
    public GameObject[] objectsToMove;
    public float moveDistance = 1f;
    public float moveDuration = 2f;

    private bool puzzleCompleted = false;

    // Called by buttons when clicked
    public void ButtonPressed()
    {
        buttonsPressed++;

        if (!puzzleCompleted && buttonsPressed >= totalButtons)
        {
            puzzleCompleted = true;
            StartCoroutine(MoveObjectsUpward());
        }
    }

    private IEnumerator MoveObjectsUpward()
    {
        foreach (GameObject obj in objectsToMove)
        {
            Vector3 start = obj.transform.position;
            Vector3 end = start + Vector3.up * moveDistance;

            float elapsed = 0f;
            while (elapsed < moveDuration)
            {
                obj.transform.position = Vector3.Lerp(start, end, elapsed / moveDuration);
                elapsed += Time.deltaTime;
                yield return null;
            }
            obj.transform.position = end;
        }
    }
}
