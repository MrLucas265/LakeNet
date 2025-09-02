using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class FileUtilityUI : MonoBehaviour
{
    public GameObject SysSoftware;
    public GameObject WindowHandel;
    private Computer com;
    private WindowManager winman;
    public List<Rect> windowRect = new List<Rect>();
    public List<float> Timers = new List<float>();
    public List<string> TempFileNames = new List<string>();
    public int TempFileIndex;
    public List<int> ID = new List<int>();
    public int windowID;
    public float native_width = 1920;
    public float native_height = 1080;
    public bool Drag;

    private Defalt defalt;

    public Vector2 scrollpos = Vector2.zero;
    public int scrollsize;

    public Rect CloseButton;
    public Rect MiniButton;
    public Rect DefaltSetting;
    public Rect DefaltBoxSetting;

    private SoundControl sc;

    public string ProgramTitle;
    public string FileName;
    public float FileSize;
    public int FileIndex;

    public int selectedID;
    public int CurrentID;

    public float Timer;

    public bool ForceDone;

    //WEBSITE STUFF
    public GameObject apps;
    private GameObject db;
    private InternetBrowser ib;
    private JailDew jd;
    private Unicom uc;
    private Test test;
    private CLICommandsV2 clic;

    public bool Local;
    public float Speed;

    public int SelectedProgram;

    public List<InfectionSystem> BlankInfections = new List<InfectionSystem>();
    public List<ProgramSystem.FileType> BlankFileType = new List<ProgramSystem.FileType>();

    public bool IsEnoughSpace;
    public bool CheckedDisk;
    public bool DownloadingOrInstalling;

    private ErrorProm ep;
    private GameObject prompt;
    private AppMan appman;

    public float matha;


    public string ProgramNameForWinMan;
    public int SelectedProgramID;
    public int SelectedWPN;
    public string PersonName;
    public string ProgramName;

    public float MathC;
    public float MathF;
    public float Total;
    public float Percent;

    public bool Option1;
    public bool Option2;
    public bool Option3;
    public bool Option4;
    public bool Option5;

    void Start()
    {
        db = GameObject.Find("Database");
        WindowHandel = GameObject.Find("WindowHandel");
        SysSoftware = GameObject.Find("System");
        apps = GameObject.Find("Applications");
        prompt = GameObject.Find("Prompts");
        appman = SysSoftware.GetComponent<AppMan>();
        com = SysSoftware.GetComponent<Computer>();
        winman = WindowHandel.GetComponent<WindowManager>();
        defalt = SysSoftware.GetComponent<Defalt>();
        clic = SysSoftware.GetComponent<CLICommandsV2>();
        ep = prompt.GetComponent<ErrorProm>();
        AfterStart();

        ProgramNameForWinMan = "FileUtility";
        ProgramName = "FileUtility";

        PersonName = "Player";

        Option1 = true;
    }

    void AfterStart()
    {
        native_height = Customize.cust.native_height;
        native_width = Customize.cust.native_width;
        //WEBSITE STUFF
        ib = apps.GetComponent<InternetBrowser>();
        jd = db.GetComponent<JailDew>();
    }

    void SelectWindowID(int WindowID)
    {
        if (Input.GetMouseButtonDown(0))
        {
            Registry.SetIntData("Player", "WindowManager", "SelectedWindow", WindowID);
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
                        if(LocalRegistry.GetStringData("Player",i, ProgramNameForWinMan, "CurrentFile") != "")
                        {
                            pwinman.RunningPrograms[i].show = true;
                        }
                        if (pwinman.RunningPrograms[i].show == true)
                        {
                            pwinman.RunningPrograms[i].windowRect = WindowClamp.ClampToScreen(GUI.Window(pwinman.RunningPrograms[i].WID, pwinman.RunningPrograms[i].windowRect, DoMyWindow, ""));
                            LocalRegistry.SetRectData(PersonName, pwinman.RunningPrograms[i].WPN, ProgramName, "Window", pwinman.RunningPrograms[i].windowRect);
                        }
                        //ColorUI(pwinman.RunningPrograms[i].WPN);
                        //GUI.color = new Color32(LocalRegistry.GetRedColorData(PersonName, pwinman.RunningPrograms[i].WPN, ProgramName, "WindowColor"),
                        //	LocalRegistry.GetGreenColorData(PersonName, pwinman.RunningPrograms[i].WPN, ProgramName, "WindowColor"),
                        //	LocalRegistry.GetBlueColorData(PersonName, pwinman.RunningPrograms[i].WPN, ProgramName, "WindowColor"),
                        //	LocalRegistry.GetAlphaColorData(PersonName, pwinman.RunningPrograms[i].WPN, ProgramName, "WindowColor"));
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
                        if (pwinman.RunningPrograms[i].WID == Registry.GetIntData(PersonName,"WindowManager","SelectedWindow"))
                        {
                            SelectedProgramID = pwinman.RunningPrograms[i].PID;
                        }

                        if (WindowID == pwinman.RunningPrograms[i].WID)
                        {
                            CloseButton = new Rect(pwinman.RunningPrograms[i].windowRect.width - 23, 2, 21, 21);
                            if (CloseButton.Contains(Event.current.mousePosition))
                            {
                                if (GUI.Button(new Rect(CloseButton), "X", GameControl.control.Skins[Registry.GetIntData("Player", "System", "Skin")].customStyles[0]))
                                {
                                    TestCode.KeywordCheck(PersonName, "Show:" + ProgramName);
                                    //WindowManager.QuitProgram(PersonName, ProgramName, pwinman.RunningPrograms[i].WPN);
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
                                    //WindowManager.QuitProgram(PersonName, ProgramName, pwinman.RunningPrograms[i].WPN);
                                }
                            }

                            RenderMain(pwinman.RunningPrograms[i].WPN);
                            RenderTitleBar(pwinman.RunningPrograms[i].WPN);
                            //LocalRegistry.SetStringData("Player", 1, "FileManager", "CurrentDirectory", "Test");//THIS WORKS ON A EPIC LEVEL
                        }
                    }
                }
            }
        }
    }

    void RenderTitleBar(int PID)
    {
        GUI.DragWindow(new Rect(2, 2, CloseButton.x - 41, 20));
        winman.WindowDragging(Registry.GetIntData(PersonName, "WindowManager", "SelectedWindow"), new Rect(2, 2, CloseButton.x - 41, 21));
        GUI.Box(new Rect(2, 2, CloseButton.x - 41, 20),LocalRegistry.GetStringData(PersonName, PID, ProgramName, "Window"));
    }

    void RenderMain(int PID)
    {
        var WindowRectInfo = LocalRegistry.GetRectData(PersonName, PID, ProgramNameForWinMan, "Window");

        if (LocalRegistry.GetStringData(PersonName, PID, "FileUtility", "CurrentFile") != "")
        {
            GUI.backgroundColor = Registry.Get32ColorData("Player", "System", "ButtonColor");
            GUI.contentColor = Registry.Get32ColorData("Player", "System", "FontColor");

            var UITitle = LocalRegistry.GetFMSData(PersonName, PID, "FileUtility", "CurrentFile");
            if (Option1)
                LocalRegistry.SetStringData(PersonName, PID, ProgramName, "Window", "Saving " + UITitle.Name + "." + UITitle.ProgramFile.Extension.ToString() + " %" + UITitle.Percentage.ToString("0"));
            if (Option2)
                LocalRegistry.SetStringData(PersonName, PID, ProgramName, "Window", "Saving " + UITitle.Name + " %" + UITitle.Percentage.ToString("0.0"));
            if (Option3)
                LocalRegistry.SetStringData(PersonName, PID, ProgramName, "Window", "Saving " + UITitle.Name + " %" + UITitle.Percentage.ToString("0.00"));
            if (Option4)
                LocalRegistry.SetStringData(PersonName, PID, ProgramName, "Window", "Saving " + UITitle.Name + " %" + UITitle.Percentage.ToString("0.##"));

            switch (LocalRegistry.GetFMSData(PersonName, PID, "FileUtility", "CurrentFile").Type)
            {
                case FileUtilitySystem.ProgramType.LocalFolderDelete:
                    GUI.Label(new Rect(5, 30, 300, 200), "File Name: " + LocalRegistry.GetFMSData(PersonName, PID, "FileUtility", "CurrentFile").ProgramFile.Location);
                    GUI.Label(new Rect(5, 50, 300, 200), "From: " + LocalRegistry.GetFMSData(PersonName, PID, "FileUtility", "CurrentFile").ProgramFile.Location);
                    break;
                case FileUtilitySystem.ProgramType.RemoteDelete:
                    break;
                case FileUtilitySystem.ProgramType.LocalDelete:
                    break;
                case FileUtilitySystem.ProgramType.Paste:
                    GUI.Label(new Rect(5, 30, 300, 200), "File Name: " + LocalRegistry.GetFMSData(PersonName, PID, "FileUtility", "CurrentFile").ProgramFile.Location);
                    GUI.Label(new Rect(5, 50, 300, 200), "To: " + LocalRegistry.GetFMSData(PersonName, PID, "FileUtility", "CurrentFile").DestinationPath);
                    GUI.Label(new Rect(5, 50, 300, 200), "From: " + LocalRegistry.GetFMSData(PersonName, PID, "FileUtility", "CurrentFile").ProgramFile.Location);
                    break;
                case FileUtilitySystem.ProgramType.Upload:
                    break;
                case FileUtilitySystem.ProgramType.Installer:
                    break;
                case FileUtilitySystem.ProgramType.Download:
                    break;
                case FileUtilitySystem.ProgramType.Save:
                    GUI.Label(new Rect(5, 30, WindowRectInfo.width, WindowRectInfo.height), "File Name: " + LocalRegistry.GetFMSData(PersonName, PID, "FileUtility", "CurrentFile").ProgramFile.Name + "." + LocalRegistry.GetFMSData(PersonName, PID, "FileUtility", "CurrentFile").ProgramFile.Extension);
                    GUI.Label(new Rect(5, 50, WindowRectInfo.width, WindowRectInfo.height), "To: " + LocalRegistry.GetFMSData(PersonName, PID, "FileUtility", "CurrentFile").DestinationPath);
                    GUI.Label(new Rect(5, 70, 300, 200), "From: " + LocalRegistry.GetFMSData(PersonName, PID, "FileUtility", "CurrentFile").ProgramFile.Location);
                    //GUI.Label(new Rect(5, 50, 300, 200), "From: " + LocalRegistry.GetFMSData(PersonName, PID, "FileUtility", "CurrentFile").ProgramFile.Location);
                    break;
            }
            //GUI.Label (new Rect (5, 30, 200, 200), "File Name: " + ProgramHandle[selectedID].FileName);
            //GUI.Label (new Rect (5, 50, 200, 200), "From: " + ProgramHandle[selectedID].Location);
            //GUI.Label (new Rect (5, 70, 200, 200), "To: " + ProgramHandle[selectedID].Target);

            //GUI.Label(new Rect(5, 30, 200, 200), "File Name: " + GameControl.control.ProgramFiles[FileIndex].Name);
            // GUI.Label(new Rect(5, 50, 200, 200), "From: " + GameControl.control.ProgramFiles[FileIndex].Location);

            float TimeRemainingPosY = 90;

            string hoursString = LocalRegistry.GetFMSData(PersonName, PID, "FileUtility", "CurrentFile").TimeRemainHour.ToString("0");
            string minutesString = LocalRegistry.GetFMSData(PersonName, PID, "FileUtility", "CurrentFile").TimeRemainMin.ToString("0");
            string secondsString = LocalRegistry.GetFMSData(PersonName, PID, "FileUtility", "CurrentFile").TimeRemainUISeconds.ToString("0");
            //string fraction = ((ProgramHandle[selectedID].TimeRemainSeconds * 100) % 100).ToString("000");


            //HOURS = DAYS
            //Minutes = HOURS
            //seconds = Minutes
            string daytag = "";
            string hourtag = "";
            string mintag = "";

            float TimeRemain = 100;

            if (WindowRectInfo.width < 200)
            {
                TimeRemain = 100;
            }
            else
            {
                TimeRemain = WindowRectInfo.width;
            }


            if (LocalRegistry.GetFMSData(PersonName, PID, "FileUtility", "CurrentFile").TimeRemainHour > 1)
            {
                GUI.Label(new Rect(2, TimeRemainingPosY, TimeRemain, WindowRectInfo.height), "Time Remaining: More than " + LocalRegistry.GetFMSData(PersonName, PID, "FileUtility", "CurrentFile").TimeRemainHour + " Days");
            }
            else
            {
                if (LocalRegistry.GetFMSData(PersonName, PID, "FileUtility", "CurrentFile").TimeRemainHour > 0)
                {
                    GUI.Label(new Rect(2, TimeRemainingPosY, TimeRemain, WindowRectInfo.height), "Time Remaining: More than " + LocalRegistry.GetFMSData(PersonName, PID, "FileUtility", "CurrentFile").TimeRemainHour + " Day");
                }
                else
                {
                    if (LocalRegistry.GetFMSData(PersonName, PID, "FileUtility", "CurrentFile").TimeRemainMin <= 0)
                    {
                        if (LocalRegistry.GetFMSData(PersonName, PID, "FileUtility", "CurrentFile").TimeRemainUISeconds > 1)
                        {
                            GUI.Label(new Rect(2, TimeRemainingPosY, TimeRemain, WindowRectInfo.height), "Time Remaining: " + secondsString + " Minutes");
                        }
                        else
                        {
                            GUI.Label(new Rect(2, TimeRemainingPosY, TimeRemain, WindowRectInfo.height), "Time Remaining: " + secondsString + " Minute");
                        }
                    }
                    else
                    {
                        if (LocalRegistry.GetFMSData(PersonName, PID, "FileUtility", "CurrentFile").TimeRemainMin > 1)
                        {
                            if (LocalRegistry.GetFMSData(PersonName, PID, "FileUtility", "CurrentFile").TimeRemainUISeconds > 1)
                            {
                                GUI.Label(new Rect(2, TimeRemainingPosY, TimeRemain, WindowRectInfo.height), "Time Remaining: About " + minutesString + " Hours and " + secondsString + " Minutes");
                            }
                            else
                            {
                                GUI.Label(new Rect(2, TimeRemainingPosY, TimeRemain, WindowRectInfo.height), "Time Remaining: About " + minutesString + " Hours and " + secondsString + " Minute");
                            }
                        }
                        else
                        {
                            if (LocalRegistry.GetFMSData(PersonName, PID, "FileUtility", "CurrentFile").TimeRemainUISeconds > 1)
                            {
                                GUI.Label(new Rect(2, TimeRemainingPosY, TimeRemain, WindowRectInfo.height), "Time Remaining: About " + minutesString + " Hour and " + secondsString + " Minutes");
                            }
                            else
                            {
                                GUI.Label(new Rect(2, TimeRemainingPosY, TimeRemain, WindowRectInfo.height), "Time Remaining: About " + minutesString + " Hour and " + secondsString + " Minute");
                            }
                        }
                    }
                }
            }

            //GUI.Label(new Rect(2, 130, 200, 200), "Items Remaining: About" + ProgramHandle[selectedID].ItemRemain.ToString("F2") + GameControl.control.SpaceName);

            GUI.Label(new Rect(2, 180, 200, 200), "Speed: " + NumberFormat.Data(LocalRegistry.GetFMSData(PersonName, PID, "FileUtility", "CurrentFile").SpeedDbl) + GameControl.control.SpaceName + "/Minutes");

            GUI.Label(new Rect(2, 200, 200, 200), "Items Remaining: 1(" + NumberFormat.Data(LocalRegistry.GetFMSData(PersonName, PID, "FileUtility", "CurrentFile").OurFileSizeDbl) + GameControl.control.SpaceName + ")");

            GUI.backgroundColor = Color.black;
            GUI.contentColor = Color.black;

            MathC = LocalRegistry.GetRectData(PersonName, PID, ProgramNameForWinMan, "Window").width - 4;
            //MathE = LocalRegistry.GetFMSData(PersonName, PID, "FileUtility", "CurrentFile").Percentage * MathD;
            Total = MathC;
            Percent = LocalRegistry.GetFMSData(PersonName, PID, "FileUtility", "CurrentFile").Percentage * 0.01f;
            MathF = Total * Percent;
            //10 + 100 * 2.25f
            GUI.Box(new Rect(2, LocalRegistry.GetRectData(PersonName, PID, ProgramNameForWinMan, "Window").height - 29, MathC, 27), "");

            var newColor = new Color32(80, 200, 120, 255);

            GUI.backgroundColor = newColor;
            GUI.contentColor = Registry.Get32ColorData("Player", "System", "FontColor");
            if(MathF > 6.6f)
            {
                GUI.Box(new Rect(3, LocalRegistry.GetRectData(PersonName, PID, ProgramNameForWinMan, "Window").height - 28, MathF - 2, 25), "");
            }
            GUI.backgroundColor = Registry.Get32ColorData("Player", "System", "ButtonColor");

            newColor = new Color32(230, 230, 230, 255);

            float Percent1 = 45 * 0.01f;
            float MathG = Total * Percent1;

            //print(LocalRegistry.GetRectDatav2(PersonName, PID, ProgramNameForWinMan + "/Window"));

            GUI.contentColor = newColor;
            GUI.Label(new Rect(MathG, LocalRegistry.GetRectData(PersonName, PID, ProgramNameForWinMan, "Window").height - 28, 100, 100), "%" + LocalRegistry.GetFMSData(PersonName, PID, "FileUtility", "CurrentFile").Percentage.ToString("F2") + "");
            GUI.contentColor = Registry.Get32ColorData("Player", "System", "FontColor");
        }
    }
}
