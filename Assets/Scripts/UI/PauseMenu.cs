using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    // references
    public GameObject pauseMenuUI;
    public GameObject optionsMenuUI;

    // variables
    public static bool gamesIsPaused = false;


    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (gamesIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    void Pause(){
        pauseMenuUI.SetActive(true);
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        Time.timeScale = 0f;
        gamesIsPaused = true;
    }

    public void Resume(){
        pauseMenuUI.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Time.timeScale = 1f;
        gamesIsPaused = false;
    }

    public void OptionsMenu()
    {
        optionsMenuUI.SetActive(true);
        pauseMenuUI.SetActive(false);
    }

    public void SaveGame()
    {
        FindObjectOfType<GameManager>().scene = SceneManager.GetActiveScene().buildIndex;
        DataPersistanceManager.instance.SaveGame();
    }

    public void BackButton()
    {
        optionsMenuUI.SetActive(false);
        pauseMenuUI.SetActive(true);
    }
    public void QuitGame()
    {
        SoundManager.instance.soundPlayer.StopSound(SoundManager.instance.inGameInstance);
        SceneManager.LoadScene(0);
    }


}
