using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    // References
    public static SoundManager instance;
    [HideInInspector]public SoundPlayer soundPlayer;
    [HideInInspector]public FMOD.Studio.EventInstance inGameInstance;
    [SerializeField]private EventReference inGameMusic;

    private GameObject player;
    private GameObject enemy;

    private GameObject boss;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        boss = GameObject.FindWithTag("Boss");
        enemy = GameObject.FindWithTag("Enemy");

         if (instance == null)
        {
            DontDestroyOnLoad(this.gameObject);
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        soundPlayer = gameObject.GetComponent<SoundPlayer>();
        print(PlaybackState(inGameInstance));

        if (PlaybackState(inGameInstance) != FMOD.Studio.PLAYBACK_STATE.PLAYING)
        {
            
            inGameInstance = soundPlayer.StartSound(inGameMusic);
             print(PlaybackState(inGameInstance));
        }
        else{
            print("already playing");
        }
        
        
    }

    private void Update() {
        float life = 100f;
        if (player != null)
        {
            life = player.GetComponent<PlayerCollider>().currentHealth;
        }
        if (enemy != null && player != null)
            ChangeAttributes(life, Vector3.Distance(enemy.transform.position, player.transform.position) + 50f);
        if(enemy != null && player != null)
            ChangeAttributes(life, Vector3.Distance(boss.transform.position, player.transform.position) + 50f);
        if(enemy == null && boss == null)
            ChangeAttributes(life, 0);
    }

    FMOD.Studio.PLAYBACK_STATE PlaybackState(FMOD.Studio.EventInstance instance)
    {
        FMOD.Studio.PLAYBACK_STATE playback_state;
        instance.getPlaybackState(out playback_state);
        return playback_state;
    }

    private void ChangeAttributes(float life, float distance)
    { 
        inGameInstance.setParameterByName("Vida", life);
        inGameInstance.setParameterByName("Danger", distance);
        inGameInstance.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));
    }

}
