using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{

    bool paused = false;
    public GameObject m_MenuObject;

    private void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            TogglePause();
        }
    }

    public void TogglePause()
    {
        if (paused == false)
        {
            Time.timeScale = 0; //Freeze game
            m_MenuObject.SetActive(true); //Show the pause menu
            paused = true;
        }
        else if (paused == true)
        {
            Time.timeScale = 1; //Unfreeze game
            m_MenuObject.SetActive(false); //
            paused = false;
        }
    }

    public void ReturnToMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0); //Load the main Menu
    }
}
