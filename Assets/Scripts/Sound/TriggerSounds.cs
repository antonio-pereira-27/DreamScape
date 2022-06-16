using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class TriggerSounds : MonoBehaviour
{
    [SerializeField]private EventReference music;
    private SoundPlayer soundPlayer;

    private void Awake() {
        soundPlayer = gameObject.GetComponent<SoundPlayer>();
    }

    private void OnTriggerEnter(Collider other) {
       if (other.tag == "Player")
       {
           soundPlayer.PlayOneShot(music);
       }
    }
}
