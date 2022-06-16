using UnityEngine;
using FMODUnity;

public class SoundPlayer : MonoBehaviour
{
    public void PlayOneShot(EventReference soundToPlay)
    {
        RuntimeManager.PlayOneShot(soundToPlay, gameObject.transform.position);
    }

    public void PlayOneShot(EventReference soundToPlay, string[] attributeToChange, float[] valueToChangeBy)
    {
        FMOD.Studio.EventInstance sound = RuntimeManager.CreateInstance(soundToPlay);
        sound.set3DAttributes(RuntimeUtils.To3DAttributes(gameObject.transform));
        for(int i = 0; i< attributeToChange.Length; i++)
        {
            sound.setParameterByName(attributeToChange[i], valueToChangeBy[i]);
        }
        sound.start();
        sound.release();
    }

    public FMOD.Studio.EventInstance StartSound(EventReference sound)
    {
        FMOD.Studio.EventInstance soundInstance;
        soundInstance = RuntimeManager.CreateInstance(sound);
        soundInstance.start();
        return soundInstance;

    }

    public void StopSound(FMOD.Studio.EventInstance soundInstance)
    {
        soundInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        soundInstance.release();
    }

    public void ChangeSoundAttributes(FMOD.Studio.EventInstance sound, string[] attributeChange, float[] valueToChange)
    {
        sound.set3DAttributes(RuntimeUtils.To3DAttributes(gameObject.transform));
        for(int i = 0; i< attributeChange.Length; i++)
        {
            sound.setParameterByName(attributeChange[i], valueToChange[i]);
        }
    }
}