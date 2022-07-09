//using UnityEngine;
//using System.Collections;
//using System.Collections.Generic;

//public class Progtive : MonoBehaviour
//{
//    public bool show;
//    public int windowID;
//    public Rect windowRect = new Rect(100, 100, 200, 200);
//    public float native_width = 1920;
//    public float native_height = 1080;
//    public bool Drag;
//    public bool Cracking;
//    public bool Animating;
//    public float Timer;
//    public float startingCount;
//	public float Counts;

//    public string Password;

//	public string sitelogin;

//	public float time;
//	public float Starttime;

//	public bool starting;
//	public bool closing;
//	public bool executing;
//	public bool stopping;

//	public bool Running;

//	public float DiskUsage;

//	public float CPUUsage;
//	public float RAMUsage;
//	public float GPUUsage;

//	public float CPUUsageWE;
//	public float RAMUsageWE;
//	public float GPUUsageWE;

//	private Defalt defalt;
//	private CLICommandsV2 cmd;
//	private WebSec ws;
//	private ErrorProm ep;
//	private InternetBrowser ib;
//	private Tracer trace;
//	private Computer com;

//	private GameObject Hardware;
//	private GameObject Prompts;
//	private GameObject SysSoftware;
//	private GameObject AppSoftware;
//	private GameObject HackingSoftware;

//    // Progtive is the one at a time sequential cracker
//    // Use this for initialization
//    void Start () 
//    {
//		Hardware = GameObject.Find("Hardware");
//		Prompts = GameObject.Find("Prompts");
//		SysSoftware = GameObject.Find("System");
//		HackingSoftware = GameObject.Find("Hacking");
//		AppSoftware = GameObject.Find("Applications");

//		Starttime = 15;
//        Counts = startingCount;
//		time = Starttime;
//		ep = Prompts.GetComponent<ErrorProm>();
//		com = SysSoftware.GetComponent<Computer>();
//		trace = HackingSoftware.GetComponent<Tracer>();
//		cmd = SysSoftware.GetComponent<CLICommandsV2>();
//		defalt = SysSoftware.GetComponent<Defalt>(); 
//		ib = AppSoftware.GetComponent<InternetBrowser>(); 
//		ws = AppSoftware.GetComponent<WebSec>();
//        windowRect.x = Customize.cust.windowx[windowID];
//        windowRect.y = Customize.cust.windowy[windowID];
//		native_height = Customize.cust.native_height;
//		native_width = Customize.cust.native_width;
//    }
	
//	// Update is called once per frame
//	void Update ()
//    {
//        if(Animating == true)
//        {
//			Running = true;
//            StartCoroutine(HackingAnimation());
//        }

//		if (time < 0) 
//		{
//			Animating = false;
//			Hacking ();
//		}

//        if(Counts < 0)
//        {
//            Counts = 0;
//            Animating = false;
//        }

//        if(Animating == false && Cracking == false)
//        {
//            Counts = startingCount;
//        }

//		if (starting == true) 
//		{
//			if (cpu.RemainingCPUUsage < CPUUsage) 
//			{
//				ep.playsound = true;
//				ep.ErrorMsg = "Not enough CPU processing power to execute.";
//				ep.ErrorTitle = "Processing Error - 121";
//				starting = false;
//				show = false;
//			}
////			if (cpu.RemainingGPU < GPUUsage) 
////			{
////				defalt.showep = true;
////				ep.playsound = true;
////				ep.ErrorMsg = "Not enough GPU processing power to execute.";
////				ep.ErrorTitle = "Processing Error - 122";
////				starting = false;
////				show = false;
////			}
////			if (cpu.RemainingRAM < RAMUsage) 
////			{
////				defalt.showep = true;
////				ep.playsound = true;
////				ep.ErrorMsg = "Not enough RAM processing power to execute.";
////				ep.ErrorTitle = "Processing Error - 123";
////				starting = false;
////				show = false;
////			}
//			if (cpu.RemainingCPUUsage >= CPUUsage) //&& cpu.RemainingGPU >= GPUUsage && cpu.RemainingRAM >= RAMUsage) 
//			{
////				cpu.CurRAMBan += RAMUsage;
////				cpu.CurGPUBandwidth += GPUUsage;
//				cpu.Usage += CPUUsage;
//				show = true;
//				starting = false;
//			} 
//		}

//		if (Running == true)
//		{
//			Animating = true;
//		}

//		if (executing == true) 
//		{
//			if (cpu.RemainingCPUUsage >= CPUUsageWE) //&& cpu.CurRAMBan >= RAMUsageWE && cpu.CurGPUBandwidth >= CPUUsageWE) 
//			{
//				//cpu.CurRAMBan += RAMUsageWE;
//				//cpu.CurGPUBandwidth += GPUUsageWE;
//				cpu.RemainingCPUUsage += CPUUsageWE;
//				VersionCheck();
//				executing = false;
//			}
//		}

//		if (stopping == true) 
//		{
//			//cpu.CurRAMBan += RAMUsageWE;
//			//cpu.CurGPUBandwidth += GPUUsageWE;
//			cpu.RemainingCPUUsage += CPUUsageWE;
//			stopping = false;
//			Running = false;
//		}

//		if(closing == true)
//		{
//			//cpu.CurRAMBan -= RAMUsage;
//			//cpu.CurGPUBandwidth -= GPUUsage;
//			cpu.RemainingCPUUsage -= CPUUsage;
//			sitelogin = "";
//			closing = false;
//			Running = false;
//			show = false;
//		}
//	}

//    void OnGUI()
//    {
//        Customize.cust.windowx[windowID] = windowRect.x;
//        Customize.cust.windowy[windowID] = windowRect.y;
//        GUI.skin = com.Skin[GameControl.control.GUIID];

//        if(show == true)
//        {
//            windowRect = GUI.Window(windowID,windowRect,DoMyWindow,"");
//        }
//    }

//    void DoMyWindow(int WindowID)
//    {
//        GUI.TextField(new Rect(5, 30, 150, 20), Password, 500);

//        GUI.DragWindow(new Rect(5, 5, 170, 21));
//        GUI.Box(new Rect(5, 5, 170, 21), "Password Breaker");

//        if (GUI.Button(new Rect(175, 5, 21, 21), "X"))
//        {
//			closing = true;
//        }

//		if (Running == false) 
//		{
//			if (GUI.Button(new Rect(157, 30, 40, 20), "Run"))
//			{
//				if (cpu.RemainingCPUUsage >= CPUUsageWE)//& cpu.RemainingGPU >= GPUUsageWE && cpu.RemainingRAM >= RAMUsageWE)
//				{
//					Run ();
//					if (Animating == false)
//					{
//						ep.playsound = true;
//						ep.ErrorMsg = "Not connected to a site or Invalid username";
//						ep.ErrorTitle = "Connection Error - 404";
//					}
//				} 
//				else
//				{
//					if (cpu.RemainingCPUUsage < CPUUsage) 
//					{
//						ep.playsound = true;
//						ep.ErrorMsg = "Not enough CPU processing power to execute.";
//						ep.ErrorTitle = "Processing Error - 121";
//						starting = false;
//					}
////					if (cpu.RemainingGPU < GPUUsage) 
////					{
////						ep.playsound = true;
////						defalt.showep = true;
////						ep.ErrorMsg = "Not enough GPU processing power to execute.";
////						ep.ErrorTitle = "Processing Error - 122";
////						starting = false;
////					}
////					if (cpu.RemainingRAM < RAMUsage) 
////					{
////						ep.playsound = true;
////						defalt.showep = true;
////						ep.ErrorMsg = "Not enough RAM processing power to execute.";
////						ep.ErrorTitle = "Processing Error - 123";
////						starting = false;
////					}
//				}
//			}
//        }
//    }

//	public void Run()
//	{
//		switch (ib.AddressBar) 
//		{

//		case "www.becassystems.com/login":
//			if (ib.Username == "Admin") 
//			{
//				executing = true;
//				sitelogin = "Becas";
//				if (ws.Monitor == true) 
//				{
//					trace.UpdateTimer = true;
//				}
//				if (Cracking == false) 
//				{
//					Animating = true;
//				}
//			} 
//			else 
//			{
//				Password = "";
//				Counts = startingCount;
//				Cracking = false;
//			}
//			break;

//		case "www.jaildew.com/login":
//			if (ib.Username == "Admin") 
//			{
//				executing = true;
//				sitelogin = "Jaildew";
//				if (ws.Monitor == true) 
//				{
//					trace.UpdateTimer = true;
//				}
//				if (Cracking == false) 
//				{
//					Animating = true;
//				}
//			} 
//			else 
//			{
//				Password = "";
//				Counts = startingCount;
//				Cracking = false;
//			}
//			break;

//		case "www.unicom.com/login":
//			if (ib.Username == "Admin") 
//			{
//				executing = true;
//				sitelogin = "Unicom";
//				if (ws.Monitor == true) 
//				{
//					trace.UpdateTimer = true;
//				}
//				if (Cracking == false)
//				{
//					Animating = true;
//				}
//			} 
//			else 
//			{
//				Password = "";
//				Counts = startingCount;
//				Cracking = false;
//			}
//			break;

//		case "www.revatest.com/login":
//			if (ib.Username == "Admin") 
//			{
//				executing = true;
//				sitelogin = "Reva Test";
//				if (ws.Monitor == true) 
//				{
//					trace.UpdateTimer = true;
//				}
//				if (Cracking == false)
//				{
//					Animating = true;
//				}
//			}
//			else 
//			{
//				Password = "";
//				Counts = startingCount;
//				Cracking = false;
//			}
//			break;
//		}
//	}
			

//	void Hacking()
//	{
//		if(ib.Username == "Admin")
//		{
//			stopping = true;
//			Password = ib.SiteAdminPass;
//			cmd.PastCommands.Add ("Password: " + Password);
//			Counts = startingCount;
//			time = Starttime;
//			Running = false;
//		}
//	}

//    IEnumerator HackingAnimation()
//    {
//        yield return new WaitForSeconds (0);
//        for(Counts = startingCount; Counts > 0; Counts--)
//        {
//            if(Animating == true)
//            {
//				time -= 1 * Time.deltaTime;
//				Password = StringGenerator.RandomMixedChar(8, 8);
//                yield return new WaitForSeconds (Timer);
//            }
//        }
//    }

//	void VersionCheck()
//	{
////		switch(GameControl.control.SoftwareVersion[2])
////        {
////		case 1:
////			startingCount = 10 / cpu.TotalCpuPower;
////			Timer = 10 / cpu.TotalCpuPower;
////			Starttime = 15;
////			CPUUsage = 1;
////			RAMUsage = 128;
////			GPUUsage = 1.5f;
////			CPUUsageWE = 1;
////			RAMUsageWE = 208;
////			GPUUsageWE = 2.5f;
////			DiskUsage = 5;
////        break;
////
////        case 2:
////			startingCount = 9/cpu.TotalCpuPower;
////			Timer = 9/cpu.TotalCpuPower;
////			Starttime = 15;
////			CPUUsage = 1.5f;
////			RAMUsage = 256;
////			GPUUsage = 2;
////			CPUUsageWE = 4;
////			RAMUsageWE = 312;
////			GPUUsageWE = 4.5f;
////			DiskUsage = 7;
////       	break;
////
////		case 3:
////			startingCount = 8/cpu.TotalCpuPower;
////			Timer = 8/cpu.TotalCpuPower;
////			Starttime = 15;
////			CPUUsage = 2.5f;
////			RAMUsage = 403;
////			GPUUsage = 2.65f;
////			CPUUsageWE = 4;
////			RAMUsageWE = 512;
////			GPUUsageWE = 4.5f;
////			DiskUsage = 10;
////			break;
////
////		case 4:
////			startingCount = 8/cpu.TotalCpuPower;
////			Timer = 8/cpu.TotalCpuPower;
////			Starttime = 15;
////			CPUUsage = 2.5f;
////			RAMUsage = 403;
////			GPUUsage = 2.65f;
////			CPUUsageWE = 4;
////			RAMUsageWE = 512;
////			GPUUsageWE = 4.5f;
////			DiskUsage = 10;
////			break;
////
////		case 5:
////			startingCount = 8/cpu.TotalCpuPower;
////			Timer = 8/cpu.TotalCpuPower;
////			Starttime = 15;
////			CPUUsage = 2.5f;
////			RAMUsage = 403;
////			GPUUsage = 2.65f;
////			CPUUsageWE = 4;
////			RAMUsageWE = 512;
////			GPUUsageWE = 4.5f;
////			DiskUsage = 10;
////			break;
////
////		case 6:
////			startingCount = 8/cpu.TotalCpuPower;
////			Timer = 8/cpu.TotalCpuPower;
////			Starttime = 15;
////			CPUUsage = 2.5f;
////			RAMUsage = 403;
////			GPUUsage = 2.65f;
////			CPUUsageWE = 4;
////			RAMUsageWE = 512;
////			GPUUsageWE = 4.5f;
////			DiskUsage = 10;
////			break;
////
////		case 7:
////			startingCount = 8/cpu.TotalCpuPower;
////			Timer = 8/cpu.TotalCpuPower;
////			Starttime = 15;
////			CPUUsage = 2.5f;
////			RAMUsage = 403;
////			GPUUsage = 2.65f;
////			CPUUsageWE = 4;
////			RAMUsageWE = 512;
////			GPUUsageWE = 4.5f;
////			DiskUsage = 10;
////			break;
////
////		case 8:
////			startingCount = 8/cpu.TotalCpuPower;
////			Timer = 8/cpu.TotalCpuPower;
////			Starttime = 15;
////			CPUUsage = 2.5f;
////			RAMUsage = 403;
////			GPUUsage = 2.65f;
////			CPUUsageWE = 4;
////			RAMUsageWE = 512;
////			GPUUsageWE = 4.5f;
////			DiskUsage = 10;
////			break;
////
////		case 9:
////			startingCount = 8/cpu.TotalCpuPower;
////			Timer = 8/cpu.TotalCpuPower;
////			Starttime = 15;
////			CPUUsage = 2.5f;
////			RAMUsage = 403;
////			GPUUsage = 2.65f;
////			CPUUsageWE = 4;
////			RAMUsageWE = 512;
////			GPUUsageWE = 4.5f;
////			DiskUsage = 10;
////			break;
////
////		case 10:
////			startingCount = 8/cpu.TotalCpuPower;
////			Timer = 8/cpu.TotalCpuPower;
////			Starttime = 15;
////			CPUUsage = 2.5f;
////			RAMUsage = 403;
////			GPUUsage = 2.65f;
////			CPUUsageWE = 4;
////			RAMUsageWE = 512;
////			GPUUsageWE = 4.5f;
////			DiskUsage = 10;
////			break;
////        }
//	}
//}