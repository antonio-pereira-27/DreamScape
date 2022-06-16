using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using FMODUnity;

public class StartMenu : MonoBehaviour
{
    // References
    [SerializeField]private GameObject optionsMenu;
    [SerializeField]private GameObject startMenu;
    [SerializeField]private GameObject controlsMenu;
    
    
    [SerializeField] private GameObject MusicSlider;
    [SerializeField] private GameObject SoundsSlider;

    private SoundPlayer soundPlayer;

    private FMOD.Studio.EventInstance musicInstance;
    [SerializeField]private EventReference mainMenuMusic;


    

    private void Start() {
        soundPlayer = gameObject.GetComponent<SoundPlayer>();

        startMenu.SetActive(true);
        optionsMenu.SetActive(false);

        musicInstance = soundPlayer.StartSound(mainMenuMusic);
    }

    public void NewGame()
    {
        DataPersistanceManager.instance.NewGame();
        SceneManager.LoadScene("Cutscene1");
        soundPlayer.StopSound(musicInstance);
    }

    public void LoadGame()
    {
        DataPersistanceManager.instance.LoadGame();
        SceneManager.LoadScene(FindObjectOfType<GameManager>().scene);
        soundPlayer.StopSound(musicInstance);
    }

    public void OptionsMenu()
    {
        ChangeAttributes(1);
        startMenu.SetActive(false);
        optionsMenu.SetActive(true);
        
    }

    public void ControlsMenu()
    {
        optionsMenu.SetActive(false);
        controlsMenu.SetActive(true);
        
    }
    
    public void ControlsBack()
    {
        controlsMenu.SetActive(false);
        optionsMenu.SetActive(true);
    }

    public void Back()
    {
        ChangeAttributes(0);
        startMenu.SetActive(true);
        optionsMenu.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    private void ChangeAttributes(int parameter)
    {
        musicInstance.setParameterByName("Parameter 1", parameter);
    }
    
}
