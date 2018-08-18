using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float runSpeed = 5.0f;
    public float jumpForce = 20.0f;
    public int jumpLimit = 1;
    public LayerMask whatIsGround;

    private int jumpCount = 0;
    private bool isGrounded = false;
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update ()
    {
        float x = Input.GetAxis("Horizontal") * Time.deltaTime * runSpeed;
        Vector2 y = new Vector2(0.0f, jumpForce);

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded == true)
        {
            rb.AddForce(y, ForceMode2D.Impulse);
        }

        transform.Translate(x, 0, 0);
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
