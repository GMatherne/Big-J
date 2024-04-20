using UnityEngine;
using UnityEngine.UI;

public class SliderManager : MonoBehaviour
{
    public Slider xSensitivitySlider;
    public Slider ySensitivitySlider;
    public Slider soundEffectsVolumeSlider;
    public Slider musicVolumeSlider;

    private void Start(){
        xSensitivitySlider.value = GameManager.xSensitivityMultiplier;
        ySensitivitySlider.value = GameManager.ySensitivityMultiplier;
        musicVolumeSlider.value = GameManager.musicVolume;
        soundEffectsVolumeSlider.value = GameManager.soundEffectsVolume;
    }

    public void SetXSensitivity(float sensitivity){
        GameManager.xSensitivityMultiplier = sensitivity;
    }

    public void SetYSensitivity(float sensitivity){
        GameManager.ySensitivityMultiplier = sensitivity;
    }

    public void SetMusicVolume(float volume){
        GameManager.musicVolume = volume;
    }

    public void SetSoundEffectsVolume(float volume){
        GameManager.soundEffectsVolume = volume;
    }
}