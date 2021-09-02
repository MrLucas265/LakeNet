using UnityEngine;
using System.Collections;

public class InstallPrompt : MonoBehaviour
{
	GameObject IconObject;
	GameObject Computer;
	GameObject Desktop;

	public Rect windowRect = new Rect(100, 100, 200, 200);
	public float native_width = 1920;
	public float native_height = 1080;
	public int windowID;

	public string ErrorMsg;
	public string ErrorTitle;
	public string ConfirmationMsg;

	public AudioSource AS;

	public bool show;

	public bool playsound;

	public bool Game;

	public int SoundSelect;

	private Computer com;
	private Defalt def;
	private SoundControl sc;
    private FileUtility fu;

    private Rect CloseButton;

	public bool Install;
	public bool Run;
	public bool ShowMenu1;

	public string ProgramName;
	public string ProgramTarget;
	public string ProgramType;
	public float ProgramVersion;
	public float Size;

	public int ProgramID;

	public int IndexOFItem;

    public bool AddToQuickList;
    public string FileLocation;

	public ProgramSystem HeldFile;

	// Use this for initialization
	void Start () 
	{
		Computer = GameObject.Find("System");
		IconObject = GameObject.Find("IconObject");
		Desktop = GameObject.Find("Desktop");
		com = Computer.GetComponent<Computer>();
		def = Computer.GetComponent<Defalt>();
		sc = Computer.GetComponent<SoundControl>();
        fu = Computer.GetComponent<FileUtility>();
        native_height = Customize.cust.native_height;
		native_width = Customize.cust.native_width;
		CloseButton = new Rect (275, 5, 21, 21);
        FileLocation = "";
	}

	// Update is called once per frame
	void Update () 
	{
		if (playsound == true)
		{
			playsound = false;
			sc.SoundSelect = SoundSelect;
			sc.PlaySound();
		}
	}

	void OnGUI()
	{
		Customize.cust.windowx[windowID] = windowRect.x;
		Customize.cust.windowy[windowID] = windowRect.y;

		GUI.skin = com.Skin[GameControl.control.GUIID];
		//set up scaling
		//float rx = Screen.width / native_width;
		//float ry = Screen.height / native_height;

		//GUI.matrix = Matrix4x4.TRS (Vector3.zero, Quaternion.identity, new Vector3 (rx, ry, 1)); 

		if(show == true)
		{
			GUI.color = com.colors[Customize.cust.WindowColorInt];
			windowRect = GUI.Window(windowID,windowRect,DoMyWindow,"");
		}
	}

	void UpdateUI()
	{
		switch (ProgramName) 
		{
		case "Password Breaker":
			ProgramID = 2;
			break;
		case "Trace Tracker":
			ProgramID = 3;
			break;
		case "System Map":
			ProgramID = 4;
			break;
		case "Directory Cracker":
			ProgramID = 5;
			break;
		}
	}

//	void ProgressBar()
//	{
//		icon.IconXPos = 0;
//		icon.IconYPos = 0;
//		icon.IconWH = 48;
//		icon.IconPicSelect = 1;
//		icon.IconName = ProgramName;
//		icon.IconProgramList = ProgramName;
//	}

	void DoMyWindow(int WindowID)
	{
		GUI.backgroundColor = com.colors[Customize.cust.ButtonColorInt];
		GUI.contentColor = com.colors[Customize.cust.FontColorInt];

		GUI.DragWindow (new Rect (5, 5, 270, 21));
		GUI.Box (new Rect (5, 5, 270, 21), ErrorTitle);

		if (CloseButton.Contains (Event.current.mousePosition)) 
		{
			if (GUI.Button (new Rect (CloseButton), "X", com.Skin [GameControl.control.GUIID].customStyles [0])) 
			{
				show = false;
				this.enabled = false;
			}
		} 
		else
		{
			GUI.backgroundColor = com.colors[Customize.cust.ButtonColorInt];
			GUI.contentColor = com.colors[Customize.cust.FontColorInt];
			if (GUI.Button (new Rect (CloseButton), "X", com.Skin [GameControl.control.GUIID].customStyles [1])) 
			{
				show = false;
				this.enabled = false;
			}
		}

		GUI.TextArea((new Rect (5, 30, 290, 90)),ErrorMsg);

		if (ShowMenu1 == true) 
		{
			if(GUI.Button(new Rect(5, 125, 75, 20),"Install"))
			{
                if (fu.ProgramHandle.Count <= 0)
                {
                    fu.Local = true;
					fu.ProgramHandle.Add(new FileUtilitySystem("Installer", ProgramName, FileLocation,"", "", ProgramTarget, "", ProgramType, false, true, true, false, ProgramVersion, 0, 0, 0, 0, 0, 0, 0, Size, 0, 0, 0, FileUtilitySystem.ProgramType.Installer));
					//fu.ProgramHandle.Add(new FileUtilitySystem("Installer", ProgramName, FileLocation, "", ProgramTarget, "", ProgramType, false, true, true, false, ProgramVersion, 0, 0, 0, 0, 0, 0, 0, Size, 0, 0, 0, FileUtilitySystem.ProgramType.Installer));
					fu.AddWindow();
                }
				ShowMenu1 = false;
				show = false;
				this.enabled = false;
			}

			AddToQuickList = GUI.Toggle(new Rect (100, 125, 100, 20),AddToQuickList,"Add To QuickList");

			FileLocation = GUI.TextField (new Rect (100, 175, 100, 20), FileLocation);
		}

		if (Install == true) 
		{
			UpdateUI();
			if(GUI.Button(new Rect(5, 125, 75, 20),"Yes"))
			{
				ShowMenu1 = true;
			}

			if(GUI.Button(new Rect(220, 125, 75, 20),"Not now"))
			{
				show = false;
				this.enabled = false;
			}
		}

		if (Run == true) 
		{
			if(GUI.Button(new Rect(5, 125, 75, 20),ConfirmationMsg))
			{
				show = false;
				this.enabled = false;
				Run = false;
			}

			if(GUI.Button(new Rect(220, 125, 75, 20),"Not now"))
			{
				show = false;
				this.enabled = false;
				Run = false;
			}
		}
	}
}
