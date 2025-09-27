using UnityEngine;

public class ButtonPress : MonoBehaviour
{
    public Transform button; // The part of the button that will move
    public Vector3 pressedOffset; // Amount we will move the button

    private bool isPressed = false;

    void Start()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Collission Detected");
        if (other.CompareTag("Player") && !isPressed)
        {
            isPressed = true;
            Debug.Log("Button Pressed");

            button.localPosition += pressedOffset;
            // TODO: Add Sound and other actions here
        }
    }

    void Update()
    {

    }
}
