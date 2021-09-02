using UnityEngine;
using System.Collections;

public class Tracer : MonoBehaviour
{
    public float timer;
    public float MaxTime;
    public bool startTrace;
    public bool show;
    private Computer com;
	private SystemMap sm;
	private WebSec ws;
	private Defalt def;
	private InternetBrowser ib;
	private SoundControl sc;
    public int windowID;
    public Rect windowRect = new Rect(100, 100, 200, 200);
    public float native_width = 1920;
    public float native_height = 1080;
    public bool Drag;
    public AudioSource beep;
    public float beeptimer;
    public float beepcooldown;

	public bool starting;
	public bool stopping;
	public bool executing;
	public bool closing;

	public bool UpdateTimer;

	public float ItemCount;

	private GameObject Hardware;
	private GameObject Prompts;
	private GameObject SysSoftware;
	private GameObject AppSoftware;
	private GameObject HackingSoftware;

    public float percenttimer;
	public float percentcooldown;

	public float ColorPercentage;
	public float CompletionPercentage;

	public float PercentTime;

	public Color32 windowColor = new Color32(0,0,0,0);

	public float Pitch;

	// Use this for initialization
	void Start ()
    {
		Hardware = GameObject.Find("Hardware");
		Prompts = GameObject.Find("Prompts");
		SysSoftware = GameObject.Find("System");
        HackingSoftware = GameObject.Find("Hacking");
		AppSoftware = GameObject.Find("Applications");

		com = SysSoftware.GetComponent<Computer>();
		def = SysSoftware.GetComponent<Defalt>();
		sc = SysSoftware.GetComponent<SoundControl>();

		ws = AppSoftware.GetComponent<WebSec>();
		sm = AppSoftware.GetComponent<SystemMap>();
		ib = AppSoftware.GetComponent<InternetBrowser>();

		windowRect.x = Customize.cust.windowx[windowID];
		windowRect.y = Customize.cust.windowy[windowID];
        beeptimer = beepcooldown;
		native_height = Customize.cust.native_height;
		native_width = Customize.cust.native_width;
		Pitch = 0.5f;
	}

	void TraceColor()
	{

		if (startTrace != true) 
		{
			GUI.color = com.colors[Customize.cust.WindowColorInt];
		}
		else 
		{
			Color32 windowColor;
			windowColor.r = (byte)255;
			windowColor.g = (byte)ColorPercentage;
			windowColor.b = (byte)ColorPercentage;
			windowColor.a = (byte)255;
			GUI.color = windowColor;
		}
	}
	
	// Update is called once per frame
	void Update () 
    {
		if (UpdateTimer == true) 
		{
			if (MaxTime == 0) 
			{
				MaxTime = sm.BouncedConnections.Count / ws.SecLevel * 60;
				percentcooldown = MaxTime / sm.BouncedConnections.Count;
				sm.ConnectionsLeft = sm.BouncedConnections.Count;
				timer = MaxTime;
			}
			startTrace = true;
			UpdateTimer = false;
		}

		if (starting == true)
		{
			show = true;
			starting = false;
		}

		if (executing == true)
		{
			timer = MaxTime;
			executing = false;
		}

		if (stopping == true)
		{
			startTrace = false;
			stopping = false;
		}

		if (closing == true)
		{
			closing = false;
			show = false;
		}

		if (startTrace == true)
        {
            timer -= Time.deltaTime;
			percenttimer -= Time.deltaTime;
            if(show == true)
            {
                beepcooldown = timer*0.14f;
				beeptimer += Time.deltaTime;

				ColorPercentage = 255 - beeptimer / beepcooldown * 100 * 2.55f;
				CompletionPercentage = 100 - timer / MaxTime * 100 * 1.00f;
				PercentTime = 100 - percenttimer / percentcooldown * 100 * 1.00f;
				//PercentTime = sm.PercentageChange / MaxTime;

				if (percenttimer <= 0) 
				{
					percenttimer = percentcooldown;
					sm.ConnectionsLeft--;
				}

				//if (CurrentPercentage > sm.PercentageChange)
				//{
				//	CurrentPercentage = 0;
				//	sm.ConnectionsLeft--;
				//}

				if (beeptimer>=beepcooldown)
				{
					Pitch = Pitch + 0.01f;
					if (beeptimer > 0.15f) 
					{
						TraceColor();
						sc.PlayTraceTrackerSound(12, 1);
						beeptimer = 0;
					}
				}
            }

            if(timer <= 0)
            {
				Pitch = 0.5f;
				if (ib.SiteAdminPass == "") 
				{
					ib.SiteAdminPass = StringGenerator.RandomMixedChar(8, 8);
					stopping = true;
					timer = 0;
				}
            }

        }
        else
        {
			ib.showAddressBar = true;
            timer = MaxTime;
        }
	}

    void OnGUI()
    {
		Customize.cust.windowx[windowID] = windowRect.x;
		Customize.cust.windowy[windowID] = windowRect.y;
        GUI.skin = com.Skin[GameControl.control.GUIID];

        //float rx = Screen.width / native_width;
        //float ry = Screen.height / native_height;

        //GUI.matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, new Vector3(rx, ry, 1));

        if(show == true)
        {
			TraceColor();
			windowRect = WindowClamp.ClampToScreen(GUI.Window(windowID,windowRect,DoMyWindow,""));
        }
    }

    void DoMyWindow(int WindowID)
    {
		if(GUI.Button(new Rect(162,2,21,21),"X",com.Skin [GameControl.control.GUIID].customStyles [0]))
		{
			closing = true;
			show = false;
		}

		GUI.backgroundColor = com.colors[Customize.cust.ButtonColorInt];
		GUI.contentColor = com.colors[Customize.cust.FontColorInt];

        GUI.DragWindow(new Rect(2,2,150,21));
        GUI.Box(new Rect(2,2,159,21), "Trace Tracker");

        if(startTrace == true)
        {
			GUI.Label(new Rect(40, 20, 500, 500), "Timer: " + timer.ToString("F2"));
        }
        else
        {
            GUI.Label(new Rect(40, 20, 500, 500), "Timer: Inactive");
        }
    }
}
