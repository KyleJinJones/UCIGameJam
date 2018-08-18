using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIFunc : MonoBehaviour {
    public GameObject pauseMenu;
    public GameObject gameoverMenu;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ChangeLevel(int index)
    {
        SceneManager.LoadScene(index);
    }
    public void Pause()
    {
        if (pauseMenu.activeSelf)
        { 
            pauseMenu.SetActive(false);
            Time.timeScale = 1f;
        }
        else
        {
            pauseMenu.SetActive(true);
            Time.timeScale = 0f;
        }
    }
    public void GameOver()
    {
        gameoverMenu.SetActive(true);
    }
    public void Exit()
    {
        Application.Quit();
    }
}
