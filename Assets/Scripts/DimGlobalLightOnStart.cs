using UnityEngine;
using UnityEngine.Rendering.Universal; // Required for Light2D

public class DimGlobalLightOnStart : MonoBehaviour
{
    private Light2D globalLight;

    void Start()
    {
        globalLight = GetComponent<Light2D>();
        if (globalLight != null)
        {
            globalLight.intensity = 0f; // Set intensity to 0 when game starts
        }
    }
}
