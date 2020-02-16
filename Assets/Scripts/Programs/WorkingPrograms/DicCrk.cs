using UnityEngine;
using System.Collections;

public class DicCrk : MonoBehaviour 
{
	public bool show;
	public int windowID;
	public Rect windowRect = new Rect(100, 100, 200, 200);
	public float native_width = 1920;
	public float native_height = 1080;
	public bool Drag;

	public bool close;
	public bool execute;

	private Defalt defalt;
	private CLI cmd;
	private WebSec ws;
	private ErrorProm ep;
	private InternetBrowser ib;
	private CPU cpu;
	private Tracer trace;
	private Computer com;
	private PasswordList pl;
	private SoundControl sc;

	private GameObject Hardware;
	private GameObject Prompts;
	private GameObject SysSoftware;
	private GameObject AppSoftware;
	private GameObject HackingSoftware;

	public float timer;
	public float startTime;

	public float percentage;
	public float StartingCount;
	public float CurrentCount;

	public string Password;
	public string CurrentWord;

	public bool Matched;

	public Rect CloseButton;
	public Rect ExecuteButton;

	public int WordCount;

	// Progtive is the one at a time sequential cracker
	// Use this for initialization
	void Start ()
	{
		Hardware = GameObject.Find ("Hardware");
		Prompts = GameObject.Find ("Prompts");
		SysSoftware = GameObject.Find ("System");
		HackingSoftware = GameObject.Find ("Hacking");
		AppSoftware = GameObject.Find ("Applications");

		ep = Prompts.GetComponent<ErrorProm>();
		com = SysSoftware.GetComponent<Computer>();
		trace = HackingSoftware.GetComponent<Tracer>();
		cmd = SysSoftware.GetComponent<CLI>();
		defalt = SysSoftware.GetComponent<Defalt>(); 
		ib = AppSoftware.GetComponent<InternetBrowser>(); 
		ws = AppSoftware.GetComponent<WebSec>();
		cpu = Hardware.GetComponent<CPU>();
		pl = SysSoftware.GetComponent<PasswordList>();
		sc = SysSoftware.GetComponent<SoundControl>();

		CloseButton = new Rect (125, 1, 21, 21);
		ExecuteButton = new Rect (45, 100, 60, 24);

		StartingCount = pl.PasswordWords.Count;
	}

	void OnGUI()
	{
		Customize.cust.windowx[windowID] = windowRect.x;
		Customize.cust.windowy[windowID] = windowRect.y;
		GUI.skin = com.Skin[GameControl.control.GUIID];

		if(show == true)
		{
			GUI.color = com.colors[Customize.cust.WindowColorInt];
			windowRect = WindowClamp.ClampToScreen(GUI.Window(windowID,windowRect,DoMyWindow,""));
		}

		if (execute == true) 
		{
			Execute ();
//			if (pl.Words.Count > 0)
//			{
//				Execute ();
//			} 
//			else 
//			{
//				pl.AddWordDatabase();
//				StartingCount = pl.Words.Count;
//				execute = false;
//			}
		}
	}

	void DoMyWindow(int WindowID)
	{
		if (CloseButton.Contains (Event.current.mousePosition))
		{
			if (GUI.Button (new Rect (CloseButton), "X", com.Skin [GameControl.control.GUIID].customStyles [0])) 
			{
				show = false;
			}
		}

		if (Matched == true)
		{
			execute = false;
			pl.PasswordWords.RemoveRange(0,pl.PasswordWords.Count);
			GUI.Label(new Rect(20,20,100,24),"Matched Word");
			CurrentWord = GUI.TextField(new Rect(20,40,100,24),CurrentWord);
		}

		if (execute == false)
		{
			if (!ExecuteButton.Contains (Event.current.mousePosition))
			{
				GUI.backgroundColor = com.colors[Customize.cust.ButtonColorInt];
				GUI.contentColor = com.colors[Customize.cust.FontColorInt];
				GUI.Button (new Rect (ExecuteButton), "Execute", com.Skin [GameControl.control.GUIID].customStyles [1]);
			} 
			else 
			{
				if (GUI.Button (new Rect (ExecuteButton), "Execute", com.Skin [GameControl.control.GUIID].customStyles [0])) 
				{
					Matched = false;
					if (pl.PasswordWords.Count <= 0) 
					{
						pl.PasswordListResource ();
						StartingCount = pl.PasswordWords.Count;
						execute = true;
					} 
					else 
					{
						StartingCount = pl.PasswordWords.Count;
						execute = true;
					}
				}
			}
		} 
		else
		{
			GUI.backgroundColor = com.colors[Customize.cust.ButtonColorInt];
			GUI.contentColor = com.colors[Customize.cust.FontColorInt];

			percentage = CurrentCount / StartingCount * 100;
			float CountUpWords = CurrentCount - StartingCount;
			//GUI.Label (new Rect (10, 20, 100, 24),"" +  percentage.ToString("F2") + "%");
			//GUI.Label (new Rect (10, 20, 100, 24),"" +  CurrentCount + "/" + StartingCount);
			GUI.Label (new Rect (-4, 20, 100, 24),"" +  CountUpWords + "/" + StartingCount);
			for(int i = 0; i < pl.PasswordWords.Count; i++)
			{
				CurrentCount = pl.PasswordWords.Count;
				GUI.Label (new Rect (2, 40 + 20 * i, 100, 24), pl.PasswordWords [i]);
				CurrentWord = pl.PasswordWords[0].Trim();
			}
		}

		if (!CloseButton.Contains (Event.current.mousePosition))
		{
			GUI.backgroundColor = com.colors[Customize.cust.ButtonColorInt];
			GUI.contentColor = com.colors[Customize.cust.FontColorInt];
			if (GUI.Button (new Rect (CloseButton), "X", com.Skin [GameControl.control.GUIID].customStyles [1])) 
			{
				show = false;
			}
		} 

		GUI.DragWindow(new Rect(1, 1, 124, 21));
		GUI.Box(new Rect(1, 1, 124, 21), "Dictionary Cracker");
	}

	void Execute()
	{
		timer -= Time.deltaTime;
		if (timer <= 0)
		{

            for (int a = 0; a < ib.CurrentAccounts.Count; a++)
            {
                if (ib.CurrentAccounts[a].UserName == ib.Username)
                {
                    if (CurrentWord != ib.CurrentAccounts[a].Password.Trim())
                    {
                        pl.PasswordWords.RemoveAt(0);
                        WordCount++;
                        timer = startTime;
                    }
                    else
                    {
                        Matched = true;
                    }
                }
            }
		}

		if (pl.PasswordWords.Count < 1)
		{
			Matched = false;
			execute = false;
		}

		if (WordCount > 100)
		{
			sc.SoundSelect = 5;
			sc.Pitch = 1;
			sc.PlaySound();
			WordCount = 0;
		}
	}
}
