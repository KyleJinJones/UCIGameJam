using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverManager : MonoBehaviour {

    public GameObject Player;
    public GameObject DeathText;
    public GameObject RestartButton;

    // Update is called once per frame
    void Update () {
		if (Player == null)
        {
            Time.timeScale = 0;
            DeathText.SetActive(true);
            RestartButton.SetActive(true);
        }
	}
}
