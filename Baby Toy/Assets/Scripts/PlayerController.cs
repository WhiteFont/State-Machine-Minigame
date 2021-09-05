using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Animator instructionTextAnimation;

    public float speed;
    public float groundSpeed;
    public float floatSpeed;

    public float jumpForce;
    private float moveInput;

    private Rigidbody2D rb;
    private SpriteRenderer sr;

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

    public bool aReady;
    public bool dReady;

    public List<Sprite> spriteList;
    public int currentSprite;

    // Start is called before the first frame update
    void Start()
    {
        extraJumps = extraJumpsValue;
        extraDashes = extraJumpsValue;
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        aReady = false;
        dReady = true;
        currentSprite = 0;
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

        if (Input.anyKey && !(Input.GetMouseButton(0) || Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(2) || Input.GetMouseButton(3) || Input.GetMouseButton(4) || Input.GetMouseButtonDown(5) || Input.GetMouseButtonDown(6)))
        {
            instructionTextAnimation.SetBool("FirstInput", true);
        }
        if (dReady)
        {
            if (Input.GetKeyDown(KeyCode.D) && extraDashes > 0)
            {
                Dash();
                extraDashes--;
                aReady = true;
                dReady = false;
            }
        }
        else if (aReady)
        {
            if (Input.GetKeyDown(KeyCode.A) && extraDashes > 0)
            {
                Dash();
                extraDashes--;
                dReady = true;
                aReady = false;
            }
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
                currentSprite++;
                if (currentSprite >= spriteList.Count - 1)
            {
                currentSprite = 0;
            }
            sr.sprite = spriteList[currentSprite];
            rb.AddForce(new Vector2(1f * dashDistance, 0f));  
            }  else if (!facingRight){
            rb.AddForce(new Vector2(-1f * dashDistance, 0f));
            }
    }
}
