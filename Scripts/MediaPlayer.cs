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

	private GameObject WindowHandel;
	private WindowManager winman;

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
	public string ProgramNameForWinMan;

	public FileInfo Filename;

	// Use this for initialization
	void Start()
	{
		RegProgramName = "MediaPlayer";
		RegPersonName = "Player";
		ProgramNameForWinMan = "Media Player";

		System = GameObject.Find("System");
		WindowHandel = GameObject.Find("WindowHandel");

		video = GetComponent<VideoPlayer>();
		audio = GetComponent<AudioSource>();

		com = System.GetComponent<Computer>();

		appman = System.GetComponent<AppMan>();

		winman = WindowHandel.GetComponent<WindowManager>();

		SetPos();
		SetUIPos();

		if(Registry.GetStringData(RegPersonName, RegProgramName, "Path") == "")
        {
			Registry.SetStringData(RegPersonName, RegProgramName, "Path", "E:/");
		}

		//Registry.CreateNewKeyValue(RegPersonName, RegPersonName, "Looping", "", false, 0, 0);

		AddFileDirectory();

		Registry.SetStringData(RegPersonName, RegProgramName, "Menu", "Home");

        if (Registry.GetBoolData("Player", "MediaPlayer", "Startup") == true)
        {
            TestCode.KeywordCheck("Player", "Run:" + "MediaPlayer" + ";");
        }
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

	void WindowSizeUpdate()
    {
        windowRect.x = Registry.GetRectData(RegPersonName, RegProgramName, "Fullscreen").x;
        windowRect.y = Registry.GetRectData(RegPersonName, RegProgramName, "Fullscreen").y;
        windowRect.width = Registry.GetRectData(RegPersonName, RegProgramName, "Fullscreen").width;
		windowRect.height = Registry.GetRectData(RegPersonName, RegProgramName, "Fullscreen").height;
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
		get { return IsDone; }
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
			if(file.EndsWith(".mp4"))
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
		//video.isLooping = !video.isLooping;
        video.isLooping = Registry.GetBoolData(RegPersonName, RegProgramName, "Loop");
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
		//GUI.depth = -29;
		GUI.skin = GameControl.control.Skins[Registry.GetIntData("Player", "System", "Skin")];

		if (show == true)
		{
			GUI.color = Registry.Get32ColorData("Player", "System", "WindowColor");

			for (int PersonCount = 0; PersonCount < PersonController.control.People.Count; PersonCount++)
			{
				var pwinman = PersonController.control.People[PersonCount].Gateway;

				if (pwinman.RunningPrograms.Count > 0)
				{
					for (int i = 0; i < pwinman.RunningPrograms.Count; i++)
					{
						if (pwinman.RunningPrograms[i].ProgramName == RegProgramName)
						{
							//ColorUI(pwinman.RunningPrograms[i].WPN);
							//GUI.color = new Color32(LocalRegistry.GetRedColorData(PersonName, pwinman.RunningPrograms[i].WPN, ProgramName, "WindowColor"),
							//	LocalRegistry.GetGreenColorData(PersonName, pwinman.RunningPrograms[i].WPN, ProgramName, "WindowColor"),
							//	LocalRegistry.GetBlueColorData(PersonName, pwinman.RunningPrograms[i].WPN, ProgramName, "WindowColor"),
							//	LocalRegistry.GetAlphaColorData(PersonName, pwinman.RunningPrograms[i].WPN, ProgramName, "WindowColor"));

							pwinman.RunningPrograms[i].windowRect = WindowClamp.ClampToScreen(GUI.Window(pwinman.RunningPrograms[i].WID, pwinman.RunningPrograms[i].windowRect, DoMyWindow, ""));

                            if (Registry.GetBoolData(RegPersonName, RegProgramName, "Fullscreen") == true)
							{
                                pwinman.RunningPrograms[i].windowRect.x = Registry.GetRectData(RegPersonName, RegProgramName, "Fullscreen").x;
                                pwinman.RunningPrograms[i].windowRect.y = Registry.GetRectData(RegPersonName, RegProgramName, "Fullscreen").y;
                                pwinman.RunningPrograms[i].windowRect.width = Registry.GetRectData(RegPersonName, RegProgramName, "Fullscreen").width;
								pwinman.RunningPrograms[i].windowRect.height = Registry.GetRectData(RegPersonName, RegProgramName, "Fullscreen").height;
								WindowSizeUpdate();
							}
							else
							{
								windowRect = pwinman.RunningPrograms[i].windowRect;
							}
						}
					}
				}
			}
			QThread.MakeThread(SetPos);
            QThread.MakeThread(SetUIPos);
        }

		//if (IsDone)
  //      {
		//	if(AutoPlay)
  //          {
		//		Registry.SetIntData(RegPersonName, RegProgramName, "SelectedFile", Registry.GetIntData(RegPersonName, RegProgramName, "SelectedFile") + 1);
		//		if (Registry.GetIntData(RegPersonName, RegProgramName, "SelectedFile") > PlayList.Count)
		//		{
		//			Registry.SetIntData(RegPersonName, RegProgramName, "SelectedFile", 0);
		//		}

		//		Registry.SetStringData(RegPersonName, RegProgramName, "CurrentFilePath", PlayList[Registry.GetIntData(RegPersonName, RegProgramName, "SelectedFile")]);

		//		Registry.SetBoolData(RegPersonName, RegProgramName, "LoadVideo", true);

		//		if (!IsPrepared)
		//		{
		//			Registry.SetBoolData(RegPersonName, RegProgramName, "LoadVideo", true);
		//			PlayVideo();
		//		}
		//		else
		//		{
		//			PlayVideo();
		//		}
		//	}
  //      }
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

    //private IEnumerator SeekTimeRoutine()
    //{
    //	Registry.SetBoolData(RegPersonName, RegProgramName, "Seek", true);

    //	// do as instructed in the mail from Valentin Simonov & Dominique Leroux (28.06.2017)
    //	// DominiqueLRX: the idea is to drain the accumulated audio sample queue before seeking
    //	if (video)
    //	{
    //		// stop the player
    //		video.Stop();
    //		if (audio)
    //			audio.Stop();
    //		//isPlaying = false;
    //		yield return null;

    //		// prepare the video player
    //		video.Prepare();

    //		// wait until video is prepared
    //		Debug.Log("Preparing the video...");

    //		//float waitTillTime = Time.time + 5f;  // wait 5 seconds max
    //		while (!video.isPrepared)
    //		{
    //			yield return null;
    //		}

    //		string sMessage = video.isPrepared ? "Video prepared" : "Video NOT prepared";
    //		Debug.Log(sMessage);

    //		// start playing (otherwise it starts from time/frame 0).
    //		video.Play();
    //		video.Pause();

    //		yield return null;

    //		video.time = Seektime;
    //		Debug.Log(string.Format("VideoPlayer time set to: {0:F3}", Seektime));
    //		Seektime = -1.0f;
    //		//yield return null;
    //	}

    //	Registry.SetBoolData(RegPersonName, RegProgramName, "Seek", false);
    //}


    void SetPos()
    {
        Registry.SetRectData(RegPersonName, RegProgramName, "CloseButton", new Rect(windowRect.width - 21, 1, 21, 20));
        Registry.SetRectData(RegPersonName, RegProgramName, "MiniButton", new Rect(Registry.GetRectData(RegPersonName, RegProgramName, "CloseButton").x - 21, 1, 21, 20));
        Registry.SetRectData(RegPersonName, RegProgramName, "SettingsButton", new Rect(Registry.GetRectData(RegPersonName, RegProgramName, "MiniButton").x - 21, 1, 21, 20));
        Registry.SetRectData(RegPersonName, RegProgramName, "ListButton", new Rect(Registry.GetRectData(RegPersonName, RegProgramName, "SettingsButton").x - 21, 1, 21, 20));
        Registry.SetRectData(RegPersonName, RegProgramName, "HomeButton", new Rect(Registry.GetRectData(RegPersonName, RegProgramName, "ListButton").x - 21, 1, 21, 20));
        Registry.SetRectData(RegPersonName, RegProgramName, "DefaltBoxSetting", new Rect(1, 1, Registry.GetRectData(RegPersonName, RegProgramName, "HomeButton").x - 1, 20));
        Registry.SetRectData(RegPersonName, RegProgramName, "VideoRect", new Rect(0, 0, windowRect.width, windowRect.height));
    }

    //void SetUIPos()
    //{
    //    Registry.SetRectData(RegPersonName, RegProgramName, "PlayUI", new SRect(new Rect(48, 22, 21, 21)));
    //    Registry.SetRectData(RegPersonName, RegProgramName, "FowardUI", new SRect(new Rect(96, 22, 21, 21)));
    //    Registry.SetRectData(RegPersonName, RegProgramName, "BackUI", new SRect(new Rect(24, 22, 21, 21)));
    //    Registry.SetRectData(RegPersonName, RegProgramName, "StopUI", new SRect(new Rect(74, 22, 21, 21)));
    //    Registry.SetRectData(RegPersonName, RegProgramName, "LoopUI", new SRect(new Rect(118, 22, 21, 21)));
    //    Registry.SetRectData(RegPersonName, RegProgramName, "MuteUI", new SRect(new Rect(2, windowRect.height - 23, 21, 21)));
    //    var MuteUIPos = Registry.GetRectData(RegPersonName, RegProgramName, "MuteUI");
    //    Registry.SetRectData(RegPersonName, RegProgramName, "Volume", new Rect(MuteUIPos.width + 5, windowRect.height - 20, 100, 21));
    //    var VolumeUIPos = Registry.GetRectData(RegPersonName, RegProgramName, "Volume");
    //    Registry.SetRectData(RegPersonName, RegProgramName, "Seek", new Rect(5, windowRect.height - 60, windowRect.width - 10, 21));
    //    Registry.SetRectData(RegPersonName, RegProgramName, "VolumeTextUI", new Rect(VolumeUIPos.x + VolumeUIPos.width + 5, VolumeUIPos.y - 5, 100, 21));
    //}

    void SetUIPos()
	{
        var MuteUIPos = Registry.GetRectData(RegPersonName, RegProgramName, "MuteUI");
        var VolumeUIPos = Registry.GetRectData(RegPersonName, RegProgramName, "Volume");
        var PlayUIPos = Registry.GetRectData(RegPersonName, RegProgramName, "PlayUI");
        var BackUIPos = Registry.GetRectData(RegPersonName, RegProgramName, "BackUI");
        var StopUIPos = Registry.GetRectData(RegPersonName, RegProgramName, "StopUI");
        //This makes PlayPosMath exactly the half way point between the seekbar and the window height
        var SeekUIPos = Registry.GetRectData(RegPersonName, RegProgramName, "Seek");
        var PlayPosMath = windowRect.height - (windowRect.height - SeekUIPos.y) / 2;

		var CenterMath = windowRect.width / 2;

        Registry.SetRectData(RegPersonName, RegProgramName, "MuteUI", new SRect(new Rect(windowRect.width-180, windowRect.height - 23, 21, 21)));
        Registry.SetRectData(RegPersonName, RegProgramName, "Volume", new Rect(windowRect.width - 150, windowRect.height - 20, 100, 21));
        Registry.SetRectData(RegPersonName, RegProgramName, "Seek", new Rect(5, windowRect.height - 60, windowRect.width - 10, 21));
        Registry.SetRectData(RegPersonName, RegProgramName, "VolumeTextUI", new Rect(VolumeUIPos.x+VolumeUIPos.width+5, VolumeUIPos.y-5, 100, 21));

        Registry.SetRectData(RegPersonName, RegProgramName, "PlayUI", new SRect(new Rect(CenterMath, PlayPosMath, 21, 21)));
        Registry.SetRectData(RegPersonName, RegProgramName, "FowardUI", new SRect(new Rect(PlayUIPos.x+21+2+10, PlayPosMath, 21, 21)));
        Registry.SetRectData(RegPersonName, RegProgramName, "BackUI", new SRect(new Rect(PlayUIPos.x-21-2-10, PlayPosMath, 21, 21)));
        Registry.SetRectData(RegPersonName, RegProgramName, "StopUI", new SRect(new Rect(BackUIPos.x-21-2-10, PlayPosMath, 21, 21)));
        Registry.SetRectData(RegPersonName, RegProgramName, "LoopUI", new SRect(new Rect(StopUIPos.x-21-2-10, PlayPosMath, 21, 21)));
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

		LoopVideo();

        //if(Registry.GetBoolData(RegPersonName, RegProgramName, "Loop") == true)
        //{
        //	LoopVideo();
        //	Registry.SetBoolData(RegPersonName, RegProgramName, "Loop", false);
        //}

        //Registry.SetBoolData(RegPersonName, RegProgramName, "Looping", IsLooping);

        if (!video.isPlaying)
		{
			if (GUI.Button(Registry.GetRectData(RegPersonName, RegProgramName, "PlayUI"), ">"))
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
			if (GUI.Button(Registry.GetRectData(RegPersonName, RegProgramName, "PlayUI"), "||"))
			{
				PauseVideo();
			}
		}

		if (GUI.Button(Registry.GetRectData(RegPersonName, RegProgramName, "StopUI"), "[]"))
		{
			StopVideo();
		}

		if (Registry.GetBoolData(RegPersonName, RegProgramName, "Loop") == true)
		{
			if (GUI.Button(Registry.GetRectData(RegPersonName, RegProgramName, "LoopUI"), "(<->)"))
			{
				Registry.SetBoolData(RegPersonName, RegProgramName, "Loop", !Registry.GetBoolData(RegPersonName, RegProgramName, "Loop"));
			}
		}
		else
		{
			if (GUI.Button(Registry.GetRectData(RegPersonName, RegProgramName, "LoopUI"), "(<X>)"))
			{
				Registry.SetBoolData(RegPersonName, RegProgramName, "Loop", !Registry.GetBoolData(RegPersonName, RegProgramName, "Loop"));
			}
		}

		if (GUI.Button(Registry.GetRectData(RegPersonName, RegProgramName, "FowardUI"), ">>"))
		{
			Registry.SetIntData(RegPersonName, RegProgramName, "SelectedFile", Registry.GetIntData(RegPersonName, RegProgramName, "SelectedFile")+1);
			if (Registry.GetIntData(RegPersonName, RegProgramName, "SelectedFile") > PlayList.Count)
			{
				Registry.SetIntData(RegPersonName, RegProgramName, "SelectedFile",0);
			}

			Registry.SetStringData(RegPersonName, RegProgramName, "CurrentFilePath",PlayList[Registry.GetIntData(RegPersonName, RegProgramName, "SelectedFile")]);

			Registry.SetBoolData(RegPersonName, RegProgramName, "LoadVideo", true);
		}


		if (GUI.Button(Registry.GetRectData(RegPersonName, RegProgramName, "BackUI"), "<<"))
		{
			if (Registry.GetIntData(RegPersonName, RegProgramName, "SelectedFile") > 0)
			{
				Registry.SetIntData(RegPersonName, RegProgramName, "SelectedFile", Registry.GetIntData(RegPersonName, RegProgramName, "SelectedFile") - 1);
			}

			Registry.SetStringData(RegPersonName, RegProgramName, "CurrentFilePath", PlayList[Registry.GetIntData(RegPersonName, RegProgramName, "SelectedFile")]);

			Registry.SetBoolData(RegPersonName, RegProgramName, "LoadVideo", true);
		}

		if(Registry.GetRectData(RegPersonName, RegProgramName, "Seek").Contains(Event.current.mousePosition))
        {
			if(Input.GetMouseButtonDown(0))
            {
				//Registry.SetBoolData(RegPersonName, RegProgramName, "Seek",true);
			}
			if(Input.GetMouseButton(0))
            {
				if (Registry.GetBoolData(RegPersonName, RegProgramName, "Seek"))
				{
					video.Pause();
					Seek(Registry.GetFloatData(RegPersonName, RegProgramName, "Seek"));
				}
			}
			if (Input.GetMouseButtonUp(0))
			{
				video.Play();
				//Registry.SetBoolData(RegPersonName, RegProgramName, "Seek", false);
			}
		}

		Registry.SetFloatData(RegPersonName, RegProgramName, "Seek", GUI.HorizontalSlider(Registry.GetRectData(RegPersonName, RegProgramName, "Seek"), (float)NTime, 0, 1));

		float TempVol = Registry.GetFloatData(RegPersonName, RegProgramName, "Volume") * 100;
		Registry.SetFloatData(RegPersonName, RegProgramName, "Volume",GUI.HorizontalSlider(Registry.GetRectData(RegPersonName, RegProgramName, "Volume"), Registry.GetFloatData(RegPersonName, RegProgramName, "Volume"), 0, 1));
		audio.volume = Registry.GetFloatData(RegPersonName, RegProgramName, "Volume");
		audio.mute = Registry.GetBoolData(RegPersonName, RegProgramName, "Volume");
        //var minutes = Duration / 60;
        //var seconds = (Duration % 60);
        string DurationDisplay = FormatTime3(Duration);
		string TimerDisplay = FormatTime3((float)Time);
		GUI.Label(new Rect(windowRect.width - 120, 22, 200, 21), "" + TimerDisplay + "/" + DurationDisplay);

		if (Registry.GetBoolData(RegPersonName, RegProgramName, "Volume") == false)
		{
            GUI.Label(Registry.GetRectData(RegPersonName, RegProgramName, "VolumeTextUI"), "%" + TempVol.ToString("F0"));

            if (GUI.Button(Registry.GetRectData(RegPersonName, RegProgramName, "MuteUI"), "SP"))
            {
                Registry.SetBoolData(RegPersonName, RegProgramName, "Volume", true);
            }
        }
		else
		{
            GUI.Label(Registry.GetRectData(RegPersonName, RegProgramName, "VolumeTextUI"), "%" + "X");

            if (GUI.Button(Registry.GetRectData(RegPersonName, RegProgramName, "MuteUI"), "SM"))
            {
                Registry.SetBoolData(RegPersonName, RegProgramName, "Volume", !Registry.GetBoolData(RegPersonName, RegProgramName, "Volume"));
            }
        }
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
		if (Registry.GetBoolData(RegPersonName, RegProgramName, "Fullscreen"))
		{
			if (GUI.Button(new Rect(2, 111, ButtonWidth, 21), "Miniplayer"))
			{
				Registry.SetBoolData(RegPersonName, RegProgramName, "Fullscreen",false);
				winman.ForceWindowResize("Player", windowID);
			}
		}
		else
		{
			if (GUI.Button(new Rect(2, 111, ButtonWidth, 21), "Fullscreen"))
			{
				Registry.SetBoolData(RegPersonName, RegProgramName, "Fullscreen", true);
			}
		}
	}

	void List()
	{
		GUILayout.BeginArea(new Rect(0, 24, windowRect.width, windowRect.height-24));
		scrollpos = GUILayout.BeginScrollView(scrollpos, true, true);
		int FirstIndex = (int)(scrollpos.y / 32);
		FirstIndex = Mathf.Clamp(FirstIndex, 0, Mathf.Max(0, PlayList.Count - 32));
		GUILayout.Space(FirstIndex * 32);

		for (int i = FirstIndex; i < Mathf.Min(PlayList.Count, FirstIndex + 32); i++)
		{
			string item = PlayList[i];
			//GUILayout.BeginVertical("box", GUILayout.Height(21));

			Filename = new FileInfo(PlayList[i]);
			if (GUILayout.Button(Filename.Name))
			{
				Registry.SetIntData(RegPersonName, RegProgramName, "SelectedFile", i);
				Registry.SetStringData(RegPersonName, RegProgramName, "CurrentFilePath", PlayList[Registry.GetIntData(RegPersonName, RegProgramName, "SelectedFile")]);
				Registry.SetBoolData(RegPersonName, RegProgramName, "LoadVideo", true);
				Registry.SetStringData(RegPersonName, RegProgramName, "Menu", "Home");
			}

			//GUILayout.EndVertical();
		}
		GUILayout.Space(Mathf.Max(0, (PlayList.Count - FirstIndex - 32) * 32));
		GUILayout.EndScrollView();
		GUILayout.EndArea();
	}

	void SelectWindowID(int WindowID)
	{
		if (Input.GetMouseButtonDown(0))
		{
			Registry.SetIntData("Player", "WindowManager", "SelectedWindow", WindowID);
			windowID = Registry.GetIntData("Player","WindowManager","SelectedWindow");
		}
	}

	void DoMyWindow(int WindowID)
	{
		SelectWindowID(WindowID);

		for (int PersonCount = 0; PersonCount < PersonController.control.People.Count; PersonCount++)
		{
            if (PersonController.control.People[PersonCount].Name == "Player")
            {
				var pwinman = PersonController.control.People[PersonCount].Gateway;

				if (pwinman.RunningPrograms.Count > 0)
				{
					if (WindowID == Registry.GetIntData("Player","WindowManager","SelectedWindow"))
					{
						winman.WindowResize("Player", Registry.GetIntData("Player","WindowManager","SelectedWindow"));
					}
				}
			}
		}

		if (Event.current.type == EventType.KeyDown && Event.current.keyCode == KeyCode.H)
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
				if (GUI.Button(Registry.GetRectData(RegPersonName, RegProgramName, "CloseButton"), "X", GameControl.control.Skins[Registry.GetIntData("Player", "System", "Skin")].customStyles[0]))
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
				GUI.backgroundColor = Registry.Get32ColorData("Player", "System", "ButtonColor");
				GUI.contentColor = Registry.Get32ColorData("Player", "System", "FontColor");
				GUI.Button(new Rect(Registry.GetRectData(RegPersonName, RegProgramName, "CloseButton")), "X", GameControl.control.Skins[Registry.GetIntData("Player", "System", "Skin")].customStyles[1]);
			}

			if (Registry.GetRectData(RegPersonName, RegProgramName, "MiniButton").Contains(Event.current.mousePosition))
			{
				if (GUI.Button(Registry.GetRectData(RegPersonName, RegProgramName, "MiniButton"), "-", GameControl.control.Skins[Registry.GetIntData("Player", "System", "Skin")].customStyles[2]))
				{
					//minimize = !minimize;
					//Minimize();
				}
			}
			else
			{
				GUI.backgroundColor = Registry.Get32ColorData("Player", "System", "ButtonColor");
				GUI.contentColor = Registry.Get32ColorData("Player", "System", "FontColor");
				if (GUI.Button(Registry.GetRectData(RegPersonName, RegProgramName, "MiniButton"), "-", GameControl.control.Skins[Registry.GetIntData("Player", "System", "Skin")].customStyles[2]))
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
