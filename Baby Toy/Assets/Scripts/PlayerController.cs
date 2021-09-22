using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Animator instructionTextAnimation;

    public float speed;

    private float moveInput;

    private Rigidbody2D rb;
    private SpriteRenderer sr;

    private bool facingRight = true;

    private bool isGrounded;
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask whatIsGround;

    public List<Sprite> spriteList;
    private int currentSprite;

    private bool aReady = true;
    private bool dReady = false;

    public float dashDistance;

    private bool isGrabbed = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        currentSprite = 0;
    }

    public void Grabbed()
    {
        isGrabbed = true;
    }

    public void Dropped()
    {
        isGrabbed = false;
    }    

    private void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);

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

        if (!isGrabbed)
        {
            if (aReady)
            {
                if (Input.GetKeyDown(KeyCode.A))
                {
                    Dash();
                    dReady = true;
                    aReady = false;
                }
            }

            if (dReady)
            {
                if (Input.GetKeyDown(KeyCode.D))
                {
                    Dash();
                    aReady = true;
                    dReady = false;
                }
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                Flip();
            }
        }
        else if (isGrabbed)
        {
            
        }
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
            if (currentSprite >= 4)
            {
                currentSprite = 0;
            }
            sr.sprite = spriteList[currentSprite];
            gameObject.transform.position += new Vector3(dashDistance, 0f, 0f);
        }
        else if (!facingRight)
        {
            currentSprite++;
            if (currentSprite >= 4)
            {
                currentSprite = 0;
            }
            sr.sprite = spriteList[currentSprite];
            gameObject.transform.position += new Vector3(-dashDistance, 0f, 0f);
        }
    }
}
