using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Threading;
using UnityEngine;

public class Notepad : MonoBehaviour
{
    public bool quit;

    private GameObject Puter;

    private GameObject WindowHandel;
    private WindowManager winman;

    private Computer com;
    private SoundControl sc;
    //private FileExplorer fp;
    private AppMan appman;

    public float native_width = 1920;
    public float native_height = 1080;

    public string ProgramNameForWinMan;

    public int SelectedProgramID;
    public int SelectedWPN;

    private Rect CloseButton;
    public Rect CurrentTimeRect;
    public Rect CurrentDateRect;

    public bool ShowSettings;

    // Vars for context menu

    public string PersonName;
    public string ProgramName;

    public float LastClick;

    // Use this for initialization
    void Start()
    {
        ProgramNameForWinMan = "Notepad";

        Puter = GameObject.Find("System");
        WindowHandel = GameObject.Find("WindowHandel");
        com = Puter.GetComponent<Computer>();
        sc = Puter.GetComponent<SoundControl>();
        native_height = Customize.cust.native_height;
        native_width = Customize.cust.native_width;

        //fp = Puter.GetComponent<FileExplorer>();
        appman = Puter.GetComponent<AppMan>();

        winman = WindowHandel.GetComponent<WindowManager>();

        ProgramName = "Notepad";
        PersonName = "Player";
        //LocalRegistry.AddNewKey(PersonName, 1, "Test");
    }

    void SelectWindowID(int WindowID)
    {
        if (Input.GetMouseButtonDown(0))
        {
            Registry.SetIntData("Player", "WindowManager", "SelectedWindow", WindowID);
        }
    }

    bool GUIKeyDown(KeyCode key)
    {
        if (Event.current.type == EventType.KeyDown)
            return (Event.current.keyCode == key);
        return false;

    }

    void GUIControls(int PID)
    {
    }


    void ColorUI(int WPN)
    {
        LocalRegistry.SetColorData(PersonName, WPN, ProgramName, "FontColor", new SColor(new Color32(0, 0, 0, 255)));

        LocalRegistry.SetColorData(PersonName, WPN, ProgramName, "WindowColor", new SColor(new Color32(0, 0, 0, 255)));
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

                        LocalRegistry.SetRectData(PersonName, i, ProgramNameForWinMan, "Window", pwinman.RunningPrograms[i].windowRect);
                    }
                }
            }
        }
    }

    void TitleBarStuff(int PID)
    {
        if (LocalRegistry.GetStringData(PersonName, PID, ProgramName, "OpenedFile") == "")
        {
            LocalRegistry.SetStringData(PersonName, PID, ProgramName, "Window", "Untitled");
        }
        else
        {
            LocalRegistry.GetStringData(PersonName, PID, ProgramName, "Window");
        }

        if(LocalRegistry.GetRectData(PersonName, PID, ProgramName, "Window").width <= 220)
        {
            GUI.Box(new Rect(40, 2, CloseButton.x - 79, 20), LocalRegistry.GetStringData(PersonName, PID, ProgramName, "Window"));
        }
        else
        {
            GUI.Box(new Rect(40, 2, CloseButton.x - 79, 20), ProgramNameForWinMan + "-" + LocalRegistry.GetStringData(PersonName, PID, ProgramName, "Window"));
        }
        GUI.DragWindow(new Rect(40, 2, CloseButton.x - 79, 20));
        winman.WindowDragging(Registry.GetIntData(PersonName,"WindowManager","SelectedWindow"), new Rect(40, 2, CloseButton.x - 79, 21));

        if(GUI.Button(new Rect(2, 2, 37, 20), "File"))
        {
            if(LocalRegistry.GetStringData(PersonName, PID, ProgramName, "MenuBar") != "File")
            {
                LocalRegistry.SetStringData(PersonName, PID, ProgramName, "MenuBar", "File");
            }
            else
            {
                LocalRegistry.SetStringData(PersonName, PID, ProgramName, "MenuBar", "");
                LocalRegistry.RemoveAllMenuButtonData(PersonName, PID, ProgramName, "MenuBar");
            }
        }
    }

    void CheckInitalRun(int WindowID)
    {
        if (LocalRegistry.GetBoolData(PersonName, WindowID, ProgramName, "InitalRun") == false)
        {
            LocalRegistry.SetIntData(PersonName, WindowID, ProgramName, "SelectedFile", -1);
            LocalRegistry.SetBoolData(PersonName, WindowID, ProgramName, "InitalRun", true);
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
                    winman.WindowResize(PersonName, Registry.GetIntData(PersonName,"WindowManager","SelectedWindow"));
                }

                for (int i = 0; i < pwinman.RunningPrograms.Count; i++)
                {
                    if (pwinman.RunningPrograms[i].ProgramName == ProgramNameForWinMan)
                    {
                        if (pwinman.RunningPrograms[i].WID == Registry.GetIntData(PersonName,"WindowManager","SelectedWindow"))
                        {
                            GUIControls(pwinman.RunningPrograms[i].WPN);
                            SelectedProgramID = pwinman.RunningPrograms[i].PID;
                        }

                        if (WindowID == pwinman.RunningPrograms[i].WID)
                        {
                            CloseButton = new Rect(pwinman.RunningPrograms[i].windowRect.width - 23, 2, 20, 20);
                            if (CloseButton.Contains(Event.current.mousePosition))
                            {
                                if (GUI.Button(new Rect(CloseButton), "X", GameControl.control.Skins[Registry.GetIntData("Player", "System", "Skin")].customStyles[0]))
                                {
                                    WindowManager.QuitProgram(PersonName, ProgramName, pwinman.RunningPrograms[i].WPN);
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
                                    WindowManager.QuitProgram(PersonName, ProgramName, pwinman.RunningPrograms[i].WPN);
                                }
                            }

                            TitleBarStuff(pwinman.RunningPrograms[i].WPN);

                            RenderFileUI(pwinman.RunningPrograms[i].WPN);

                            RenderRibbonUI(pwinman.RunningPrograms[i].WPN);
                        }
                    }
                }
            }
        }
    }

    void PlayClickSound()
    {
        sc.SoundSelect = 3;
        sc.PlaySound();
    }

    void HistorySystem(string Action, int PID)
    {

        //PageHistory

        //LocalRegistry.SetStringData(PersonName, PID, ProgramName, "PageHistory", LocalRegistry.GetStringData(PersonName, PID, ProgramName, "CurrentDirectory"));

        if (Action == "Add")
        {
            if (LocalRegistry.GetStringListDataCount(PersonName, PID, ProgramName, "PageHistory") == 0)
            {
                LocalRegistry.AddStringListData(PersonName, PID, ProgramName, "PageHistory", "");
            }

            LocalRegistry.AddStringListData(PersonName, PID, ProgramName, "PageHistory", LocalRegistry.GetProgramData(PersonName, PID, ProgramName, "Test 1", LocalRegistry.GetIntData(PersonName, PID, ProgramName, "SelectedFile")).Target);
        }


        if (Action == "Back")
        {

            if (LocalRegistry.GetStringListDataCount(PersonName, PID, ProgramName, "PageHistory") > 0)
            {
                LocalRegistry.RemoveAtStringListData(PersonName, PID, ProgramName, "PageHistory",
                    LocalRegistry.GetLastStringListData(PersonName, PID, ProgramName, "PageHistory"));
            }

            if (LocalRegistry.GetStringListDataCount(PersonName, PID, ProgramName, "PageHistory") == 0)
            {
                LocalRegistry.AddStringListData(PersonName, PID, ProgramName, "PageHistory", "");
            }

            LocalRegistry.SetIntData(PersonName, PID, ProgramName, "SelectedFile", -1);

            LocalRegistry.SetStringData(PersonName, PID, ProgramName, "CurrentDirectory",
                LocalRegistry.GetStringListData(PersonName, PID, ProgramName, "PageHistory",
                LocalRegistry.GetLastStringListData(PersonName, PID, ProgramName, "PageHistory")));

            LocalRegistry.SetStringData(PersonName, PID, ProgramName, "TypedDirectory",
                LocalRegistry.GetStringData(PersonName, PID, ProgramName, "CurrentDirectory"));
        }
    }

    void RenderRibbonUI(int PID)
    {
        //LocalRegistry.SetRectData(PersonName, PID, ProgramName, "Ribbon", new SRect(new Rect(200,20,100,20)));
        int ButtonSize = 21;

        if (LocalRegistry.GetMenuButtonCountData(PersonName, PID, ProgramName, "MenuBar") > 0)
        {
            if (LocalRegistry.GetStringData(PersonName, PID, ProgramName, "SelectedMenu") == "")
            {
                if (LocalRegistry.MenuDataListContains(PersonName, PID, ProgramName, "MenuBar", "Back"))
                {
                    LocalRegistry.RemoveMenuButtonData(PersonName, PID, ProgramName, "MenuBar", "Back");
                }
            }
            else
            {
                if (!LocalRegistry.MenuDataListContains(PersonName, PID, ProgramName, "MenuBar", "Back"))
                {
                    LocalRegistry.AddMenuButtonData(PersonName, PID, ProgramName, "MenuBar", new MenuButtonSystem("Back", "Back", 40, ButtonSize));
                }
            }
        }

        if (LocalRegistry.GetMenuButtonCountData(PersonName, PID, ProgramName, "MenuBar") == 0)
        {
            switch (LocalRegistry.GetStringData(PersonName, PID, ProgramName, "MenuBar"))
            {
                case "File":
                    if (LocalRegistry.GetIntData(PersonName, PID, ProgramName, "MenuBar") >= 0)
                    {
                        LocalRegistry.AddMenuButtonData(PersonName, PID, ProgramName, "MenuBar", new MenuButtonSystem("New", "New", 40, ButtonSize));
                        LocalRegistry.AddMenuButtonData(PersonName, PID, ProgramName, "MenuBar", new MenuButtonSystem("Save", "Save", 40, ButtonSize));
                        LocalRegistry.AddMenuButtonData(PersonName, PID, ProgramName, "MenuBar", new MenuButtonSystem("Save As", "Save As", 60, ButtonSize));
                        LocalRegistry.AddMenuButtonData(PersonName, PID, ProgramName, "MenuBar", new MenuButtonSystem("Open", "Open", 45, ButtonSize));
                        LocalRegistry.AddMenuButtonData(PersonName, PID, ProgramName, "MenuBar", new MenuButtonSystem("Title", "Title", 40, ButtonSize));
                        LocalRegistry.AddMenuButtonData(PersonName, PID, ProgramName, "MenuBar", new MenuButtonSystem("Type", "Type", 40, ButtonSize));
                        LocalRegistry.AddMenuButtonData(PersonName, PID, ProgramName, "MenuBar", new MenuButtonSystem("Compile", "Compile", 60, ButtonSize));

                    }
                    break;
            }
        }


        //if (GUI.Button(new Rect(183, 24, 45, 21), "Compile"))
        //{
        //    TestCode.KeywordCheck("Player", TypedText);
        //}

        for (int i = 0; i < LocalRegistry.GetMenuButtonCountData(PersonName, PID, ProgramName, "MenuBar"); i++)
        {
            if (i == 0)
            {
                LocalRegistry.SetMenuButtonPosXData(PersonName, PID, ProgramName, "MenuBar", i, 2);

            }
            if (i >= 1)
            {
                LocalRegistry.SetMenuButtonPosXData(PersonName, PID, ProgramName, "MenuBar", i,
                    LocalRegistry.GetMenuButtonData(PersonName, PID, ProgramName, "MenuBar", i - 1).PosX +
                    LocalRegistry.GetMenuButtonData(PersonName, PID, ProgramName, "MenuBar", i - 1).Width + 1);
            }

            if (GUI.Button(new Rect(LocalRegistry.GetMenuButtonData(PersonName, PID, ProgramName, "MenuBar", i).PosX, LocalRegistry.GetRectData(PersonName, PID, ProgramName, "TypedDirectory").y + ButtonSize + 2, LocalRegistry.GetMenuButtonData(PersonName, PID, ProgramName, "MenuBar", i).Width, ButtonSize), LocalRegistry.GetMenuButtonData(PersonName, PID, ProgramName, "MenuBar", i).Name))
            {
                if (LocalRegistry.GetMenuButtonData(PersonName, PID, ProgramName, "MenuBar", i).Menu != "")
                {
                    LocalRegistry.SetStringData(PersonName, PID, ProgramName, "SelectedMenu",LocalRegistry.GetMenuButtonData(PersonName, PID, ProgramName, "MenuBar", i).Menu);
                }
            }
        }
    }

    void OpenFld(int PID)
    {
        PlayClickSound();

        HistorySystem("Add", PID);

        LocalRegistry.SetStringData(PersonName, PID, ProgramName, "CurrentDirectory", LocalRegistry.GetProgramData(PersonName, PID, ProgramName, "Test 1", LocalRegistry.GetIntData(PersonName, PID, ProgramName, "SelectedFile")).Target);
        LocalRegistry.SetStringData(PersonName, PID, ProgramName, "TypedDirectory", LocalRegistry.GetStringData(PersonName, PID, ProgramName, "CurrentDirectory"));
        LocalRegistry.RemoveAllProgramData(PersonName, PID, ProgramName, "Test 1");
        LocalRegistry.SetIntData(PersonName, PID, ProgramName, "SelectedFile", -1);
    }

    void OpenFile(int PID)
    {
        PlayClickSound();

        var PName = LocalRegistry.GetProgramData(PersonName, PID, ProgramName, "Test 1", LocalRegistry.GetIntData(PersonName, PID, ProgramName, "SelectedFile")).Name;

        TestCode.KeywordCheck(PersonName, "Open:" + PName + ";");

        LocalRegistry.SetIntData(PersonName, PID, ProgramName, "SelectedFile", -1);
    }

    void RenderFileUI(int PID)
    {
        switch (LocalRegistry.GetStringData(PersonName, PID, ProgramName, "SelectedMenu"))
        {
            case "":
                RenderTextEditor(PID);
                break;
            case "Save As":
                RenderFileSaveAs(PID);
                break;
            case "New":
                LocalRegistry.SetStringData(PersonName, PID, ProgramName, "SelectedMenu", "");
                LocalRegistry.SetStringData(PersonName, PID, ProgramName, "TypedText", "");
                LocalRegistry.SetStringData(PersonName, PID, ProgramName, "OpenedFile", "");
                break;
            case "Back":
                LocalRegistry.SetStringData(PersonName, PID, ProgramName, "SelectedMenu", "");
                LocalRegistry.RemoveMenuButtonData(PersonName, PID, ProgramName, "MenuBar","Back");
                break;
            case"Compile":
                TestCode.KeywordCheck("Player", LocalRegistry.GetStringData(PersonName, PID, ProgramName, "TypedText"));
                LocalRegistry.SetStringData(PersonName, PID, ProgramName, "SelectedMenu", "");
                break;
        }
    }

    void RenderTextEditor(int PID)
    {
        if (LocalRegistry.GetMenuButtonCountData(PersonName, PID, ProgramName, "MenuBar") > 0)
        {
            LocalRegistry.SetRectData(PersonName, PID, ProgramName, "TypedText", new Rect(2, 46,
            LocalRegistry.GetRectData(PersonName, PID, ProgramName, "Window").width - 4,
            LocalRegistry.GetRectData(PersonName, PID, ProgramName, "Window").height - 48));
        }
        else
        {
            LocalRegistry.SetRectData(PersonName, PID, ProgramName, "TypedText", new Rect(2, 25,
            LocalRegistry.GetRectData(PersonName, PID, ProgramName, "Window").width - 4,
            LocalRegistry.GetRectData(PersonName, PID, ProgramName, "Window").height - 27));
        }

        LocalRegistry.SetStringData(PersonName, PID, ProgramName, "TypedText", GUI.TextArea(
            LocalRegistry.GetRectData(PersonName, PID, ProgramName, "TypedText"),
            LocalRegistry.GetStringData(PersonName, PID, ProgramName, "TypedText")));
    }

    void RenderFileSaveAs(int PID)
    {

        var FileName = LocalRegistry.GetStringData(PersonName, PID, ProgramName, "TypedTitle");
        var SaveLocation = LocalRegistry.GetStringData(PersonName, PID, ProgramName, "SaveLocation");
        var TypedText = LocalRegistry.GetStringData(PersonName, PID, ProgramName, "TypedText");

        GUI.Label(new Rect(5, 50, 150, 21), "File Name");
        LocalRegistry.SetStringData(PersonName, PID, ProgramName, "TypedTitle",
            GUI.TextField(new Rect(5, 100, 140, 21),
            LocalRegistry.GetStringData(PersonName, PID, ProgramName, "TypedTitle")));

        GUI.Label(new Rect(5, 150, 150, 21), "File Location");
        LocalRegistry.SetStringData(PersonName, PID, ProgramName, "SaveLocation",
            GUI.TextField(new Rect(5, 200, 140, 21),
            LocalRegistry.GetStringData(PersonName, PID, ProgramName, "SaveLocation")));

        if (FileName != "")
        {
            if (SaveLocation != "")
            {
                if (GUI.Button(new Rect(50, 250, 80, 21), "Add File"))
                {
                    LocalRegistry.SetStringData(PersonName, PID, ProgramName, "Window",FileName);
                    TestCode.KeywordCheck("Player", "SaveAs?" + FileName + "?" + SaveLocation + "?" + "Txt" + "?" + TypedText + ";");
                }
            }
        }
    }
}
