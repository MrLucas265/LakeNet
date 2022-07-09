using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using System.IO;
using System.Linq;

public class MediaPlayer : MonoBehaviour
{

	public VideoPlayer video;
	public AudioSource audio;

	private GameObject Prompt;
	private GameObject Computer;
	private GameObject System;

	public int windowID;
	public Rect windowRect = new Rect(100, 100, 512, 512);
	public float native_width = 1920;
	public float native_height = 1080;
	public bool Drag;
	private Computer com;
	public bool show;
	public bool quit;

	private ErrorProm ep;
	private CustomTheme ct;
	private DesktopEnviroment os;
	private Desktop desk;
	private AppMan appman;
	private SoundControl sc;

	public Vector2 scrollpos = Vector2.zero;
	public int scrollsize;

	public Rect Play;
	public Rect Stop;
	public Rect Mute;
	public Rect VolumeSlider;
	public Rect Seeker;

	public bool isDone;

	public bool minimize;

	public List<string> PlayList = new List<string>();

	public List<string> DisplayPlayList = new List<string>();

	public float Seektime;


	public bool NextFrame;
	public bool BackFrame;

	public bool AutoPlay;
	public bool StopOnClose;

	public string RegProgramName;
	public string RegPersonName;

	public FileInfo Filename;

	// Use this for initialization
	void Start()
	{
		RegProgramName = "MediaPlayer";
		RegPersonName = "Player";

		System = GameObject.Find("System");

		video = GetComponent<VideoPlayer>();
		audio = GetComponent<AudioSource>();
		windowRect.width = 260;
		windowRect.height = 220;
		windowRect.x = Customize.cust.windowx[windowID];
		windowRect.y = Customize.cust.windowy[windowID];

		com = System.GetComponent<Computer>();

		appman = System.GetComponent<AppMan>();

		SetPos();

		SetVideoPos();

		if(Registry.GetStringData(RegPersonName, RegProgramName, "Path") == "")
        {
			Registry.SetStringData(RegPersonName, RegProgramName, "Path", "E:/");
		}

		//Registry.CreateNewKeyValue(RegPersonName, RegPersonName, "Looping", "", false, 0, 0);

		AddFileDirectory();

		Registry.SetStringData(RegPersonName, RegProgramName, "Menu", "Home");
	}

	void SetVideoPos()
	{
		Registry.SetRectData(RegPersonName, RegProgramName, "VideoRect", new Rect(0,0,windowRect.width,windowRect.height));
	}

	void SetPos()
	{
		Registry.SetRectData(RegPersonName, RegProgramName, "CloseButton", new Rect(windowRect.width - 21, 1, 21, 20));
		Registry.SetRectData(RegPersonName, RegProgramName, "MiniButton", new Rect(Registry.GetRectData(RegPersonName, RegProgramName, "CloseButton").x - 21, 1, 21, 20));
		Registry.SetRectData(RegPersonName, RegProgramName, "SettingsButton", new Rect(Registry.GetRectData(RegPersonName, RegProgramName, "MiniButton").x - 21, 1, 21, 20));
		Registry.SetRectData(RegPersonName, RegProgramName, "ListButton", new Rect(Registry.GetRectData(RegPersonName, RegProgramName, "SettingsButton").x - 21, 1, 21, 20));
		Registry.SetRectData(RegPersonName, RegProgramName, "HomeButton", new Rect(Registry.GetRectData(RegPersonName, RegProgramName, "ListButton").x - 21, 1, 21, 20));
		Registry.SetRectData(RegPersonName, RegProgramName, "DefaltBoxSetting", new Rect(1, 1, Registry.GetRectData(RegPersonName, RegProgramName, "HomeButton").x - 1, 20));
		Registry.SetRectData(RegPersonName, RegProgramName, "Volume", new Rect(2, windowRect.height - 15, 200, 21));
	}

	// Update is called once per frame
	void Update()
	{
		if(Registry.GetBoolData(RegPersonName,RegProgramName,"LoadPlaylist") == true)
		{
			PlayList.RemoveRange(0, PlayList.Count);
			AddFileDirectory();
			Registry.SetBoolData(RegPersonName, RegProgramName, "LoadPlaylist", false);
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
		foreach (string file in Directory.GetFiles(Registry.GetStringData(RegPersonName, RegProgramName, "Path")))
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

		SetVideoPos();
	}

	void RenderMovie()
	{
		GUI.DrawTexture(new Rect(Registry.GetRectData(RegPersonName, RegProgramName, "VideoRect")), video.targetTexture);
	}

	void RenderControls()
	{
		if (Registry.GetBoolData(RegPersonName, RegProgramName, "LoadVideo"))
		{
			LoadVideoPath(Registry.GetStringData(RegPersonName, RegProgramName, "CurrentFilePath"));
			if (video.controlledAudioTrackCount == 0)
			{
				video.controlledAudioTrackCount = 1;
				video.EnableAudioTrack(0, true);
				video.SetTargetAudioSource(0, audio);
				LoadVideoPath(Registry.GetStringData(RegPersonName, RegProgramName, "CurrentFilePath"));
			}
			if (!audio.isPlaying)
			{
				LoadVideoPath(Registry.GetStringData(RegPersonName, RegProgramName, "CurrentFilePath"));
			}
			if (AutoPlay == false)
			{
				video.Pause();
			}
			Registry.SetBoolData(RegPersonName, RegProgramName, "LoadVideo", false);

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

		if(Registry.GetBoolData(RegPersonName, RegProgramName, "Loop") == true)
		{
			LoopVideo();
			Registry.SetBoolData(RegPersonName, RegProgramName, "Loop", false);
		}

		Registry.SetBoolData(RegPersonName, RegProgramName, "Looping", IsLooping);

		if (!video.isPlaying)
		{
			if (GUI.Button(new Rect(48, 22, 21, 21), ">"))
			{
				if(!IsPrepared)
                {
					Registry.SetBoolData(RegPersonName, RegProgramName, "LoadVideo", true);
					PlayVideo();
				}
				else
                {
					PlayVideo();
				}
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
			Registry.SetIntData(RegPersonName, RegProgramName, "SelectedFile", Registry.GetIntData(RegPersonName, RegProgramName, "SelectedFile")+1);
			if (Registry.GetIntData(RegPersonName, RegProgramName, "SelectedFile") > PlayList.Count)
			{
				Registry.SetIntData(RegPersonName, RegProgramName, "SelectedFile",0);
			}

			Registry.SetStringData(RegPersonName, RegProgramName, "CurrentFilePath",PlayList[Registry.GetIntData(RegPersonName, RegProgramName, "SelectedFile")]);

			Registry.SetBoolData(RegPersonName, RegProgramName, "LoadVideo", true);
		}


		if (GUI.Button(new Rect(24, 22, 21, 21), "<<"))
		{
			if (Registry.GetIntData(RegPersonName, RegProgramName, "SelectedFile") > 0)
			{
				Registry.SetIntData(RegPersonName, RegProgramName, "SelectedFile", Registry.GetIntData(RegPersonName, RegProgramName, "SelectedFile") - 1);
			}

			Registry.SetStringData(RegPersonName, RegProgramName, "CurrentFilePath", PlayList[Registry.GetIntData(RegPersonName, RegProgramName, "SelectedFile")]);

			Registry.SetBoolData(RegPersonName, RegProgramName, "LoadVideo", true);
		}

		float TempVol = Registry.GetFloatData(RegPersonName, RegProgramName, "Volume") * 100;
		Registry.SetFloatData(RegPersonName, RegProgramName, "Volume",GUI.HorizontalSlider(Registry.GetRectData(RegPersonName, RegProgramName, "Volume"), Registry.GetFloatData(RegPersonName, RegProgramName, "Volume"), 0, 1));
		GUI.Label(new Rect(204, windowRect.height - 22, 100, 21), "% " + TempVol.ToString("F0"));
		audio.volume = Registry.GetFloatData(RegPersonName, RegProgramName, "Volume");
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

		Registry.SetStringData(RegPersonName, RegProgramName, "Path", GUI.TextField(new Rect(40, 22, 125, 21), Registry.GetStringData(RegPersonName, RegProgramName, "Path")));

		if (GUI.Button(new Rect(170, 22, 80, 21), "Load"))
		{
			//PlayList.RemoveRange()
			Registry.SetBoolData(RegPersonName, RegProgramName, "LoadPlaylist", true);
		}

		float ButtonWidth = 175;

		if (Registry.GetBoolData(RegPersonName, RegProgramName, "Loop") == true)
		{
			if (GUI.Button(new Rect(2, 44, ButtonWidth, 21), "Loop Enabled"))
			{
				Registry.SetBoolData(RegPersonName, RegProgramName, "Loop", false);
			}
		}
		else
		{
			if (GUI.Button(new Rect(2, 44, ButtonWidth, 21), "Loop Disabled"))
			{
				Registry.SetBoolData(RegPersonName, RegProgramName, "Loop", true);
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
        scrollpos = GUILayout.BeginScrollView(scrollpos, true, true);
        int FirstIndex = (int)(scrollpos.y / 32);
        FirstIndex = Mathf.Clamp(FirstIndex, 0, Mathf.Max(0, PlayList.Count - 10));
        GUILayout.Space(FirstIndex * 32);

        for (int i = FirstIndex; i < Mathf.Min(PlayList.Count, FirstIndex + 10); i++)
        {
            string item = PlayList[i];
            GUILayout.BeginVertical("box", GUILayout.Height(32));

            Filename = new FileInfo(PlayList[i]);
            if (GUILayout.Button(Filename.Name))
            {
                Registry.SetIntData(RegPersonName, RegProgramName, "SelectedFile", i);
                Registry.SetStringData(RegPersonName, RegProgramName, "CurrentFilePath", PlayList[Registry.GetIntData(RegPersonName, RegProgramName, "SelectedFile")]);
                Registry.SetBoolData(RegPersonName, RegProgramName, "LoadVideo", true);
                Registry.SetStringData(RegPersonName, RegProgramName, "Menu", "Home");
            }

            GUILayout.EndVertical();
        }
        GUILayout.Space(Mathf.Max(0, (PlayList.Count - FirstIndex - 10) * 32));
        GUILayout.EndScrollView();

	}

	void DoMyWindow(int WindowID)
	{
		if (Event.current.type == EventType.keyDown && Event.current.keyCode == KeyCode.H)
		{
			Registry.SetBoolData(RegPersonName, RegProgramName, "HideUI", !Registry.GetBoolData(RegPersonName, RegProgramName, "HideUI"));
		}

		if (Registry.GetStringData(RegPersonName, RegProgramName, "Menu") == "Home")
		{
			RenderMovie();
		}

		if (Registry.GetBoolData(RegPersonName, RegProgramName, "HideUI") == false)
		{
			if (Registry.GetRectData(RegPersonName, RegProgramName, "CloseButton").Contains(Event.current.mousePosition))
			{
				if (GUI.Button(Registry.GetRectData(RegPersonName, RegProgramName, "CloseButton"), "X", com.Skin[GameControl.control.GUIID].customStyles[0]))
				{
					if (StopOnClose == true)
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
				GUI.Button(new Rect(Registry.GetRectData(RegPersonName, RegProgramName, "CloseButton")), "X", com.Skin[GameControl.control.GUIID].customStyles[1]);
			}

			if (Registry.GetRectData(RegPersonName, RegProgramName, "MiniButton").Contains(Event.current.mousePosition))
			{
				if (GUI.Button(Registry.GetRectData(RegPersonName, RegProgramName, "MiniButton"), "-", com.Skin[GameControl.control.GUIID].customStyles[2]))
				{
					//minimize = !minimize;
					//Minimize();
				}
			}
			else
			{
				GUI.backgroundColor = com.colors[Customize.cust.ButtonColorInt];
				GUI.contentColor = com.colors[Customize.cust.FontColorInt];
				if (GUI.Button(Registry.GetRectData(RegPersonName, RegProgramName, "MiniButton"), "-", com.Skin[GameControl.control.GUIID].customStyles[2]))
				{
					//minimize = !minimize;
					//Minimize();
				}
			}

			if (GUI.Button(Registry.GetRectData(RegPersonName, RegProgramName, "SettingsButton"), "S"))
			{
				Registry.SetStringData(RegPersonName, RegProgramName, "Menu","Settings");
			}
			if (GUI.Button(Registry.GetRectData(RegPersonName, RegProgramName, "ListButton"), "L"))
			{
				Registry.SetStringData(RegPersonName, RegProgramName, "Menu", "List");
			}
			if (GUI.Button(Registry.GetRectData(RegPersonName, RegProgramName, "HomeButton"), "H"))
			{
				Registry.SetStringData(RegPersonName, RegProgramName, "Menu", "Home");
			}

			switch (Registry.GetStringData(RegPersonName, RegProgramName, "Menu"))
			{
				case "Settings":
					Settings();
					break;
				case "List":
					List();
					break;
				case "Home":
					RenderControls();
					break;
			}

			GUI.DragWindow(Registry.GetRectData(RegPersonName, RegProgramName, "DefaltBoxSetting"));
			GUI.Box(Registry.GetRectData(RegPersonName, RegProgramName, "DefaltBoxSetting"), "Media Player - " + Registry.GetStringData(RegPersonName, RegProgramName, "Menu"));
		}
	}
}
