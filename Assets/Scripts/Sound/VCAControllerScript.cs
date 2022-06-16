using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VCAControllerScript : MonoBehaviour
{
    private FMOD.Studio.VCA _vcaController;

    private Slider _volumeSlider;
    [SerializeField]
    private string _vcaName;

    private void Start()
    {
        _vcaController = FMODUnity.RuntimeManager.GetVCA("vca:/"+_vcaName);
        _volumeSlider = GetComponent<Slider>();
        float volume;
        _vcaController.getVolume(out volume);
        _volumeSlider.value = volume;
    }

    // Update is called once per frame
    public void SetVolume(float volume)
    {
        _vcaController.setVolume(volume);
    }
}
