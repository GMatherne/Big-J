using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    public void QuitPressed(){
       
    }

    public void PlayPressed(){
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("Level Select"));
    }

    public void SettingsPressed(){
       
    }

    public void MenuBackPressed(){
        
    }

    public void GameBackPressed(){
        
    }

    public void ResumePressed(){

    }
}