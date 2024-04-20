using System.Runtime.Serialization;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public static int levelsUnlocked;

    public static float musicVolume;
    public static float soundEffectsVolume;
    
    public static float xSensitivityMultiplier;
    public static float ySensitivityMultiplier;

    private void Start(){
        levelsUnlocked = 1;

        musicVolume = 1f;
        soundEffectsVolume = 1f;
        
        xSensitivityMultiplier = 1.05f;
        ySensitivityMultiplier = 1.05f;
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