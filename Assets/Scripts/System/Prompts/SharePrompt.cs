//using UnityEngine;
//using System.Collections;
//
//public class SharePrompt : MonoBehaviour
//{
//	public Rect windowRect = new Rect(100, 100, 200, 200);
//	public float native_width = 1920;
//	public float native_height = 1080;
//	public int windowID;
//
//	public string ConfirmMsg;
//	public string ConfirmTitle;
//
//	public bool show;
//
//	public bool playsound;
//
//	private GameObject ComputerObject;
//	private Defalt def;
//	private Computer com;
//	private Upgrade upg;
//
//	public bool Bought;
//
//	public int ShareQTY;
//	public float Value;
//
//	public bool showDropDown;
//
//	public Rect CloseButton;
//
//	// Use this for initialization
//	void Start () 
//	{
//		ComputerObject = GameObject.Find("System");
//		com = ComputerObject.GetComponent<Computer>();
//		def = ComputerObject.GetComponent<Defalt>();
//
//		native_height = Customize.cust.native_height;
//		native_width = Customize.cust.native_width;
//		CloseButton = new Rect(178, 1, 21, 21);
//		ConfirmTitle = "Share Trading System";
//	}
//
//	// Update is called once per frame
//	void Update () 
//	{
//		
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
//		if (CloseButton.Contains (Event.current.mousePosition)) 
//		{
//			if (GUI.Button (new Rect (CloseButton), "X", com.Skin [GameControl.control.GUIID].customStyles [0])) 
//			{
//			}
//		} 
//		else
//		{
//			GUI.backgroundColor = com.colors[Customize.cust.ButtonColorInt];
//			GUI.contentColor = com.colors[Customize.cust.FontColorInt];
//			if (GUI.Button (new Rect (CloseButton), "X", com.Skin [GameControl.control.GUIID].customStyles [1])) 
//			{
//			}
//		}
//
//		GUI.backgroundColor = Color.white;
//
//		Value = GUI.HorizontalSlider(new Rect (1, 24, 198, 21),Value,0,100);
//
//		GUI.backgroundColor = com.colors[Customize.cust.ButtonColorInt];
//		GUI.contentColor = com.colors[Customize.cust.FontColorInt];
//
//		GUI.DragWindow (new Rect (1, 1, 170, 21));
//		GUI.Box (new Rect (1, 1, 176, 21), ConfirmTitle);
//		GUI.TextArea((new Rect (1, 130, 198, 70)),ConfirmMsg);
//
//		ShareQTY = (int)Value;
//
//		GUI.Label (new Rect (1, 30, 200, 200),"Purchase Share Ammount: " + ShareQTY);
//		GUI.Label (new Rect (1, 50, 200, 200),"Company Name: " + StockSystem.stockcon.SelectedCompanyName);
//		GUI.Label (new Rect (1, 70, 200, 200),"Current Price Per Share: " + StockSystem.stockcon.SelectedPrice);
//		GUI.Label (new Rect (1, 90, 200, 200),"Total Share Price: " + StockSystem.stockcon.SelectedPrice * ShareQTY);
//
//		if (GUI.Button (new Rect (70, 108, 60, 20), "Refresh")) 
//		{
//			StockSystem.stockcon.SelectedPrice = StockSystem.stockcon.CurrentSharePrice[StockSystem.stockcon.SelectedCompanyIndex];
//		}
//
//		if(GUI.Button(new Rect(148, 108, 50, 20),"Buy"))
//		{
//			ConfirmMsg = "";
//
//			if(GameControl.control.Balance[GameControl.control.SelectedBank] < StockSystem.stockcon.CurrentSharePrice[StockSystem.stockcon.SelectedCompanyIndex] * ShareQTY)
//			{
//				//ConfirmTitle = "Error-269 Transaction Error";
//				ConfirmMsg = "Error-269 Transaction Error. Transaction Could not complete due to insuffcient funds.";
//			}
//
//			if(StockSystem.stockcon.SelectedPrice != StockSystem.stockcon.CurrentSharePrice[StockSystem.stockcon.SelectedCompanyIndex])
//			{
//				//ConfirmTitle = "Internal Error-501";
//				ConfirmMsg = "Internal Error-501. Transaction Could not complete due to stock update please refresh.";
//			}
//
//			if(StockSystem.stockcon.CurrentSharePrice[StockSystem.stockcon.SelectedCompanyIndex] == StockSystem.stockcon.SelectedPrice)
//			{
//				if (ShareQTY > 0)
//				{
//					if(GameControl.control.Balance[GameControl.control.SelectedBank] >= StockSystem.stockcon.CurrentSharePrice[StockSystem.stockcon.SelectedCompanyIndex] * ShareQTY)
//					{
//						GameControl.control.Balance[GameControl.control.SelectedBank] -= StockSystem.stockcon.CurrentSharePrice[StockSystem.stockcon.SelectedCompanyIndex] * ShareQTY;
//						GameControl.control.SharesBoughtOffName.Add (StockSystem.stockcon.SelectedCompanyName);
//						GameControl.control.SharesBoughtPrice.Add (StockSystem.stockcon.SelectedPrice);
//						GameControl.control.SharesBrought.Add (ShareQTY);
//						playsound = true;
//						ConfirmMsg = "Transaction Completed Successfully.";
//						StockSystem.stockcon.Refresh = true;
//					}
//				}
//
//				if (ShareQTY <= 0)
//				{
//					ConfirmMsg = "Internal Error-502. Transaction Could not complete due to lack of set Ammount of Shares";
//				}
//			}
//		}
//
//		if(GUI.Button(new Rect(2, 108, 50, 20),"Sell"))
//		{
//			ConfirmMsg = "";
//
//			if (StockSystem.stockcon.SelectedPortIndex >= GameControl.control.SharesBrought.Count)
//			{
//				ConfirmMsg = "Internal Error-504 Transaction Error. Transaction Could not complete due to share not found please select another.";
//			}
//
//			if (StockSystem.stockcon.SelectedPortIndex < GameControl.control.SharesBrought.Count)
//			{
//				if(GameControl.control.SharesBrought[StockSystem.stockcon.SelectedPortIndex] < ShareQTY)
//				{
//					//ConfirmTitle = "Error-269 Transaction Error";
//					ConfirmMsg = "Internal Error-503 Transaction Error. Transaction Could not complete due to insuffcient shares.";
//				}
//
//				if(StockSystem.stockcon.SelectedPrice != StockSystem.stockcon.CurrentSharePrice[StockSystem.stockcon.SelectedCompanyIndex])
//				{
//					//ConfirmTitle = "Internal Error-501";
//					ConfirmMsg = "Internal Error-501. Transaction Could not complete due to stock update please try again.";
//				}
//
//				if(StockSystem.stockcon.CurrentSharePrice[StockSystem.stockcon.SelectedCompanyIndex] == StockSystem.stockcon.SelectedPrice)
//				{
//					if (ShareQTY > 0)
//					{
//						if(GameControl.control.SharesBrought[StockSystem.stockcon.SelectedPortIndex] >= ShareQTY)
//						{
//							GameControl.control.Balance[GameControl.control.SelectedBank] += StockSystem.stockcon.CurrentSharePrice[StockSystem.stockcon.SelectedCompanyIndex] * ShareQTY;
//							GameControl.control.SharesBrought[StockSystem.stockcon.SelectedPortIndex] -= ShareQTY;
//
//							if (GameControl.control.SharesBrought [StockSystem.stockcon.SelectedPortIndex] <= 0) 
//							{
//								GameControl.control.SharesBoughtOffName.RemoveAt (StockSystem.stockcon.SelectedPortIndex);
//								GameControl.control.SharesBoughtPrice.RemoveAt (StockSystem.stockcon.SelectedPortIndex);
//								GameControl.control.SharesBrought.RemoveAt (StockSystem.stockcon.SelectedPortIndex);
//								StockSystem.stockcon.Refresh = true;
//							}
//
//							playsound = true;
//							ConfirmMsg = "Transaction Completed Successfully.";
//						}
//					}
//
//					if (ShareQTY <= 0)
//					{
//						ConfirmMsg = "Internal Error-502. Transaction Could not complete due to lack of set Ammount of Shares";
//					}
//				}
//			}
//		}
//
//		if(GUI.Button(new Rect(300, 125, 50, 20),"Cancel"))
//		{
//		}
//	}
//}
