using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathWall : MonoBehaviour {

    public float wallSpeed = 0.0f;

    private void Update()
    {
        //float x = Time.deltaTime * wallSpeed;
        transform.Translate(wallSpeed, 0, 0);
    }
}
