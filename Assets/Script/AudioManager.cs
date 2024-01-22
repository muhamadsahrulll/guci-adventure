using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager instance;
    public AudioSource backgroundMusic;
    //public AudioSource soundEffect;

    // Tambahkan semua variabel atau method yang diperlukan untuk mengelola audio di sini

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayBackgroundMusic(AudioClip clip)
    {
        backgroundMusic.clip = clip;
        backgroundMusic.Play();
    }

    public void PlaySoundEffect(AudioClip clip)
    {
        //soundEffect.clip = clip;
        //soundEffect.Play();
    }
}
