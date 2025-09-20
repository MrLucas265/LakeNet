using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Threading;
using UnityEngine;

public class MessageUI : MonoBehaviour
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
        ProgramNameForWinMan = "Discord";

        Puter = GameObject.Find("System");
        WindowHandel = GameObject.Find("WindowHandel");
        com = Puter.GetComponent<Computer>();
        sc = Puter.GetComponent<SoundControl>();
        native_height = Customize.cust.native_height;
        native_width = Customize.cust.native_width;

        //fp = Puter.GetComponent<FileExplorer>();
        appman = Puter.GetComponent<AppMan>();

        winman = WindowHandel.GetComponent<WindowManager>();

        ProgramName = "Discord";
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

    void CheckInitalRun(int WPN)
    {
        if (LocalRegistryv2.GetBoolData(PersonName, WPN, ProgramName, "InitalRun") == false)
        {
            LocalRegistryv2.SetBoolData(PersonName, WPN, ProgramName, "ShowContacts", true);
            LocalRegistryv2.SetIntData(PersonName, WPN, ProgramName, "SelectedContact", -1);
            LocalRegistryv2.SetBoolData(PersonName, WPN, ProgramName, "InitalRun", true);
        }
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

                        LocalRegistryv2.SetRectData(PersonName, i, ProgramNameForWinMan, "WindowRect", pwinman.RunningPrograms[i].windowRect);
                    }
                }
            }
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
                if (WindowID == Registry.GetIntData(PersonName, "WindowManager", "SelectedWindow"))
                {
                    winman.WindowResize(PersonName, Registry.GetIntData(PersonName, "WindowManager", "SelectedWindow"));
                }

                for (int i = 0; i < pwinman.RunningPrograms.Count; i++)
                {
                    if (pwinman.RunningPrograms[i].ProgramName == ProgramNameForWinMan)
                    {
                        CheckInitalRun(pwinman.RunningPrograms[i].WPN);

                        if (pwinman.RunningPrograms[i].WID == Registry.GetIntData(PersonName, "WindowManager", "SelectedWindow"))
                        {
                            //GUIControls(pwinman.RunningPrograms[i].WPN);
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

                            RenderMainUI(pwinman.RunningPrograms[i].WPN);
                        }
                    }
                }
            }
        }
    }

    void TitleBarStuff(int PID)
    {
        LocalRegistryv2.SetStringData(PersonName, PID, ProgramName, "WindowName", "Discord-MessageUI");
        GUI.Box(new Rect(40, 2, CloseButton.x - 79, 20), LocalRegistryv2.GetStringData(PersonName, PID, ProgramName, "WindowName"));
        GUI.DragWindow(new Rect(40, 2, CloseButton.x - 79, 20));
        winman.WindowDragging(Registry.GetIntData(PersonName, "WindowManager", "SelectedWindow"), new Rect(40, 2, CloseButton.x - 79, 21));

        if (GUI.Button(new Rect(2, 2, 37, 20), "File"))
        {
            LocalRegistryv2.AddStringListData(PersonName, PID, ProgramName, "ContactsList", "Test,Test,Test,Test");
        }
    }

    void RenderMainUI(int WPN)
    {
        if (LocalRegistryv2.GetIntData(PersonName, WPN, ProgramName, "SelectedContact") >= 0)
        {
            RenderTextEditor(WPN);
        }

        if (LocalRegistryv2.GetBoolData(PersonName, WPN, ProgramName, "ShowContacts") == true)
        {
            RenderContactsList(WPN);
        }
    }

    void RenderTextEditor(int WPN)
    {
        LocalRegistryv2.SetRectData(PersonName, WPN, ProgramName, "TypedTextRect", new Rect(103, 25,
        LocalRegistryv2.GetRectData(PersonName, WPN, ProgramName, "WindowRect").width - 105,
        LocalRegistryv2.GetRectData(PersonName, WPN, ProgramName, "WindowRect").height - 27));

        LocalRegistryv2.SetStringData(PersonName, WPN, ProgramName, "TypedText", GUI.TextArea(
            LocalRegistryv2.GetRectData(PersonName, WPN, ProgramName, "TypedTextRect"),
            LocalRegistryv2.GetStringData(PersonName, WPN, ProgramName, "TypedText")));
    }
    void RenderContactsList(int WPN)
    {
        if (LocalRegistryv2.GetStringListDataCount(PersonName, WPN, ProgramName, "ContactsList") > 0)
        {
            LocalRegistryv2.SetVector2Data(PersonName, WPN, ProgramName, "ContactsScrollPos", 
                GUI.BeginScrollView(new Rect(2, CloseButton.y+CloseButton.height+2,
                100, LocalRegistryv2.GetRectData(PersonName, WPN, ProgramName, "WindowRect").height-23),
                LocalRegistryv2.GetVector2Data(PersonName, WPN, ProgramName, "ContactsScrollPos"),
                new Rect(0, 0, 0, LocalRegistryv2.GetIntData(PersonName, WPN, ProgramName, "ContactsScrollSize") * 21)));

            LocalRegistryv2.SetIntData(PersonName, WPN, ProgramName, "ContactsScrollSize", LocalRegistryv2.GetStringListDataCount(PersonName, WPN, ProgramName, "ContactsList"));

            for (int m = 0; m < LocalRegistryv2.GetStringListDataCount(PersonName, WPN, ProgramName, "ContactsList"); m++)
            {
                if (GUI.Button(new Rect(0, 21 * m, 100, 20), "" + LocalRegistryv2.GetStringListData(PersonName, WPN, ProgramName, "ContactsList", m)))
                {
                    LocalRegistryv2.SetIntData(PersonName, WPN, ProgramName, "SelectedContact", m);
                }
            }

            GUI.EndScrollView();
        }

        //void PlayClickSound()
        //{
        //    sc.SoundSelect = 3;
        //    sc.PlaySound();
        //}

        //bool GUIKeyDown(KeyCode key)
        //{
        //    if (Event.current.type == EventType.KeyDown)
        //        return (Event.current.keyCode == key);
        //    return false;

        //}

        //void GUIControls(int PID)
        //{
        //}


        //void ColorUI(int WPN)
        //{
        //    LocalRegistry.SetColorData(PersonName, WPN, ProgramName, "FontColor", new SColor(new Color32(0, 0, 0, 255)));

        //    LocalRegistry.SetColorData(PersonName, WPN, ProgramName, "WindowColor", new SColor(new Color32(0, 0, 0, 255)));
        //}
    }
}
