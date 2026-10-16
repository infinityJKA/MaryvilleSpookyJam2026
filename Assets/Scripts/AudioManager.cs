using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public Sound[] sfxSounds, musicSounds;
    public AudioSource sfxSource, musicSource;

    public void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        PlayMusic("Theme");
    }

    public void StopMusic()
    {
        musicSource.Stop();
    }

    public void PlayMusic(string name)
    {
        Sound s = Array.Find(musicSounds, sound => sound.name == name);

        if(s != null)
        {
            musicSource.clip = s.clip;
            musicSource.Play();
        }
    }

    public void PlaySfx(string name)
    {
        Sound s = Array.Find(sfxSounds, sound => sound.name == name);

        if(s != null)
        {
            sfxSource.PlayOneShot(s.clip);
        }
    }

}

[Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;
}