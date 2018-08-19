using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trampoline : MonoBehaviour {

    private AudioSource SoundSource;

    Rigidbody2D rb;

    public float xForce = 0.0f;
    public float yForce = 0.0f;

    private void Start() {
        SoundSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Vector2 launcher = new Vector2(xForce, yForce);
        rb = collision.GetComponent<Rigidbody2D>();

        rb.velocity = new Vector2(0, 0);

        rb.AddForce(launcher, ForceMode2D.Impulse);

        SoundSource.Play();
    }
}
