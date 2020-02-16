using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soundtracks : MonoBehaviour
{
	public List<AudioClip> AudioClips = new List<AudioClip>();
	public List<int> PreviousSongs = new List<int>();
	public AudioClip CurrentlyPlaying;
	public AudioSource AudioSources;
	public int SoundSelect;
	public int LastSoundSelect;
	public float Pitch;
	public float CurrentVolume;
	public float Timer;
	public float Cooldown;
    public bool Setup;
	// Use this for initialization
	void Start ()
	{
		Pitch = 1;

		SoundSelect = Random.Range(0, AudioClips.Count - 1);

        if (Setup == true)
        {
            Timer = 1;
        }
        else
        {
            Timer = 60;
        }
	}

	void Update()
	{
		Timer -= 1 * Time.deltaTime;

        if (Setup == true)
        {
            AudioSources.volume = 0.1f;
        }
        else
        {
            AudioSources.volume = Customize.cust.SoundtrackVolume;
        }

		if (Customize.cust.EnableSoundTrack == true||Setup == true)
		{
			AudioSources.mute = false;
			if (Timer <= 0 && !AudioSources.isPlaying)
			{
				if (PreviousSongs.Count > 0)
				{
					if (PreviousSongs.Contains(SoundSelect))
					{
						SoundSelect = Random.Range(0, AudioClips.Count - 1);
					}
					else
					{
						if (SoundSelect != LastSoundSelect)
						{
							Cooldown = Random.Range(30, 300);
							Timer = Cooldown;
							CurrentlyPlaying = AudioClips[SoundSelect];
							LastSoundSelect = SoundSelect;
							PreviousSongs.Add(SoundSelect);
							PlaySound();
						}
					}
				}
				else
				{
					if (SoundSelect != LastSoundSelect)
					{
						Cooldown = Random.Range(30, 300);
						Timer = Cooldown;
						CurrentlyPlaying = AudioClips[SoundSelect];
						LastSoundSelect = SoundSelect;
						PreviousSongs.Add(SoundSelect);
						PlaySound();
					}

					if (Cooldown == 0)
					{
						Cooldown = Random.Range(30, 300);
						Timer = Cooldown;
						CurrentlyPlaying = AudioClips[SoundSelect];
						LastSoundSelect = SoundSelect;
						PreviousSongs.Add(SoundSelect);
						PlaySound();
					}
				}
			}
		}
		else
		{
			AudioSources.mute = true;
		}

		if (AudioSources.isPlaying)
		{
			Cooldown = Random.Range(30, 300);
			Timer = Cooldown;
		}
		else
		{

		}

		if (PreviousSongs.Count > 4)
		{
			PreviousSongs.RemoveAt(0);
		}
	}

	public void PlaySound()
	{
		AudioSources.PlayOneShot (AudioClips [SoundSelect]);
		AudioSources.pitch = Pitch;
	}
}
