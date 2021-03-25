using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    //Checks Menu Selection ---------------------------------
    public void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ControlsScheme()
    {

    }

    public void ExitGame()
    {
        //Debug.Log("Quit");
        Application.Quit();
    }
    //Checks Menu Selection ---------------------------------
}
