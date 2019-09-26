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
        //Load The Volume Settings From PlayerPrefs (Registry)
        masterVolume.value = PlayerPrefs.GetFloat("masterVol");
        sfxVolume.value = PlayerPrefs.GetFloat("sfxVol");
        musicVolume.value = PlayerPrefs.GetFloat("musicVol");
        drivingVolume.value = PlayerPrefs.GetFloat("masterVol");
        
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

    #region

    public void SetPickupSpawnState(bool newstate) //Toggle weather the game spawns magic mushrooms and other pickups
    {
        PlayerPrefs.SetInt("SpawnPickups", System.Convert.ToInt32(newstate));
        Debug.Log(System.Convert.ToInt32(newstate));
    }

    public void SetVolumeMaster(float volume)
    {
        mixer.SetFloat("masterVol", volume);
        PlayerPrefs.SetFloat("masterVol", volume);
    }

    public void SetVolumeSFX(float volume)
    {
        mixer.SetFloat("sfxVol", volume);
        PlayerPrefs.SetFloat("sfxVol", volume);
    }

    public void SetVolumeMusic(float volume)
    {
        mixer.SetFloat("musicVol", volume);
        PlayerPrefs.SetFloat("musicVol", volume);
    }

    public void SetVolumeDriving(float volume)
    {
        mixer.SetFloat("drivingVol", volume);
        PlayerPrefs.SetFloat("drivingVol", volume);
    }

    #endregion VolumeMethods`

    public void Back()
    {
        m_OptionsMenu.SetActive(false);  //Turn Off the Options Menu
        m_MainMenu.SetActive(true);    //Turn On The Main Menu
    }

}
