using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;

public class MusicManager : MonoBehaviour
{
    private float music;
    [SerializeField] private AudioMixerGroup mixer;
    private void Start()
    {
        InstValues();
        InstMusic();
    }

    private void InstValues()
    {
        if (PlayerPrefsSafe.HasKey(MeaningString.music))
            music = PlayerPrefsSafe.GetFloat(MeaningString.music) / 100;
    }

    private void InstMusic()
    {
        mixer.audioMixer.SetFloat("musicVolume", Mathf.Lerp(-80, 0, music));
    }
}
