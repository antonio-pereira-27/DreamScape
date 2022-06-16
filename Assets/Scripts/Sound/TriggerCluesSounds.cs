using FMODUnity;
using UnityEngine;

public class TriggerCluesSounds : MonoBehaviour
{
    [SerializeField]private EventReference music;
    private SoundPlayer soundPlayer;
    private float timer = 0.1f;

    private void Awake() {
        soundPlayer = gameObject.GetComponent<SoundPlayer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.GetComponent<NoteAppear>().reveal)
        {
            if (timer > 0f)
            {
                soundPlayer.PlayOneShot(music);
                timer -= Time.deltaTime;
            }
                
            
        }
    }
}
