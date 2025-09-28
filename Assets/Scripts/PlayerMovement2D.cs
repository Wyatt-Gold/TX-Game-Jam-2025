using UnityEngine;
using UnityEngine.TextCore.Text;

public class PlayerMovement2D : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] float maxDistance;
    [SerializeField] Vector3 boxSize;
    [SerializeField] LayerMask layerMask;
    public float moveSpeed = 3f;

    //jumping strength
    public float frogPower = 2;
    private float jumpCooldown = 0.3f;
    private float joc = 0;

    private Vector2 movement;
    public bool player1 = true;
    public bool Player2IJKLControls = false;
    private KeyCode[] controls = new KeyCode[4];

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
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
        } else
        {
            controls[0] = KeyCode.UpArrow;
            controls[1] = KeyCode.LeftArrow;
            controls[2] = KeyCode.DownArrow;
            controls[3] = KeyCode.RightArrow;
        }

        
    }

    void FixedUpdate()
    {
        movement.x = 0;

        // Jumping
        if (Input.GetKey(controls[0]) && GroundCheck())
        {
            if (Time.time > joc)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, frogPower);
                joc = Time.time + jumpCooldown;
            }
        }

        // Fast fall
        if (Input.GetKey(controls[2]))
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y - 1f);
        }

        // Left/right movement
        if (Input.GetKey(controls[1])) movement.x = -1 * moveSpeed;
        if (Input.GetKey(controls[3])) movement.x = moveSpeed;

        // Apply horizontal movement while preserving vertical motion
        rb.linearVelocity = new Vector2(movement.x, rb.linearVelocity.y);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawCube(transform.position - transform.up * maxDistance, boxSize);
    }
    private bool GroundCheck()
    {
        if (Physics2D.BoxCast(transform.position, boxSize, 0, -transform.up, maxDistance, layerMask))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
