using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    public static void QuitPressed(){
       Application.Quit();
    }

    public static void PlayPressed(){
        SceneManager.LoadScene("Level Select");
    }

    public static void SettingsPressed(){
       SceneManager.LoadScene("Settings");
    }

    public static void BackPressed(){
        SceneManager.LoadScene("Main Menu");
    }

    public static void LevelPressed(string level){

        string name = "Level ";

        if(level.Length == 1){
            name += "0";
        }

        name += level;

        SceneManager.LoadScene(name);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}