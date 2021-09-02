using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundControl : MonoBehaviour
{
	private GameObject Sounds;
	public List<AudioClip> AudioClips = new List<AudioClip>();
	public AudioSource SystemAudioSource;
	//public AudioSource MusicAudioSource;
	public AudioSource TraceTrackerAudioSource;
	public AudioSource SoundTrackAudioSource;
	public AudioSource NotficationAudioSource;
	public int SoundSelect;
	public float Pitch;
	public float CurrentVolume;
	// Use this for initialization
	void Start ()
	{
		Sounds = GameObject.Find("System");
		SystemAudioSource = Sounds.GetComponent<AudioSource>();

		Sounds = GameObject.Find("Trace Tracker");
		TraceTrackerAudioSource = Sounds.GetComponent<AudioSource>();

		Sounds = GameObject.Find("Soundtracks");
		SoundTrackAudioSource = Sounds.GetComponent<AudioSource>();

		Sounds = GameObject.Find("Notification");
		NotficationAudioSource = Sounds.GetComponent<AudioSource>();

		Pitch = 1;
	}

	public void PlaySound()
	{
		SystemAudioSource.PlayOneShot (AudioClips [SoundSelect]);
		SystemAudioSource.pitch = Pitch;
	}

	public void PlayTraceTrackerSound(int SelectedSound,float Pitch)
	{
		TraceTrackerAudioSource.PlayOneShot(AudioClips[SelectedSound]);
		TraceTrackerAudioSource.pitch = Pitch;
	}

	public void Update()
	{
		SystemAudioSource.volume = Customize.cust.Volume;
		//MusicAudioSource.volume = Customize.cust.MusicVolume;
		TraceTrackerAudioSource.volume = Customize.cust.TraceBeepsVolume;
		SoundTrackAudioSource.volume = Customize.cust.SoundtrackVolume;
		NotficationAudioSource.volume = Customize.cust.NotiVolume;
	}

	public void SetVolume()
	{
		SystemAudioSource.volume = Customize.cust.Volume;
	}

    public void SetSetupVolume()
    {
		SystemAudioSource.volume = CurrentVolume;
    }
}
