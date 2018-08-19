using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaBar : MonoBehaviour {

    public Slider mainSlider;
    public Text Stext;
    public int Stamina = 0;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            Stamina++;
        }
        Stext.text = 100 - Stamina+"";
        mainSlider.value = Stamina;
	}
}
