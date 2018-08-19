using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour {
    //InfoObstacle obInfo;
    public int primaryLayer = 0;
    public int secondaryLayer = 8;
    public GameObject renderObject;
    public Color primaryColor = Color.white;
    public Color secondaryColor = Color.gray;
    private MeshRenderer renderer;
    public bool isPrimary = true;
    // Use this for initialization
    void Start()
    {
        renderer = GetComponent<MeshRenderer>();
        renderer.material.color = primaryColor;
        if (isPrimary)
        {
            gameObject.layer = primaryLayer;
            renderer.material.color = primaryColor;
        }
        else
        {
            gameObject.layer = secondaryLayer;
            renderer.material.color = secondaryColor;
        }
    }

    public void SwapLayer()
    {
        isPrimary = !isPrimary;
        if (isPrimary)
        {
            gameObject.layer = secondaryLayer;
            renderer.material.color = secondaryColor;
        }
        else
        {
            gameObject.layer = primaryLayer;
            renderer.material.color = primaryColor;
        }
            
    }
}
