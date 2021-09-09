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

    public float regularGravity;

    public float xOffset;
    public float yOffset;

    public List<Sprite> spriteList;
    public int currentSprite;

    public bool aReady = true;
    public bool dReady = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        currentSprite = 0;
    }

    private void FixedUpdate()
    {
        if (aReady)
        {

        }
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
}
