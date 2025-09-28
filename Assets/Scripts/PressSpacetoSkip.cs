using UnityEngine;
using UnityEngine.SceneManagement;

public class PressSpacetoSkip : MonoBehaviour
{
    

    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKey)
        {
            //if (!string.IsNullOrEmpty("RoomOne"))
            //{
                SceneManager.LoadScene("RoomOne");
            //}
            //else
            //{
             //   Debug.LogWarning("Next scene name not set.");
            //}
        }

    }
}
