using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float runSpeed = 5.0f;
    public float jumpStun = 0.25f;
    public float jumpForce = 20.0f;
    public float wallJumpForceX = 5.0f;
    public int jumpLimit = 1;
    public float wallCheckDistance = 1.1f;
    public LayerMask wallLayerCheck;

    private float stunTimer;
    private int jumpCount = 0;
    private bool isGrounded = false;
    private Rigidbody2D rb;

    private void Start()
    {
        stunTimer = jumpStun;
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate ()
    {
        float x = Input.GetAxis("Horizontal") * runSpeed;
        stunTimer += Time.deltaTime;
        Vector2 y = new Vector2(0.0f, jumpForce);

        //wall collision detectors
        RaycastHit2D wallHitLeft = Physics2D.Raycast(transform.position, Vector2.left, wallCheckDistance, wallLayerCheck);
        RaycastHit2D wallHitRight = Physics2D.Raycast(transform.position, Vector2.right, wallCheckDistance, wallLayerCheck);

        //smooth wall sliding
        if (wallHitLeft.collider != null)
        {
            if (x < 0)
                x = 0;
        }
        if (wallHitRight.collider != null)
        {
            if (x > 0)
                x = 0;
        }

        //jumping
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isGrounded)
            {
                rb.AddForce(y, ForceMode2D.Impulse);
            }
            else if (isGrounded == false && wallHitRight.collider != null)
            {
                //stunTimer = 0;
                rb.velocity = new Vector2(0, rb.velocity.y);
                rb.AddForce(new Vector2(-wallJumpForceX, jumpForce), ForceMode2D.Impulse);
            }
            else if (isGrounded == false && wallHitLeft.collider != null)
            {
                //stunTimer = 0;
                rb.velocity = new Vector2(0, rb.velocity.y);
                rb.AddForce(new Vector2(wallJumpForceX, jumpForce), ForceMode2D.Impulse);
            }
        }
        
        rb.AddForce(new Vector2(x, 0));

        //if(stunTimer > jumpStun)
        //    rb.velocity = new Vector2(x, rb.velocity.y);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        isGrounded = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isGrounded = false;
    }
}
