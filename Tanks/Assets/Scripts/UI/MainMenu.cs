using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject m_OptionsMenu;
    public GameObject m_MainMenu;

    public void Play()
    {
        SceneManager.LoadScene(1); //Load the Game
    }

    public void Options()
    {
        m_OptionsMenu.SetActive(true);  //Turn On the Options Menu
        m_MainMenu.SetActive(false);    //Turn Off The Main Menu
    }

    public void Quit()
    {
        Application.Quit();
    }
}
