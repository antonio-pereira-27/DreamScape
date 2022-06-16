using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlBloodEffect : MonoBehaviour
{
    Blip blip;
    PlayerCollider playerCollider;

    private void Awake() {
        playerCollider = gameObject.GetComponent<PlayerCollider>();
        blip = gameObject.GetComponentInChildren<Blip>();
    }

    private void Update() {
        if (playerCollider.hited)
        {
            StartCoroutine(ActivateBlood());
        }
    }

    IEnumerator ActivateBlood(){
        blip.enabled = true;
        yield  return new WaitForSeconds(2.0f);
        blip.enabled = false;
        playerCollider.hited = false;
    }
}
