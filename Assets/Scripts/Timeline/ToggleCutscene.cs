using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ToggleCutscene : MonoBehaviour
{
    public string cutscene;
    public bool toggle;
    // Update is called once per frame
    void Update()
    {
        if (toggle)
        {
            gameObject.SetActive(false);
            toggle = false;
            SceneManager.LoadScene(cutscene);
        }
    }
}
