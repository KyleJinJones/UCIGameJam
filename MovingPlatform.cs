using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour {

    public float speed = 1;
    public float distance = 3;
    Rigidbody2D rb;
    private float origposition;
	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
        origposition = transform.position.x;
        
	}
	
	// Update is called once per frame
	void Update () {
        Vector2 force = new Vector2();
        if(transform.position.x-origposition>=distance || (transform.position.x-origposition <=-distance))
        { speed = -speed;
            origposition = transform.position.x;
        }

        force.x = speed;
        rb.MovePosition(rb.position + force * Time.deltaTime);
    }
}
