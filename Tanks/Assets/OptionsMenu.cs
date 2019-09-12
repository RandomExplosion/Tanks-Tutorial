using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    public GameObject m_OptionsMenu;
    public GameObject m_MainMenu;
    public AudioMixer mixer;
    public Slider masterVolume;
    public Slider sfxVolume;
    public Slider musicVolume;
    public Slider drivingVolume;

    private void Start()
    {
        //Load The Volume Settings From PlayerPrefs
        masterVolume.value = PlayerPrefs.GetFloat("masterVol");
        sfxVolume.value = PlayerPrefs.GetFloat("sfxVol");
        musicVolume.value = PlayerPrefs.GetFloat("musicVol");
        drivingVolume.value = PlayerPrefs.GetFloat("masterVol");
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

    public void SetGraphicsQuality(int qualityindex)
    {
        QualitySettings.SetQualityLevel(qualityindex);
    }

    public void Back()
    {
        m_OptionsMenu.SetActive(false);  //Turn Off the Options Menu
        m_MainMenu.SetActive(true);    //Turn On The Main Menu
    }

}
