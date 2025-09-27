using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public CharacterController character;


    // Strength of the gravity
    public float grav = -2.0f;

    // WheEEEEeEeeeeE
    public float speedScalar = 1.0f;
    public float frogPower = -2.0f; 

    // The time before another jump can begin
    private float jumpCooldown = 0.0f;

    private Vector2 movement;
    public bool player1 = true;
    private KeyCode[] controls = new KeyCode[4];
    
    void Start()
    {
        character = GetComponent<CharacterController>();
        
        // Creates a set of key codes by player.
        // The movement settings will be dependent on those codes.
        if (player1)
        {
            controls[0] = KeyCode.W;
            controls[1] = KeyCode.A;
            controls[2] = KeyCode.S;
            controls[3] = KeyCode.D;
        } else
        {
            controls[0] = KeyCode.UpArrow;
            controls[1] = KeyCode.LeftArrow;
            controls[2] = KeyCode.DownArrow;
            controls[3] = KeyCode.RightArrow;
        }

    }

    void Update()
    {
        movement.x = 0f;
        movement.y = 0f;

        if (Input.GetKey(controls[0]) && character.isGrounded)
        {
            if (Time.time > jumpCooldown)
            {
                Debug.Log("jump");
                StartCoroutine(Jump());
                jumpCooldown = Time.time + 0.2f;
            }
        }
        if (Input.GetKey(controls[2])) movement.y += grav; //Get to ground faster
        if (Input.GetKey(controls[1])) movement.x += -3f;
        if (Input.GetKey(controls[3])) movement.x += 3f;

        movement.y += grav;

    }

    private void FixedUpdate()
    {
        FinalizeMovement();
    }

    // A coroutine which adds upwards velocity to the character for a few moments.
    System.Collections.IEnumerator Jump()
    {
        // Currently using jump strength as a multiple of gravity
        float jumpStr = grav * frogPower;
        //Debug.Log(jumpStr);
        for (; jumpStr > 0; jumpStr += grav * Time.deltaTime)
        {
            Debug.Log(jumpStr);
            movement.y += jumpStr;
            yield return null;
            if (Time.time > jumpCooldown && character.isGrounded) yield break;
        }

        
    }

    void FinalizeMovement()
    {
        //movement.Normalize();
        //Debug.Log(character.isGrounded + movement.ToString());
        character.Move(movement * speedScalar * Time.deltaTime);
    }
}
