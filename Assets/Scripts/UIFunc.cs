using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIFunc : MonoBehaviour {
    public static GameObject pauseMenu;
    public static GameObject gameoverMenu;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public static void ChangeLevel(int index)
    {
        SceneManager.LoadScene(index);
    }
    public static void Pause()
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
    public static void GameOver()
    {
        gameoverMenu.SetActive(true);
    }
    public static void Exit()
    {
        Application.Quit();
    }
}
