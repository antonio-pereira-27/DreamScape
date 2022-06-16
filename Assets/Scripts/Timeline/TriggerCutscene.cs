using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class TriggerCutscene : MonoBehaviour
{
    public string cutscene;
    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Player")
        {
            SceneManager.LoadScene(cutscene);
        }
    }
}
