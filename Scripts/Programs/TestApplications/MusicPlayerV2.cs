using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public class MusicPlayerV2 : MonoBehaviour
{
	public enum SeekDirection { Forward, Backward }

	public AudioSource source;
	public List<AudioClip> clips = new List<AudioClip>();

	public int currentIndex = 0;

	private FileInfo[] soundFiles;
	private List<string> validExtensions = new List<string> { ".ogg", ".wav" }; // Don't forget the "." i.e. "ogg" won't work - cause Path.GetExtension(filePath) will return .ext, not just ext.
	private string absolutePath = "./"; // relative path to where the app is running - change this to "./music" in your case

	public Rect windowRect;
	public float native_width = 1920;
	public float native_height = 1080;
	public int windowID;

	private GameObject Puter;
	private Computer com;

	public bool show;

	public Rect CloseButton;
	public Rect MiniButton;
	public Rect ListButton;

	public bool Paused;

	public float Volume;

	public float CurrentTime;

	public float Min;

	public string Cat;

	public Vector2 scrollpos = Vector2.zero;
	public int scrollsize;

	public int Page;

	public bool ReloadSongFiles;

	public List<string> Files = new List<string>();

	public AudioClip clip;


	enum Menus
	{
		Home,
		Settings,
		List,
	}


	Menus SelectedMenu;

	void Start()
	{
		//being able to test in unity
		//if (Application.isEditor) absolutePath = "Assets/";
		Puter = GameObject.Find("System");
		com = Puter.GetComponent<Computer>();

		if (source == null) source = gameObject.AddComponent<AudioSource>();

		ListButton = new Rect(133, 2, 21, 21);
		MiniButton = new Rect(155, 2, 21, 21);
		CloseButton = new Rect(177, 2, 21, 21);

		Paused = true;

		Volume = Customize.cust.MusicVolume;
		source.volume = Volume;

		windowRect.x = Customize.cust.windowx[windowID];
		windowRect.y = Customize.cust.windowy[windowID];

		if (Customize.cust.SaveSelectedTrack)
		{
			currentIndex = Customize.cust.SelectedTrack;
		}

		windowRect.width = 200;
		windowRect.height = 100;
	}

	void Seek(SeekDirection d)
	{
		if (d == SeekDirection.Forward)
			currentIndex = (currentIndex + 1) % clips.Count;
		else
		{
			currentIndex--;
			if (currentIndex < 0) currentIndex = clips.Count - 1;
		}
	}

	void PlayCurrent()
	{
		clip = clips[currentIndex];
		source.clip = clips[currentIndex];
		source.Play();
		Paused = false;
	}

	void PauseCurrent()
	{
		source.Pause();
		Paused = true;
	}

	void StopCurrent()
	{
		source.Stop();
		Paused = true;
	}


	void AddingSongs()
	{
		//Grabs all files from FileDirectory
		string[] files;
		files = Directory.GetFiles(Customize.cust.MusicPath);

		//Checks all files and stores all WAV files into the Files list.
		for (int i = 0; i < files.Length; i++)
		{
			if (files[i].EndsWith(".wav"))
			{
				Files.Add(files[i]);
				//clips.Add(new WWW(files[i]).GetAudioClip(false, true, AudioType.WAV));

				WWW www = new WWW("file://" + files[i]);

				AudioClip clip = www.GetAudioClip(false);

				clip.name = Path.GetFileName(files[i]);
				clips.Add(clip);
			}

			if (files[i].EndsWith(".ogg"))
			{
				Files.Add(files[i]);
				clips.Add(new WWW(files[i]).GetAudioClip(false, true, AudioType.OGGVORBIS));
			}
		}
	}

	public void PlaySong(int _listIndex)
	{
		clip = clips[_listIndex];
		source.clip = clip;
		source.Play();
	}

	void ReloadSounds()
	{
		clips.Clear();
		// get all valid files
		var info = new DirectoryInfo(Customize.cust.MusicPath);
		soundFiles = info.GetFiles()
			.Where(f => IsValidFileType(f.Name))
			.ToArray();

		// and load them
		foreach (var s in soundFiles)
			StartCoroutine(LoadFile(s.FullName));
	}

	bool IsValidFileType(string fileName)
	{
		return validExtensions.Contains(Path.GetExtension(fileName));
		// Alternatively, you could go fileName.SubString(fileName.LastIndexOf('.') + 1); that way you don't need the '.' when you add your extensions
	}

	IEnumerator LoadFile(string path)
	{
		WWW www = new WWW("file://" + path);

		AudioClip clip = www.GetAudioClip(false);
		while (!clip.isReadyToPlay)
			yield return www;

		clip.name = Path.GetFileName(path);
		clips.Add(clip);
	}

	void MenuSwitcher()
	{
		switch (SelectedMenu)
		{
			case Menus.Home:
				if (clips.Count > 0)
				{
					HomeScreen();
				}
				Cat = "Home";
				if (GUI.Button(new Rect(MiniButton), "S"))
				{
					SelectedMenu = Menus.Settings;
				}

				if (GUI.Button(new Rect(ListButton), "L"))
				{
					SelectedMenu = Menus.List;
				}
				break;
			case Menus.Settings:
				Cat = "Setting";
				SettingsUI();
				if (GUI.Button(new Rect(MiniButton), "H"))
				{
					SelectedMenu = Menus.Home;
				}

				if (GUI.Button(new Rect(ListButton), "L"))
				{
					SelectedMenu = Menus.List;
				}
				break;
			case Menus.List:
				TrackListUI();
				Cat = "List";
				if (GUI.Button(new Rect(MiniButton), "S"))
				{
					SelectedMenu = Menus.Settings;
				}

				if (GUI.Button(new Rect(ListButton), "H"))
				{
					SelectedMenu = Menus.Home;
				}
				break;
		}
	}

	void Update()
	{

		if (ReloadSongFiles == true)
		{
			if (Customize.cust.MusicPath != "")
			{
				AddingSongs();
			}
			ReloadSongFiles = false;
		}

		if (clips.Count > 0)
		{
			if (!source.isPlaying && !Paused)
			{
				Seek(SeekDirection.Forward);
				PlayCurrent();
			}
		}
	}

	void OnGUI()
	{
		Customize.cust.windowx[windowID] = windowRect.x;
		Customize.cust.windowy[windowID] = windowRect.y;

		GUI.skin = GameControl.control.Skins[Registry.GetIntData("Player", "System", "Skin")];

		if (show == true)
		{
			GUI.color = Registry.Get32ColorData("Player", "System", "WindowColor");
			windowRect = WindowClamp.ClampToScreen(GUI.Window(windowID, windowRect, DoMyWindow, ""));
		}
	}

	void TitleBar()
	{
		if (CloseButton.Contains(Event.current.mousePosition))
		{
			if (GUI.Button(new Rect(CloseButton), "X", GameControl.control.Skins[Registry.GetIntData("Player", "System", "Skin")].customStyles[0]))
			{
				show = false;
			}
		}
		else
		{
			GUI.backgroundColor = Registry.Get32ColorData("Player", "System", "ButtonColor");
			GUI.contentColor = Registry.Get32ColorData("Player", "System", "FontColor");
			GUI.Button(new Rect(CloseButton), "X", GameControl.control.Skins[Registry.GetIntData("Player", "System", "Skin")].customStyles[1]);
		}
	}

	void DoMyWindow(int WindowID)
	{
		TitleBar();

		GUI.backgroundColor = Registry.Get32ColorData("Player", "System", "ButtonColor");
		GUI.contentColor = Registry.Get32ColorData("Player", "System", "FontColor");

		GUI.DragWindow(new Rect(2, 2, 130, 21));
		GUI.Box(new Rect(2, 2, 130, 21), "Music Player " + Cat);

		MenuSwitcher();
	}

	void TrackListUI()
	{
		scrollpos = GUI.BeginScrollView(new Rect(2, 25, 195, 75), scrollpos, new Rect(0, 0, 0, scrollsize * 22));
		for (scrollsize = 0; scrollsize < clips.Count; scrollsize++)
		{
			if (GUI.Button(new Rect(0, 22 * scrollsize, 180, 20), "" + clips[scrollsize]))
			{
				currentIndex = scrollsize;
				if (Customize.cust.AutoPlayTrack == true)
				{
					PlayCurrent();
				}
			}
		}

		GUI.EndScrollView();
	}

	void SettingsUI()
	{
		switch (Page)
		{
			case 0:
				Customize.cust.MusicVolume = Volume;
				source.volume = Volume;
				Volume = GUI.HorizontalSlider(new Rect(95, 85, 100, 21), Volume, 0, 1);
				float VolumeUI = Volume * 100;
				GUI.Label(new Rect(106, 67, 100, 21), "Volume: %" + VolumeUI.ToString("F0"));

				source.loop = Customize.cust.LoopTrack;

				Customize.cust.AutoPlayTrack = GUI.Toggle(new Rect(2, 20, 200, 21), Customize.cust.AutoPlayTrack, "Auto play selected track");
				Customize.cust.LoopTrack = GUI.Toggle(new Rect(2, 35, 200, 21), Customize.cust.LoopTrack, "Loop selected track");
				Customize.cust.SaveSelectedTrack = GUI.Toggle(new Rect(2, 50, 200, 21), Customize.cust.SaveSelectedTrack, "Save Track Position");

				if (Customize.cust.SaveSelectedTrack)
				{
					Customize.cust.SelectedTrack = currentIndex;
				}

				if (GUI.Button(new Rect(2, 75, 20, 21), ">"))
				{
					Page = 1;
				}
				break;
			case 1:
				Customize.cust.MusicPath = GUI.TextField(new Rect(2, 25, 150, 21), Customize.cust.MusicPath);
				if (GUI.Button(new Rect(2, 75, 20, 21), "<"))
				{
					Page = 0;
				}
				break;
		}
	}

	void HomeScreen()
	{

		if (clips.Count > 0)
		{
			GUI.TextArea(new Rect(2, 25, 195, 40), "" + clips[currentIndex]);
		}

		if (GUI.Button(new Rect(2, 76, 21, 21), "<<"))
		{
			Seek(SeekDirection.Backward);
			PlayCurrent();
		}

		if (Paused)
		{
			if (GUI.Button(new Rect(24, 76, 21, 21), ">"))
			{
				PlaySong(currentIndex);
			}
		}
		else
		{
			if (GUI.Button(new Rect(24, 76, 21, 21), "||"))
			{
				PauseCurrent();
			}
		}

		if (GUI.Button(new Rect(46, 76, 21, 21), "[]"))
		{
			StopCurrent();
		}

		if (GUI.Button(new Rect(68, 76, 21, 21), ">>"))
		{
			Seek(SeekDirection.Forward);
			PlayCurrent();
		}

		CurrentTime = source.time;
		CurrentTime = GUI.HorizontalSlider(new Rect(95, 85, 100, 21), CurrentTime, 0, clips[currentIndex].length);

		//		if (GUI.Button(new Rect(),"Reload")) 
		//		{
		//			ReloadSounds();
		//		}

		DisplayTime();
	}

	void DisplayTime()
	{
		float AudioLength = clips[currentIndex].length;

		int sminutes = Mathf.FloorToInt(source.time / 60F);
		int sseconds = Mathf.FloorToInt(source.time - sminutes * 60);
		int aminutes = Mathf.FloorToInt(AudioLength / 60F);
		int aseconds = Mathf.FloorToInt(AudioLength - aminutes * 60);
		string niceTime = string.Format("{0:0}:{1:00}", sminutes, sseconds);
		string aniceTime = string.Format("{0:0}:{1:00}", aminutes, aseconds);

		GUI.Label(new Rect(106, 67, 100, 21), niceTime + " / " + aniceTime);
	}
}