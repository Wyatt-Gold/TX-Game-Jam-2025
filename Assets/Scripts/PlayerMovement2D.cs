using UnityEngine;

public class PlayerMovement2D : MonoBehaviour
{
    Rigidbody2D rb;
    SpriteRenderer sr;

    [Header("Movement Settings")]
    public float moveSpeed = 3f;
    public float frogPower = 2;
    private float jumpCooldown = 0.3f;
    private float joc = 0;

    [Header("Ground Check Settings")]
    [SerializeField] float maxDistance;
    [SerializeField] Vector3 boxSize;
    [SerializeField] LayerMask layerMask;

    [Header("Control Settings")]
    public bool player1 = true;
    public bool Player2IJKLControls = false;
    private KeyCode[] controls = new KeyCode[4];

    [Header("Animation Sprites")]
    public Sprite[] idleSprites;  // Length = 2
    public Sprite[] runSprites;   // Length = 2
    public Sprite[] jumpSprites;  // Length = 3

    private Vector2 movement;
    private float animTimer = 0f;
    private int animFrame = 0;

    private bool isJumping = false;
    private bool wasGrounded = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponentInChildren<SpriteRenderer>();

        // Setup controls
        if (player1)
        {
            controls[0] = KeyCode.W;
            controls[1] = KeyCode.A;
            controls[2] = KeyCode.S;
            controls[3] = KeyCode.D;
        }
        else if (Player2IJKLControls)
        {
            controls[0] = KeyCode.I;
            controls[1] = KeyCode.J;
            controls[2] = KeyCode.K;
            controls[3] = KeyCode.L;
        }
        else
        {
            controls[0] = KeyCode.UpArrow;
            controls[1] = KeyCode.LeftArrow;
            controls[2] = KeyCode.DownArrow;
            controls[3] = KeyCode.RightArrow;
        }
    }

    void FixedUpdate()
    {
        HandleMovement();
        HandleAnimation();
    }

    void HandleMovement()
    {
        movement.x = 0;

        // JUMP
        if (Input.GetKey(controls[0]) && GroundCheck())
        {
            if (Time.time > joc)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, frogPower);
                joc = Time.time + jumpCooldown;
                isJumping = true;
            }
        }

        // FAST FALL
        if (Input.GetKey(controls[2]))
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y - 1f);
        }

        // LEFT/RIGHT MOVEMENT
        if (Input.GetKey(controls[1]))
        {
            movement.x = -moveSpeed;
            sr.flipX = false; // Sprite faces LEFT by default
        }
        if (Input.GetKey(controls[3]))
        {
            movement.x = moveSpeed;
            sr.flipX = true; // Flip to face RIGHT
        }

        // APPLY HORIZONTAL MOVEMENT
        rb.linearVelocity = new Vector2(movement.x, rb.linearVelocity.y);
    }

    void HandleAnimation()
    {
        bool isGrounded = GroundCheck();
        bool isMoving = Mathf.Abs(rb.linearVelocity.x) > 0.1f;

        // Jumping
        if (!isGrounded)
        {
            // Set sprite to first run frame as jump frame
            if (runSprites != null && runSprites.Length > 0)
            {
                sr.sprite = runSprites[0];
            }

            // Rotate while jumping based on direction
            float angle = 0f;
            if (rb.linearVelocity.x < -0.1f)
            {
                angle = 15f; // Jumping left
            }
            else if (rb.linearVelocity.x > 0.1f)
            {
                angle = -15f; // Jumping right
            }

            sr.transform.localEulerAngles = new Vector3(0, 0, angle);

            wasGrounded = false;
            return; // Skip other animations while airborne
        }

        // Grounded: reset rotation
        sr.transform.localEulerAngles = Vector3.zero;

        // Running
        animTimer += Time.fixedDeltaTime;
        if (isMoving)
        {
            if (runSprites != null && runSprites.Length > 0 && animTimer > 0.05f)
            {
                animTimer = 0f;
                animFrame = (animFrame + 1) % runSprites.Length;
                sr.sprite = runSprites[animFrame];
            }
        }
        else
        {
            // Idle
            if (idleSprites != null && idleSprites.Length > 0 && animTimer > 0.25f)
            {
                animTimer = 0f;
                animFrame = (animFrame + 1) % idleSprites.Length;
                sr.sprite = idleSprites[animFrame];
            }
        }

        wasGrounded = true;
    }

    private bool GroundCheck()
    {
        return Physics2D.BoxCast(transform.position, boxSize, 0, -transform.up, maxDistance, layerMask);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawCube(transform.position - transform.up * maxDistance, boxSize);
    }
}
