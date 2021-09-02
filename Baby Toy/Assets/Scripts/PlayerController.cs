using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float groundSpeed;
    public float floatSpeed;

    public float jumpForce;
    private float moveInput;

    private Rigidbody2D rb;

    private bool facingRight = true;

    private bool isGrounded;
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask whatIsGround;

    private int extraJumps;
    public int extraJumpsValue;

    private int extraDashes;
    public int extraDashesValue;
    public float dashDistance;

    public float floatGravity;
    public float regularGravity;

    public float xOffset;
    public float yOffset;

    // Start is called before the first frame update
    void Start()
    {
        extraJumps = extraJumpsValue;
        extraDashes = extraJumpsValue;
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);
        moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);

        if (facingRight == false && moveInput > 0)
        {
            Flip();
        } else if (facingRight == true && moveInput < 0){
            Flip();
        }

    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire3") && extraDashes > 0)
        {
            Dash();
            extraDashes--;
        }

        if (Input.GetButtonDown("Jump") && extraJumps > 0)
        {
            rb.velocity = Vector2.up * jumpForce;
            extraJumps--;
        } else if (Input.GetButtonDown("Jump") && extraJumps == 0 && isGrounded == true)
        {
            rb.velocity = Vector2.up * jumpForce;
        }


        if (Input.GetButton("Jump") && extraJumps == 0 && !isGrounded & rb.velocity.y < 0)
        {
            speed = floatSpeed;
            rb.gravityScale = floatGravity;
        } else
        {
            rb.gravityScale = regularGravity;
        }

        if (isGrounded)
        {
            extraJumps = extraJumpsValue;
            speed = groundSpeed;
            extraDashes = extraDashesValue;
            //rb.gravityScale = 1f;
        }

        Debug.DrawRay(transform.position + new Vector3(xOffset, yOffset), new Vector3(1f, 0f, 0f), Color.green);
        Debug.DrawRay(transform.position + new Vector3(-xOffset, yOffset), new Vector3(-1f, 0f, 0f), Color.red);
    }

    private void Flip()
    {
        facingRight = !facingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }

    private void Dash()
    {
            if (facingRight)
            {
                rb.AddForce(new Vector2(1f * dashDistance, 0f));  
            }  else if (!facingRight){
            rb.AddForce(new Vector2(-1f * dashDistance, 0f));
            }
    }
}
