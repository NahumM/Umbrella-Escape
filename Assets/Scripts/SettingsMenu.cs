 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField] GameManager gameManagerScript;
    [SerializeField] AudioMixer mixer;
    [SerializeField] GameObject toggleNotification, toggleVibration, sliderEffects, sliderMusic;
    private bool isVibrationOn;
    private bool isNotificationOn;

    private void Start()
    {
        LoadData();
    }


    public void SetMusicVolume(float volume)
    {
        mixer.SetFloat("MusicVolume", volume);
        gameManagerScript.musicVolume = volume;
    }
    
    public void SetEffectsVolume(float volume)
    {
        mixer.SetFloat("EffectsVolume", volume);
        gameManagerScript.effectsVolume = volume;
    }

    public void SetVibrationOn(bool answer)
    {
        gameManagerScript._isVibrationOn = answer;
    }

    public void SetNotificationOn(bool answer)
    {
        isNotificationOn = answer;
    }

    void LoadData()
    {
        ToggleController notScript = toggleNotification.GetComponent<ToggleController>();
        ToggleController vibScript = toggleVibration.GetComponent<ToggleController>();
        notScript._toggle.isOn = new LoadData().GetBool("Notification");
        vibScript._toggle.isOn = new LoadData().GetBool("Vibration");
        notScript.Toggle();
        vibScript.Toggle();
        sliderEffects.GetComponent<Slider>().value = gameManagerScript.effectsVolume;
        sliderMusic.GetComponent<Slider>().value = gameManagerScript.musicVolume;
    }

    void SaveData()
    {
        new SaveData("Vibration", toggleVibration.GetComponent<ToggleController>()._toggle.isOn);
        new SaveData("Notification", toggleNotification.GetComponent<ToggleController>()._toggle.isOn);
    }

    private void OnApplicationFocus(bool focus)
    {
        if (!focus)
            SaveData();
    }

    private void OnDisable() => SaveData();
    private void OnApplicationQuit() => SaveData();

}
