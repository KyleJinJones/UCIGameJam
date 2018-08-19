using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {
    public Text timer;
    public int basetime =90;
    int frames = 0;
    
    // Use this for initialization
    void Start () {
        timer.text = basetime+"";	
	}
	
	// Update is called once per frame
	void Update () {
        frames++;
        if (frames > 60)
        {
            frames = 0;
            basetime--;
            timer.text = basetime + "";
        }
	}
}
