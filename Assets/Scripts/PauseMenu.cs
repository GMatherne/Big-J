using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{

    public GameObject pauseMenuUI;
    public GameObject pauseScreen;
    public GameObject settingsScreen;
    public GameObject reticle;

    private void Update(){
        if(Input.GetKeyDown(GameManager.pauseKey)){
            if(GameManager.paused){
                Resume();
            }else{
                Pause();
            }
        }
    }

    private void Resume(){
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        Unfreeze();
    }

    private void Pause(){

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        pauseMenuUI.SetActive(true);
        reticle.SetActive(false);
        
        Time.timeScale = 0f;

        GameManager.paused = true;
    }

    public void Unfreeze(){
        pauseMenuUI.SetActive(false);
        settingsScreen.SetActive(false);
        pauseScreen.SetActive(true);
        reticle.SetActive(true);
        
        Time.timeScale = 1f;

        GameManager.paused = false;
    }

    public void RestartPressed(){
        Unfreeze();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LevelSelectPressed(){
        Unfreeze();
        SceneManager.LoadScene("Level Select");
    }

    public void ResumePressed(){
        Resume();
    }

    public void SettingsPressed(){
        settingsScreen.SetActive(true);
        pauseScreen.SetActive(false);
    }

    public void BackPressed(){
        settingsScreen.SetActive(false);
        pauseScreen.SetActive(true);
    }
}