using UnityEngine;

public class Boing : MonoBehaviour
{

    //For maximum boing-age
    public float boingFactor = 1.0f;
    public float tooMuchBoing = 30f;

    void Start()
    {
        
    }

    // Reverses the linear velocity of the colliding Rigidbody2D.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Rigidbody2D bounce = collision.gameObject.GetComponent<Rigidbody2D>();
        float newVelocity = bounce.linearVelocityY * -boingFactor;
        if (newVelocity > tooMuchBoing) newVelocity = tooMuchBoing;
        bounce.linearVelocityY = newVelocity;
    }
}
