using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soundManager : MonoBehaviour
{
    public AudioClip buttonAudioClip;
    public AudioClip dimissAudioClip;
    public AudioClip diceAudioClip;
    public AudioClip winAudioClip;
    public AudioClip safeHouseAudioClip;
    public AudioClip playerAudioClip;

    public static AudioSource buttonAudioSource;
    public static AudioSource dimissAudioSource;
    public static AudioSource diceAudioSource;
    public static AudioSource winAudioSource;
    public static AudioSource safeHousAudioSource;
    public static AudioSource playerAudioSource;


    AudioSource AddAudioClip(AudioClip clip,bool playOnAwake,bool loop,float volume)
    {
        AudioSource audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = clip;
        audioSource.playOnAwake = playOnAwake;
        audioSource.loop = loop;
        audioSource.volume = volume;
        return audioSource;
    }
    // Start is called before the first frame update
    void Start()
    {
        buttonAudioSource = AddAudioClip(buttonAudioClip,false,false,1.0f);
        dimissAudioSource = AddAudioClip(dimissAudioClip, false, false, 1.0f);
        diceAudioSource = AddAudioClip(diceAudioClip, false, false, 1.0f);
        winAudioSource = AddAudioClip(winAudioClip, false, false, 1.0f);
        safeHousAudioSource = AddAudioClip(safeHouseAudioClip, false, false, 1.0f);
        playerAudioSource = AddAudioClip(playerAudioClip, false, false, 1.0f);
    }

    
}
