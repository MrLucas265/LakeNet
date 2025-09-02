using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SystemMap : MonoBehaviour 
{	
	GUIStyle Map;
	private GameObject SysApp;
	private GameObject SoftApp;
	private GameObject HackApp;
	public bool show;
	private Computer com;
	private InternetBrowser ib;
	private Defalt defalt;
	private Tracer tt;
	public int windowID;
	public Vector2 GatewayPos;
	public Rect windowRect = new Rect(100, 100, 200, 200);
	public float native_width = 1920;
	public float native_height = 1080;
	public bool Drag;
	public Texture2D texture;
	public List<Vector2> BouncedConnections = new List<Vector2>();
	public List<string> KnowenConnections = new List<string>();
	public List<string> LastBounce = new List<string>();
	public List<string> BounceIPs = new List<string>();

	public string Message;

	public int Total;

	public int Index;

	public int ConnectionsLeft;

	public float PercentageChange;


	public Vector2 JaildewPos = new Vector2(200, 60);
	public string JaildewIP = "192.168.88";
	public Vector2 UniconPos = new Vector2(25, 60);
	public string UniconIP = "192.168.27";
	public Vector2 RevaPos = new Vector2(35, 95);
	public string RevaIP = "192.168.96";
	public Vector2 RevaTestPos = new Vector2(90, 110);
	public string RevaTestIP = "192.168.17";
	public Vector2 LecBankPos = new Vector2(100, 75);
	public string LecBankIP = "192.168.21";
	public Vector2 AcademicPos = new Vector2(125, 60);
	public string AcademicIP = "192.168.11";
	public Vector2 BecasPos = new Vector2(150, 100);
	public string BecasIP = "192.168.11";
	public Vector2 CabbagePos = new Vector2(200, 100);
	public string CabbageIP = "192.168.26";

	public string CompanyName = "Masssive Electronic Producer";
	public string CompanyAbv = "MEP";
	public string CompanyIP = "128.0.0.1";
	public Vector2 CompanyCords = new Vector2(0, 0);

	public float w;
	public float h;
	public float wh;

	public bool maximize;
	public bool minimize;

	public float WidthScale;
	public float HeightScale;

	public float ZoomLevel;

	public float MapViewLevel;

	public float MaxZoom;

	public float PanHorizontal;
	public float PanVertical;

	public float PanningSpeed;
	public Vector3 lastMousePos = Vector3.zero;

	public float DefaultX;
	public float DefaultY;

	public float ClampDiff;
	public float RightClamp;
	public float DownClamp;

	public float ModX;
	public float ModY;

	public float ComX;
	public float ComY;

	public float TestX;
	public float TestY;

	public float MathX = 0;
	public float ZoomInt = 0;

	// Use this for initialization
	void Start ()
	{
		SysApp = GameObject.Find("System");
		SoftApp = GameObject.Find("Applications");
		HackApp = GameObject.Find("Hacking");

		com = SysApp.GetComponent<Computer>();
		ib = SoftApp.GetComponent<InternetBrowser>();
		tt = HackApp.GetComponent<Tracer>();
		defalt  = SysApp.GetComponent<Defalt>();
		//windowRect.x = GameControl.control.windowx[windowID];
		//windowRect.y = GameControl.control.windowy[windowID];

		windowRect.x = 83;
		windowRect.y = 20;

		DefaultX = -15;
		DefaultY = 30;
		w = 30;
		h = 15;

		HeightScale = 1;
		WidthScale = 1;

		native_height = Customize.cust.native_height;
		native_width = Customize.cust.native_width;

		AddGateWayPos();

		UpdateGatewayPos();

		PanningSpeed = 25;
		ZoomLevel = 0.98f;
		PanHorizontal = 40;

		RevaTestPos = new Vector2(110, 90);
		JaildewPos = new Vector2(200, 60);
		UniconPos = new Vector2(25, 60);
		RevaPos = new Vector2(35, 95);
		LecBankPos = new Vector2(80, 75);
		AcademicPos = new Vector2 (125, 60);
	}

	void AddGateWayPos()
	{
//		if (GatewayPos != GameControl.control.GatewayPos) 
//		{
//			GatewayPos = GameControl.control.GatewayPos;
//			BounceIPs.Add (GameControl.control.fullip);
//		}
	}

	void UpdateGatewayPos()
	{
		GatewayPos.x = GameControl.control.GatewayPosX;
		GatewayPos.y = GameControl.control.GatewayPosY;
	}

	void Update()
	{

	}

	void Minimize()
	{
		if (minimize == true) 
		{
			windowRect = (new Rect(windowRect.x,windowRect.y,windowRect.width,25));
		}
		else
		{
			//private Rect DefaltSetting = new Rect (windowRect.x,windowRect.y,300,205);
			windowRect = (new Rect(windowRect.x,windowRect.y,windowRect.width,300));
		}
	}

	void Maximize()
	{
		if (maximize == true) 
		{
			native_width = 940;
			native_height = 560;
		}
		else
		{
			//private Rect DefaltSetting = new Rect (windowRect.x,windowRect.y,300,205);
			native_width = 626;
			native_height = 373;
		}
	}

	public void Disconnect()
	{
		ib.SiteName = "";
		ib.Inputted = "";
		ib.AddressBar = "";
		BounceIPs.Clear ();
		BouncedConnections.Clear ();
		LastBounce.Clear ();
		tt.startTrace = false;
		ib.showAddressBar = true;
		ib.SiteAdminPass = StringGenerator.RandomMixedChar(8, 8);
	}

	public void Connect()
	{
		switch (ib.Inputted) 
		{
		case "www.unicom.com":
			if (!BouncedConnections.Contains (UniconPos)) 
			{
				BouncedConnections.Add (UniconPos);
				BounceIPs.Add (UniconIP);
			}
			break;
		case "www.jaildew.com":
			if (!BouncedConnections.Contains (JaildewPos)) 
			{
				BouncedConnections.Add (JaildewPos);
				BounceIPs.Add (JaildewIP);
			}
			break;
		case "www.becassystems.com":
			if (!BouncedConnections.Contains (BecasPos)) 
			{
				BouncedConnections.Add (BecasPos);
				BounceIPs.Add (BecasIP);
			}
			break;
		}
	}

	void OnGUI()
	{
		GUI.skin = GameControl.control.Skins[Registry.GetIntData("Player", "System", "Skin")];
		Map = GameControl.control.Skins[Registry.GetIntData("Player", "System", "Skin")].customStyles[3];
		Customize.cust.windowx[windowID] = windowRect.x;
		Customize.cust.windowy[windowID] = windowRect.y;

		//float rx = Screen.width / native_width;
		//float ry = Screen.height / native_height;

		//GUI.matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, new Vector3(rx, ry, 1));

		w = 30 * WidthScale;
		h = 15 * HeightScale;

		if(show == true)
		{
			windowRect = WindowClamp.ClampToScreen(GUI.Window(windowID,windowRect,DoMyWindow,""));
		}
	}

	void ZoomMath()
	{
		ComX = ClampDiff * 10 * 70;
		ComY = ClampDiff * 10 * 25;

		ClampDiff = ZoomLevel - 1.1f;
		RightClamp = 0-ClampDiff * 10 * 70;
		DownClamp = 0-ClampDiff * 10 * 25;

		if (Event.current.type == EventType.KeyDown && Event.current.keyCode == KeyCode.D || Event.current.type == EventType.KeyDown && Event.current.keyCode == KeyCode.RightArrow) 
		{
			if (PanHorizontal > RightClamp) 
			{
				PanHorizontal -= 1 * PanningSpeed;
				ModX -= PanningSpeed;
			}
		}
		if (Event.current.type == EventType.KeyDown && Event.current.keyCode == KeyCode.A || Event.current.type == EventType.KeyDown && Event.current.keyCode == KeyCode.LeftArrow) 
		{
			if (PanHorizontal < 0) 
			{
				PanHorizontal += 1 * PanningSpeed;
				ModX += PanningSpeed;
			}
		}
		if (Event.current.type == EventType.KeyDown && Event.current.keyCode == KeyCode.W || Event.current.type == EventType.KeyDown && Event.current.keyCode == KeyCode.UpArrow) 
		{
			if (PanVertical >= 0)
			{

			} 
			else
			{
				PanVertical += 1 * PanningSpeed;
				ModY += PanningSpeed;
			}
		}
		if (Event.current.type == EventType.KeyDown && Event.current.keyCode == KeyCode.S || Event.current.type == EventType.KeyDown && Event.current.keyCode == KeyCode.DownArrow) 
		{
			if(PanVertical > DownClamp)
				PanVertical -= 1 * PanningSpeed;
			ModY -= PanningSpeed;

			if(ModY <= 0)
			{
				ModY = 0;
			}
		}

		if (Event.current.type == EventType.KeyDown && Event.current.keyCode == KeyCode.Equals || Event.current.type == EventType.KeyDown && Event.current.keyCode == KeyCode.KeypadPlus) 
		{
			if (ZoomLevel <= 3.9) 
			{
				ZoomLevel += 0.1f;
				ModX += 75-TestX;//75
				ModY += 25-TestY;//25
			}

			ZoomInt = ZoomLevel * 10 - 10;

			MathX = 20 * ZoomInt;
		}
		if (Event.current.type == EventType.KeyDown && Event.current.keyCode == KeyCode.Minus || Event.current.type == EventType.KeyDown && Event.current.keyCode == KeyCode.KeypadMinus) 
		{
			if (ZoomLevel >= 1.1f) 
			{
				ZoomLevel -= 0.1f;
				ModX -= 75-TestX;//75
				ModY -= 25-TestY;//25
				if (PanHorizontal >= 0)
				{
					PanHorizontal += 50;
					PanHorizontal -= 50;
				}
				if (PanHorizontal <= RightClamp) 
				{
					PanHorizontal += 50;
					PanHorizontal += 50;
				}
			}

			ZoomInt = ZoomLevel * 10 - 10;

			MathX = 20 * ZoomInt;
		}

		if (PanHorizontal <= RightClamp) 
			PanHorizontal = RightClamp;

		if (PanVertical <= DownClamp) 
			PanVertical = DownClamp;

		if (ZoomLevel <= 1.1f) 
		{
			ModX = 0;
			ModY = 0;
			PanHorizontal = 0;
			PanVertical = 0;
			ZoomLevel = 1.1f;
		}
	}

	void DrawLine()
	{
		if (LastBounce.Count > 1)
		{
			LastBounce.RemoveAt(0);
		}

		if (BouncedConnections.Count > 0) 
		{
			if (ConnectionsLeft >= 1) 
			{
				Drawing.DrawLine(GatewayPos,BouncedConnections[0],Color.white,2,false);
			}
			else
			{
				Drawing.DrawLine(GatewayPos,BouncedConnections[0],Color.red,2,false);
			}
		}

		if (BouncedConnections.Count > 1) 
		{
			if (ConnectionsLeft >= 2) 
			{
				Drawing.DrawLine(BouncedConnections[0],BouncedConnections[1],Color.white,2,false);
			}
			else
			{
				Drawing.DrawLine(BouncedConnections[0],BouncedConnections[1],Color.red,2,false);
			}
		}

		if (BouncedConnections.Count > 2) 
		{
			if (ConnectionsLeft >= 3) 
			{
				Drawing.DrawLine(BouncedConnections[1],BouncedConnections[2],Color.white,2,false);
			}
			else
			{
				Drawing.DrawLine(BouncedConnections[1],BouncedConnections[2],Color.red,2,false);
			}
		}

		if (BouncedConnections.Count > 3) 
		{
			if (ConnectionsLeft >= 4) 
			{
				Drawing.DrawLine(BouncedConnections[2],BouncedConnections[3],Color.white,2,false);
			}
			else
			{
				Drawing.DrawLine(BouncedConnections[2],BouncedConnections[3],Color.red,2,false);
			}
		}

		if (BouncedConnections.Count > 4) 
		{
			if (ConnectionsLeft >= 5) 
			{
				Drawing.DrawLine(BouncedConnections[3],BouncedConnections[4],Color.white,2,false);
			}
			else
			{
				Drawing.DrawLine(BouncedConnections[3],BouncedConnections[4],Color.red,2,false);
			}
		}

		if (BouncedConnections.Count > 5) 
		{
			if (ConnectionsLeft >= 6) 
			{
				Drawing.DrawLine(BouncedConnections[4],BouncedConnections[5],Color.white,2,false);
			}
			else
			{
				Drawing.DrawLine(BouncedConnections[4],BouncedConnections[5],Color.red,2,false);
			}
		}

		if (BouncedConnections.Count > 6) 
		{
			if (ConnectionsLeft >= 7) 
			{
				Drawing.DrawLine(BouncedConnections[5],BouncedConnections[6],Color.white,2,false);
			}
			else
			{
				Drawing.DrawLine(BouncedConnections[5],BouncedConnections[6],Color.red,2,false);
			}
		}

		if (BouncedConnections.Count > 7) 
		{
			if (ConnectionsLeft >= 8) 
			{
				Drawing.DrawLine (BouncedConnections [6], BouncedConnections [7], Color.white, 2, false);
			}
			else
			{
				Drawing.DrawLine (BouncedConnections [6], BouncedConnections [7], Color.red, 2, false);
			}
		}
	}

	void DoMyWindow(int WindowID)
	{
		GUI.DragWindow(new Rect(1,1,windowRect.width-22,21));
		GUI.Box(new Rect(1,1,windowRect.width-22,21), "Map Viewer");
		GUI.DrawTexture(new Rect(-15+PanHorizontal,30+PanVertical,560*ZoomLevel,240*ZoomLevel), texture);
		if(GUI.Button(new Rect(windowRect.width-22,1,21,21),"X"))
		{
			show = false;
		}

		if (BouncedConnections.Count > 0) 
		{
			if (ib.AddressBar != "")
			{
				Message = "Connected";
			}
			else 
			{
				Message = "Connect";
			}
			if(GUI.Button(new Rect(5,270,80,21),Message))
			{

				//Domain.Search(LastBounce[0]);

				switch(LastBounce[0])
				{
				case "JD":
					ib.AddressBar = "www.jaildew.com";
					ib.Inputted = "www.jaildew.com";
					break;
				case "UC":
					ib.AddressBar = "www.unicon.com";
					ib.Inputted = "www.unicon.com";
					break;
				case "REV":
					ib.AddressBar = "www.reva.com";
					ib.Inputted = "www.reva.com";
					break;
				case "RTS":
					ib.AddressBar = "www.revatest.com";
					ib.Inputted = "www.revatest.com";
					break;
				case "LEC":
					ib.AddressBar = "www.lecbank.com";
					ib.Inputted = "www.lecbank.com";
					break;
				case "ACA":
					ib.AddressBar = "www.academicstudies.com/signin";
					ib.Inputted = "www.academicstudies.com/signin";
					break;
				case "BCAS":
					ib.AddressBar = "www.becassystems.com";
					ib.Inputted = "www.becassystems.com";
					break;
				case "CABC":
					ib.AddressBar = "www.cabbagecorp.com";
					ib.Inputted = "www.cabbagecorp.com";
					break;
				}
			}
		}

		if (ib.AddressBar == "")
		{
			if (GUI.Button (new Rect (95, 270, 80, 20), "Clear")) 
			{
				BounceIPs.Clear ();
				BouncedConnections.Clear ();
				LastBounce.Clear ();
				ib.AddressBar = "";
				ib.Inputted = "";
			}
		} 
		else 
		{
			if (GUI.Button (new Rect (95, 270, 80, 20), "Disconnect")) 
			{
				Disconnect();
			}
		}

		if (BouncedConnections.Count > 0)
		{
			PercentageChange = 100 / BouncedConnections.Count;
		}

		GUI.Button (new Rect (GatewayPos.x + PanHorizontal + MathX, GatewayPos.y+PanVertical, 30, 15), "GW");

		if (GameControl.control.Sites.Contains ("www.cabbagecorp.com"))
		{
			if (GUI.Button (new Rect (UniconPos.x + PanHorizontal + MathX, UniconPos.y, w, h), "CABC",Map)) 
			{
				if (BouncedConnections.Contains (CabbagePos)) 
				{
					BouncedConnections.Remove (CabbagePos);

					if (BounceIPs.Contains (CabbageIP))
					{
						BounceIPs.Remove (CabbageIP);
					} 
				} 
				else
				{
					if (LastBounce.Contains ("UC"))
					{

					} 
					else
					{
						LastBounce.Add ("UC");
					}
					BouncedConnections.Add (CabbagePos);

					if (!BounceIPs.Contains (CabbageIP))
					{
						BounceIPs.Add (CabbageIP);
					} 
				}
			}
		}

		if (GameControl.control.Sites.Contains ("www.unicon.com"))
		{
			if (GUI.Button (new Rect (UniconPos.x + PanHorizontal + MathX, UniconPos.y, w, h), "UC",Map)) 
			{
				if (BouncedConnections.Contains (UniconPos)) 
				{
					BouncedConnections.Remove (UniconPos);

					if (BounceIPs.Contains (UniconIP))
					{
						BounceIPs.Remove (UniconIP);
					} 
				} 
				else
				{
					if (LastBounce.Contains ("UC"))
					{

					} 
					else
					{
						LastBounce.Add ("UC");
					}
					BouncedConnections.Add (UniconPos);

					if (!BounceIPs.Contains (UniconIP))
					{
						BounceIPs.Add (UniconIP);
					} 
				}
			}
		}

		if (GameControl.control.Sites.Contains("www.jaildew.com"))
		{
			if (GUI.Button (new Rect (JaildewPos.x+PanHorizontal+MathX, JaildewPos.y + PanVertical, w, h), "JD",Map)) 
			{
				if (BouncedConnections.Contains (JaildewPos)) 
				{
					BouncedConnections.Remove (JaildewPos);
					BounceIPs.Remove (JaildewIP);
				} 
				else
				{
					if (LastBounce.Contains ("JD"))
					{

					} 
					else
					{
						LastBounce.Add ("JD");
					}
					BouncedConnections.Add (JaildewPos);
					BounceIPs.Add(JaildewIP);
					ConnectionsLeft++;
				}
			}
		}

		if (GameControl.control.Sites.Contains ("www.becassystems.com"))
		{
			if (GUI.Button (new Rect (BecasPos.x + PanHorizontal + MathX, BecasPos.y, w+10, h), "BCAS",Map)) 
			{
				if (BouncedConnections.Contains (BecasPos)) 
				{
					BouncedConnections.Remove (BecasPos);
					BounceIPs.Remove (BecasIP);
				} 
				else
				{
					if (LastBounce.Contains ("BAS"))
					{

					} 
					else
					{
						LastBounce.Add ("BAS");
					}
					BouncedConnections.Add (BecasPos);
					BounceIPs.Add(BecasIP);
					ConnectionsLeft++;
				}
			}
		}

		if (GUI.Button (new Rect (RevaPos.x + PanHorizontal + MathX, RevaPos.y, w, h), "RVA",Map)) 
		{
			if (BouncedConnections.Contains (RevaPos)) 
			{
				BouncedConnections.Remove (RevaPos);
				BounceIPs.Remove (RevaIP);
			} 
			else
			{
				if (LastBounce.Contains ("REV"))
				{

				} 
				else
				{
					LastBounce.Add ("REV");
				}
				BouncedConnections.Add (RevaPos);
				BounceIPs.Add(RevaIP);
				ConnectionsLeft++;
			}
		}

		if (GUI.Button (new Rect (LecBankPos.x + PanHorizontal + MathX, LecBankPos.y, w, h), "LEC",Map)) 
		{
			if (BouncedConnections.Contains (LecBankPos)) 
			{
				BouncedConnections.Remove (LecBankPos);
				BounceIPs.Remove (LecBankIP);
			} 
			else
			{
				if (LastBounce.Contains ("LEC"))
				{

				} 
				else
				{
					LastBounce.Add ("LEC");
				}
				BouncedConnections.Add (LecBankPos);
				BounceIPs.Add(LecBankIP);
				ConnectionsLeft++;
			}
		}

		if (GUI.Button (new Rect (RevaTestPos.x + PanHorizontal + MathX, RevaTestPos.y, w, h), "RTS",Map)) 
		{
			if (BouncedConnections.Contains (RevaTestPos)) 
			{
				BouncedConnections.Remove (RevaTestPos);
				BounceIPs.Remove (RevaTestIP);
			} 
			else
			{
				if (LastBounce.Contains ("RTS"))
				{

				} 
				else
				{
					LastBounce.Add ("RTS");
				}
				BouncedConnections.Add (RevaTestPos);
				BounceIPs.Add(RevaTestIP);
				ConnectionsLeft++;
			}
		}
		if (GameControl.control.Sites.Contains ("www.academicstudies.com"))
		{
			if (GUI.Button (new Rect (AcademicPos.x + PanHorizontal + MathX, AcademicPos.y, w, h), "ACA",Map)) 
			{
				if (BouncedConnections.Contains (AcademicPos)) 
				{
					BouncedConnections.Remove (AcademicPos);
					BounceIPs.Remove (AcademicIP);
				} 
				else
				{
					if (LastBounce.Contains ("ACA"))
					{

					} 
					else
					{
						LastBounce.Add ("ACA");
					}
					BouncedConnections.Add (AcademicPos);
					BounceIPs.Add(AcademicIP);
					ConnectionsLeft++;
				}
			}
		}

		DrawLine();

		ZoomMath();
	}
}
