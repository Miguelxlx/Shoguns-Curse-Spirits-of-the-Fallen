using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Parameters")]
    [SerializeField] private float speed;
    [SerializeField] private float jumpPower;

    [Header("Coyote Time")]
    [SerializeField] private float coyoteTime; //How much time the player can hang in the air before jumping
    private float coyoteCounter; //How much time passed since the player ran off the edge

    [Header("Multiple Jumps")]
    [SerializeField] private int extraJumps; //How much time the player can hang in the air before jumping
    private int jumpCounter;

    [Header("Wall Jumping")]
    [SerializeField] private float wallJumpX; //How much time the player can hang in the air before jumping
    [SerializeField] private float wallJumpY;

    [Header("Layers")]
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;

    [Header("Player Scale")]
    [SerializeField] private Vector3 playerScale;

    private Rigidbody2D body;
    private Animator anim;
    private BoxCollider2D boxCollider;

    private float wallJumpCooldown;
    private float horizontalInput;

    private bool isRight;

    private void Awake()
    {
        //Grab Referencefrom object
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();

        //Get the scale from the Transform component
        playerScale = transform.localScale;
    }

    private void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");

        //Flip player when moving left and right
        if (horizontalInput > 0.01f)
        {
            isRight = true;
            transform.localScale = playerScale;
        }
        else if (horizontalInput < -0.01f)
        {
            isRight = false;
            transform.localScale = new Vector3(-playerScale.x, playerScale.y, playerScale.z);

        }

        //Set animator parameters
        anim.SetBool("Run", horizontalInput != 0);
        anim.SetBool("Grounded", isGrounded());

        //Jump
        if (Input.GetKeyDown(KeyCode.UpArrow))
            Jump();

        //Adjustable Jump Height
        if (Input.GetKeyUp(KeyCode.UpArrow))
            body.velocity = new Vector2(body.velocity.x, body.velocity.y / 2);

        body.gravityScale = 7;
        body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);

        if (isGrounded())
        {
            coyoteCounter = coyoteTime; //Reset coyote counter when on the ground
            jumpCounter = extraJumps;
        }
        else
            coyoteCounter -= Time.deltaTime;

    }

    private void Jump()
    {
        if (coyoteCounter < 0 && jumpCounter <= 0) return;

        // SoundManager.instance.PlaySound(JumpSound);

        if (isGrounded())
            body.velocity = new Vector2(body.velocity.x, jumpPower);
        else
        {
            //If not on the ground and coyote counter bigger than 0 do a normal jump
            if (coyoteCounter > 0)
                body.velocity = new Vector2(body.velocity.x, jumpPower);
            else
            {
                if (jumpCounter > 0)
                {
                    body.velocity = new Vector2(body.velocity.x, jumpPower);
                    jumpCounter--;
                }
            }
        }

        coyoteCounter = 0;

}

    private bool isGrounded()
    {
        //Checks for collision underneath the player
        RaycastHit2D raycast = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return raycast.collider != null;
    }

    public bool canAttack()
    {
        return true;
    }

    public bool isLookingRight()
    {
        return isRight;
    }
}
