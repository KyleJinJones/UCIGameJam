using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ObstacleManager : MonoBehaviour {
    public List<int> layerGroup = new List<int>();
    public List<Obstacle> obstacles = new List<Obstacle>();
    // Use this for initialization
    void Start ()
    {

	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.G))
        {
            SwapLayers();
        }
            
	}
    void SwapLayers()
    {
        foreach (Obstacle obstacle in obstacles)
        {
            obstacle.SwapLayer();
        }
    }
}
