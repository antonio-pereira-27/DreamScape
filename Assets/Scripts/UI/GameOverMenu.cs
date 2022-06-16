using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    public void RestartGame()
    {
        SceneManager.LoadScene(InventoryManager.Instance.gameData.scene);
        FindObjectOfType<DataPersistanceManager>().LoadGame();
    }

    public void QuitGame()
    {
        SoundManager.instance.soundPlayer.StopSound(SoundManager.instance.inGameInstance);
        SceneManager.LoadScene(0);
    }
}
