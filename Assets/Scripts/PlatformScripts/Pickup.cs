using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour {
    public int staminagain = 30;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        

   
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            float stamina = other.GetComponent<StaminaBar>().Stamina;
            if (stamina - staminagain <0)
            {
                other.GetComponent<StaminaBar>().Stamina = 0;
            }
            else
            {
                other.GetComponent<StaminaBar>().Stamina -= staminagain;
            }
            Destroy(this.gameObject);
        }
    }
}
