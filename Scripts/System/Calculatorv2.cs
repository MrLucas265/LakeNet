using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Calculatorv2 : MonoBehaviour
{
	private Computer Com;
	private WindowManager WinMan;
	private GameObject System;
	private GameObject WindowHandel;

	public string ProgramNameForWinMan;
    public string PersonName;

    private Rect CloseButton;

    public bool quit;

    // Use this for initialization
    void Start()
	{
		System = GameObject.Find("System");
		WindowHandel = GameObject.Find("WindowHandel");

		Com = System.GetComponent<Computer>();
		WinMan = WindowHandel.GetComponent<WindowManager>();

		ProgramNameForWinMan = "Calculator";
	}
	void OnGUI()
	{
		GUI.skin = GameControl.control.Skins[Registry.GetIntData("Player", "System", "Skin")];
		GUI.color = Registry.Get32ColorData("Player", "System", "WindowColor");

		for (int PersonCount = 0; PersonCount < PersonController.control.People.Count; PersonCount++)
		{
			var pwinman = PersonController.control.People[PersonCount].Gateway;

			if (pwinman.RunningPrograms.Count > 0)
			{
				for (int i = 0; i < pwinman.RunningPrograms.Count; i++)
				{
					if (pwinman.RunningPrograms[i].ProgramName == ProgramNameForWinMan)
					{
						//ColorUI(pwinman.RunningPrograms[i].WPN);
						//GUI.color = new Color32(LocalRegistry.GetRedColorData(PersonName, pwinman.RunningPrograms[i].WPN, ProgramName, "WindowColor"),
						//	LocalRegistry.GetGreenColorData(PersonName, pwinman.RunningPrograms[i].WPN, ProgramName, "WindowColor"),
						//	LocalRegistry.GetBlueColorData(PersonName, pwinman.RunningPrograms[i].WPN, ProgramName, "WindowColor"),
						//	LocalRegistry.GetAlphaColorData(PersonName, pwinman.RunningPrograms[i].WPN, ProgramName, "WindowColor"));

						pwinman.RunningPrograms[i].windowRect = WindowClamp.ClampToScreen(GUI.Window(pwinman.RunningPrograms[i].WID, pwinman.RunningPrograms[i].windowRect, DoMyWindow, ""));
					}
				}
			}
		}
	}

    bool GUIKeyDown(KeyCode key)
    {
        if (Event.current.type == EventType.KeyDown)
            return (Event.current.keyCode == key);
        return false;
    }

    void SelectWindowID(int WindowID)
	{
		if (Input.GetMouseButtonDown(0))
		{
			Registry.SetIntData("Player", "WindowManager", "SelectedWindow", WindowID);
		}
	}

    void DoMyWindow(int WindowID)
    {
        SelectWindowID(WindowID);

        for (int PersonCount = 0; PersonCount < PersonController.control.People.Count; PersonCount++)
        {
            var pwinman = PersonController.control.People[PersonCount].Gateway;

            if (pwinman.RunningPrograms.Count > 0)
            {
                if (WindowID == Registry.GetIntData(PersonName,"WindowManager","SelectedWindow"))
                {
                    WinMan.WindowResize("Player", Registry.GetIntData(PersonName,"WindowManager","SelectedWindow"));
                }

                for (int i = 0; i < pwinman.RunningPrograms.Count; i++)
                {
                    if (pwinman.RunningPrograms[i].WID == WindowID)
                    {
                        CloseButton = new Rect(pwinman.RunningPrograms[i].windowRect.width - 23, 2, 21, 21);
                        if (CloseButton.Contains(Event.current.mousePosition))
                        {
                            if (GUI.Button(new Rect(CloseButton), "X", GameControl.control.Skins[Registry.GetIntData("Player", "System", "Skin")].customStyles[0]))
                            {
                                WindowManager.QuitProgram(PersonName, ProgramNameForWinMan, pwinman.RunningPrograms[i].WPN);
                            }

                            GUI.backgroundColor = Registry.Get32ColorData("Player", "System", "ButtonColor");
                            GUI.contentColor = Registry.Get32ColorData("Player", "System", "FontColor");
                        }
                        else
                        {
                            GUI.backgroundColor = Registry.Get32ColorData("Player", "System", "ButtonColor");
                            GUI.contentColor = Registry.Get32ColorData("Player", "System", "FontColor");

                            if (GUI.Button(new Rect(CloseButton), "X", GameControl.control.Skins[Registry.GetIntData("Player", "System", "Skin")].customStyles[1]))
                            {
                                WindowManager.QuitProgram(PersonName, ProgramNameForWinMan, pwinman.RunningPrograms[i].WPN);
                            }
                        }

                        KeyboardInput(pwinman.RunningPrograms[i].WPN);

                        RenderUI(PersonCount, pwinman.RunningPrograms[i].WPN);
                    }
                }
            }
        }
    }

    void RenderUI(int PersonID, int WindowID)
    {
        var pwinman = PersonController.control.People[PersonID].Gateway;

        if (LocalRegistry.GetStringData(PersonName, WindowID, ProgramNameForWinMan, "Operator") != "")
        {
            //if (LocalRegistry.GetDoubleData(PersonName, WindowID, ProgramNameForWinMan, "Value") != 0)
            //{
            //    GUI.TextField(new Rect(3, 60, 120, 23), "" + LocalRegistry.GetStringData(PersonName, WindowID, ProgramNameForWinMan, "Result") + " " + LocalRegistry.GetStringData(PersonName, WindowID, ProgramNameForWinMan, "Operator") + " " + LocalRegistry.GetDoubleData(PersonName, WindowID, ProgramNameForWinMan, "Value"));
            //}
            //else
            //{
            //    GUI.TextField(new Rect(3, 60, 120, 23), "" + LocalRegistry.GetStringData(PersonName, WindowID, ProgramNameForWinMan, "Result") + " " + LocalRegistry.GetStringData(PersonName, WindowID, ProgramNameForWinMan, "Operator"));
            //}
            GUI.TextField(new Rect(2, 24, pwinman.RunningPrograms[WindowID].windowRect.width - 4, 23), "" + LocalRegistry.GetDoubleData(PersonName, WindowID, ProgramNameForWinMan, "FirstNumber") + " " + LocalRegistry.GetStringData(PersonName, WindowID, ProgramNameForWinMan, "Operator") + " " + LocalRegistry.GetStringData(PersonName, WindowID, ProgramNameForWinMan, "Result"));
        }
        else
        {
            GUI.TextField(new Rect(2, 24, pwinman.RunningPrograms[WindowID].windowRect.width - 4, 23), "" + LocalRegistry.GetStringData(PersonName, WindowID, ProgramNameForWinMan, "Result"));
        }
    }

    void Reset(int WindowID)
    {
        if (LocalRegistry.GetStringData(PersonName, WindowID, ProgramNameForWinMan, "Result") == "0")
        {
            LocalRegistry.SetStringData(PersonName, WindowID, ProgramNameForWinMan, "Result","");
        }
    }

    void KeyboardInput(int WindowID)
    {
        if (GUIKeyDown(KeyCode.Alpha0) || GUIKeyDown(KeyCode.Keypad0))
        {
            Reset(WindowID);
            LocalRegistry.SetStringData(PersonName, WindowID, ProgramNameForWinMan, "Result", LocalRegistry.GetStringData(PersonName, WindowID, ProgramNameForWinMan, "Result") + 0);
        }

        if (GUIKeyDown(KeyCode.Alpha1) || GUIKeyDown(KeyCode.Keypad1))
        {
            Reset(WindowID);
            LocalRegistry.SetStringData(PersonName, WindowID, ProgramNameForWinMan, "Result", LocalRegistry.GetStringData(PersonName, WindowID, ProgramNameForWinMan, "Result") + 1);
        }
        if (GUIKeyDown(KeyCode.Alpha2) || GUIKeyDown(KeyCode.Keypad2))
        {
            Reset(WindowID);
            LocalRegistry.SetStringData(PersonName, WindowID, ProgramNameForWinMan, "Result", LocalRegistry.GetStringData(PersonName, WindowID, ProgramNameForWinMan, "Result") + 2);
        }
        if (GUIKeyDown(KeyCode.Alpha3) || GUIKeyDown(KeyCode.Keypad3))
        {
            Reset(WindowID);
            LocalRegistry.SetStringData(PersonName, WindowID, ProgramNameForWinMan, "Result", LocalRegistry.GetStringData(PersonName, WindowID, ProgramNameForWinMan, "Result") + 3);
        }
        if (GUIKeyDown(KeyCode.Alpha4) || GUIKeyDown(KeyCode.Keypad4))
        {
            Reset(WindowID);
            LocalRegistry.SetStringData(PersonName, WindowID, ProgramNameForWinMan, "Result", LocalRegistry.GetStringData(PersonName, WindowID, ProgramNameForWinMan, "Result") + 4);
        }
        if (GUIKeyDown(KeyCode.Alpha5) || GUIKeyDown(KeyCode.Keypad5))
        {
            Reset(WindowID);
            LocalRegistry.SetStringData(PersonName, WindowID, ProgramNameForWinMan, "Result", LocalRegistry.GetStringData(PersonName, WindowID, ProgramNameForWinMan, "Result") + 5);
        }
        if (GUIKeyDown(KeyCode.Alpha6) || GUIKeyDown(KeyCode.Keypad6))
        {
            Reset(WindowID);
            LocalRegistry.SetStringData(PersonName, WindowID, ProgramNameForWinMan, "Result", LocalRegistry.GetStringData(PersonName, WindowID, ProgramNameForWinMan, "Result") + 6);
        }
        if (GUIKeyDown(KeyCode.Alpha7) || GUIKeyDown(KeyCode.Keypad7))
        {
            Reset(WindowID);
            LocalRegistry.SetStringData(PersonName, WindowID, ProgramNameForWinMan, "Result", LocalRegistry.GetStringData(PersonName, WindowID, ProgramNameForWinMan, "Result") + 7);
        }
        if (GUIKeyDown(KeyCode.Alpha8) || GUIKeyDown(KeyCode.Keypad8))
        {
            Reset(WindowID);
            LocalRegistry.SetStringData(PersonName, WindowID, ProgramNameForWinMan, "Result", LocalRegistry.GetStringData(PersonName, WindowID, ProgramNameForWinMan, "Result") + 8);
        }
        if (GUIKeyDown(KeyCode.Alpha9) || GUIKeyDown(KeyCode.Keypad9))
        {
            Reset(WindowID);
            LocalRegistry.SetStringData(PersonName, WindowID, ProgramNameForWinMan, "Result", LocalRegistry.GetStringData(PersonName, WindowID, ProgramNameForWinMan, "Result") + 9);
        }

        if (GUIKeyDown(KeyCode.Backspace))
        {
            if (LocalRegistry.GetStringData(PersonName, WindowID, ProgramNameForWinMan, "Result") != "0")
            {
                LocalRegistry.SetStringData(PersonName, WindowID, ProgramNameForWinMan, "Result",
                    LocalRegistry.GetStringData(PersonName, WindowID, ProgramNameForWinMan, "Result").Remove
                    (LocalRegistry.GetStringData(PersonName, WindowID, ProgramNameForWinMan, "Result").Length - 1));

                if (LocalRegistry.GetStringData(PersonName, WindowID, ProgramNameForWinMan, "Result") == "")
                {
                    LocalRegistry.SetStringData(PersonName, WindowID, ProgramNameForWinMan, "Result","0");
                }
            }
        }

        if (GUIKeyDown(KeyCode.Slash) || GUIKeyDown(KeyCode.KeypadDivide))
        {
            LocalRegistry.SetStringData(PersonName, WindowID, ProgramNameForWinMan, "Operator", "/");
            LocalRegistry.SetBoolData(PersonName, WindowID, ProgramNameForWinMan, "Operator", true);
            Math1(WindowID);
        }
        if (GUIKeyDown(KeyCode.Asterisk) || GUIKeyDown(KeyCode.KeypadMultiply))
        {
            LocalRegistry.SetStringData(PersonName, WindowID, ProgramNameForWinMan, "Operator", "*");
            LocalRegistry.SetBoolData(PersonName, WindowID, ProgramNameForWinMan, "Operator", true);
            Math1(WindowID);
        }
        if (GUIKeyDown(KeyCode.Minus) || GUIKeyDown(KeyCode.KeypadMinus))
        {
            LocalRegistry.SetStringData(PersonName, WindowID, ProgramNameForWinMan, "Operator", "-");
            LocalRegistry.SetBoolData(PersonName, WindowID, ProgramNameForWinMan, "Operator", true);
            Math1(WindowID);
        }
        if (GUIKeyDown(KeyCode.Plus) || GUIKeyDown(KeyCode.KeypadPlus))
        {
            LocalRegistry.SetStringData(PersonName, WindowID, ProgramNameForWinMan, "Operator", "+");
            LocalRegistry.SetBoolData(PersonName, WindowID, ProgramNameForWinMan, "Operator", true);
            Math1(WindowID);
        }

        if (GUIKeyDown(KeyCode.Period) || GUIKeyDown(KeyCode.KeypadPeriod))
        {
            if (!LocalRegistry.GetStringData(PersonName, WindowID, ProgramNameForWinMan, "Result").Contains("."))
            {
                LocalRegistry.SetStringData(PersonName, WindowID, ProgramNameForWinMan, "Result", LocalRegistry.GetStringData(PersonName, WindowID, ProgramNameForWinMan, "Result") + ".");
            }
        }

        if (GUIKeyDown(KeyCode.Delete))
        {
            LocalRegistry.SetStringData(PersonName, WindowID, ProgramNameForWinMan, "Result", "0");
            LocalRegistry.SetDoubleData(PersonName, WindowID, ProgramNameForWinMan, "Result", 0);
            LocalRegistry.SetStringData(PersonName, WindowID, ProgramNameForWinMan, "Operator", "");
            LocalRegistry.SetStringData(PersonName, WindowID, ProgramNameForWinMan, "Pastop", "");
            LocalRegistry.SetDoubleData(PersonName, WindowID, ProgramNameForWinMan, "Value", 0);
            LocalRegistry.SetDoubleData(PersonName, WindowID, ProgramNameForWinMan, "Value1", 0);
            LocalRegistry.SetDoubleData(PersonName, WindowID, ProgramNameForWinMan, "FirstNumber", 0);
            LocalRegistry.SetDoubleData(PersonName, WindowID, ProgramNameForWinMan, "SecondNumber", 0);
            LocalRegistry.SetDoubleData(PersonName, WindowID, ProgramNameForWinMan, "ResultNumber", 0);
        }

        if (GUIKeyDown(KeyCode.Return) || GUIKeyDown(KeyCode.Equals))
        {
            CalculateMath(WindowID);
        }
    }

    void Math1(int WindowID)
    {
        if (LocalRegistry.GetStringData(PersonName, WindowID, ProgramNameForWinMan, "Result") == "0")
        {

        }
        else
        {
            LocalRegistry.SetStringData(PersonName, WindowID, ProgramNameForWinMan, "Pastop", LocalRegistry.GetStringData(PersonName, WindowID, ProgramNameForWinMan, "Operator"));
            LocalRegistry.SetDoubleData(PersonName, WindowID, ProgramNameForWinMan, "SecondNumber", LocalRegistry.GetDoubleData(PersonName, WindowID, ProgramNameForWinMan, "FirstNumber"));
            LocalRegistry.SetDoubleData(PersonName, WindowID, ProgramNameForWinMan, "FirstNumber", double.Parse(LocalRegistry.GetStringData(PersonName, WindowID, ProgramNameForWinMan, "Result")));
            switch (LocalRegistry.GetStringData(PersonName, WindowID, ProgramNameForWinMan, "Pastop"))
            {
                case "+":
                    LocalRegistry.SetDoubleData(PersonName, WindowID, ProgramNameForWinMan, "ResultNumber", LocalRegistry.GetDoubleData(PersonName, WindowID, ProgramNameForWinMan, "FirstNumber") + LocalRegistry.GetDoubleData(PersonName, WindowID, ProgramNameForWinMan, "SecondNumber"));
                    break;
                case "-":
                    LocalRegistry.SetDoubleData(PersonName, WindowID, ProgramNameForWinMan, "ResultNumber", LocalRegistry.GetDoubleData(PersonName, WindowID, ProgramNameForWinMan, "FirstNumber") - LocalRegistry.GetDoubleData(PersonName, WindowID, ProgramNameForWinMan, "SecondNumber"));
                    break;
                case "/":
                    LocalRegistry.SetDoubleData(PersonName, WindowID, ProgramNameForWinMan, "ResultNumber", LocalRegistry.GetDoubleData(PersonName, WindowID, ProgramNameForWinMan, "FirstNumber") / LocalRegistry.GetDoubleData(PersonName, WindowID, ProgramNameForWinMan, "SecondNumber"));
                    break;
                case "*":
                    LocalRegistry.SetDoubleData(PersonName, WindowID, ProgramNameForWinMan, "ResultNumber", LocalRegistry.GetDoubleData(PersonName, WindowID, ProgramNameForWinMan, "FirstNumber") * LocalRegistry.GetDoubleData(PersonName, WindowID, ProgramNameForWinMan, "SecondNumber"));
                    break;
            }
            LocalRegistry.SetStringData(PersonName, WindowID, ProgramNameForWinMan, "Result", "0");
        }
    }

    void CalculateMath(int WindowID)
    {
        if (LocalRegistry.GetStringData(PersonName, WindowID, ProgramNameForWinMan, "Result") != "")
        {
            if (LocalRegistry.GetBoolData(PersonName, WindowID, ProgramNameForWinMan, "Operator") == true)
            {
                switch (LocalRegistry.GetStringData(PersonName, WindowID, ProgramNameForWinMan, "Operator"))
                {
                    case "+":
                        LocalRegistry.SetStringData(PersonName, WindowID, ProgramNameForWinMan, "Pastop", LocalRegistry.GetStringData(PersonName, WindowID, ProgramNameForWinMan, "Operator"));
                        LocalRegistry.SetStringData(PersonName, WindowID, ProgramNameForWinMan, "Operator", "");
                        LocalRegistry.SetDoubleData(PersonName, WindowID, ProgramNameForWinMan, "Value1", double.Parse(LocalRegistry.GetStringData(PersonName, WindowID, ProgramNameForWinMan, "Result")));
                        LocalRegistry.SetStringData(PersonName, WindowID, ProgramNameForWinMan, "Result", (LocalRegistry.GetDoubleData(PersonName, WindowID, ProgramNameForWinMan, "Value") + double.Parse(LocalRegistry.GetStringData(PersonName, WindowID, ProgramNameForWinMan, "Result"))).ToString());
                        LocalRegistry.SetDoubleData(PersonName, WindowID, ProgramNameForWinMan, "Value", 0);
                        break;
                    case "-":
                        LocalRegistry.SetStringData(PersonName, WindowID, ProgramNameForWinMan, "Pastop", LocalRegistry.GetStringData(PersonName, WindowID, ProgramNameForWinMan, "Operator"));
                        LocalRegistry.SetStringData(PersonName, WindowID, ProgramNameForWinMan, "Operator", "");
                        LocalRegistry.SetDoubleData(PersonName, WindowID, ProgramNameForWinMan, "Value1", double.Parse(LocalRegistry.GetStringData(PersonName, WindowID, ProgramNameForWinMan, "Result")));
                        LocalRegistry.SetStringData(PersonName, WindowID, ProgramNameForWinMan, "Result", (LocalRegistry.GetDoubleData(PersonName, WindowID, ProgramNameForWinMan, "Value") - double.Parse(LocalRegistry.GetStringData(PersonName, WindowID, ProgramNameForWinMan, "Result"))).ToString());
                        LocalRegistry.SetDoubleData(PersonName, WindowID, ProgramNameForWinMan, "Value", 0);
                        break;
                    case "*":
                        LocalRegistry.SetStringData(PersonName, WindowID, ProgramNameForWinMan, "Pastop", LocalRegistry.GetStringData(PersonName, WindowID, ProgramNameForWinMan, "Operator"));
                        LocalRegistry.SetStringData(PersonName, WindowID, ProgramNameForWinMan, "Operator", "");
                        LocalRegistry.SetDoubleData(PersonName, WindowID, ProgramNameForWinMan, "Value1", double.Parse(LocalRegistry.GetStringData(PersonName, WindowID, ProgramNameForWinMan, "Result")));
                        LocalRegistry.SetStringData(PersonName, WindowID, ProgramNameForWinMan, "Result", (LocalRegistry.GetDoubleData(PersonName, WindowID, ProgramNameForWinMan, "Value") * double.Parse(LocalRegistry.GetStringData(PersonName, WindowID, ProgramNameForWinMan, "Result"))).ToString());
                        LocalRegistry.SetDoubleData(PersonName, WindowID, ProgramNameForWinMan, "Value", 0);
                        break;
                    case "/":
                        LocalRegistry.SetStringData(PersonName, WindowID, ProgramNameForWinMan, "Pastop", LocalRegistry.GetStringData(PersonName, WindowID, ProgramNameForWinMan, "Operator"));
                        LocalRegistry.SetStringData(PersonName, WindowID, ProgramNameForWinMan, "Operator", "");
                        LocalRegistry.SetDoubleData(PersonName, WindowID, ProgramNameForWinMan, "Value1", double.Parse(LocalRegistry.GetStringData(PersonName, WindowID, ProgramNameForWinMan, "Result")));
                        LocalRegistry.SetStringData(PersonName, WindowID, ProgramNameForWinMan, "Result", (LocalRegistry.GetDoubleData(PersonName, WindowID, ProgramNameForWinMan, "Value") / double.Parse(LocalRegistry.GetStringData(PersonName, WindowID, ProgramNameForWinMan, "Result"))).ToString());
                        LocalRegistry.SetDoubleData(PersonName, WindowID, ProgramNameForWinMan, "Value", 0);
                        break;
                }
            }
            else
            {
                if (LocalRegistry.GetStringData(PersonName, WindowID, ProgramNameForWinMan, "Pastop") != "")
                {
                    if (LocalRegistry.GetStringData(PersonName, WindowID, ProgramNameForWinMan, "Pastop") == "+")
                    {
                        LocalRegistry.SetStringData(PersonName, WindowID, ProgramNameForWinMan, "Result", (LocalRegistry.GetDoubleData(PersonName, WindowID, ProgramNameForWinMan, "Value1") + double.Parse(LocalRegistry.GetStringData(PersonName, WindowID, ProgramNameForWinMan, "Result"))).ToString());
                    }

                    if (LocalRegistry.GetStringData(PersonName, WindowID, ProgramNameForWinMan, "Pastop") == "-")
                    {
                        LocalRegistry.SetStringData(PersonName, WindowID, ProgramNameForWinMan, "Result", (LocalRegistry.GetDoubleData(PersonName, WindowID, ProgramNameForWinMan, "Value1") - double.Parse(LocalRegistry.GetStringData(PersonName, WindowID, ProgramNameForWinMan, "Result"))).ToString());
                    }

                    if (LocalRegistry.GetStringData(PersonName, WindowID, ProgramNameForWinMan, "Pastop") == "*")
                    {
                        LocalRegistry.SetStringData(PersonName, WindowID, ProgramNameForWinMan, "Result", (LocalRegistry.GetDoubleData(PersonName, WindowID, ProgramNameForWinMan, "Value1") * double.Parse(LocalRegistry.GetStringData(PersonName, WindowID, ProgramNameForWinMan, "Result"))).ToString());
                    }

                    if (LocalRegistry.GetStringData(PersonName, WindowID, ProgramNameForWinMan, "Pastop") == "/")
                    {
                        LocalRegistry.SetStringData(PersonName, WindowID, ProgramNameForWinMan, "Result", (LocalRegistry.GetDoubleData(PersonName, WindowID, ProgramNameForWinMan, "Value1") / double.Parse(LocalRegistry.GetStringData(PersonName, WindowID, ProgramNameForWinMan, "Result"))).ToString());
                    }
                }
            }
            LocalRegistry.SetBoolData(PersonName, WindowID, ProgramNameForWinMan, "Operator", false);
        }
    }
}
