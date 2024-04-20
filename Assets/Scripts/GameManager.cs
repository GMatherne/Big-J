using System;
using UnityEngine;
using UnityEngine.Audio;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public static int levelsUnlocked;

    public static float musicVolume;
    public static float soundEffectsVolume;
    
    public static float xSensitivityMultiplier;
    public static float ySensitivityMultiplier;

    public AudioMixer soundEffectsMixer;
    public AudioMixer musicMixer;

    public static bool paused;

    public static KeyCode pauseKey = KeyCode.Escape;
    public static KeyCode shootKey = KeyCode.Mouse0;

    private void Start(){
        levelsUnlocked = 1;

        musicVolume = 0.5f;
        soundEffectsVolume = 0.5f;
        
        xSensitivityMultiplier = 1.05f;
        ySensitivityMultiplier = 1.05f;

        soundEffectsMixer.SetFloat("SoundEffectsMixerVolume", MathF.Log10(0.5f) * 20f);
        musicMixer.SetFloat("MusicMixerVolume", MathF.Log10(0.5f) * 20f);
    }

    private void Awake()
    {

        if(Instance != null){
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

}