using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    public void QuitPressed(){
       Application.Quit();
    }

    public void PlayPressed(){
        SceneManager.LoadScene("Level Select");
    }

    public void SettingsPressed(){
       SceneManager.LoadScene("Settings");
    }

    public void MenuBackPressed(){
        SceneManager.LoadScene("Main Menu");
    }

    public void GameBackPressed(){
        SceneManager.LoadScene("Level Select");
    }

    public void ResumePressed(){

    }

    public void RestartPressed(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LevelPressed(string level){

        string name = "Level ";

        if(level.Length == 1){
            name += "0";
        }

        name += level;

        SceneManager.LoadScene(name);
    }
}