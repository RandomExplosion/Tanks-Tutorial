using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackgroundManager : MonoBehaviour
{
    void Awake()
    {
        //Get a reference to 
        GameObject[] m_existingManagers = GameObject.FindGameObjectsWithTag("BackgroundManager");

        //If there is already an indestructable instance of this object
        if (m_existingManagers.Length > 1)
        {
            Destroy(gameObject); //Destroy this object
        }

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene newscene, LoadSceneMode mode)
    {
        Time.timeScale = 1; //Ensure that the timescale is set to 1
    }

}
