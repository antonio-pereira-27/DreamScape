using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonSoundScript : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler
{

    [SerializeField]private EventReference overButton;
    [SerializeField]private EventReference clickButton;
    private SoundPlayer soundPlayer;
    // Start is called before the first frame update
    void Start()
    {
        soundPlayer = gameObject.GetComponent<SoundPlayer>();
    }

    public void OnPointerEnter(PointerEventData eventData){
        soundPlayer.PlayOneShot(overButton);
    }

    public void OnPointerClick(PointerEventData eventData){
        soundPlayer.PlayOneShot(clickButton);
    }

    
}
