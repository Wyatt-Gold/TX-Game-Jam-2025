using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal; 

public class ButtonPuzzleManager : MonoBehaviour
{
    [Header("Buttons")]
    public int totalButtons = 3;
    private int buttonsPressed = 0;

    [Header("Objects to Move")]
    public GameObject[] objectsToMove;
    public float moveDistance = 1f;
    public float moveDuration = 2f;

    [Header("Lighting")]
    public GameObject lightObject;

    private bool puzzleCompleted = false;

    // Called by buttons when clicked
    public void ButtonPressed()
    {
        buttonsPressed++;

        if (!puzzleCompleted && buttonsPressed >= totalButtons)
        {
            puzzleCompleted = true;
            StartCoroutine(MoveObjectsUpward());

            // Change light intensity
            Light2D light2D = lightObject.GetComponent<Light2D>();
            if (light2D != null)
            {
                light2D.intensity = 1f;
            }
            else
            {
                Debug.LogWarning("No Light2D component found on lightObject.");
            }
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
