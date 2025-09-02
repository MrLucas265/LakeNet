using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ErrorProm : MonoBehaviour
{
	private GameObject Puter;

    private GameObject WindowHandel;

	public float native_width = 1920;
	public float native_height = 1080;

	public string ErrorMsg;
	public string ErrorTitle;
    public int ErrorWindowID;

    public List<ErrorSystem> ErrorList = new List<ErrorSystem>();

    public AudioSource AS;

	public bool show;

	public bool playsound;

	public bool Game;

	public bool Restart;

	public int SoundSelect;

	private Computer com;
	private Defalt def;
	private SoundControl sc;
    private WindowManager winman;
    private AppMan appman;

	private Rect CloseButton;
    private Rect TitleBox;
    private Rect MessageBox;

    public int ProgramCount;

    public int SelectedWindowID;
    public int SelectedProgram;

    public Texture2D icon;

    public bool quit;

    // Use this for initialization
    void Start () 
	{
		Puter = GameObject.Find("System");
        WindowHandel = GameObject.Find("WindowHandel");
        com = Puter.GetComponent<Computer>();
		def = Puter.GetComponent<Defalt>();
		sc = Puter.GetComponent<SoundControl>();
        appman = Puter.GetComponent<AppMan>();
        winman = WindowHandel.GetComponent<WindowManager>();
        native_height = Customize.cust.native_height;
		native_width = Customize.cust.native_width;
		CloseButton = new Rect (378, 1, 21, 21);
	}

	// Update is called once per frame
	void Update () 
	{
		
	}

	void OnGUI()
	{
		//Customize.cust.windowx[windowID] = windowRect.x;
		//Customize.cust.windowy[windowID] = windowRect.y;

		GUI.skin = GameControl.control.Skins[Registry.GetIntData("Player", "System", "Skin")];

		if (playsound == true)
		{
			playsound = false;
			sc.SoundSelect = SoundSelect;
			sc.PlaySound();
		}
        for (int PersonCount = 0; PersonCount < PersonController.control.People.Count; PersonCount++)
        {
            var pwinman = PersonController.control.People[PersonCount].Gateway;

            if (pwinman.RunningPrograms.Count > 0)
            {
                ProgramCount = 0;
                for (int i = 0; i < pwinman.RunningPrograms.Count; i++)
                {
                    ProgramCount++;
                    if (pwinman.RunningPrograms[i].ProgramName == "Error Prompt")
                    {
                        CloseButton = new Rect(pwinman.RunningPrograms[i].windowRect.width - 22, 1, 21, 21);
                        //MiniButton = new Rect(CloseButton.x - 21, 1, 21, 21);
                        TitleBox = new Rect(22, 1, CloseButton.x - 22, 21);
                        MessageBox = new Rect(5, 30, 385, 90);

                        GUI.color = Registry.Get32ColorData("Player", "System", "WindowColor");
                        pwinman.RunningPrograms[i].windowRect = WindowClamp.ClampToScreen(GUI.Window(pwinman.RunningPrograms[i].WID, pwinman.RunningPrograms[i].windowRect, DoMyWindow, ""));
                    }
                }
            }
        }
	}

    void Close()
    {
        if (ProgramCount > 0)
        {
            for (int PersonCount = 0; PersonCount < PersonController.control.People.Count; PersonCount++)
            {
                var pwinman = PersonController.control.People[PersonCount].Gateway;

                for (int i = 0; i < pwinman.RunningPrograms.Count; i++)
                {
                    if (pwinman.RunningPrograms[i].WID == SelectedWindowID)
                    {
                        if (ProgramCount == 1)
                        {
                            show = false;
                            enabled = false;
                            Restart = false;
                            quit = true;
                            appman.SelectedApp = "Error Prompt";
                        }
                        else
                        {
                            quit = true;
                            appman.SelectedApp = "Error Prompt";
                        }
                        for (int e = 0; e < ErrorList.Count; e++)
                        {
                            if (ErrorList[e].WindowID == SelectedWindowID)
                            {
                                ErrorList.RemoveAt(e);
                            }
                        }
                        pwinman.RunningPrograms.RemoveAt(i);
                    }
                }
            }
        }
    }

    public void AddNewError()
    {
        ErrorList.Add(new ErrorSystem(ErrorTitle, ErrorMsg, Restart, ErrorWindowID));
        Restart = false;
    }

    void DoMyWindow(int WindowID)
    {
        SelectedWindowID = WindowID;

        for (int PersonCount = 0; PersonCount < PersonController.control.People.Count; PersonCount++)
        {
            var pwinman = PersonController.control.People[PersonCount].Gateway;

            for (int i = 0; i < pwinman.RunningPrograms.Count; i++)
            {
                if (pwinman.RunningPrograms[i].WID == SelectedWindowID)
                {
                    if (pwinman.RunningPrograms[i].PID > ErrorList.Count - 1)
                    {
                        pwinman.RunningPrograms[i].PID = ErrorList.Count - 1;
                    }
                    SelectedProgram = pwinman.RunningPrograms[i].PID;
                }
            }
        }

        if (CloseButton.Contains(Event.current.mousePosition))
        {
            if (GUI.Button(new Rect(CloseButton), "X", GameControl.control.Skins[Registry.GetIntData("Player", "System", "Skin")].customStyles[0]))
            {
                Close();
            }
        }
        else
        {
            GUI.backgroundColor = Registry.Get32ColorData("Player", "System", "ButtonColor");
            GUI.contentColor = Registry.Get32ColorData("Player", "System", "FontColor");
            GUI.Button(new Rect(CloseButton), "X", GameControl.control.Skins[Registry.GetIntData("Player", "System", "Skin")].customStyles[1]);
        }

        GUI.backgroundColor = Registry.Get32ColorData("Player", "System", "ButtonColor");
        GUI.contentColor = Registry.Get32ColorData("Player", "System", "FontColor");

        //GUI.DrawTexture(new Rect(1, 1, 21, 21), icon);

        GUI.DragWindow(new Rect(TitleBox));

        GUI.Box(new Rect(TitleBox), ErrorList[SelectedProgram].Title);

        GUI.TextArea(new Rect(MessageBox), ErrorList[SelectedProgram].Message);

        if (ErrorList[SelectedProgram].Restart == false)
        {
            if (GUI.Button(new Rect(150, 125, 50, 20), "Ok"))
            {
                Close();
            }
        }
        else
        {
            if (GUI.Button(new Rect(100, 125, 50, 20), "Restart Now"))
            {
                GameControl.control.GatewayStatus.Booted = false;
                SceneManager.LoadScene("Game");
            }

            if (GUI.Button(new Rect(200, 125, 50, 20), "Cancel"))
            {
                Close();
                Restart = false;
            }
        }
	}
}
