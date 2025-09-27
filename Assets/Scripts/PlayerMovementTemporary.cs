using UnityEngine;

public class PlayerMovementTemporary : MonoBehaviour
{
    public float moveSpeed = 4f;

    private Rigidbody2D rb;
    private Vector2 movement;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        movement.x = 0f;
        movement.y = 0f;

        if (Input.GetKey(KeyCode.W)) movement.y = 1f;
        if (Input.GetKey(KeyCode.S)) movement.y = -1f;
        if (Input.GetKey(KeyCode.A)) movement.x = -1f;
        if (Input.GetKey(KeyCode.D)) movement.x = 1f;

        movement = movement.normalized;
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
}
