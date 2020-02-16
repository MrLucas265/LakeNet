using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CornerWindows : MonoBehaviour 
{
	public int windowID;
	public Rect windowRect = new Rect(100, 100, 200, 200);
	public float native_width = 1920;
	public float native_height = 1080;
	public bool Drag;
	private Computer com;
	public GameObject computer;
	public bool show;
	public bool minimize;
	public Rect DefaltSetting;
	public Rect DefaltBoxSetting;
	public Rect CloseButton;
	public Rect MiniButton;

	public Rect LeftCorn;
	public Rect RightCorn;
	public Rect BottemEdge;
	public Rect RightEdge;

	public bool RightCornerPressed;
	public bool ButtomEdgePressed;
	public bool RightEdgePressed;

	public float Boundry;


	//CPU Stats
	public float MaxClockSpeed;
	public float CurrentClockSpeed;
	public float Voltage;
	public float Effeciency;
	public float TDP;
	public bool Boost;

	//Fan Stats
	public float MaxFlowRate;
	public float MaxFanRPM;
	public float FanSize;
	public float FlowRate;
	public float FanSpeed;
	public float FanEff;
	public float Wattage;

	public float Temp;

	public float Timer;
	public float Cooldown;

	// Use this for initialization
	void Start () 
	{
		computer = GameObject.Find("System");
		com = computer.GetComponent<Computer>();
		windowRect.x = Customize.cust.windowx[windowID];
		windowRect.y = Customize.cust.windowy[windowID];

		Cooldown = 0.0001f;
		Voltage = 1.3f;

		MaxFanRPM = 4800;
		FanSize = 92;
		FanEff = 0.9f;

		MaxClockSpeed = 3.1f;
		Effeciency = 11.8f;

		MaxFlowRate = MaxFanRPM / FanSize * 2 * FanEff;

		UpdatePos();
	}

	void StatSetup()
	{
		TDP = Effeciency * Voltage * 2 * CurrentClockSpeed;

		FlowRate = FanSpeed / FanSize * 2 * FanEff;
		Wattage = FanSpeed / FanSize / 10 * FanEff;
	}

	void UpdatePos()
	{
		DefaltBoxSetting = new Rect(0,0,windowRect.width - 40,20);
		DefaltSetting = new Rect(windowRect.x,windowRect.y,200,200);
		CloseButton = new Rect(windowRect.width-20,0,20,20);
		MiniButton = new Rect(windowRect.width - 40,0,20,20);

		//LeftCorn = new Rect(0,windowRect.height - 10,10,10);
		RightCorn = new Rect(windowRect.width - Boundry,windowRect.height - Boundry,Boundry,Boundry);
		BottemEdge = new Rect(0,windowRect.height - Boundry,windowRect.width - Boundry,Boundry);
		RightEdge = new Rect(windowRect.width - Boundry,0,Boundry,windowRect.height - Boundry);
	}

	// Update is called once per frame
	void Update ()
	{
		StatSetup();
		if (FanSpeed <= MaxFanRPM)
		{
			FanSpeed += 1f;
		}

		Timer -= 1 * Time.deltaTime;

		if (Timer <= 0)
		{
			Timer = Cooldown;

			if (Boost == true)
			{
				CurrentClockSpeed += 0.001f;
			}

			if (CurrentClockSpeed > MaxClockSpeed)
			{
				CurrentClockSpeed -= 0.001f;
			}

			if (Temp > 60) 
			{
				CurrentClockSpeed -= 0.000001f * FlowRate;
				Boost = false;
			}

			if (FlowRate < TDP)
			{
				if (FanSpeed <= MaxFanRPM)
				{
					FanSpeed += 1f;
				}
			}

			if (MaxFlowRate > TDP)
			{
				Boost = true;

				if (FanSpeed > 0)
				{
					FanSpeed -= 1f;
				}
			}

			Temp += 0.001f * TDP;
			Temp -= 0.001f * FlowRate;
		}
	}

	void Minimize()
	{
		if (minimize == true) 
		{
			windowRect = (new Rect(windowRect.x,windowRect.y,DefaltSetting.width,20));
		}
		else
		{
			//private Rect DefaltSetting = new Rect (windowRect.x,windowRect.y,300,205);
			windowRect = (new Rect(windowRect.x,windowRect.y,DefaltSetting.width,DefaltSetting.height));
		}
	}

	void OnGUI()
	{
		GUI.skin = com.Skin[GameControl.control.GUIID];

		Customize.cust.windowx[windowID] = windowRect.x;
		Customize.cust.windowy[windowID] = windowRect.y;

		//float rx = Screen.width / native_width;
		//float ry = Screen.height / native_height;

		//GUI.matrix = Matrix4x4.TRS (Vector3.zero, Quaternion.identity, new Vector3 (rx, ry, 1));

		if(show == true)
		{
			GUI.color = com.colors[Customize.cust.WindowColorInt];
			windowRect = GUI.Window(windowID,windowRect,DoMyWindow,""); 
		}
	}

	void ReSizeWindows()
	{
		if(RightCorn.Contains(Event.current.mousePosition))
		{
			if(Event.current.type == EventType.MouseDown)
			{
				Event.current.mousePosition = RightCorn.center;
				RightCornerPressed = true;
			}
			if(Event.current.type == EventType.MouseUp)
			{
				RightCornerPressed = false;
			}
		}
		if(!RightCorn.Contains(Event.current.mousePosition) && RightCornerPressed == true)
		{
			if(Event.current.type == EventType.MouseDown)
			{
				Event.current.mousePosition = RightCorn.center;
				RightCornerPressed = true;
			}
			if(Event.current.type == EventType.MouseUp)
			{
				RightCornerPressed = false;
			}
		}
		if(RightCornerPressed && Event.current.type == EventType.MouseDrag)
		{
			windowRect.width += Event.current.delta.x;
			windowRect.height += Event.current.delta.y;
			UpdatePos();
		}

		//Bottem Bar
		if(BottemEdge.Contains(Event.current.mousePosition))
		{
			if(Event.current.type == EventType.MouseDown)
			{
				Event.current.mousePosition = BottemEdge.center;
				ButtomEdgePressed = true;
			}
			if(Event.current.type == EventType.MouseUp)
			{
				ButtomEdgePressed = false;
			}
		}
		if(!BottemEdge.Contains(Event.current.mousePosition) && ButtomEdgePressed == true)
		{
			if(Event.current.type == EventType.MouseDown)
			{
				Event.current.mousePosition = BottemEdge.center;
				ButtomEdgePressed = true;
			}
			if(Event.current.type == EventType.MouseUp)
			{
				ButtomEdgePressed = false;
			}
		}
		if(ButtomEdgePressed && Event.current.type == EventType.MouseDrag)
		{
			windowRect.height += Event.current.delta.y;
			UpdatePos();
		}

		//RightEdge
		if(RightEdge.Contains(Event.current.mousePosition) && RightEdgePressed == false)
		{
			if(Event.current.type == EventType.MouseDown)
			{
				RightEdgePressed = true;
			}
			if(Event.current.type == EventType.MouseUp)
			{
				RightEdgePressed = false;
			}
		}
		if(!RightEdge.Contains(Event.current.mousePosition) && RightEdgePressed == true)
		{
			if(Event.current.type == EventType.MouseUp)
			{
				RightEdgePressed = false;
			}
			if(Event.current.type == EventType.MouseDown)
			{
				RightEdgePressed = true;
			}
		}
		if(RightEdgePressed && Event.current.type == EventType.MouseDrag)
		{
			windowRect.width += Event.current.delta.x;
			UpdatePos();
		}
	}

	void DoMyWindow(int WindowID)
	{
		if (windowRect.width > 150) 
		{
			ReSizeWindows ();
		}
		else 
		{
			windowRect.width = 150;
			ReSizeWindows ();
		}
		if (windowRect.height > 40) 
		{
			ReSizeWindows ();
		}
		else 
		{
			windowRect.height = 40;
			ReSizeWindows ();
		}
		GUI.DragWindow (new Rect (DefaltBoxSetting));
		GUI.Box(new Rect(DefaltBoxSetting),"Corner Window");

		if (CloseButton.Contains (Event.current.mousePosition)) 
		{
			if (GUI.Button (new Rect (CloseButton), "X", com.Skin [GameControl.control.GUIID].customStyles [0])) 
			{
				show = false;
			}
		} 
		else
		{
			GUI.backgroundColor = com.colors[Customize.cust.ButtonColorInt];
			GUI.contentColor = com.colors[Customize.cust.FontColorInt];
			if (GUI.Button (new Rect (CloseButton), "X", com.Skin [GameControl.control.GUIID].customStyles [1])) 
			{
				show = false;
			}
		}

		if (GUI.Button (new Rect (MiniButton), "-",com.Skin [GameControl.control.GUIID].customStyles [2])) 
		{
			minimize = !minimize;
			Minimize();
		}
	}
}