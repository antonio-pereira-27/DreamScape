using UnityEngine;
using UnityEngine.SceneManagement;
public class CutsceneController : MonoBehaviour
{
    // variables
    [SerializeField] private string nextScene;
    [SerializeField] private float time;


    // Update is called once per frame
    void Update()
    {
        if (time <= 0)
            SceneManager.LoadScene(nextScene);
        else
            time -= Time.deltaTime;
    }
}
