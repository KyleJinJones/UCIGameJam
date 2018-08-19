using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float runSpeed = 70.0f;
    public float sprintSpeed = 100.0f;
    public float jumpForce = 20.0f;
    public float wallJumpForceX = 5.0f;
    public int jumpLimit = 1;
    public float wallCheckDistance = 1.1f;
    public float wallCheckOffset = 0.0f;
    public LayerMask wallLayerCheck;
    public float speedlimit=10;
    public float sprintspeedlimit = .28f;

    private int jumpCount = 0;
    private bool isGrounded = false;
    private Rigidbody2D rb;
    private StaminaBar sb;
    private float lastxpos;
    private float mass;
    private SpriteRenderer sr;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sb = GetComponent<StaminaBar>();
        sr = GetComponent<SpriteRenderer>();
        lastxpos = transform.position.x;
        mass=rb.mass;
    }

    void FixedUpdate ()
    {
        float deltax = transform.position.x - lastxpos;
        lastxpos = transform.position.x;

        float speed = runSpeed;
        if (Input.GetKey(KeyCode.LeftShift) && sb.Stamina < 100)
        {
            speed = sprintSpeed;
        }

        float x = Input.GetAxis("Horizontal") * speed;
        Vector2 y = new Vector2(0.0f, jumpForce);

        //wall collision detectors
        RaycastHit2D wallHitLeft = Physics2D.Raycast(transform.position, new Vector2(-1.5f, wallCheckOffset), wallCheckDistance, wallLayerCheck);
        RaycastHit2D wallHitRight = Physics2D.Raycast(transform.position, new Vector2(1, wallCheckOffset), wallCheckDistance, wallLayerCheck);

        
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
                rb.velocity = new Vector2(0, rb.velocity.y);
                rb.AddForce(new Vector2(-wallJumpForceX, jumpForce), ForceMode2D.Impulse);
            }
            else if (isGrounded == false && wallHitLeft.collider != null)
            {
                rb.velocity = new Vector2(0, rb.velocity.y);
                rb.AddForce(new Vector2(wallJumpForceX, jumpForce), ForceMode2D.Impulse);
            }
        }

        if (deltax > -speedlimit && deltax < speedlimit)
        {
            rb.AddForce(new Vector2(x, 0));
        }
        else if(speed==sprintSpeed&& deltax > -sprintspeedlimit && deltax < sprintspeedlimit)
        {
            rb.AddForce(new Vector2(x, 0));
        }
        

        if ((deltax > 0 && x < 0) || (deltax < 0 && x > 0))
        {
            rb.AddForce(new Vector2(x, 0));
            
        }

        //Sprite flipping
        if (x > 0)
        {
            sr.flipX = false;
        }
        else
        {
            sr.flipX = true;
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 8)
            isGrounded = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 8)
            isGrounded = false;
    }
}
