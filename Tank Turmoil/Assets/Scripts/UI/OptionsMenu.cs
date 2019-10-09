using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class OptionsMenu : MonoBehaviour
{
    public GameObject m_OptionsMenu;        //Container for the Options Menu
    public GameObject m_MainMenu;           //Container for the Game's Menu
    public AudioMixer mixer;                //The Mixer for the Game's Audio
    public Slider masterVolume;             //Slider in options menu for the Master Volume
    public Slider sfxVolume;                //Slider in options menu for the SFX Volume
    public Slider musicVolume;              //Slider in options menu for the Music Volume
    public Slider drivingVolume;            //Slider in options menu for the Driving Volume
    public TMP_Dropdown resolutionDropdown; //Dropdown to display the current resolution
    public TMP_Dropdown qualityDropdown;    //Dropdown to display the current graphics quality
    public Toggle pickupToggle;             //Toggle to enable pickup spawning

    private Resolution[] resolutions;   //Array for all the resolutions that are valid for the player's screen

    private void Start()
    {
        

        #region Volume Init

        //MASTER VOLUME
        if (PlayerPrefs.HasKey("masterVol"))
        {
            masterVolume.value = PlayerPrefs.GetFloat("masterVol"); //Load the data
            mixer.SetFloat("masterVol", Mathf.Log10(PlayerPrefs.GetFloat("masterVol")) * 20);
        }
        else
        {
            PlayerPrefs.SetFloat("masterVol", masterVolume.value); //Default it to max
            mixer.SetFloat("masterVol", Mathf.Log10(masterVolume.value) * 20);
        }
        
        //MUSIC VOLUME
        if (PlayerPrefs.HasKey("musicVol"))
        {
            musicVolume.value = PlayerPrefs.GetFloat("musicVol"); //Load the data
            mixer.SetFloat("musicVol", Mathf.Log10(PlayerPrefs.GetFloat("musicVol")) * 20);
        }
        else
        {
            PlayerPrefs.SetFloat("musicVol", musicVolume.value); //Default it to max
            mixer.SetFloat("musicVol", Mathf.Log10(musicVolume.value) * 20);
        }

        //SFX VOLUME
        if (PlayerPrefs.HasKey("sfxVol"))
        {
            sfxVolume.value = PlayerPrefs.GetFloat("sfxVol"); //Load the data
            mixer.SetFloat("sfxVol", Mathf.Log10(PlayerPrefs.GetFloat("sfxVol")) * 20);
        }
        else
        {
            PlayerPrefs.SetFloat("sfxVol", sfxVolume.value); //Default it to max
            mixer.SetFloat("sfxVol", Mathf.Log10(sfxVolume.value) * 20);
        }

        //DRIVING VOLUME
        if (PlayerPrefs.HasKey("drivingVol"))
        {
            drivingVolume.value = PlayerPrefs.GetFloat("drivingVol"); //Load the data
            mixer.SetFloat("drivingVol", Mathf.Log10(PlayerPrefs.GetFloat("drivingVol")) * 20);
        }
        else
        {
            PlayerPrefs.SetFloat("drivingVol", drivingVolume.value); //Default it to max
            mixer.SetFloat("drivingVol", Mathf.Log10(drivingVolume.value) * 20);
        }

        #endregion Volume Init


        //If there is data in the registry for this
        if (PlayerPrefs.HasKey("SpawnPickups"))
        {
            //Load the pickup toggle state from PlayerPrefs (Registry)
            pickupToggle.isOn = System.Convert.ToBoolean(PlayerPrefs.GetInt("SpawnPickups"));
        }
        
        else //Default it to false and create a registry key
        {
            pickupToggle.isOn = false;
            PlayerPrefs.SetInt("SpawnPickups", 0);
        }

        #region Configure Resolution Dropdown
        resolutions = Screen.resolutions;           //Get all valid game resolutions    
        resolutionDropdown.ClearOptions();          //Clear all options from the resolutiodropdown
        List<string> options = new List<string>();  //Make A list to store the options as strings
        int currentResolutionIndex = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;   //Convert the resolution to a readable string
            options.Add(option);                                                    //Add the string to the list

            if (resolutions[i].width == Screen.currentResolution.width &&           //Check if i is equal to the current resolution
            resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;                                         //Take a Guess
            }
        }

        resolutionDropdown.AddOptions(options);                                     //Add the options to the dropdown
        resolutionDropdown.value = currentResolutionIndex;                          //Automatically set the dropdown value to the current resolution


        //Update the dropdown ui for both dropdowns
        resolutionDropdown.RefreshShownValue();
        #endregion

        #region Configure Quality Presets Dropdown
        string[] qualityOptionsArr = QualitySettings.names;                         //Get all available quality preset names as an array
        List<string> qualityOptionsLis = new List<string>();                        //Create list to store values from the array       
        for (int i = 0; i < qualityOptionsArr.Length; i++)                          //Copy Array to list (Converting everything to uppercase for the sake of the font >_<)
        {
            qualityOptionsLis.Add(qualityOptionsArr[i].ToUpper());                            
        }
        
        qualityDropdown.ClearOptions();                                             //Clear all options from the dropdown
        qualityDropdown.AddOptions(qualityOptionsLis);                              //Add the options from the list to the dropdown

        qualityDropdown.value = QualitySettings.GetQualityLevel();                  //Set the quality dropdown's value to match the current quality setting
        qualityDropdown.RefreshShownValue();  
        #endregion
    }

    public void SetResolution()
    {
        Screen.SetResolution(resolutions[resolutionDropdown.value].width, resolutions[resolutionDropdown.value].height, true);
    }

    public void SetGraphicsQuality()
    {
        QualitySettings.SetQualityLevel(qualityDropdown.value);
    }

    public void SetPickupSpawnState(bool newstate) //Toggle weather the game spawns magic mushrooms and other pickups
    {
        PlayerPrefs.SetInt("SpawnPickups", System.Convert.ToInt32(newstate));
        Debug.Log(System.Convert.ToInt32(newstate));
    }

    #region VolumeMethods

    public void SetVolumeMaster(/*float value*/)
    {
        mixer.SetFloat("masterVol", Mathf.Log10(masterVolume.value) * 20);
        PlayerPrefs.SetFloat("masterVol", masterVolume.value);
    }

    public void SetVolumeSFX(/*float value*/)
    {
        mixer.SetFloat("sfxVol", Mathf.Log10(sfxVolume.value) * 20);
        PlayerPrefs.SetFloat("sfxVol", sfxVolume.value);
    }

    public void SetVolumeMusic(/*float value*/)
    {
        mixer.SetFloat("musicVol", Mathf.Log10(musicVolume.value) * 20);
        PlayerPrefs.SetFloat("musicVol", musicVolume.value);
    }

    public void SetVolumeDriving(/*float value*/)
    {
        mixer.SetFloat("drivingVol", Mathf.Log10(drivingVolume.value) * 20);
        PlayerPrefs.SetFloat("drivingVol", drivingVolume.value);
    }

    #endregion VolumeMethods

    public void Back()
    {
        m_OptionsMenu.SetActive(false);  //Turn Off the Options Menu
        m_MainMenu.SetActive(true);    //Turn On The Main Menu
    }

}
