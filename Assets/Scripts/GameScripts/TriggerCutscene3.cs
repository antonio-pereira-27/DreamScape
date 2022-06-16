using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class TriggerCutscene3 : MonoBehaviour
{
    [SerializeField] private GameObject videoPlayer;
    [SerializeField] private GameObject rawImage;

    public bool picked = false;
    private float timer = 10f;

    private void Start() {
        
    }

    private void Update() {
        if (picked)
        {
            videoPlayer.SetActive(true);
            rawImage.SetActive(true);
            
        }else
        {
            videoPlayer.SetActive(false);
            rawImage.SetActive(false);
        }
        
    }
}
