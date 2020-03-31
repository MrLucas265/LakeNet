//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//
//public class Portfolio : MonoBehaviour 
//{
//	public GameObject Computer;
//
//	public Rect windowRect;
//	public float native_width = 1920;
//	public float native_height = 1080;
//	public int windowID;
//
//	public bool show;
//
//	private Computer com;
//	private Defalt def;
//
//	public Rect CloseButton;
//	public Rect MiniButton;
//	public Rect DefaltWindowSize;
//	public Rect DefaltTitleBarSize;
//
//	public bool minimize;
//
//	public string ProgramName;
//
//	public float x;
//	public float y;
//
//	public Vector2 scrollpos = Vector2.zero;
//	public int scrollsize;
//	public List<int> TotalPrices = new List<int>();
//
//	public bool SetRefresh;
//	public bool setAdd;
//
//	void Start() 
//	{
//		Computer = GameObject.Find("System");
//
//		def = Computer.GetComponent<Defalt>();
//		com = Computer.GetComponent<Computer>();
//		native_height = Customize.cust.native_height;
//		native_width = Customize.cust.native_width;
//
//		SetProgramInfo();
//
//		Refresh();
//	}
//
//	void Refresh()
//	{
//		if (TotalPrices.Count >= 1)
//		{
//			TotalPrices.RemoveRange(0, TotalPrices.Count);
//		}
//		for (int i = 0; i < GameControl.control.SharesBoughtOffName.Count; i++)
//		{
//			TotalPrices.Add(GameControl.control.SharesBrought[i] * GameControl.control.SharesBoughtPrice[i]);
//		}
//	}
//
//	void SetProgramInfo()
//	{
//		x = Customize.cust.windowx[37];
//		y = Customize.cust.windowy[37];
//		ProgramName = "Portfolio";
//		windowRect = new Rect(x, y, 400, 200);
//		CloseButton = new Rect(377,2,21,21);
//		MiniButton = new Rect(355,2,21,21);
//		DefaltWindowSize = new Rect(0,0,400,200);
//		DefaltTitleBarSize = new Rect(2,2,352,21);
//	}
//
//	void Update ()
//	{
//		
//	}
//
//	void Minimize()
//	{
//		if (minimize == true) 
//		{
//			windowRect = (new Rect(windowRect.x,windowRect.y,DefaltWindowSize.width,23));
//		}
//		else
//		{
//			windowRect = (new Rect(windowRect.x,windowRect.y,DefaltWindowSize.width,DefaltWindowSize.height));
//		}
//	}
//
//	void OnGUI()
//	{
//		Customize.cust.windowx[windowID] = windowRect.x;
//		Customize.cust.windowy[windowID] = windowRect.y;
//
//		GUI.skin = com.Skin[GameControl.control.GUIID];
//
//		if(show == true)
//		{
//			GUI.color = com.colors[Customize.cust.WindowColorInt];
//			windowRect = GUI.Window(windowID,windowRect,DoMyWindow,"");
//		}
//	}
//
//	void DoMyWindow(int WindowID)
//	{
//		GUI.DragWindow (new Rect (DefaltTitleBarSize));
//
//		if (StockSystem.stockcon.Refresh == true)
//		{
//			Refresh();
//			StockSystem.stockcon.Refresh = false;
//		}
//
//		if (CloseButton.Contains (Event.current.mousePosition)) 
//		{
//			if (GUI.Button (new Rect (CloseButton), "X", com.Skin [GameControl.control.GUIID].customStyles [0])) 
//			{
//				show = false;
//			}
//		} 
//		else
//		{
//			GUI.backgroundColor = com.colors[Customize.cust.ButtonColorInt];
//			GUI.contentColor = com.colors[Customize.cust.FontColorInt];
//			if (GUI.Button (new Rect (CloseButton), "X", com.Skin [GameControl.control.GUIID].customStyles [1])) 
//			{
//				show = false;
//			}
//		}
//
//		GUI.backgroundColor = com.colors[Customize.cust.ButtonColorInt];
//		GUI.contentColor = com.colors[Customize.cust.FontColorInt];
//
//		GUI.Box (new Rect (DefaltTitleBarSize), ProgramName);
//
//		if (MiniButton.Contains (Event.current.mousePosition)) 
//		{
//			if (GUI.Button (new Rect (MiniButton), "-",com.Skin [GameControl.control.GUIID].customStyles [2])) 
//			{
//				minimize = !minimize;
//				Minimize();
//			}
//		} 
//		else
//		{
//			if (GUI.Button (new Rect (MiniButton), "-",com.Skin [GameControl.control.GUIID].customStyles [2])) 
//			{
//				minimize = !minimize;
//				Minimize();
//			}
//		}
//
//		if (TotalPrices.Count == GameControl.control.SharesBoughtOffName.Count) 
//		{
//			RenderPortfolio();
//		}
//	}
//
//	void RenderPortfolio()
//	{
//		if (GUI.Button (new Rect (1,40,40,21), "Slot #"))
//		{
//			
//		}
//
//		if (GUI.Button (new Rect (42,40,160,21), "Company Name"))
//		{
//
//		}
//
//		if (GUI.Button (new Rect (203,40,51,21), "QTY"))
//		{
//
//		}
//
//		if (GUI.Button (new Rect (255,40,50,21), "$Each"))
//		{
//
//		}
//
//		if (GUI.Button (new Rect (306,40,75,21), "Total Price"))
//		{
//
//		}
//
//		scrollpos = GUI.BeginScrollView(new Rect(1, 65, 3977, 131), scrollpos, new Rect(0, 0, 0, scrollsize*22));
//		for (scrollsize = 0; scrollsize < GameControl.control.SharesBoughtOffName.Count; scrollsize++)
//		{
//			if (GUI.Button (new Rect (0,scrollsize * 22,40,21), "#" + scrollsize))
//			{
//				StockSystem.stockcon.SelectedPortIndex = scrollsize;
//			}
//			
//			if (GUI.Button (new Rect (41,scrollsize * 22,160,21), "" + GameControl.control.SharesBoughtOffName[scrollsize]))
//			{
//				StockSystem.stockcon.SelectedPortIndex = scrollsize;
//			}
//
//			if (GUI.Button (new Rect (202,scrollsize * 22,51,21), "" + GameControl.control.SharesBrought[scrollsize]))
//			{
//				StockSystem.stockcon.SelectedPortIndex = scrollsize;
//			}
//
//			if (GUI.Button (new Rect (254,scrollsize * 22,50,21), "$" + GameControl.control.SharesBoughtPrice[scrollsize]))
//			{
//				StockSystem.stockcon.SelectedPortIndex = scrollsize;
//			}
//
//			if (GUI.Button (new Rect (305,scrollsize * 22,75,21), "$" + TotalPrices[scrollsize]))
//			{
//				StockSystem.stockcon.SelectedPortIndex = scrollsize;
//			}
//		}
//		GUI.EndScrollView();
//	}
//}
