using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public Sound[] sfxSounds;
    public AudioSource sfxSource;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        sfxSource.volume = (float)PlayerPrefs.GetInt("volumeValue")/100.0f;
    }
    public void PlaySFX(string sfxName)
    {
        Sound s=Array.Find(sfxSounds, x => x.audioName == sfxName);

        if (s == null)
        {
            Debug.Log("Sound Not Found");
        }
        else
        {
            sfxSource.PlayOneShot(s.audioClip);
        }
    }
}
