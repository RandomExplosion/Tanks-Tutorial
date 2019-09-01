using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{

    public GameObject m_MenuObject;


    public void TogglePause()
    {
        if (Time.timeScale > 0)
        {
            Time.timeScale = 0; //Freeze game
            m_MenuObject.SetActive(true); //Show the pause menu
        }
        else if (Time.timeScale > 0)
        {
            Time.timeScale = 0; //Unfreeze game
            m_MenuObject.SetActive(false); //
        }
    }
}
