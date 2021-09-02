using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using System.IO;

public class MediaPlayer : MonoBehaviour
{

	public VideoPlayer video;
	public AudioSource audio;

	private GameObject Prompt;
	private GameObject Computer;
	private GameObject System;

	public int windowID;
	public Rect windowRect = new Rect(100, 100, 512, 512);
	public Rect VideoRect = new Rect(100, 100, 512, 512);
	public float native_width = 1920;
	public float native_height = 1080;
	public bool Drag;
	private Computer com;
	private Defalt def;
	public bool show;

	private ErrorProm ep;
	private CustomTheme ct;
	private OS os;
	private Desktop desk;
	private AppMan appman;
	private SoundControl sc;

	public List<Texture2D> BackgroundPics = new List<Texture2D>();

	public Vector2 scrollpos = Vector2.zero;
	public int scrollsize;
	public int Select;

	public Rect CloseButton;
	public Rect MiniButton;
	public Rect SettingsButton;
	public Rect ListButton;
	public Rect HomeButton;
	public Rect DefaltSetting;
	public Rect DefaltBoxSetting;

	public Rect Play;
	public Rect Stop;
	public Rect Mute;
	public Rect VolumeSlider;
	public Rect Seeker;

	public bool isDone;

	public bool minimize;

	public string CurrentFilePath = "";

	public string DirectoryPath = "";

	public List<string> PlayList = new List<string>();

	public bool loadVideo;
	public bool loadPlaylist;

	public int SelectedFile;

	public float Seektime;


	public bool NextFrame;
	public bool BackFrame;
	public bool Loop;
	public bool Looping;

	public bool AutoPlay;
	public bool StopOnClose;

	public string Menu;

	// Use this for initialization
	void Start()
	{
		System = GameObject.Find("System");

		video = GetComponent<VideoPlayer>();
		audio = GetComponent<AudioSource>();
		windowRect.width = 260;
		windowRect.height = 220;
		windowRect.x = Customize.cust.windowx[windowID];
		windowRect.y = Customize.cust.windowy[windowID];
		VideoRect.x = 2;
		VideoRect.y = 0;
		VideoRect.width = 256;
		VideoRect.height = 256;

		com = System.GetComponent<Computer>();

		appman = System.GetComponent<AppMan>();

		SetPos();

		CurrentFilePath = "C:/Users/lucas/Downloads/haachama.mp4";
		DirectoryPath = "F:/";

		AddFileDirectory();

		Menu = "Home";
	}

	void SetPos()
	{
		CloseButton = new Rect(windowRect.width - 21, 1, 21, 20);
		MiniButton = new Rect(CloseButton.x - 21, 1, 21, 20);
		SettingsButton = new Rect(MiniButton.x - 21, 1, 21, 20);
		ListButton = new Rect(SettingsButton.x - 21, 1, 21, 20);
		HomeButton = new Rect(ListButton.x - 21, 1, 21, 20);
		DefaltBoxSetting = new Rect(1, 1, HomeButton.x - 1, 20);
	}

	// Update is called once per frame
	void Update()
	{
		if(loadPlaylist == true)
		{
			PlayList.RemoveRange(0, PlayList.Count);
			AddFileDirectory();
			loadPlaylist = false;
		}
	}

	public bool IsPlaying
	{
		get { return video.isPlaying; }
	}

	public bool IsLooping
	{
		get { return video.isLooping; }
	}

	public bool IsPrepared
	{
		get { return video.isPrepared; }
	}

	public bool IsDone
	{
		get { return isDone; }
	}

	public ulong Duration
	{
		get { return (ulong)(video.frameCount / video.frameRate); }
	}

	public double Time
	{
		get { return video.time; }
	}

	public double NTime
	{
		get { return Time / Duration; }
	}

	void LoadProjectVideo(string name)
	{
		string temp = Application.dataPath + "/Videos/" + name;
		if (video.url == temp) return;

		video.url = temp;
		video.Prepare();
	}

	void AddFileDirectory()
	{
		foreach (string file in Directory.GetFiles(DirectoryPath))
		{ 
			if(file.Contains(".mp4"))
			{
				PlayList.Add(file);
			}
		}
	}

	void LoadVideoPath(string name)
	{
		string temp = name;
		video.url = temp;
		video.Prepare();
	}

	public void PlayVideo()
	{
		if (!IsPrepared) return;
		video.Play();
	}

	public void StopVideo()
	{
		if (!IsPrepared) return;
		video.Stop();
	}

	public void PauseVideo()
	{
		if (!IsPlaying) return;
		video.Pause();
	}

	public void RestartVideo()
	{
		if (IsPrepared) return;
		PauseVideo();
		Seek(0);
	}

	public void LoopVideo()
	{
		if (!IsPrepared) return;
		video.isLooping = !video.isLooping;
	}

	public void Seek(float nTime)
	{
		if (!video.canSetTime) return;
		if (!IsPrepared) return;
		nTime = Mathf.Clamp(nTime, 0, 1);
		video.time = nTime * Duration;
	}

	public void IncrementPlaybackSpeed()
	{
		if (!video.canSetPlaybackSpeed) return;
		video.playbackSpeed += 0.25f;
		video.playbackSpeed = Mathf.Clamp(video.playbackSpeed, 0, 10);
	}

	public void DecrementPlaybackSpeed()
	{
		if (!video.canSetPlaybackSpeed) return;
		video.playbackSpeed -= 0.25f;
		video.playbackSpeed = Mathf.Clamp(video.playbackSpeed, 0, 10);
	}

	public static float ToSingle(double value)
	{
		return (float)value;
	}

	void OnGUI()
	{
		GUI.skin = com.Skin[GameControl.control.GUIID];

		Customize.cust.windowx[windowID] = windowRect.x;
		Customize.cust.windowy[windowID] = windowRect.y;

		if (show == true)
		{
			GUI.color = com.colors[Customize.cust.WindowColorInt];
			windowRect = WindowClamp.ClampToScreen(GUI.Window(windowID, windowRect, DoMyWindow, ""));
		}
	}

	void RenderMovie()
	{
		GUI.DrawTexture(new Rect(VideoRect), video.targetTexture);
	}

	void RenderControls()
	{
		if (loadVideo == true)
		{
			LoadVideoPath(CurrentFilePath);
			if (video.controlledAudioTrackCount == 0)
			{
				video.controlledAudioTrackCount = 1;
				video.EnableAudioTrack(0, true);
				video.SetTargetAudioSource(0, audio);
				LoadVideoPath(CurrentFilePath);
			}
			if (!audio.isPlaying)
			{
				LoadVideoPath(CurrentFilePath);
			}
			if (AutoPlay == false)
			{
				video.Pause();
			}
			loadVideo = false;

		}
		HomeScreen();
	}

	void HomeScreen()
	{
		if(NextFrame == true)
		{
			video.Pause();
			video.frame++;
			NextFrame = false;
		}

		if (BackFrame == true)
		{
			video.Pause();
			video.frame--;
			BackFrame = false;
		}

		if(Loop == true)
		{
			LoopVideo();
			Loop = false;
		}

		Looping = IsLooping;

		if (!video.isPlaying)
		{
			if (GUI.Button(new Rect(48, 22, 21, 21), ">"))
			{
				PlayVideo();
			}
		}
		else
		{
			if (GUI.Button(new Rect(48, 22, 21, 21), "||"))
			{
				PauseVideo();
			}
		}

		if (GUI.Button(new Rect(74, 22, 21, 21), "[]"))
		{
			StopVideo();
		}

		if (GUI.Button(new Rect(96,22, 21, 21), ">>"))
		{
			SelectedFile++;
			if (SelectedFile > PlayList.Count)
			{
				SelectedFile = 0;
			}

			CurrentFilePath = PlayList[SelectedFile];

			loadVideo = true;
		}


		if (GUI.Button(new Rect(24, 22, 21, 21), "<<"))
		{
			if (SelectedFile > 0)
			{
				SelectedFile--;
			}

			CurrentFilePath = PlayList[SelectedFile];

			loadVideo = true;
		}

		float TempVol = audio.volume * 100;
		audio.volume = GUI.HorizontalSlider(new Rect(2, windowRect.height - 15, 200, 21), audio.volume, 0, 1);
		GUI.Label(new Rect(204, windowRect.height - 22, 100, 21), "% " + TempVol.ToString("F0"));

		//var minutes = Duration / 60;
		//var seconds = (Duration % 60);
		string DurationDisplay = FormatTime3(Duration);
		string TimerDisplay = FormatTime3((float)Time);
		GUI.Label(new Rect(windowRect.width - 120, 22, 200, 21), "" + TimerDisplay + "/" + DurationDisplay);
	}

	public string FormatTime(float time)
	{
		int hours = (int)time;
		int minutes = (int)time / 60;
		int seconds = (int)time - 60 * minutes;
		int milliseconds = (int)(1000 * (time - minutes * 60 - seconds));
		return string.Format("{0:00}:{1:00}:{2:00}", hours, minutes, seconds);
	}

	public string FormatTime1(float time)
	{
		double sec = time / 10;
		double min = sec / 60.0d;
		double hr = min / 60.0d;
		double ms = 1000 * (time - min * 60 - sec);

		return string.Format("{0:00}:{1:00}:{2:00}", hr, min, sec);
	}

	public string FormatTime3(float time)
	{
		int minutes = Mathf.FloorToInt((time % 3600F)/60);
		int hours = Mathf.FloorToInt(time / 3600F);
		int seconds = Mathf.FloorToInt(time % 60);
		return string.Format("{0:00}:{1:00}:{2:00}",hours, minutes, seconds);
	}

	public string FormatTime2(float time)
	{
		double sec = time / 10;
		double min = sec / 60.0d;
		double hr = min / 60.0d;
		double ms = 1000 * (time - min * 60 - sec);

		return string.Format("{0:00}:{1:00}:{2:00}", hr, min, sec);
	}

	void Settings()
	{
		GUI.Label(new Rect(2, 22, 60, 21), "Path: ");
		DirectoryPath =  GUI.TextField(new Rect(40, 22, 125, 21), DirectoryPath);

		if (GUI.Button(new Rect(170, 22, 80, 21), "Load"))
		{
			//PlayList.RemoveRange()
			loadPlaylist = true;
		}

		float ButtonWidth = 175;

		if(Loop == true)
		{
			if (GUI.Button(new Rect(2, 44, ButtonWidth, 21), "Loop Enabled"))
			{
				Loop = false;
			}
		}
		else
		{
			if (GUI.Button(new Rect(2, 44, ButtonWidth, 21), "Loop Disabled"))
			{
				Loop = true;
			}
		}

		if (AutoPlay == true)
		{
			if (GUI.Button(new Rect(2, 66, ButtonWidth, 21), "Autoplay Enabled"))
			{
				AutoPlay = false;
			}
		}
		else
		{
			if (GUI.Button(new Rect(2, 66, ButtonWidth, 21), "Autoplay Disabled"))
			{
				AutoPlay = true;
			}
		}

		if (StopOnClose == true)
		{
			if (GUI.Button(new Rect(2, 88, ButtonWidth, 21), "Stop on Close Enabled"))
			{
				StopOnClose = false;
			}
		}
		else
		{
			if (GUI.Button(new Rect(2, 88, ButtonWidth, 21), "Stop on Close Disabled"))
			{
				StopOnClose = true;
			}
		}
	}

	void List()
	{
		scrollpos = GUI.BeginScrollView(new Rect(2,22,250,windowRect.height-21), scrollpos, new Rect(0, 0, 0, scrollsize * 21));
		for (scrollsize = 0; scrollsize < PlayList.Count; scrollsize++)
		{
			if (GUI.Button(new Rect(1, 1 + scrollsize * 21, 225, 20), PlayList[scrollsize]))
			{
				SelectedFile = scrollsize;
				CurrentFilePath = PlayList[SelectedFile];
				loadVideo = true;
				Menu = "Home";
			}
		}
		GUI.EndScrollView();
	}

	void DoMyWindow(int WindowID)
	{
		if (CloseButton.Contains(Event.current.mousePosition))
		{
			if (GUI.Button(new Rect(CloseButton), "X", com.Skin[GameControl.control.GUIID].customStyles[0]))
			{
				if(StopOnClose == true)
				{
					video.Stop();
				}
				appman.SelectedApp = "Media Player";
			}
		}
		else
		{
			GUI.backgroundColor = com.colors[Customize.cust.ButtonColorInt];
			GUI.contentColor = com.colors[Customize.cust.FontColorInt];
			GUI.Button(new Rect(CloseButton), "X", com.Skin[GameControl.control.GUIID].customStyles[1]);
		}

		if (MiniButton.Contains(Event.current.mousePosition))
		{
			if (GUI.Button(new Rect(MiniButton), "-", com.Skin[GameControl.control.GUIID].customStyles[2]))
			{
				//minimize = !minimize;
				//Minimize();
			}
		}
		else
		{
			GUI.backgroundColor = com.colors[Customize.cust.ButtonColorInt];
			GUI.contentColor = com.colors[Customize.cust.FontColorInt];
			if (GUI.Button(new Rect(MiniButton), "-", com.Skin[GameControl.control.GUIID].customStyles[2]))
			{
				//minimize = !minimize;
				//Minimize();
			}
		}

		if (GUI.Button(new Rect(SettingsButton), "S"))
		{
			Menu = "Settings";
		}
		if (GUI.Button(new Rect(ListButton), "L"))
		{
			Menu = "List";
		}
		if (GUI.Button(new Rect(HomeButton), "H"))
		{
			Menu = "Home";
		}

		switch(Menu)
		{
			case "Settings":
				Settings();
				break;
			case "List":
				List();
				break;
			case "Home":
				RenderMovie();
				RenderControls();
				break;
		}

		GUI.DragWindow(new Rect(DefaltBoxSetting));
		GUI.Box(new Rect(DefaltBoxSetting), "Media Player - " + Menu);
	}
}
