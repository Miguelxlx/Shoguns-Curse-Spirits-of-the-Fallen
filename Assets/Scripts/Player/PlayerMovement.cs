using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Parameters")]
    [SerializeField] private float speed;
    [SerializeField] private float jumpPower;
    [SerializeField] private float gravityScale;

    [Header("Coyote Time")]
    [SerializeField] private float coyoteTime; //How much time the player can hang in the air before jumping
    private float coyoteCounter; //How much time passed since the player ran off the edge

    [Header("Multiple Jumps")]
    [SerializeField] private int extraJumps; //How much time the player can hang in the air before jumping
    private int jumpCounter;

    [Header("Wall Jumping")]
    [SerializeField] private float wallJumpX; //How much time the player can hang in the air before jumping
    [SerializeField] private float wallJumpY;

    [Header("Jump Sound")]
    [SerializeField] private AudioClip JumpSound;

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
        body.gravityScale = gravityScale;

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
        {
            Jump();

            if(Input.GetKeyDown(KeyCode.UpArrow) && !isGrounded() && wallJumpCooldown > 0)
            {
                SoundManager.instance.PlaySound(JumpSound);
            }
        }
        //Adjustable Jump Height
        if (Input.GetKeyUp(KeyCode.UpArrow))
            body.velocity = new Vector2(body.velocity.x, body.velocity.y / 2);

        if (!isGrounded() && onWall() && Input.GetKeyUp(KeyCode.LeftArrow) && Input.GetKeyUp(KeyCode.RightArrow))
        {
            Debug.Log("On Wall");
            body.gravityScale = 0;
            body.velocity = Vector2.zero;
        }
        else
        {
            body.gravityScale = 7;
            body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);

            if (isGrounded())
            {
                coyoteCounter = coyoteTime; //Reset coyote counter when on the ground
                jumpCounter = extraJumps; //Reset jump counter to extra jump value
            }
            else
                coyoteCounter -= Time.deltaTime; //Start decreasing coyote counter when not on the ground
        }

    }

    private void Jump()
    {
        if (coyoteCounter < 0 && jumpCounter <= 0) return;

        if (onWall() && !isGrounded())
            WallJump();
        else
        {
            if (isGrounded())
            {
                body.velocity = new Vector2(body.velocity.x, jumpPower);
            }
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

}

    private bool isGrounded()
    {
        //Checks for collision underneath the player
        RaycastHit2D raycast = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return raycast.collider != null;
    }

    private void WallJump()
    {
        Debug.Log("Wall Jump");
        body.AddForce(new Vector2(-Mathf.Sign(transform.localScale.x) * wallJumpX, wallJumpY));
        wallJumpCooldown = 0;
    }

    private bool onWall()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, new Vector2(transform.localScale.x, 0), 0.1f, wallLayer);
        return raycastHit.collider != null;
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
