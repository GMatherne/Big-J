using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [Header("Paused Canvas")]
    public GameObject pauseMenuUI;
    public GameObject pauseScreen;
    public GameObject settingsScreen;

    [Header("Game Canvas")]
    public GameObject reticle;

    [Header("Audio")]
    public AudioSource audioSource;

    private void Update(){
        if(Input.GetKeyDown(GameManager.Instance.pauseKey)){
            if(GameManager.Instance.paused){
                Resume();
            }else{
                Pause();
            }
        }
    }

    private void Resume(){

        audioSource.UnPause();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        Unfreeze();
    }

    private void Pause(){

        audioSource.Pause();

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        pauseMenuUI.SetActive(true);
        reticle.SetActive(false);
        
        Time.timeScale = 0f;

        GameManager.Instance.paused = true;
    }

    public void Unfreeze(){
        pauseMenuUI.SetActive(false);
        settingsScreen.SetActive(false);
        pauseScreen.SetActive(true);
        reticle.SetActive(true);
        
        Time.timeScale = 1f;

        GameManager.Instance.paused = false;
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