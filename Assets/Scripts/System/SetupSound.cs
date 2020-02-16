using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetupSound : MonoBehaviour
{
    public List<AudioClip> AudioClips = new List<AudioClip>();
    public AudioSource AudioSources;
    public int SoundSelect;
    public float Pitch;
    public float CurrentVolume;
    public bool Mute;
    // Use this for initialization
    void Start()
    {
        Pitch = 1;
        CurrentVolume = 0.1f;
        SoundSelect = Random.Range(0, AudioClips.Count);
    }

    private void Update()
    {
        SetVolume();

        if (Mute == true)
        {
            AudioSources.mute = true;
        }
        else
        {
            AudioSources.mute = false;
        }
    }

    public void PlaySound()
    {
        AudioSources.PlayOneShot(AudioClips[SoundSelect]);
        AudioSources.pitch = Pitch;
    }

    public void SetVolume()
    {
        AudioSources.volume = CurrentVolume;
    }
}
