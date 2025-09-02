using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using System.Runtime.Serialization.Formatters.Binary;

public class Customize : MonoBehaviour 
{
	public static Customize cust;

	public int ButtonColorInt;
	public int WindowColorInt;
	public int FontColorInt;

	public int AA;
	public int VSync;
	public bool FullScreen;

	public bool UseCustomBG;

	public string DateFormat;
	public string TimeFormat;

	public List<string> CustomTexFileNames = new List<string>();

	public float SSActiveTime;
	public string ScreenSaverType;
	public bool ScreenSaverEnabled;

	public float[] windowx;
	public float[] windowy;
	public int RezX;
	public int RezY;
	public int RezSelect;

	public int native_width;
	public int native_height;
	public float UIScale;
	public int FontSize;

	public string ProfilePath;
	public string ProfileName;

	public string GatewayName;

	public int GUIID;

	public float Volume;
    public float NotiVolume;
    public float TraceBeepsVolume;

    public bool BlackMode;

	public int DesktopWidth;
	public int DesktopHeight;

	public bool SideNoti;
	public bool PlayNotiSound;
    public int SelectedNotiSound;

	public bool AutoPlayTrack;
	public bool LoopTrack;
	public bool SaveSelectedTrack;
	public int SelectedTrack;
	public float MusicVolume;
	public string MusicPath;

	public string DownloadPath;

	public float MouseSpeed;
	public float DoubleClickDelayIcon;
	public float DoubleClickDelayMenu;
	public float CursorSize;
	public bool CustomCursorSize;
	public bool DoubleClickEnable;
	public bool UsingCustomCursor;

	public int SelectedTimeFormat;

	public string WebBrowserHomepage;

	public int DeletionAmt;
	public int TerminalFontSize;
	public string Mode;
	public string TerminalCommandCharacterSplit;
	public string TerminalSpaceCharacterSplit;
	public float TerminalTextPosMod;

	public bool EnableSoundTrack;
	public float SoundtrackVolume;

	public int DesktopfontSize;

    public bool AdvancedMode;

	public bool CustomThemeColorEnabled;
	public bool CustomThemeSelectorEnabled;

	public bool EnableAutoSave;
	public float AutoSaveTime;

	public string ActualFilePath;

	void Awake()
	{
		if (!Directory.Exists (ProfilePath)) 
		{
			if (ProfilePath != "") 
			{
				Directory.CreateDirectory(ProfilePath);
			}
		}

		if(cust == null)
		{
			DontDestroyOnLoad(gameObject);
			cust = this;
		}
		else if(cust != this)
		{
			Destroy(gameObject);
		}
	}
	// Use this for initialization
	void Start () 
	{
		//ActualFilePath = Application.dataPath + "/saves/" + ProfileController.procon.VersionNumber + "/custom/" + ProfileName + ".dat";
	}

	// Update is called once per frame
	void Update ()
	{
		ActualFilePath = Application.dataPath + "/saves/" + ProfileController.procon.VersionNumber + "/custom/" + ProfileName + ".dat";
	}

	public void DeleteFile ()
	{
		File.Delete(Application.dataPath + "/saves/" + ProfileController.procon.VersionNumber + "/custom/" + ProfileName);
	}

	public void Save()
	{
		BinaryFormatter bf = new BinaryFormatter ();
		FileStream file = File.Create (ActualFilePath + ".dat");
		CustomData data = new CustomData ();

		data.windowx = windowx;
		data.windowy = windowy;
		data.GUIID = GUIID;
		data.RezX = RezX;
		data.RezY = RezY;
		data.RezSelect = RezSelect;
		data.AA = AA;
		data.VSync = VSync;
		data.FullScreen = FullScreen;
		data.CustomTexFileNames = CustomTexFileNames;
		data.SSActiveTime = SSActiveTime;
		data.ScreenSaverType = ScreenSaverType;
		data.ScreenSaverEnabled = ScreenSaverEnabled;
		data.DateFormat = DateFormat;
		data.TimeFormat = TimeFormat;
		data.ButtonColorInt = ButtonColorInt;
		data.WindowColorInt = WindowColorInt;
		data.FontColorInt = FontColorInt;
		data.UseCustomBG = UseCustomBG;
		data.native_width = native_width;
		data.native_height = native_height;
		data.UIScale = UIScale;
		data.FontSize = FontSize;
		data.Volume = Volume;
        data.NotiVolume = NotiVolume;
        data.TraceBeepsVolume = TraceBeepsVolume;
        data.DesktopWidth = DesktopWidth;
		data.DesktopHeight = DesktopHeight;
		data.SideNoti = SideNoti;
		data.PlayNotiSound = PlayNotiSound;
        data.SelectedNotiSound = SelectedNotiSound;
        data.AutoPlayTrack = AutoPlayTrack;
		data.LoopTrack = LoopTrack;
		data.SaveSelectedTrack = SaveSelectedTrack;
		data.SelectedTrack = SelectedTrack;
		data.MusicVolume = MusicVolume;
		data.MusicPath = MusicPath;
		data.DownloadPath = DownloadPath;
		data.MouseSpeed = MouseSpeed;
		data.CursorSize = CursorSize;
		data.CustomCursorSize = CustomCursorSize;
		data.DoubleClickDelayIcon = DoubleClickDelayIcon;
		data.DoubleClickDelayMenu = DoubleClickDelayMenu;
		data.DoubleClickEnable = DoubleClickEnable;
		data.UsingCustomCursor = UsingCustomCursor;
		data.SelectedTimeFormat = SelectedTimeFormat;
		data.WebBrowserHomepage = WebBrowserHomepage;
		data.DeletionAmt = DeletionAmt;
		data.TerminalFontSize = TerminalFontSize;
		data.TerminalTextPosMod = TerminalTextPosMod;
		data.TerminalCommandCharacterSplit = TerminalCommandCharacterSplit;
		data.TerminalSpaceCharacterSplit = TerminalSpaceCharacterSplit;
		data.Mode = Mode;
		data.GatewayName = GatewayName;
		data.EnableSoundTrack = EnableSoundTrack;
		data.SoundtrackVolume = SoundtrackVolume;
		data.DesktopfontSize = DesktopfontSize;
        data.AdvancedMode = AdvancedMode;
		data.EnableAutoSave = EnableAutoSave;
		data.AutoSaveTime = AutoSaveTime;

        bf.Serialize (file, data);
		file.Close();
	}

	public void Load()
	{
		if (File.Exists (ActualFilePath + ".dat"))
		{
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = File.Open (ActualFilePath + ".dat",FileMode.Open);
			CustomData data = (CustomData)bf.Deserialize (file);
			file.Close ();

			windowx = data.windowx;
			windowy = data.windowy;
			GUIID = data.GUIID;
			RezX =  data.RezX;
			RezY = data.RezY;
			RezSelect = data.RezSelect;
			AA = data.AA;
			VSync = data.VSync;
			FullScreen = data.FullScreen;
			CustomTexFileNames = data.CustomTexFileNames;
			SSActiveTime = data.SSActiveTime;
			ScreenSaverType = data.ScreenSaverType;
			ScreenSaverEnabled = data.ScreenSaverEnabled;
			DateFormat = data.DateFormat;
			TimeFormat = data.TimeFormat;
			ButtonColorInt = data.ButtonColorInt;
			WindowColorInt = data.WindowColorInt;
			FontColorInt = data.FontColorInt;
			UseCustomBG = data.UseCustomBG;
			native_width = data.native_width;
			native_height = data.native_height;
			UIScale = data.UIScale;
			FontSize = data.FontSize;
			Volume = data.Volume;
            NotiVolume = data.NotiVolume;
            TraceBeepsVolume = data.TraceBeepsVolume;
            DesktopWidth = data.DesktopWidth;
			DesktopHeight = data.DesktopHeight;
			SideNoti = data.SideNoti;
			PlayNotiSound = data.PlayNotiSound;
            SelectedNotiSound = data.SelectedNotiSound;
			AutoPlayTrack = data.AutoPlayTrack;
			LoopTrack = data.LoopTrack;
			SaveSelectedTrack = data.SaveSelectedTrack;
			SelectedTrack = data.SelectedTrack;
			MusicVolume = data.MusicVolume;
			MusicPath = data.MusicPath;
			DownloadPath = data.DownloadPath;
			MouseSpeed = data.MouseSpeed;
			CursorSize = data.CursorSize;
			CustomCursorSize = data.CustomCursorSize;
			DoubleClickDelayIcon = data.DoubleClickDelayIcon;
			DoubleClickDelayMenu = data.DoubleClickDelayMenu;
			DoubleClickEnable = data.DoubleClickEnable;
			UsingCustomCursor = data.UsingCustomCursor;
			SelectedTimeFormat = data.SelectedTimeFormat;
			WebBrowserHomepage = data.WebBrowserHomepage;
			DeletionAmt = data.DeletionAmt;
			TerminalFontSize = data.TerminalFontSize;
			TerminalTextPosMod = data.TerminalTextPosMod;
			TerminalCommandCharacterSplit = data.TerminalCommandCharacterSplit;
			TerminalSpaceCharacterSplit = data.TerminalSpaceCharacterSplit;
			Mode = data.Mode;
			GatewayName = data.GatewayName;
			EnableSoundTrack = data.EnableSoundTrack;
			SoundtrackVolume = data.SoundtrackVolume;
			DesktopfontSize = data.DesktopfontSize;
            AdvancedMode = data.AdvancedMode;
			EnableAutoSave = data.EnableAutoSave;
			AutoSaveTime = data.AutoSaveTime;
        }
	}

	[Serializable]
	class CustomData
	{
		public int GUIID;

		public int ButtonColorInt;
		public int WindowColorInt;
		public int FontColorInt;

		public int AA;
		public int VSync;
		public bool FullScreen;

		public bool UseCustomBG;

		public string DateFormat;
		public string TimeFormat;

		public string GatewayName;

		public List<string> CustomTexFileNames = new List<string>();

		public float SSActiveTime;
		public string ScreenSaverType;
		public bool ScreenSaverEnabled;

		public float[] windowx;
		public float[] windowy;
		public int RezX;
		public int RezY;
		public int RezSelect;

		public int native_width;
		public int native_height;
		public float UIScale;
		public int FontSize;

		public float Volume;
        public float NotiVolume;
        public float TraceBeepsVolume;

        public int DesktopWidth;
		public int DesktopHeight;

		public bool SideNoti;
		public bool PlayNotiSound;
        public int SelectedNotiSound;
        public bool AutoPlayTrack;
		public bool LoopTrack;
		public bool SaveSelectedTrack;
		public int SelectedTrack;
		public float MusicVolume;
		public string MusicPath;

		public string DownloadPath;

		public float MouseSpeed;
		public float DoubleClickDelayIcon;
		public float DoubleClickDelayMenu;
		public float CursorSize;
		public bool CustomCursorSize;
		public bool DoubleClickEnable;
		public bool UsingCustomCursor;

		public int SelectedTimeFormat;

		public string WebBrowserHomepage;

		public int DeletionAmt;
		public int TerminalFontSize;
		public float TerminalTextPosMod;
		public string TerminalCommandCharacterSplit;
		public string TerminalSpaceCharacterSplit;
		public string Mode;

		public bool EnableSoundTrack;
		public float SoundtrackVolume;

		public int DesktopfontSize;

        public bool AdvancedMode;

		public bool EnableAutoSave;
		public float AutoSaveTime;
	}
}
