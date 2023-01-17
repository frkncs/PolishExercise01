using System.Collections;
using UnityEngine;

public class AudioReactor : MonoBehaviour
{
    private static AudioReactor _instance;
    
    public static bool isSoundActive = true;
    

    private void Awake()
    {
        _instance = this;
        SetSoundActive(PlayerPrefs.GetInt("Sound", 1) == 1);
    }

    public static void SetSoundActive(bool value) 
    {
        isSoundActive = value;
        PlayerPrefs.SetInt("Sound", isSoundActive ? 1 : 0);
        PlayerPrefs.Save();
    }

    public static void PlaySfx(AudioGroup audioGroup)
    {
        if(isSoundActive) _instance.StartCoroutine(_instance.PlayAudio(audioGroup.Get_Clip(), audioGroup.Get_Vol(), audioGroup.Get_Pitch()));
    }

    public static void PlaySfxOnPosition(AudioGroup audioGroup, Vector3 targetPosition)
    {
        if (isSoundActive) _instance.StartCoroutine(_instance.PlayAudio(audioGroup.Get_Clip(), audioGroup.Get_Vol(), audioGroup.Get_Pitch(), targetPosition));
    }

    public static void PlaySfx(AudioClip audioClip, float volume = 1.0f, float pitch = 1.0f)
    {
        if (isSoundActive) _instance.StartCoroutine(_instance.PlayAudio(audioClip, volume, pitch));
    }

    public IEnumerator PlayAudio(AudioClip clip, float volume, float pitch)
    {
        AudioSource audioSource = _instance.gameObject.AddComponent<AudioSource>();
        audioSource.volume = volume;
        audioSource.pitch = pitch;
        audioSource.clip = clip;
        audioSource.Play();
        yield return new WaitForSecondsRealtime(clip.length + Time.deltaTime);
        Destroy(audioSource);
    }

    public IEnumerator PlayAudio(AudioClip clip, float volume, float pitch, Vector3 targetPosition)
    {
        GameObject audioInstance = new GameObject("sfx");
        AudioSource audioSource = audioInstance.AddComponent<AudioSource>();
        audioSource.spatialize = true;
        audioSource.spatialBlend = 1.0f;
        audioSource.minDistance = 1.0f;
        audioSource.maxDistance = 500.0f;
        audioSource.rolloffMode = AudioRolloffMode.Linear;
        audioSource.volume = volume;
        audioSource.pitch = pitch;
        audioSource.clip = clip;
        //audioInstance = Instantiate(audioInstance, targetPosition, Quaternion.identity);
        audioSource.Play();
        yield return new WaitForSecondsRealtime(clip.length + Time.deltaTime);
        Destroy(audioInstance);
    }
}
