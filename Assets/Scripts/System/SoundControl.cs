using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundControl : MonoBehaviour
{
	public List<AudioClip> AudioClips = new List<AudioClip>();
	public AudioSource AudioSources;
	public int SoundSelect;
	public float Pitch;
	public float CurrentVolume;
	// Use this for initialization
	void Start ()
	{
		Pitch = 1;
	}

	public void PlaySound()
	{
		AudioSources.PlayOneShot (AudioClips [SoundSelect]);
		AudioSources.pitch = Pitch;
	}

	public void SetVolume()
	{
		AudioSources.volume = Customize.cust.Volume;
	}

    public void SetSetupVolume()
    {
        AudioSources.volume = CurrentVolume;
    }
}
