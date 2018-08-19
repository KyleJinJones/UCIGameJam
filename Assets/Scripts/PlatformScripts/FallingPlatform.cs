using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour {

    // Use this for initialization
    Rigidbody2D rb;

    //controls the speed at which the object moves before acceleration
    public float speed = -10;

    //controls the acceleration of the object
    public float acceleration = -.1f;

    //controls time delay before acceleration
    public float delay = 120;

    //increment counter for the time elapsed
    float time = 0;

    //Registers if the player has touched the platform
    bool touched = false;



    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //Raycasts to see if player is underneath
        if (touched == false)
        {
            LayerMask layerMask = 1 << 9;
            if (Physics2D.Raycast(transform.position, transform.TransformDirection(Vector3.up), 5, layerMask))
            {
                touched = true;
            }
        }



        else
        {
            //moves the object
            Vector2 force = new Vector2();
            force.y = speed;
            rb.MovePosition(rb.position + force * Time.deltaTime);

            //increases the timer or the acceleration if the timer exceeds the delay
            if (time >= delay)
            {
                speed = speed + acceleration;
            }
            else
            {
                time++;
            }
        }
    }

}

