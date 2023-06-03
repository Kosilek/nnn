using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SettingsPanel : MonoBehaviour
{
    //Переменные панелей
    [SerializeField] private GameObject menuPanel;
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private GameObject settingsSavePanel;
    //Основные переменные SettingsPanel
    [SerializeField] private Text musicText;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Text soundText;
    [SerializeField] private Slider soundSlider;
    private float musicValue;
    private float soundValue;

    private void Start()
    {
        if (PlayerPrefsSafe.HasKey(MeaningString.music) && PlayerPrefsSafe.HasKey(MeaningString.sound))
        {
            musicValue = PlayerPrefsSafe.GetFloat(MeaningString.music);
            musicSlider.value = musicValue / 100;
            soundValue = PlayerPrefsSafe.GetFloat(MeaningString.sound);
            soundSlider.value = soundValue / 100;
        }
    }

    private void Update()
    {
        musicValue = TransferValue(musicValue, musicText, musicSlider);
        soundValue = TransferValue(soundValue, soundText, soundSlider);
        Debug.Log($"musicPrefs = {PlayerPrefsSafe.GetFloat(MeaningString.music)}");
        Debug.Log($"soundPrefs = {PlayerPrefsSafe.GetFloat(MeaningString.sound)}");
    }

    private float TransferValue(float value, Text text, Slider slider)
    {
        value = slider.value;
        value *= 100;
        value = Mathf.Round(value);
        text.text = value.ToString();  
        return value;
    }

    public void Save()
    {
        PlayerPrefsSafe.SetFloat(MeaningString.music, musicValue);
        PlayerPrefsSafe.SetFloat (MeaningString.sound, soundValue);
    }

    public void Back()
    {
            if (PlayerPrefsSafe.GetFloat(MeaningString.music) ==  musicValue && PlayerPrefsSafe.GetFloat(MeaningString.sound) == soundValue)
            {
                menuPanel.SetActive(true);
                settingsPanel.SetActive(false);            
            }
            else
            {
                settingsSavePanel.SetActive(true);
            } 
    }

    public void Yes() //SettingSavePanel
    {
        if (PlayerPrefsSafe.HasKey(MeaningString.music))
        {
            musicSlider.value = PlayerPrefsSafe.GetFloat (MeaningString.music) / 100;
        }
        else { musicSlider.value = 0;}
        if (PlayerPrefsSafe.HasKey(MeaningString.sound))
        {
            soundSlider.value = PlayerPrefsSafe.GetFloat(MeaningString.sound) / 100;
        }
        else { soundSlider.value = 0;}
        menuPanel.SetActive(true);
        settingsSavePanel.SetActive(false);
        settingsPanel.SetActive(false);
    }

    public void No() //SettingSavePanel
    {
        settingsSavePanel.SetActive(false);
    }
}
