using UnityEngine;

public class TitleAnimation : MonoBehaviour
{
    [Header("Scale Settings")]
    public float scaleAmount = 0.2f;         // How much to grow/shrink
    public float scaleSpeed = 2f;            // Speed of the scaling animation

    [Header("Tilt Settings")]
    public float tiltAngle = 15f;            // Max tilt angle in degrees
    public float tiltSpeed = 2f;             // Speed of tilting

    private Vector3 initialScale;
    private Quaternion initialRotation;

    void Start()
    {
        initialScale = transform.localScale;
        initialRotation = transform.rotation;
    }

    void Update()
    {
        // Scale animation using sine wave
        float scaleOffset = Mathf.Sin(Time.time * scaleSpeed) * scaleAmount;
        transform.localScale = initialScale + new Vector3(scaleOffset, scaleOffset, scaleOffset);

        // Tilt animation using sine wave
        float tiltOffset = Mathf.Sin(Time.time * tiltSpeed) * tiltAngle;
        transform.rotation = initialRotation * Quaternion.Euler(0f, 0f, tiltOffset);
    }
}
