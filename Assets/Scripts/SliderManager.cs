using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SliderManager : MonoBehaviour
{
    [Header("Sliders")]
    public Slider xSensitivitySlider;
    public Slider ySensitivitySlider;
    public Slider soundVolumeSlider;
    public Slider musicVolumeSlider;

    [Header("Audio")]
    public AudioMixer audioMixer;

    private void Start(){
        xSensitivitySlider.value = GameManager.Instance.xSensitivityMultiplier;
        ySensitivitySlider.value = GameManager.Instance.ySensitivityMultiplier;
        musicVolumeSlider.value = GameManager.Instance.musicVolume;
        soundVolumeSlider.value = GameManager.Instance.soundVolume;
    }

    public void SetXSensitivity(float sensitivity){
        GameManager.Instance.xSensitivityMultiplier = sensitivity;
    }

    public void SetYSensitivity(float sensitivity){
        GameManager.Instance.ySensitivityMultiplier = sensitivity;
    }

    public void SetSoundEffectsVolume(float volume){
        GameManager.Instance.soundVolume = volume;
        audioMixer.SetFloat("SoundVolume", MathF.Log10(volume) * 20f + GameManager.Instance.mixerOffset);
    }

    public void SetMusicVolume(float volume){
        GameManager.Instance.musicVolume = volume;
        audioMixer.SetFloat("MusicVolume", MathF.Log10(volume) * 20f + GameManager.Instance.mixerOffset);
    }
}