using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerCutscenes : MonoBehaviour
{

    public bool picked = false;
    [SerializeField]
    private GameObject cutscene;

    void Update()
    {
        if (picked && cutscene != null)
        {
            cutscene.SetActive(true);  
        }

    }


    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Player")
        {
            picked = true;
        }
    }
}
