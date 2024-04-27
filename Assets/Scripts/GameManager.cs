using System;
using UnityEngine;
using UnityEngine.Audio;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Game")]
    public int levelsUnlocked;
    public bool paused;

    [Header("Audio")]
    public float musicVolume;
    public float soundVolume;
    public float mixerOffset;

    public AudioMixer audioMixer;
    
    [Header("Controls")]
    public float xSensitivityMultiplier;
    public float ySensitivityMultiplier;

    public KeyCode pauseKey = KeyCode.Escape;
    public KeyCode shootKey = KeyCode.Mouse0;

    [Header("Shooting")]
    public float weaponSwapShootingCooldown;
    public bool ableToShoot;
    public bool ableToSwapWeapon;

    private void Start(){
        audioMixer.SetFloat("SoundVolume", MathF.Log10(soundVolume) * 20f + mixerOffset);
        audioMixer.SetFloat("MusicVolume", MathF.Log10(musicVolume) * 20f + mixerOffset);
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