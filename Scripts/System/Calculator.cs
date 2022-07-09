//using System.Collections;
//using System.Collections.Generic;
//using System;
//using System.Text.RegularExpressions;
//using UnityEngine;

//public class Calculator : MonoBehaviour
//{
//    public bool show;
//    public float native_width = 1920;
//    public float native_height = 1080;
//    public bool Drag;

//    private Computer com;
//    private Defalt def;
//    private AppMan appman;
//    private WindowManager winman;

//    private GameObject Hardware;
//    private GameObject Prompts;
//    private GameObject SysSoftware;
//    private GameObject AppSoftware;
//    private GameObject HackingSoftware;
//    private GameObject WindowHandel;

//    public bool Calculate;

//    public List<string> Result = new List<string>();
//    public List<double> Value = new List<double>();
//    public List<double> Value1 = new List<double>();
//    public List<string> pastop = new List<string>();
//    public List<string> Operator = new List<string>();
//    public List<bool> OperatorPressed = new List<bool>();

//    public Rect CloseButton;
//    public Rect MiniButton;
//    private Rect TitleBox;

//    public GUIStyle Style;

//    public bool quit;

//    public int ProgramCount;
//    public int SelectedWindowID;
//    public int SelectedProgram;

//    void Start()
//    {
//        Hardware = GameObject.Find("Hardware");
//        Prompts = GameObject.Find("Prompts");
//        SysSoftware = GameObject.Find("System");
//        HackingSoftware = GameObject.Find("Hacking");
//        AppSoftware = GameObject.Find("Applications");
//        WindowHandel = GameObject.Find("WindowHandel");

//        winman = WindowHandel.GetComponent<WindowManager>();
//        com = SysSoftware.GetComponent<Computer>();
//        def = SysSoftware.GetComponent<Defalt>();
//        appman = SysSoftware.GetComponent<AppMan>();
//        // windowRect.x = Customize.cust.windowx[windowID];
//        //windowRect.y = Customize.cust.windowy[windowID];
//        native_height = Customize.cust.native_height;
//        native_width = Customize.cust.native_width;

//        //AddNewItems();

//    }

//    public void AddNewItems()
//    {
//        Result.Add("");
//        Value.Add(0);
//        Value1.Add(0);
//        pastop.Add("");
//        Operator.Add("");
//        OperatorPressed.Add(false);
//    }

//    public void RemoveItems()
//    {
//        Result.RemoveRange(0, Result.Count);
//        Value.RemoveRange(0, Value.Count);
//        Value1.RemoveRange(0, Value1.Count);
//        pastop.RemoveRange(0, pastop.Count);
//        Operator.RemoveRange(0, Operator.Count);
//        OperatorPressed.RemoveRange(0, OperatorPressed.Count);
//    }

//    public void RemoveAtItems()
//    {
//        Result.RemoveAt(SelectedProgram);
//        Value.RemoveAt(SelectedProgram);
//        Value1.RemoveAt(SelectedProgram);
//        pastop.RemoveAt(SelectedProgram);
//        Operator.RemoveAt(SelectedProgram);
//        OperatorPressed.RemoveAt(SelectedProgram);
//        SelectedProgram = 0;
//    }

//    void Reset()
//    {
//        if (Result[SelectedProgram] == "0")
//        {
//            Result[SelectedProgram] = "";
//        }
//    }

//    bool GUIKeyDown(KeyCode key)
//    {
//        if (Event.current.type == EventType.KeyDown)
//            return (Event.current.keyCode == key);
//        return false;

//    }

//    void KeyboardInput()
//    {
//        if (GUIKeyDown(KeyCode.Alpha0) || GUIKeyDown(KeyCode.Keypad0))
//        {
//            Reset();
//            Result[SelectedProgram] += 0;
//        }

//        if (GUIKeyDown(KeyCode.Alpha1) || GUIKeyDown(KeyCode.Keypad1))
//        {
//            Reset();
//            Result[SelectedProgram] += 1;
//        }
//        if (GUIKeyDown(KeyCode.Alpha2) || GUIKeyDown(KeyCode.Keypad2))
//        {
//            Reset();
//            Result[SelectedProgram] += 2;
//        }
//        if (GUIKeyDown(KeyCode.Alpha3) || GUIKeyDown(KeyCode.Keypad3))
//        {
//            Reset();
//            Result[SelectedProgram] += 3;
//        }
//        if (GUIKeyDown(KeyCode.Alpha4) || GUIKeyDown(KeyCode.Keypad4))
//        {
//            Reset();
//            Result[SelectedProgram] += 4;
//        }
//        if (GUIKeyDown(KeyCode.Alpha5) || GUIKeyDown(KeyCode.Keypad5))
//        {
//            Reset();
//            Result[SelectedProgram] += 5;
//        }
//        if (GUIKeyDown(KeyCode.Alpha6) || GUIKeyDown(KeyCode.Keypad6))
//        {
//            Reset();
//            Result[SelectedProgram] += 6;
//        }
//        if (GUIKeyDown(KeyCode.Alpha7) || GUIKeyDown(KeyCode.Keypad7))
//        {
//            Reset();
//            Result[SelectedProgram] += 7;
//        }
//        if (GUIKeyDown(KeyCode.Alpha8) || GUIKeyDown(KeyCode.Keypad8))
//        {
//            Reset();
//            Result[SelectedProgram] += 8;
//        }
//        if (GUIKeyDown(KeyCode.Alpha9) || GUIKeyDown(KeyCode.Keypad9))
//        {
//            Reset();
//            Result[SelectedProgram] += 9;
//        }

//        if (GUIKeyDown(KeyCode.Backspace))
//        {
//            if (Result[SelectedProgram] != "0")
//            {
//                Result[SelectedProgram] = Result[SelectedProgram].Remove(Result[SelectedProgram].Length - 1);
//                if (Result[SelectedProgram] == "")
//                {
//                    Result[SelectedProgram] = "0";
//                }
//            }
//        }

//        if (GUIKeyDown(KeyCode.Slash) || GUIKeyDown(KeyCode.KeypadDivide))
//        {
//            Operator[SelectedProgram] = "/";
//            Value[SelectedProgram] += double.Parse(Result[SelectedProgram]);
//            Result[SelectedProgram] = "0";
//            OperatorPressed[SelectedProgram] = true;
//        }
//        if (GUIKeyDown(KeyCode.Asterisk) || GUIKeyDown(KeyCode.KeypadMultiply))
//        {
//            Operator[SelectedProgram] = "*";
//            Value[SelectedProgram] += double.Parse(Result[SelectedProgram]);
//            Result[SelectedProgram] = "0";
//            OperatorPressed[SelectedProgram] = true;
//        }
//        if (GUIKeyDown(KeyCode.Minus) || GUIKeyDown(KeyCode.KeypadMinus))
//        {
//            Operator[SelectedProgram] = "-";
//            Value[SelectedProgram] += double.Parse(Result[SelectedProgram]);
//            Result[SelectedProgram] = "0";
//            OperatorPressed[SelectedProgram] = true;
//        }
//        if (GUIKeyDown(KeyCode.Plus) || GUIKeyDown(KeyCode.KeypadPlus))
//        {
//            Operator[SelectedProgram] = "+";
//            Value[SelectedProgram] += double.Parse(Result[SelectedProgram]);
//            Result[SelectedProgram] = "0";
//            OperatorPressed[SelectedProgram] = true;
//        }

//        if (GUIKeyDown(KeyCode.Period) || GUIKeyDown(KeyCode.KeypadPeriod))
//        {
//            if (!Result.Contains("."))
//            {
//                Result[SelectedProgram] += ".";
//            }
//        }

//        if (GUIKeyDown(KeyCode.Delete))
//        {
//            Result[SelectedProgram] = "0";
//            Operator[SelectedProgram] = "";
//            pastop[SelectedProgram] = "";
//            Value[SelectedProgram] = 0;
//            Value1[SelectedProgram] = 0;
//        }

//        if (GUIKeyDown(KeyCode.Return) || GUIKeyDown(KeyCode.Equals))
//        {
//            if (Result[SelectedProgram] != "")
//            {
//                if (OperatorPressed[SelectedProgram] == true)
//                {
//                    switch (Operator[SelectedProgram])
//                    {
//                        case "+":
//                            pastop[SelectedProgram] = Operator[SelectedProgram];
//                            Operator[SelectedProgram] = "";
//                            Value1[SelectedProgram] = double.Parse(Result[SelectedProgram]);
//                            Result[SelectedProgram] = (Value[SelectedProgram] + double.Parse(Result[SelectedProgram])).ToString();
//                            Value[SelectedProgram] = 0;
//                            break;
//                        case "-":
//                            pastop[SelectedProgram] = Operator[SelectedProgram];
//                            Operator[SelectedProgram] = "";
//                            Value1[SelectedProgram] = double.Parse(Result[SelectedProgram]);
//                            Result[SelectedProgram] = (Value[SelectedProgram] - double.Parse(Result[SelectedProgram])).ToString();
//                            Value[SelectedProgram] = 0;
//                            break;
//                        case "*":
//                            pastop[SelectedProgram] = Operator[SelectedProgram];
//                            Operator[SelectedProgram] = "";
//                            Value1[SelectedProgram] = double.Parse(Result[SelectedProgram]);
//                            Result[SelectedProgram] = (Value[SelectedProgram] * double.Parse(Result[SelectedProgram])).ToString();
//                            Value[SelectedProgram] = 0;
//                            break;
//                        case "/":
//                            pastop[SelectedProgram] = Operator[SelectedProgram];
//                            Operator[SelectedProgram] = "";
//                            Value1[SelectedProgram] = double.Parse(Result[SelectedProgram]);
//                            Result[SelectedProgram] = (Value[SelectedProgram] / double.Parse(Result[SelectedProgram])).ToString();
//                            Value[SelectedProgram] = 0;
//                            break;
//                    }
//                }
//                else
//                {
//                    if (pastop[SelectedProgram] != "")
//                    {
//                        if (pastop[SelectedProgram] == "+")
//                        {
//                            Result[SelectedProgram] = (Value1[SelectedProgram] + double.Parse(Result[SelectedProgram])).ToString();
//                        }
//                        if (pastop[SelectedProgram] == "-")
//                        {
//                            Result[SelectedProgram] = (Value1[SelectedProgram] - double.Parse(Result[SelectedProgram])).ToString();
//                        }
//                        if (pastop[SelectedProgram] == "*")
//                        {
//                            Result[SelectedProgram] = (Value1[SelectedProgram] * double.Parse(Result[SelectedProgram])).ToString();
//                        }
//                        if (pastop[SelectedProgram] == "/")
//                        {
//                            Result[SelectedProgram] = (Value1[SelectedProgram] / double.Parse(Result[SelectedProgram])).ToString();
//                        }
//                    }
//                }
//                OperatorPressed[SelectedProgram] = false;
//            }
//        }
//    }

//    void OnGUI()
//    {
//        //Customize.cust.windowx[windowID] = windowRect.x;
//        //Customize.cust.windowy[windowID] = windowRect.y;
//        GUI.skin = com.Skin[GameControl.control.GUIID];

//        ProgramCount = 0;

//        for (int PersonCount = 0; PersonCount < PersonController.control.People.Count; PersonCount++)
//        {
//            var pwinman = PersonController.control.People[PersonCount].Gateway;

//            if (pwinman.RunningPrograms.Count > 0)
//            {
//                for (int i = 0; i < pwinman.RunningPrograms.Count; i++)
//                {
//                    if (pwinman.RunningPrograms[i].ProgramName == "Calculator")
//                    {
//                        if (ProgramCount > Result.Count)
//                        {
//                            ProgramCount = 0;
//                        }
//                        else
//                        {
//                            ProgramCount++;
//                        }
//                        CloseButton = new Rect(pwinman.RunningPrograms[i].windowRect.width - 23, 2, 21, 21);
//                        MiniButton = new Rect(CloseButton.x - 22, 2, 21, 21);
//                        TitleBox = new Rect(2, 2, MiniButton.x - 3, 21);

//                        GUI.color = com.colors[Customize.cust.WindowColorInt];
//                        pwinman.RunningPrograms[i].windowRect = WindowClamp.ClampToScreen(GUI.Window(pwinman.RunningPrograms[i].WID, pwinman.RunningPrograms[i].windowRect, DoMyWindow, ""));
//                    }
//                }
//            }
//        }
//    }

//    void Close()
//    {
//        if (ProgramCount > 0)
//        {
//            for (int i = 0; i < winman.RunningPrograms.Count; i++)
//            {
//                if (winman.RunningPrograms[i].WID == SelectedWindowID)
//                {
//                    if (winman.RunningPrograms[i].PID == SelectedProgram)
//                    {
//                        if (ProgramCount == 1)
//                        {
//                            show = false;
//                            enabled = false;
//                            quit = true;
//                            RemoveItems();
//                            appman.SelectedApp = "Calculator";
//                        }
//                        else
//                        {
//                            quit = true;
//                            RemoveAtItems();
//                            appman.SelectedApp = "Calculator";
//                        }
//                        winman.RunningPrograms.RemoveAt(i);
//                    }
//                }
//            }
//        }
//    }

//    void DoMyWindow(int WindowID)
//    {
//        SelectedWindowID = WindowID;

//        GUI.backgroundColor = com.colors[Customize.cust.ButtonColorInt];
//        GUI.contentColor = com.colors[Customize.cust.FontColorInt];

//        for (int i = 0; i < winman.RunningPrograms.Count; i++)
//        {
//            if (winman.RunningPrograms[i].WID == SelectedWindowID)
//            {
//                if (winman.RunningPrograms[i].PID > Result.Count - 1)
//                {
//                    winman.RunningPrograms[i].PID = Result.Count - 1;
//                }
//                SelectedProgram = winman.RunningPrograms[i].PID;
//            }
//        }

//        for (int i = 0; i < winman.RunningPrograms.Count; i++)
//        {
//            if (winman.RunningPrograms[i].WID == SelectedWindowID)
//            {
//                // int FoundWindowID = i;
//                //Rect CloseLocal = new Rect(winman.RunningPrograms[FoundWindowID].windowRect.width - 22, 1, 21, 21);
//                if (new Rect(CloseButton).Contains(Event.current.mousePosition))
//                {
//                    if (GUI.Button(new Rect(CloseButton), "X", com.Skin[GameControl.control.GUIID].customStyles[0]))
//                    {
//                        Close();
//                    }
//                }
//            }
//        }

//        KeyboardInput();

//        GUI.DragWindow(new Rect(TitleBox));
//        GUI.Box(new Rect(TitleBox), "Calculator");

//        if (GUI.Button(new Rect(MiniButton), "-", com.Skin[GameControl.control.GUIID].customStyles[2]))
//        {
//            Close();
//        }

//        if (Operator[SelectedProgram] != "")
//        {
//            //			if (Value != 0) 
//            //			{
//            //				GUI.TextField (new Rect (3, 25, 120, 23), "" + Result + " " + Operator + " " + Value);
//            //			} 
//            //			else
//            //			{
//            //				GUI.TextField (new Rect (3, 25, 120, 23), "" + Result + " " + Operator);
//            //			}
//            GUI.TextField(new Rect(2, 24, winman.RunningPrograms[SelectedProgram].windowRect.width - 4, 23), "" + Value[SelectedProgram] + " " + Operator[SelectedProgram] + " " + Result[SelectedProgram]);
//        }
//        else
//        {
//            GUI.TextField(new Rect(2, 24, winman.RunningPrograms[SelectedProgram].windowRect.width - 4, 23), "" + Result[SelectedProgram]);
//        }

//        //		for (int i = 1; i < 9; i++) 
//        //		{
//        //			if (GUI.Button (new Rect (60, 170, 50, 25), "" + i)) 
//        //			{
//        //				Result += i;
//        //			}
//        //		}

//        //		if (GUI.Button (new Rect (60, 170, 50, 25), "+-")) 
//        //		{
//        //			if (Result < 0)
//        //			{
//        //				Result = (Value - Value * 2 double.Parse (Result)).ToString();
//        //			} 
//        //			else 
//        //			{
//        //				Result = Result * 2;
//        //			}
//        //		}

//        if (GUI.Button(new Rect(35, 170, 25, 25), "0", com.Skin[GameControl.control.GUIID].customStyles[2]))
//        {
//            Reset();
//            Result[SelectedProgram] += 0;
//        }

//        if (GUI.Button(new Rect(5, 140, 25, 25), "1", com.Skin[GameControl.control.GUIID].customStyles[2]))
//        {
//            Reset();
//            Result[SelectedProgram] += 1;
//        }
//        if (GUI.Button(new Rect(35, 140, 25, 25), "2", com.Skin[GameControl.control.GUIID].customStyles[2]))
//        {
//            Reset();
//            Result[SelectedProgram] += 2;
//        }
//        if (GUI.Button(new Rect(65, 140, 25, 25), "3", com.Skin[GameControl.control.GUIID].customStyles[2]))
//        {
//            Reset();
//            Result[SelectedProgram] += 3;
//        }
//        if (GUI.Button(new Rect(5, 110, 25, 25), "4", com.Skin[GameControl.control.GUIID].customStyles[2]))
//        {
//            Reset();
//            Result[SelectedProgram] += 4;
//        }
//        if (GUI.Button(new Rect(35, 110, 25, 25), "5", com.Skin[GameControl.control.GUIID].customStyles[2]))
//        {
//            Reset();
//            Result[SelectedProgram] += 5;
//        }
//        if (GUI.Button(new Rect(65, 110, 25, 25), "6", com.Skin[GameControl.control.GUIID].customStyles[2]))
//        {
//            Reset();
//            Result[SelectedProgram] += 6;
//        }
//        if (GUI.Button(new Rect(5, 80, 25, 25), "7", com.Skin[GameControl.control.GUIID].customStyles[2]))
//        {
//            Reset();
//            Result[SelectedProgram] += 7;
//        }
//        if (GUI.Button(new Rect(35, 80, 25, 25), "8", com.Skin[GameControl.control.GUIID].customStyles[2]))
//        {
//            Reset();
//            Result[SelectedProgram] += 8;
//        }
//        if (GUI.Button(new Rect(65, 80, 25, 25), "9", com.Skin[GameControl.control.GUIID].customStyles[2]))
//        {
//            Reset();
//            Result[SelectedProgram] += 9;
//        }

//        if (GUI.Button(new Rect(65, 170, 25, 25), ".", com.Skin[GameControl.control.GUIID].customStyles[2]))
//        {
//            if (!Result.Contains("."))
//            {
//                Result[SelectedProgram] += ".";
//            }
//        }

//        if (GUI.Button(new Rect(35, 50, 25, 25), "C", com.Skin[GameControl.control.GUIID].customStyles[2]))
//        {
//            Result[SelectedProgram] = "0";
//            Operator[SelectedProgram] = "";
//            pastop[SelectedProgram] = "";
//            Value[SelectedProgram] = 0;
//            Value1[SelectedProgram] = 0;
//        }

//        if (GUI.Button(new Rect(5, 50, 25, 25), "CE", com.Skin[GameControl.control.GUIID].customStyles[2]))
//        {
//            Result[SelectedProgram] = "";
//            Operator[SelectedProgram] = "";
//            Result[SelectedProgram] += Value[SelectedProgram];
//            Value[SelectedProgram] = 0;
//        }
//        if (GUI.Button(new Rect(65, 50, 25, 25), "<-", com.Skin[GameControl.control.GUIID].customStyles[2]))
//        {
//            if (Result[SelectedProgram] != "0")
//            {
//                Result[SelectedProgram] = Result[SelectedProgram].Remove(Result[SelectedProgram].Length - 1);
//                if (Result[SelectedProgram] == "")
//                {
//                    Result[SelectedProgram] = "0";
//                }
//            }
//        }

//        if (GUI.Button(new Rect(5, 170, 25, 25), "+/-", com.Skin[GameControl.control.GUIID].customStyles[2]))
//        {
//            Result[SelectedProgram] = (double.Parse(Result[SelectedProgram]) - double.Parse(Result[SelectedProgram]) * 2).ToString();
//        }

//        if (GUI.Button(new Rect(95, 50, 25, 25), "/", com.Skin[GameControl.control.GUIID].customStyles[2]))
//        {
//            Operator[SelectedProgram] = "/";
//            Value[SelectedProgram] += double.Parse(Result[SelectedProgram]);
//            Result[SelectedProgram] = "0";
//            OperatorPressed[SelectedProgram] = true;
//        }
//        if (GUI.Button(new Rect(95, 80, 25, 25), "*", com.Skin[GameControl.control.GUIID].customStyles[2]))
//        {
//            Operator[SelectedProgram] = "*";
//            Value[SelectedProgram] += double.Parse(Result[SelectedProgram]);
//            Result[SelectedProgram] = "0";
//            OperatorPressed[SelectedProgram] = true;
//        }
//        if (GUI.Button(new Rect(95, 110, 25, 25), "-", com.Skin[GameControl.control.GUIID].customStyles[2]))
//        {
//            Operator[SelectedProgram] = "-";
//            Value[SelectedProgram] += double.Parse(Result[SelectedProgram]);
//            Result[SelectedProgram] = "0";
//            OperatorPressed[SelectedProgram] = true;
//        }
//        if (GUI.Button(new Rect(95, 140, 25, 25), "+", com.Skin[GameControl.control.GUIID].customStyles[2]))
//        {
//            Operator[SelectedProgram] = "+";
//            Value[SelectedProgram] += double.Parse(Result[SelectedProgram]);
//            Result[SelectedProgram] = "0";
//            OperatorPressed[SelectedProgram] = true;
//        }
//        if (GUI.Button(new Rect(95, 170, 25, 25), "=", com.Skin[GameControl.control.GUIID].customStyles[2]))
//        {
//            if (Result[SelectedProgram] != "")
//            {
//                if (OperatorPressed[SelectedProgram] == true)
//                {
//                    switch (Operator[SelectedProgram])
//                    {
//                        case "+":
//                            pastop[SelectedProgram] = Operator[SelectedProgram];
//                            Operator[SelectedProgram] = "";
//                            Value1[SelectedProgram] = double.Parse(Result[SelectedProgram]);
//                            Result[SelectedProgram] = (Value[SelectedProgram] + double.Parse(Result[SelectedProgram])).ToString();
//                            Value[SelectedProgram] = 0;
//                            break;
//                        case "-":
//                            pastop[SelectedProgram] = Operator[SelectedProgram];
//                            Operator[SelectedProgram] = "";
//                            Value1[SelectedProgram] = double.Parse(Result[SelectedProgram]);
//                            Result[SelectedProgram] = (Value[SelectedProgram] - double.Parse(Result[SelectedProgram])).ToString();
//                            Value[SelectedProgram] = 0;
//                            break;
//                        case "*":
//                            pastop[SelectedProgram] = Operator[SelectedProgram];
//                            Operator[SelectedProgram] = "";
//                            Value1[SelectedProgram] = double.Parse(Result[SelectedProgram]);
//                            Result[SelectedProgram] = (Value[SelectedProgram] * double.Parse(Result[SelectedProgram])).ToString();
//                            Value[SelectedProgram] = 0;
//                            break;
//                        case "/":
//                            pastop[SelectedProgram] = Operator[SelectedProgram];
//                            Operator[SelectedProgram] = "";
//                            Value1[SelectedProgram] = double.Parse(Result[SelectedProgram]);
//                            Result[SelectedProgram] = (Value[SelectedProgram] / double.Parse(Result[SelectedProgram])).ToString();
//                            Value[SelectedProgram] = 0;
//                            break;
//                    }
//                }
//                else
//                {
//                    if (pastop[SelectedProgram] != "")
//                    {
//                        if (pastop[SelectedProgram] == "+")
//                        {
//                            Result[SelectedProgram] = (Value1[SelectedProgram] + double.Parse(Result[SelectedProgram])).ToString();
//                        }
//                        if (pastop[SelectedProgram] == "-")
//                        {
//                            Result[SelectedProgram] = (Value1[SelectedProgram] - double.Parse(Result[SelectedProgram])).ToString();
//                        }
//                        if (pastop[SelectedProgram] == "*")
//                        {
//                            Result[SelectedProgram] = (Value1[SelectedProgram] * double.Parse(Result[SelectedProgram])).ToString();
//                        }
//                        if (pastop[SelectedProgram] == "/")
//                        {
//                            Result[SelectedProgram] = (Value1[SelectedProgram] / double.Parse(Result[SelectedProgram])).ToString();
//                        }
//                    }
//                }
//                OperatorPressed[SelectedProgram] = false;
//            }
//        }

//        if (!CloseButton.Contains(Event.current.mousePosition))
//        {
//            GUI.backgroundColor = com.colors[Customize.cust.ButtonColorInt];
//            GUI.contentColor = com.colors[Customize.cust.FontColorInt];
//            GUI.Button(new Rect(CloseButton), "X", com.Skin[GameControl.control.GUIID].customStyles[1]);
//        }
//    }
//}
