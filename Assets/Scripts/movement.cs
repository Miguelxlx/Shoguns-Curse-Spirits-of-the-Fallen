using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5.0f;

    private Rigidbody2D rb;
    private Vector2 movementInput;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // Get input
        movementInput.x = Input.GetAxis("Horizontal");
        movementInput.y = Input.GetAxis("Vertical");
        movementInput.Normalize(); // This ensures diagonal movement isn't faster.
    }

    private void FixedUpdate()
    {
        // Move the character
        rb.MovePosition(rb.position + movementInput * moveSpeed * Time.fixedDeltaTime);
    }
}
