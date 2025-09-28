using UnityEngine;

public class CrudeAnimLoop : MonoBehaviour
{

    public float loopInterval = 0.33f;
    public Sprite[] sprites;
    private float time;
    private byte index = 0;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time > loopInterval)
        {
            time -= loopInterval;
            index++;
            if (index == sprites.Length) index = 0;
            gameObject.GetComponent<SpriteRenderer>().sprite = sprites[index];
        }
    }
}
