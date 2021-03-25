using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {

    //Variables ---------------------------------
    public static bool isPaused = false;

    public GameObject pauseMenu;
    //Variables ---------------------------------

    //Update ---------------------------------
    void Update () {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                Continue();
            }
            else
            {
                Pause();
            }
        }
	}
    //Update ---------------------------------

    //Pause Game ---------------------------------
    private void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }
    //Pause Game ---------------------------------

    //Checks Menu Selection ---------------------------------
    public void Continue()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    /* //Not necessary as of now
    public void MainMenu()
    {
        //Debug.Log("Returning to Main Menu");
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }
    */

    public void Quit()
    {
        //Debug.Log("Quitting Game");
        Application.Quit();
    }
    //Checks Menu Selection ---------------------------------
}
