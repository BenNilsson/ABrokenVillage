using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance = null;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    public AudioMixerGroup MusicMixer;
    public AudioMixerGroup SFXMixer;
    private string currentTrack;
    public AudioSource backgroundMusic;
    private void Start()
    {
        PlayMusic("BackgroundMusic", 0.5f);
    }
    public void PlaySound(string name, float volume)
    {
        AudioSource SFX = (GameObject.Instantiate(Resources.Load("sfx/AudioSource")) as GameObject).GetComponent<AudioSource>();
        SFX.clip = (AudioClip)Resources.Load("sfx/" + name);
        SFX.outputAudioMixerGroup = SFXMixer;
        SFX.volume = volume;
        SFX.Play();
    }
    public void PlaySoundRandomPitch(string name, float volume, float pitch1, float pitch2)
    {
        AudioSource SFX = (GameObject.Instantiate(Resources.Load("sfx/AudioSource")) as GameObject).GetComponent<AudioSource>();
        SFX.clip = (AudioClip)Resources.Load("sfx/" + name);
        SFX.outputAudioMixerGroup = SFXMixer;
        SFX.volume = volume;
        SFX.pitch = (Random.Range(pitch1, pitch2));
        SFX.Play();
    }
    public void PlayMusic(string trackName, float volume)
    {
        if (instance.currentTrack == trackName)
        {
            return;
        }
        AudioSource music = instance.backgroundMusic;
        if (music.isPlaying)
        {
            music.Stop();
        }
        instance.currentTrack = trackName;

        AudioClip clip = (AudioClip)Resources.Load("music/" + trackName);
        music.outputAudioMixerGroup = MusicMixer;
        music.clip = clip;
        music.volume = volume;
        music.Play();
    }
    public static class StopMusic
    {
        public static IEnumerator FadeOut (AudioSource audioSource, float FadeTime)
        {
            float startVolume = audioSource.volume;

            while (audioSource.volume > 0)
            {
                audioSource.volume -= startVolume * Time.deltaTime / FadeTime;
                yield return null;
            }
            audioSource.Stop();
            audioSource.volume = startVolume;
        }
    }
}
