using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AccSetup : MonoBehaviour
{
    public Rect windowRect;
    public float native_width = 1920;
    public float native_height = 1080;
    public int windowID;
    public bool show;
    public bool showSetup;
    public GUISkin Skin;
    public GUIStyle style;
    public GUIStyle style1;

    public GUIStyle Title;
    public GUIStyle Button;

    public string Text;

    public List<Color> Colors = new List<Color>();
    public Color32 rgb1 = new Color32(0, 0, 0, 0);
    public Color32 buttonColor = new Color32(0, 0, 0, 0);
    public Color32 fontColor = new Color32(0, 0, 0, 0);
    //public List<string> Colour = new List<string>();
    public int ColourName;
    public int SelectedColour;
    public int CurrentColor;

    public ColorSystem Colour;
    public OSFPCSystem OSFPC;

    public float timer;
    public float timer1;
    public float MaxTimer;

    public bool Red;
    public bool Green;
    public bool Blue;

    public bool TakeRed;
    public bool TakeGreen;
    public bool TakeBlue;

    public int minRange;
    public int maxRange;

    public int SelectedMenu;
    public string InputtedText;
    public string SerialKey;
    public string MatchedKey;
    public bool RunKeyGen;
    public float Timer;
    public int Count;
    public int SelectedCharacter;
    public float SelectedTime;
    public float ScoredTime;

    private SoundControlLogin sc;
    private SetupSound ss;

    public string GatewayName;
    public string PrimaryUsername;
    public string PrimaryPassword;
    public string PrimaryRetypedPassword;
    public string PrimaryPassHint;

    public bool showPass;

    public int Select;

    public Texture2D WorldMap;

    public float MapX;
    public float MapY;

    public float TempMapX;
    public float TempMapY;

    public string SelectedLocation;

    public List<OperatingSystems> AvalibleOS = new List<OperatingSystems>();
    public List<Texture2D> OSDesktopPreview = new List<Texture2D>();
    public OperatingSystems SelectedOS;
    public int SelectedPreview;

    public string OSDesc;

    public string ErrorMsg;

    public float SnowTimer;
    public float SnowCD;
    public float SpawnTimer;
    public float SpawnCD;
    public List<float> TexturePOSX = new List<float>();
    public List<float> TexturePOSY = new List<float>();
    public Texture2D[] SnowPIC;

    public float SnowMod;

    public Vector2 scrollpos = Vector2.zero;
    public int scrollsize;
    public int Res;
    public List<string> Rez = new List<string>();

    public int SelectedProfilePic;

    public Texture2D[] VolumeIcon;

    private SetupSound AccSound;

    private Serial serial;

    public bool DevMode;

    public bool ShortCommand;

    byte[] pixelMatrix;

    public string CommandCharacterCheck;
    public string SpacingCharacterCheck;

    public string Message;

    public bool AcceptedKey;

    public bool CreatePlayerData;

    public bool RegCheck;

    public List<DiskPartSystem> Partitions = new List<DiskPartSystem>();
    public List<DiskPartSystem> PartitionsBlank = new List<DiskPartSystem>();
    public List<InfectionSystem> BlankInfections = new List<InfectionSystem>();
    public List<ProgramSystem.FileType> BlankFileType = new List<ProgramSystem.FileType>();
    public List<ProgramSystem.FileType> DocumentFileType = new List<ProgramSystem.FileType>();

    public List<BankLogsSystem> BlankBankLogs = new List<BankLogsSystem>();

    public List<BankAccountsSystem> StartingBank = new List<BankAccountsSystem>();

    // Use this for initialization
    void Start()
    {
        AccSound = GetComponent<SetupSound>();
        AccSound.PlaySound();
        show = true;
        windowRect = new Rect(0, 0, Screen.width, Screen.height);
        timer = 10;
        buttonColor.a = 255;

        fontColor.r = 255;
        fontColor.g = 255;
        fontColor.b = 255;
        fontColor.a = 255;

        rgb1.r = 0;
        rgb1.g = 0;
        rgb1.b = 50;
        rgb1.a = 255;

        buttonColor.r = 50;
        buttonColor.b = 100;

        //SelectedMenu = 14;

        Customize.cust.RezX = 640;
        Customize.cust.RezY = 480;
        Customize.cust.FullScreen = true;
        Customize.cust.TerminalFontSize = 14;
        Customize.cust.TerminalTextPosMod = 10;
        Customize.cust.Mode = "Small";
        CommandCharacterCheck = " ";
        SpacingCharacterCheck = @"\";
        Customize.cust.DeletionAmt = 200;
        Screen.SetResolution(Customize.cust.RezX, Customize.cust.RezY, Customize.cust.FullScreen);


        SerialKey = StringGenerator.RandomCapsWithNumbersChar(25, 25);
        sc = GetComponent<SoundControlLogin>();
        ss = GetComponent<SetupSound>();

        AfterStart();

        SpawnCD = 0.5f;

        UpdateRezList();

        Reset();

        sc.CurrentVolume = 1f;
        ss.CurrentVolume = 0.025f;

        DevMode = false;

        serial = GetComponent<Serial>();

        OSFPC.BackgroundAddress = "";
        OSFPC.MouseCursorAddress = "";
        OSFPC.ScreenSaverBackgroundAddress = "";
        OSFPC.ScreenSaverPictureAddress = "";


        AvalibleOS.Add(new OperatingSystems("TreeOS", OperatingSystems.OSName.TreeOS));
        AvalibleOS.Add(new OperatingSystems("FluidicIceOS", OperatingSystems.OSName.FluidicIceOS));
        AvalibleOS.Add(new OperatingSystems("AppatureOS", OperatingSystems.OSName.AppatureOS));
        AvalibleOS.Add(new OperatingSystems("EthelOS", OperatingSystems.OSName.EthelOS));
    }

    void Reset()
    {
        //GameControl.control.Save();
        ProfileController.procon.Save();
        //Customize.cust.Save();
        //PersonController.control.Save();
        //ThreadQ.RunNewThread(GameControl.control.Save);
        //ThreadQ.RunNewThread(ProfileController.procon.Save);
        //ThreadQ.RunNewThread(PersonController.control.Save);
        //ThreadQ.RunNewThread(Customize.cust.Save);
    }

    void AfterStart()
    {
        maxRange = 175;
        MaxTimer = 0.0325f;
    }

    void ChristmasUpdate()
    {
        sc.SetSetupVolume();

        SpawnTimer += Time.deltaTime;
        if (SpawnTimer >= SpawnCD)
        {
            if (TexturePOSX.Count < 100)
            {
                TexturePOSX.Add(Random.Range(0, Screen.width));
                TexturePOSY.Add(Random.Range(-50, -150));
            }
            SpawnTimer = 0;
        }

        if (showSetup == true)
        {
            timer1 += Time.deltaTime;
            if (timer1 >= MaxTimer)
            {
                LoadPresetColors();
                if (TexturePOSX.Count > 0)
                {
                    SnowFall();
                }
            }
            SnowTimer += Time.deltaTime;
            if (SnowTimer >= SnowCD)
            {
                SnowFall();
                SnowTimer = 0;
            }
        }
    }

    void NormalUpdate()
    {
        sc.SetSetupVolume();

        if (showSetup == true)
        {
            timer1 += Time.deltaTime;
            if (timer1 >= MaxTimer)
            {
                LoadPresetColors();
            }
        }
    }

    void DefaultMouseSettings()
    {
        Registry.SetFloatData("Player", "System", "DoubleClickSpeed", 0.5f);
        float IconDelay;
        IconDelay = Registry.GetFloatData("Player", "System", "DoubleClickSpeed") * 1000;
        Registry.SetIntData("Player", "System", "DoubleClickSpeed", (int)IconDelay);
        Registry.SetStringData("Player", "System", "DoubleClickSpeed", "Double Click Delay " + IconDelay.ToString("F0") + "ms");
    }

    void ColorSchemeSettings()
    {
        Registry.SetColorData("Player", "System", "ButtonColor", new SColor(new Color32(255,255,255,255)));
        Registry.SetColorData("Player", "System", "WindowColor", new SColor(new Color32(255, 255, 255, 255)));
        Registry.SetColorData("Player", "System", "FontColor", new SColor(new Color32(0, 0, 0, 255)));

        Registry.SetFloatColorData("Player", "System", "ButtonColor", new ColorSystem(255, 255, 255, 255));
        Registry.SetFloatColorData("Player", "System", "WindowColor", new ColorSystem(255, 255, 255, 255));
        Registry.SetFloatColorData("Player", "System", "FontColor", new ColorSystem(0, 0, 0, 255));

        Registry.SetBoolData("Player", "System", "ButtonColor",true);
        Registry.SetBoolData("Player", "System", "WindowColor", true); 
        Registry.SetBoolData("Player", "System", "FontColor", true);
    }

    void DesktopSettings()
    {
        Registry.SetBoolData("Player", "System", "ShowDesktopBackground", true);
        Registry.SetBoolData("Player", "System", "ShowDesktopIcons", true);
    }

    void OSThemeSettings()
    {
        Registry.SetIntData("Player", "System", "Skin", 1);
    }

    void Update()
    {
        if (CreatePlayerData == true)
        {
            StartSetupV2();
        }

        if (RegCheck == true)
        {
            CreatePlayerData = false;
            RegistryLoader.RegLoad.RunRegLoad = true;
            DefaultMouseSettings();
            ColorSchemeSettings();
            DesktopSettings();
            OSThemeSettings();
        }

        if(Registry.GetBoolData("Player","System","ShowDesktopBackground") == true)
        {
            POST();
        }
        string day = System.DateTime.Now.ToString("dd/MM");

        if (day == "25/12")
        {
            ChristmasUpdate();
            Message = "Have a Merry Christmas";
        }
        else if (day == "26/12")
        {
            NormalUpdate();
            Message = "Boxing day is here i hope you all got amazing gifts i guess you could use this day to put away and orgnize things";
        }
        else if (day == "31/10")
        {
            NormalUpdate();
            Message = "Happy Halloween";
        }
        else if (day == "1/1")
        {
            NormalUpdate();
            Message = "Happy New Year";
        }
        else if (day == "1/4")
        {
            NormalUpdate();
            Message = "April Fools";
        }
        else if (day == "4/5")
        {
            NormalUpdate();
            Message = "May the 4th be with you always.";
        }
        else if (day == "8/4")
        {
            NormalUpdate();
            Message = "I want to wish my mum a happy birthday.";
        }
        else if (day == "18/1")
        {
            NormalUpdate();
            Message = "I want to wish my dad a happy birthday.";
        }
        else if (day == "22/4")
        {
            NormalUpdate();
            Message = "Earth day everybody.";
        }
        else if (day == "2/8")
        {
            NormalUpdate();
            Message = "Today is friendship day remember to send regards to those you call friends.";
        }
        else if (day == "13/8")
        {
            NormalUpdate();
            Message = "Today is wrong handed day im kidding its lefthanded day today";
        }
        else if (day == "31/12")
        {
            NormalUpdate();
            Message = "New Years Eve cheers to the next year everybody";
        }
        else if (day == "18/12")
        {
            NormalUpdate();
            Message = "On this day back in 2015 is the day i did a first build of this project.";
        }
        else if (day == "25/11")
        {
            NormalUpdate();
            Message = "This date in 2019 was the day I added the special events messages";
        }
        else
        {
            NormalUpdate();
        }
    }

    void OnGUI()
    {
        Customize.cust.windowx[windowID] = windowRect.x;
        Customize.cust.windowy[windowID] = windowRect.y;

        GUI.skin = Skin;

        for (int i = 0; i < TexturePOSY.Count; i++)
        {
            GUI.depth = -10;
            GUI.DrawTexture(new Rect(TexturePOSX[i], TexturePOSY[i], 16, 16), SnowPIC[0]);
        }

        if (show == true)
        {
            GUI.color = buttonColor;
            GameControl.control.ChangeColor = true;
            windowRect = GUI.Window(1, windowRect, DoMyWindow, "");
        }
        else
        {
            GameControl.control.ChangeColor = false;
        }
    }

    void Style()
    {
        style.normal.background = Skin.button.normal.background;
        style.hover.background = Skin.button.hover.background;
        style.active.background = Skin.button.active.background;
        style.normal.background = Skin.button.normal.background;
        style.hover.background = Skin.button.hover.background;
        style.active.background = Skin.button.active.background;

        style.normal.textColor = fontColor;
        style.hover.textColor = fontColor;
        style.active.textColor = fontColor;
        style.normal.textColor = fontColor;
        style.hover.textColor = fontColor;
        style.active.textColor = fontColor;
    }

    void LoadPresetColors()
    {
        timer1 = 0;
        timer += Time.deltaTime;

        if (timer > 2)
        {
            SelectedColour += 1;

            if (SelectedColour > 3)
            {
                SelectedColour = 1;
            }

            timer = 0;
        }

        if (Red == true)
        {
            if (buttonColor.r >= maxRange)
            {
                TakeRed = true;
                Red = false;
                timer = 5;
            }
            else
            {
                if (TakeRed == false)
                {
                    buttonColor.r++;
                }
            }
        }

        if (Green == true)
        {
            if (buttonColor.g >= maxRange)
            {
                TakeGreen = true;
                Green = false;
                timer = 5;
            }
            else
            {
                if (TakeGreen == false)
                {
                    buttonColor.g++;
                }
            }
        }

        if (Blue == true)
        {
            if (buttonColor.b >= maxRange)
            {
                TakeBlue = true;
                Blue = false;
                timer = 5;
            }
            else
            {
                if (TakeBlue == false)
                {
                    buttonColor.b++;
                }
            }
        }

        if (TakeBlue == true)
        {
            if (buttonColor.b > minRange)
            {
                buttonColor.b--;
            }
            else
            {
                TakeBlue = false;
            }
        }

        if (TakeRed == true)
        {
            if (buttonColor.r > minRange)
            {
                buttonColor.r--;
            }
            else
            {
                TakeRed = false;
            }
        }

        if (TakeGreen == true)
        {
            if (buttonColor.g > minRange)
            {
                buttonColor.g--;
            }
            else
            {
                TakeGreen = false;
            }
        }

        switch (SelectedColour)
        {
            case 1:
                Red = true;
                Green = false;
                Blue = false;
                break;
            case 2:
                Red = false;
                Green = true;
                Blue = false;
                break;
            case 3:
                Red = false;
                Green = false;
                Blue = true;
                break;
        }

        GameControl.control.Red = buttonColor.r;
        GameControl.control.Green = buttonColor.g;
        GameControl.control.Blue = buttonColor.b;
    }

    void SnowFall()
    {
        for (int i = 0; i < TexturePOSY.Count; i++)
        {
            if (TexturePOSX[i] == 0)
            {
                TexturePOSX[i] = Random.Range(0, Screen.width);
            }
            //			if(TexturePOSY [i] == 0)
            //			{
            //				TexturePOSY [i] = Random.Range(-100,-300);
            //			}
            if (TexturePOSY[i] < Screen.height + 1000)
            {
                //TexturePOSY [i] += SnowMod * Time.deltaTime;
                TexturePOSY[i] += Random.Range(1, 2);
            }
            if (TexturePOSY[i] > Screen.height)
            {
                TexturePOSX[i] = Random.Range(0, Screen.width);
                TexturePOSY[i] = Random.Range(-50, -150);
            }
        }

    }

    void Lisence()
    {
        if (Event.current.type == EventType.KeyDown && Event.current.keyCode == KeyCode.Return)
        {
            SelectedMenu = SelectedMenu + 1;
        }

        string Notice = "TOS is under development " +
            " But thank you for testing this version of the game " +
            "" + Message;

        GUI.TextArea(new Rect(Screen.width / 3f, Screen.height / 6f, 350, 200), Notice);

        if (ProfileController.procon.Profiles.Count > 1)
        {
            if (GUI.Button(new Rect(Screen.width / 3f, Screen.height / 1.5f, 75, 25), "< Back", Button))
            {
                SceneManager.LoadScene("Login");
            }
        }

        if (GUI.Button(new Rect(Screen.width / 2f, Screen.height / 1.5f, 100, 25), "Accept", Button))
        {
            SelectedMenu = SelectedMenu + 1;
        }

        if (Event.current.type == EventType.KeyDown && Event.current.keyCode == KeyCode.Backspace)
        {
            if (ProfileController.procon.Profiles.Count > 1)
            {
                SceneManager.LoadScene("Login");
            }
        }
    }

    void SerialKeySystem()
    {
        if (AcceptedKey == true)
        {
            if (GUI.Button(new Rect(Screen.width / 2f, Screen.height / 1.5f, 75, 25), "Next", Button))
            {
                SelectedMenu = SelectedMenu + 1;
                GatewayName = "Gateway";

            }

            if (Event.current.type == EventType.KeyDown && Event.current.keyCode == KeyCode.Return)
            {
                SelectedMenu = SelectedMenu + 1;
                GatewayName = "Gateway";
            }
        }
        GUI.TextArea(new Rect(Screen.width / 3f, Screen.height / 6f, 350, 100), "Please enter your 25 digit product code down below.");

        InputtedText = GUI.TextField(new Rect(Screen.width / 2.75f, Screen.height / 2, 250, 25), InputtedText).ToUpper();

        if (InputtedText == "DEV")
        {
            DevAccount();
        }

        if (InputtedText == "INSIDER")
        {
            InsiderAccount();
        }

        if (InputtedText == "TEST1")
        {
            TestAccount();
        }

        if (InputtedText == "TEST2")
        {
            TestAccount1();
        }

        if(InputtedText == "STREAM")
        {
            SelectedMenu = 12;
        }

        if (InputtedText == serial.SerialKey || InputtedText == SerialKey || InputtedText == "THNKUFOR8UY1N9TH3FNVMLEV1" || InputtedText == "7H4NKY0UF0R73271N97H3G4M3" || InputtedText == "M33RY-CHR12-7M42F-R0MFN-VMD3V" || InputtedText == "SKIP")
        {
            AcceptedKey = true;
        }
        else
        {
            AcceptedKey = false;
            //			if (GUI.Button (new Rect (Screen.width / 1.5f, Screen.height / 1.5f, 150, 25), "AUTO")) 
            //			{
            //				InputtedText = "THNKUFOR8UY1N9TH3FNVMLEV1";
            //			}

            if (DevMode == true)
            {
                if (GUI.Button(new Rect(Screen.width / 1.5f, Screen.height / 1.5f, 100, 25), "BYPASS"))
                {
                    InputtedText = "7H4NKY0UF0R73271N97H3G4M3";
                }
            }

            if (GUI.Button(new Rect(Screen.width / 2.5f, Screen.height / 1.5f, 150, 25), "GENERATE KEY", Button))
            {
                InputtedText = "";
                SelectedCharacter = 0;
                RunKeyGen = true;
                ScoredTime = 0;
                PasswordBreakerV2();
            }
        }

        if (GUI.Button(new Rect(2, 2, 75, 25), "< Back", Button))
        {
            SelectedMenu = SelectedMenu - 1;
        }

        GUI.Label(new Rect(Screen.width / 3f, Screen.height / 1.25f, 150, 25), MatchedKey);

        if (Event.current.type == EventType.KeyDown && Event.current.keyCode == KeyCode.Backspace)
        {
            SelectedMenu = SelectedMenu - 1;
        }
    }

    void Username()
    {
        if (Event.current.type == EventType.KeyDown && Event.current.keyCode == KeyCode.Return)
        {
            SelectedMenu = SelectedMenu + 1;
        }

        float XPos = 40f;
        float YPos = 20f;
        GUI.Label(new Rect(Screen.width / XPos, Screen.height / YPos, 300, 50), "Account and Gateway Name", Title);

        GUI.Label(new Rect(Screen.width / XPos, Screen.height / YPos + 100, 150, 25), "Computer Name", style1);
        GatewayName = GUI.TextField(new Rect(Screen.width / XPos, Screen.height / YPos + 125, 300, 25), GatewayName);

        GUI.Label(new Rect(Screen.width / XPos, Screen.height / YPos + 200, 150, 25), "Account Name", style1);
        PrimaryUsername = GUI.TextField(new Rect(Screen.width / XPos, Screen.height / YPos + 225, 300, 25), PrimaryUsername);

        GUI.Label(new Rect(Screen.width / XPos, Screen.height / YPos + 250, 150, 25), ErrorMsg, style1);

        if (GUI.Button(new Rect(Screen.width / XPos + 300, Screen.height / YPos + 300, 75, 25), "Next", Button))
        {
            if (ProfileController.procon.Profiles.Contains(PrimaryUsername))
            {
                ErrorMsg = "Account name already exists please choose another name.";
            }
            else
            {
                SelectedMenu = SelectedMenu + 1;
            }
        }

        if (GUI.Button(new Rect(Screen.width / XPos + 200, Screen.height / YPos + 300, 75, 25), "Back", Button))
        {
            SelectedMenu = SelectedMenu - 1;
        }

        if (Event.current.type == EventType.KeyDown && Event.current.keyCode == KeyCode.Backspace)
        {
            SelectedMenu = SelectedMenu - 1;
        }
        //GUI.Label(new Rect(Screen.width / 2f, Screen.height / 1.25f, 150, 25),"",style1);
    }

    void Password()
    {
        if (Event.current.type == EventType.KeyDown && Event.current.keyCode == KeyCode.Return)
        {
            SelectedMenu = SelectedMenu + 1;
        }

        float XPos = 40f;
        float YPos = 20f;
        GUI.Label(new Rect(Screen.width / XPos, Screen.height / YPos, 300, 50), "Account Security", Title);

        if (showPass == true)
        {
            GUI.Label(new Rect(Screen.width / XPos, Screen.height / YPos + 100, 150, 25), "Password", style1);
            PrimaryPassword = GUI.TextField(new Rect(Screen.width / XPos, Screen.height / YPos + 125, 300, 25), PrimaryPassword);

            GUI.Label(new Rect(Screen.width / XPos, Screen.height / YPos + 200, 150, 25), "Retype Password", style1);
            PrimaryRetypedPassword = GUI.TextField(new Rect(Screen.width / XPos, Screen.height / YPos + 225, 300, 25), PrimaryRetypedPassword);

            if (GUI.Button(new Rect(Screen.width / XPos + 400, Screen.height / YPos + 150, 75, 25), "(o)", Button))
            {
                showPass = !showPass;
            }
        }
        else
        {
            GUI.Label(new Rect(Screen.width / XPos, Screen.height / YPos + 100, 150, 25), "Password", style1);
            PrimaryPassword = GUI.PasswordField(new Rect(Screen.width / XPos, Screen.height / YPos + 125, 300, 25), PrimaryPassword, "*"[0]);

            GUI.Label(new Rect(Screen.width / XPos, Screen.height / YPos + 200, 150, 25), "Retype Password", style1);
            PrimaryRetypedPassword = GUI.PasswordField(new Rect(Screen.width / XPos, Screen.height / YPos + 225, 300, 25), PrimaryRetypedPassword, "*"[0]);

            if (GUI.Button(new Rect(Screen.width / XPos + 400, Screen.height / YPos + 150, 75, 25), "(-)", Button))
            {
                showPass = !showPass;
            }
        }

        GUI.Label(new Rect(Screen.width / XPos, Screen.height / YPos + 300, 150, 25), "Password Hint", style1);
        PrimaryPassHint = GUI.TextField(new Rect(Screen.width / XPos, Screen.height / YPos + 325, 300, 25), PrimaryPassHint);

        if (GUI.Button(new Rect(Screen.width / XPos + 300, Screen.height / YPos + 400, 75, 25), "Next", Button))
        {
            SelectedMenu = SelectedMenu + 1;
        }


        if (GUI.Button(new Rect(Screen.width / XPos + 200, Screen.height / YPos + 400, 75, 25), "Back", Button))
        {
            SelectedMenu = SelectedMenu - 1;
        }

        if (Event.current.type == EventType.KeyDown && Event.current.keyCode == KeyCode.Backspace)
        {
            SelectedMenu = SelectedMenu - 1;
        }
    }

    void TimeSettings()
    {
        //      if (GUI.Button (new Rect (Screen.width / 2f, Screen.height / 1.5f, 150, 25), "Next")) 
        //{
        //	SelectedMenu = SelectedMenu + 1;
        //}

        //if (Event.current.type == EventType.keyDown && Event.current.keyCode == KeyCode.Return) 
        //{
        //	SelectedMenu = SelectedMenu + 1;
        //}

        //if (Event.current.type == EventType.keyDown && Event.current.keyCode == KeyCode.Backspace) 
        //{
        //	SelectedMenu = SelectedMenu - 1;
        //}
    }

    public void RezSelect()
    {

        switch (Customize.cust.RezSelect)
        {
            case 0:
                Customize.cust.RezX = 640;
                Customize.cust.RezY = 480;
                break;

            case 1:
                Customize.cust.RezX = 800;
                Customize.cust.RezY = 600;
                break;

            case 2:
                Customize.cust.RezX = 1024;
                Customize.cust.RezY = 768;
                break;

            case 3:
                Customize.cust.RezX = 1152;
                Customize.cust.RezY = 864;
                break;

            case 4:
                Customize.cust.RezX = 1280;
                Customize.cust.RezY = 720;
                break;

            case 5:
                Customize.cust.RezX = 1280;
                Customize.cust.RezY = 768;
                break;

            case 6:
                Customize.cust.RezX = 1366;
                Customize.cust.RezY = 768;
                break;

            case 7:
                Customize.cust.RezX = 1600;
                Customize.cust.RezY = 900;
                break;

            case 8:
                Customize.cust.RezX = 1680;
                Customize.cust.RezY = 1050;
                break;

            case 9:
                Customize.cust.RezX = 1920;
                Customize.cust.RezY = 1080;
                break;
            case 10:
                Customize.cust.RezX = 2560;
                Customize.cust.RezY = 1440;
                break;
            case 11:
                Screen.SetResolution(640, 480, Customize.cust.FullScreen);
                Customize.cust.RezX = 640;
                Customize.cust.RezY = 480;
                break;
            case 12:
                Customize.cust.RezX = 751;
                Customize.cust.RezY = 785;
                break;
        }
    }

    void UpdateRezList()
    {
        Rez.Add("640x480");
        Rez.Add("800x600");
        Rez.Add("1024x768");
        Rez.Add("1152x864");
        Rez.Add("1280x720");
        Rez.Add("1280x768");
        Rez.Add("1366x768");
        Rez.Add("1600x900");
        Rez.Add("1680x1050");
        Rez.Add("1920x1080");
        Rez.Add("1440x2560");
        Rez.Add("751x785");
    }

    void CustomSettings()
    {
        float XPos1 = 40f;
        float YPos1 = 20f;

        if (GUI.Button(new Rect(Screen.width / XPos1 + 300, Screen.height / YPos1 + 400, 75, 25), "Next", Button))
        {
            SelectedMenu = SelectedMenu + 1;
        }

        if (GUI.Button(new Rect(Screen.width / XPos1 + 200, Screen.height / YPos1 + 400, 75, 25), "Back", Button))
        {
            SelectedMenu = SelectedMenu - 2;
        }

        if (Event.current.type == EventType.KeyDown && Event.current.keyCode == KeyCode.Return)
        {
            SelectedMenu = SelectedMenu + 1;
        }

        scrollpos = GUI.BeginScrollView(new Rect(5, 60, 120, 100), scrollpos, new Rect(0, 0, 0, Res * 21));
        for (Res = 0; Res < Rez.Count; Res++)
        {
            if (GUI.Button(new Rect(2, Res * 21, 100, 20), "" + Rez[Res], Button))
            {
                Customize.cust.RezSelect = Res;
                RezSelect();
            }
        }
        GUI.EndScrollView();

        if (Event.current.type == EventType.KeyDown && Event.current.keyCode == KeyCode.DownArrow)
        {
            if (Customize.cust.RezSelect < Rez.Count - 1)
            {
                scrollpos.y += 21;
                Customize.cust.RezSelect = Customize.cust.RezSelect + 1;
            }
        }

        if (Event.current.type == EventType.KeyDown && Event.current.keyCode == KeyCode.UpArrow)
        {
            if (Customize.cust.RezSelect > 0)
            {
                scrollpos.y -= 21;
                Customize.cust.RezSelect = Customize.cust.RezSelect - 1;
            }
        }

        GUI.Label(new Rect(170, 100, 300, 20), "Selected Rez: " + Rez[Customize.cust.RezSelect], style1);

        if (Customize.cust.FullScreen == true)
        {
            if (GUI.Button(new Rect(170, 60, 120, 20), "Fullscreen", Button))
            {
                Customize.cust.FullScreen = false;
            }
        }
        else
        {
            if (GUI.Button(new Rect(170, 60, 120, 20), "Window", Button))
            {
                Customize.cust.FullScreen = true;
            }
        }

        if (Event.current.type == EventType.KeyDown && Event.current.keyCode == KeyCode.Backspace)
        {
            SelectedMenu = SelectedMenu - 2;
        }
    }

    void OSSettings()
    {
        float XPos = 40f;
        float YPos = 20f;
        GUI.Label(new Rect(Screen.width / XPos, Screen.height / YPos, 300, 50), "Gateway OS", Title);

        GUI.Label(new Rect(Screen.width / XPos, Screen.height / YPos + 100, 300, 25), "Select one of the following Operating systems below to install", style1);
        //PrimaryPassHint = GUI.TextField (new Rect (Screen.width / XPos, Screen.height / YPos + 325, 300, 25),PrimaryPassHint);

        for (int i = 0; i < AvalibleOS.Count; i++)
        {
            if (GUI.Button(new Rect(Screen.width / XPos, Screen.height / YPos + 150 + 23 * i, 150, 22), AvalibleOS[i].Name.ToString()))
            {
                //SelectedOS.Name = AvalibleOS[i].Name;
                SelectedOS = AvalibleOS[i];
            }
        }

        switch (SelectedOS.Name)
        {
            case OperatingSystems.OSName.CSOSV1:
                OSDesc = "CSOSv1.0 uses a CLI as its desktop this OS is for proffesinal software users";
                break;
            case OperatingSystems.OSName.AppatureOS:
                OSDesc = "AppatureOS is all about keeping the desktop light for more screen space";
                GUI.DrawTexture(new Rect(Screen.width / XPos + 200, Screen.height / YPos + 250, 300, 200), OSDesktopPreview[0]);
                break;
            case OperatingSystems.OSName.TreeOS:
                OSDesc = "TreeOS is a very user friendly OS with basic icon functionaility";
                GUI.DrawTexture(new Rect(Screen.width / XPos + 200, Screen.height / YPos + 250, 300, 200), OSDesktopPreview[1]);
                break;
            case OperatingSystems.OSName.FluidicIceOS:
                OSDesc = "FluidicIceOS is another very user friendly OS with a title bar";
                GUI.DrawTexture(new Rect(Screen.width / XPos + 200, Screen.height / YPos + 250, 300, 200), OSDesktopPreview[2]);
                break;
        }

        GUI.TextArea(new Rect(Screen.width / XPos + 200, Screen.height / YPos + 150, 300, 75), OSDesc);

        if (GUI.Button(new Rect(Screen.width / XPos + 100, Screen.height / YPos + 400, 75, 25), "Next", Button))
        {
            SelectedMenu = SelectedMenu + 1;
        }

        if (Event.current.type == EventType.KeyDown && Event.current.keyCode == KeyCode.Return)
        {
            SelectedMenu = SelectedMenu + 1;
        }

        if (Event.current.type == EventType.KeyDown && Event.current.keyCode == KeyCode.Backspace)
        {
            SelectedMenu = SelectedMenu - 1;
        }


        if (GUI.Button(new Rect(Screen.width / XPos, Screen.height / YPos + 400, 75, 25), "Back", Button))
        {
            SelectedMenu = SelectedMenu - 1;
        }
    }

    void LocationSettings()
    {
        float XPos = Screen.width / 1f;
        float YPos = Screen.height / 1.5f;
        float W = 0.06f * XPos;
        float H = 0.06f * YPos;
        GUI.BeginGroup(new Rect(0, 0, XPos, YPos));

        GUI.Box(new Rect(0, 0, XPos, YPos), "");
        GUI.DrawTexture(new Rect(0, 0, XPos, YPos), WorldMap);

        if (GUI.Button(new Rect(0.84f * XPos, 0.73f * YPos, W, H), "AU", Button))
        {
            SelectedLocation = "Australia";
            MapX = 500;
            MapY = 212;
        }

        if (GUI.Button(new Rect(0.09f * XPos, 0.25f * YPos, W, H), "US", Button))
        {
            SelectedLocation = "USA";
            MapX = 70;
            MapY = 90;
        }

        if (GUI.Button(new Rect(0.11f * XPos, 0.11f * YPos, W, H), "CA", Button))
        {
            SelectedLocation = "Canada";
            MapX = 40;
            MapY = 70;
        }

        if (GUI.Button(new Rect(0.65f * XPos, 0.09f * YPos, W, H), "RU", Button))
        {
            SelectedLocation = "Russia";
            MapX = 310;
            MapY = 65;
        }

        if (GUI.Button(new Rect(0.48f * XPos, 0.19f * YPos, W, H), "EU", Button))
        {
            SelectedLocation = "Europe";
            MapX = 285;
            MapY = 83;
        }

        if (GUI.Button(new Rect(0.75f * XPos, 0.3f * YPos, W, H), "CN", Button))
        {
            SelectedLocation = "China";
            MapX = 450;
            MapY = 120;
        }

        if (GUI.Button(new Rect(0.25f * XPos, 0.63f * YPos, W, H), "BR", Button))
        {
            SelectedLocation = "Brazil";
            MapX = 130;
            MapY = 170;
        }

        GUI.EndGroup();

        GUI.Label(new Rect(Screen.width / 20f, Screen.height / 1.35f, 150, 25), "Selected Country: " + SelectedLocation);

        float XPos1 = 40f;
        float YPos1 = 20f;

        if (GUI.Button(new Rect(Screen.width / XPos1 + 300, Screen.height / YPos1 + 400, 75, 25), "Next", Button))
        {
            SelectedMenu = SelectedMenu + 1;
        }

        if (GUI.Button(new Rect(Screen.width / XPos1 + 200, Screen.height / YPos1 + 400, 75, 25), "Back", Button))
        {
            SelectedMenu = SelectedMenu - 1;
        }

        if (Event.current.type == EventType.KeyDown && Event.current.keyCode == KeyCode.Return)
        {
            SelectedMenu = SelectedMenu + 1;
        }

        if (Event.current.type == EventType.KeyDown && Event.current.keyCode == KeyCode.Backspace)
        {
            SelectedMenu = SelectedMenu - 1;
        }
    }

    void ProfilePicSettings()
    {
        float XPos = 40f;
        float YPos = 20f;

        if (GUI.Button(new Rect(Screen.width / XPos + 300, Screen.height / YPos + 400, 75, 25), "Next", Button))
        {
            SelectedMenu = SelectedMenu + 2;
        }

        if (GUI.Button(new Rect(Screen.width / XPos + 200, Screen.height / YPos + 400, 75, 25), "Back", Button))
        {
            SelectedMenu = SelectedMenu - 1;
        }

        if (Event.current.type == EventType.KeyDown && Event.current.keyCode == KeyCode.Return)
        {
            SelectedMenu = SelectedMenu + 2;
        }

        GUI.Label(new Rect(5, 5, 100, 21), "Profile Pics", Title);

        if (SelectedProfilePic < 2)
        {
            GUI.Label(new Rect(125, 100, 200, 22), "Selected Account Picture", style1);
        }

        if (SelectedProfilePic >= 2)
        {
            GUI.Label(new Rect(125, 100, 200, 22), "Selected Account Picture", style1);
            GUI.DrawTexture(new Rect(125, 125, 224, 224), GameControl.control.UserPic[SelectedProfilePic]);
        }

        scrollpos = GUI.BeginScrollView(new Rect(5, 60, 77, 320), scrollpos, new Rect(0, 0, 0, scrollsize * 64));
        for (scrollsize = 0; scrollsize < GameControl.control.UserPic.Count; scrollsize++)
        {
            if (GUI.Button(new Rect(0, scrollsize * 64, 64, 64), GameControl.control.UserPic[scrollsize]))
            {
                SelectedProfilePic = scrollsize;
                //ProfileController.procon.ProfileID[Select] = pui.ProfilePicID;
            }
        }
        GUI.EndScrollView();

        if (Event.current.type == EventType.KeyDown && Event.current.keyCode == KeyCode.DownArrow)
        {
            if (SelectedProfilePic < GameControl.control.UserPic.Count - 1)
            {
                scrollpos.y += 64;
                SelectedProfilePic = SelectedProfilePic + 1;
            }
        }

        if (Event.current.type == EventType.KeyDown && Event.current.keyCode == KeyCode.UpArrow)
        {
            if (SelectedProfilePic > 0)
            {
                scrollpos.y -= 64;
                SelectedProfilePic = SelectedProfilePic - 1;
            }
        }

        if (Event.current.type == EventType.KeyDown && Event.current.keyCode == KeyCode.Backspace)
        {
            SelectedMenu = SelectedMenu - 1;
        }
    }

    void DevAccount()
    {
        GatewayName = "Gateway";
        PrimaryUsername = "LakeNet Dev";
        PrimaryPassword = "a";
        PrimaryPassHint = "This is a dev account.";
        SelectedProfilePic = 4;
        SelectedLocation = "Australia";
        MapX = 500;
        MapY = 212;
        ShortCommand = true;
        SpacingCharacterCheck = "_";
        SelectedMenu = 9;

        GameControl.control.ProgramFiles.Add(new ProgramSystem("Notepad", "", "", "", "", "", "C:/Real", "Notepad", "", "", ProgramSystem.FileExtension.Real, ProgramSystem.FileExtension.Null, 0, 0, 0, 0, 0, 0, 0, 100, 1.0f, 0, 0, 0, 0, 0, 0, 0, false, false, false, false, BlankInfections, BlankFileType));
        GameControl.control.ProgramFiles.Add(new ProgramSystem("Steam", "", "", "", "", "", "C:/Real", "D:/Steam/Steam.exe", "", "", ProgramSystem.FileExtension.Real, ProgramSystem.FileExtension.Null, 0, 0, 0, 0, 0, 0, 0, 100, 1.0f, 0, 0, 0, 0, 0, 0, 0, false, false, false, false, BlankInfections, BlankFileType));
        GameControl.control.ProgramFiles.Add(new ProgramSystem("File Explorer", "", "", "", "", "", "C:/Real", "C:/Users", "", "", ProgramSystem.FileExtension.Real, ProgramSystem.FileExtension.Null, 0, 0, 0, 0, 0, 0, 0, 100, 1.0f, 0, 0, 0, 0, 0, 0, 0, false, false, false, false, BlankInfections, BlankFileType));
        GameControl.control.ProgramFiles.Add(new ProgramSystem("Discord", "", "", "", "", "", "C:/Real", "C:/Users/lucas/AppData/Local/Discord/app-0.0.309/Discord.exe", "", "", ProgramSystem.FileExtension.Real, ProgramSystem.FileExtension.Null, 0, 0, 0, 0, 0, 0, 0, 100, 1.0f, 0, 0, 0, 0, 0, 0, 0, false, false, false, false, BlankInfections, BlankFileType));
        GameControl.control.ProgramFiles.Add(new ProgramSystem("Alfaykun king cover", "", "", "", "", "", "C:/Real", "C:/Users/lucas/Pictures/Alfaykun King Cover louder.mp4", "", "", ProgramSystem.FileExtension.Real, ProgramSystem.FileExtension.Null, 0, 0, 0, 0, 0, 0, 0, 100, 1.0f, 0, 0, 0, 0, 0, 0, 0, false, false, false, false, BlankInfections, BlankFileType));
        GameControl.control.ProgramFiles.Add(new ProgramSystem("German Mili Cover", "", "", "", "", "", "C:/Real", "F:/Photos/Masterful.mp4", "", "", ProgramSystem.FileExtension.Real, ProgramSystem.FileExtension.Null, 0, 0, 0, 0, 0, 0, 0, 100, 1.0f, 0, 0, 0, 0, 0, 0, 0, false, false, false, false, BlankInfections, BlankFileType));
    }

    void InsiderAccount()
    {
        GatewayName = "Gateway";
        PrimaryUsername = "LakeNet Insider";
        PrimaryPassword = "a";
        PrimaryPassHint = "This is an insider account.";
        SelectedProfilePic = 4;
        SelectedLocation = "Australia";
        MapX = 500;
        MapY = 212;
        ShortCommand = true;
        SpacingCharacterCheck = "_";
        SelectedMenu = 9;

        GameControl.control.ProgramFiles.Add(new ProgramSystem("Notepad", "", "", "", "", "", "C:/Real", "Notepad", "", "", ProgramSystem.FileExtension.Real, ProgramSystem.FileExtension.Null, 0, 0, 0, 0, 0, 0, 0, 100, 1.0f, 0, 0, 0, 0, 0, 0, 0, false, false, false, false, BlankInfections, BlankFileType));
        GameControl.control.ProgramFiles.Add(new ProgramSystem("Steam", "", "", "", "", "", "C:/Real", "D:/Steam/Steam.exe", "", "", ProgramSystem.FileExtension.Real, ProgramSystem.FileExtension.Null, 0, 0, 0, 0, 0, 0, 0, 100, 1.0f, 0, 0, 0, 0, 0, 0, 0, false, false, false, false, BlankInfections, BlankFileType));
        GameControl.control.ProgramFiles.Add(new ProgramSystem("File Explorer", "", "", "", "", "", "C:/Real", "C:/Users", "", "", ProgramSystem.FileExtension.Real, ProgramSystem.FileExtension.Null, 0, 0, 0, 0, 0, 0, 0, 100, 1.0f, 0, 0, 0, 0, 0, 0, 0, false, false, false, false, BlankInfections, BlankFileType));
        GameControl.control.ProgramFiles.Add(new ProgramSystem("Discord", "", "", "", "", "", "C:/Real", "C:/Users/lucas/AppData/Local/Discord/app-0.0.309/Discord.exe", "", "", ProgramSystem.FileExtension.Real, ProgramSystem.FileExtension.Null, 0, 0, 0, 0, 0, 0, 0, 100, 1.0f, 0, 0, 0, 0, 0, 0, 0, false, false, false, false, BlankInfections, BlankFileType));
        GameControl.control.ProgramFiles.Add(new ProgramSystem("Alfaykun king cover", "", "", "", "", "", "C:/Real", "C:/Users/lucas/Pictures/Alfaykun King Cover louder.mp4", "", "", ProgramSystem.FileExtension.Real, ProgramSystem.FileExtension.Null, 0, 0, 0, 0, 0, 0, 0, 100, 1.0f, 0, 0, 0, 0, 0, 0, 0, false, false, false, false, BlankInfections, BlankFileType));
        GameControl.control.ProgramFiles.Add(new ProgramSystem("German Mili Cover", "", "", "", "", "", "C:/Real", "F:/Photos/Masterful.mp4", "", "", ProgramSystem.FileExtension.Real, ProgramSystem.FileExtension.Null, 0, 0, 0, 0, 0, 0, 0, 100, 1.0f, 0, 0, 0, 0, 0, 0, 0, false, false, false, false, BlankInfections, BlankFileType));
    }

    void TestAccount()
    {
        GatewayName = "Gateway";
        PrimaryUsername = "Test Account #1";
        PrimaryPassword = "a";
        PrimaryPassHint = "This is a test account.";
        SelectedProfilePic = 4;
        SelectedLocation = "Australia";
        MapX = 500;
        MapY = 212;
        ShortCommand = true;
        SpacingCharacterCheck = "_";
        SelectedMenu = 9;
    }

    void TestAccount1()
    {
        GatewayName = "Gateway";
        PrimaryUsername = "Test Account #2";
        PrimaryPassword = "a";
        PrimaryPassHint = "This is a test account.";
        SelectedProfilePic = 4;
        SelectedLocation = "Australia";
        MapX = 500;
        MapY = 212;
        ShortCommand = true;
        SpacingCharacterCheck = "_";
        SelectedMenu = 9;
    }

    void StartSetupV2()
    {
        if (PersonController.control.People.Count > 0)
        {
            for(int i = 0; i < PersonController.control.People.Count;i++)
            {
                if (PersonController.control.People[i].Name == "Player")
                {
                    var person = PersonController.control.People[i];

                    person.Gateway.Motherboard.CPUSockets.Add(new SocketSystem(88, 108));

                    person.Gateway.Motherboard.StorageSlots.Add(new SocketSystem(130, 180));
                    person.Gateway.Motherboard.StorageSlots.Add(new SocketSystem(130, 180));

                    person.Gateway.Motherboard.RAMSlots.Add(new SocketSystem(138, 70));

                    person.Gateway.CPU.Add(new CPUSystem("Zion Z-14", "Zion", "Z-14", "140", 32, 1, 0.5f, 0.1f, 0.5f, 0, 0, 0, 0, 0, 1, 0.01f, 0, 100, 100, 0, "", 0, 20, 0.0025f, 0, 0, 0));
                    person.Gateway.StorageDevices.Add(new StorageDevice("EasternVirtual 256", "", "", "", 133, 0, 256, 256, 15, 0.001f, 100, 100, 0, 0.0025f, 0.14f, StorageDevice.StorageType.HDD));
                    person.Gateway.StorageDevices.Add(new StorageDevice("EasternVirtual 128", "", "", "", 133, 0, 128, 128, 15, 0.001f, 100, 100, 0, 0.0025f, 0.14f, StorageDevice.StorageType.HDD));

                    person.Gateway.Name = person.Name + "'s Gateway";
                    person.Gateway.GPU.Add(new GPUSystem("Qividia 970", "PCI-E", 970, 2, 0.1f, 2f, 256, 0, 0, 0, 1, 0.01f, 100, 100, 0, 120, 0.0025f));
                    person.Gateway.RAM.Add(new RamSystem("Vortex 2GB", "DDR1", 0, 2048, 0, 0, 100, 0.01f, 100, 100, 0, 0.0025f, 0));
                    person.Gateway.PSU.Add(new PowerSupplySystem("Toughpower-2pack", "", 450, 0, 0, 0.01f, 100, 100, 0, 0.0025f));
                    person.Gateway.Modem.Add(new ModemSystem("TUGs Basic Modem", "", "", "", 0.56f, 0.56f, 0.56f, 0.28f, 0, 15, 0.01f, 100, 100, 0, 0.0025f, 50, 25, "", ModemSystem.ModemConnectionType.DialUp));

                    person.Gateway.StorageDevices.First().OS.Add(new OperatingSystems("Kernal-Sanders", OperatingSystems.OSName.SafeMode));
                    person.Gateway.StorageDevices.First().OS.Add(new OperatingSystems(SelectedOS.Title, SelectedOS.Name));
                    person.Gateway.StorageDevices.First().InstalledOS.Add(SelectedOS.Title);

                    person.Gateway.StorageDevices.First().OS[0].Partitions.Add(new DiskPartSystem("System", "C", 8, 0, 0, 0));
                    person.Gateway.StorageDevices.First().OS[1].Partitions.Add(new DiskPartSystem("System", "C", 120, 0, 0, 0));

                    person.Gateway.Timer.InitalTimer = 1;

                    for (int j = 0; j < person.Gateway.StorageDevices.Count; j++)
                    {
                        for (int k = 0; k < person.Gateway.StorageDevices[j].OS.Count; k++)
                        {
                            person.Gateway.StorageDevices[j].OS[k].Colour.Window = new ColorSystem(255, 255, 255, 255);
                            person.Gateway.StorageDevices[j].OS[k].Colour.Button = new ColorSystem(255, 255, 255, 255);
                            person.Gateway.StorageDevices[j].OS[k].Colour.Font = new ColorSystem(0, 0, 0, 255);

                            person.Gateway.StorageDevices[j].OS[k].Options.FirstBoot = true;
                            person.Gateway.StorageDevices[j].OS[k].Options.EnableDesktopEnviroment = true;
                            person.Gateway.StorageDevices[j].OS[k].FPC.ShowDesktopBackground = true;
                            person.Gateway.StorageDevices[j].OS[k].FPC.ShowDesktopIcons = true;
                            person.Gateway.StorageDevices[j].OS[k].SerialKey = SerialKey;

                            for (int l = 0; l < person.Gateway.StorageDevices[i].OS[j].Partitions.Count; l++)
                            {
                                if (l == 1)
                                {
                                    person.Gateway.StorageDevices[j].OS[k].Partitions[l].Files.Add(
                                        new ProgramSystemv2(
                                            "Kernal-Sanders",                              // Name
                                            "",                                 // Content
                                            "",                                 // Description
                                            "C:/System",                           // Path
                                            "",                                 // Target
                                            "",                                 // IconPath
                                            "",                                 // PicPath
                                            ProgramSystemv2.FileExtension.os,  //FileExt
                                            0,                                 // FileSize
                                            100,                                // Health
                                            1,                                  // Version
                                            1,                                  // Permissions
                                            ProgramSystemv2.ProgramTypes.Null,  // Programtype
                                            new ResourceManagerSystem()         // ResourceManager
                                        )
                                    );
                                }
                                if (l == 0)
                                {
                                    person.Gateway.StorageDevices[j].OS[k].Partitions[l].Files.Add(
                                        new ProgramSystemv2(
                                            "" + SelectedOS.Name,                              // Name
                                            "",                                 // Content
                                            "",                                 // Description
                                            "C:/System",                          // Path
                                            "",                                 // Target
                                            "",                                 // IconPath
                                            "",                                 // PicPath
                                            ProgramSystemv2.FileExtension.os,  //FileExt
                                            0,                                 // FileSize
                                            100,                                // Health
                                            1,                                  // Version
                                            1,                                  // Permissions
                                            ProgramSystemv2.ProgramTypes.Null,  // Programtype
                                            new ResourceManagerSystem()         // ResourceManager
                                        )
                                    );
                                }
                            }

                            if (person.Gateway.StorageDevices[j].OS[k].Title == SelectedOS.Title)
                            {
                                for (int l = 0; l < person.Gateway.StorageDevices[j].OS[k].Partitions.Count; l++)
                                {
                                    var Files = person.Gateway.StorageDevices[j].OS[k].Partitions[l].Files;

                                    Files.Add(
                                        new ProgramSystemv2(
                                            "C:/",                              // Name
                                            "",                                 // Content
                                            "",                                 // Description
                                            "Gateway",                           // Path
                                            "C:/",                        // Target
                                            "",                                 // IconPath
                                            "",                                 // PicPath
                                            ProgramSystemv2.FileExtension.dir,  //FileExt
                                            0,                                 // FileSize
                                            100,                                // Health
                                            1,                                  // Version
                                            1,                                  // Permissions
                                            ProgramSystemv2.ProgramTypes.Null,  // Programtype
                                            new ResourceManagerSystem()         // ResourceManager
                                        )
                                    );

                                    Files.Add(
                                        new ProgramSystemv2(
                                            "Downloads",                              // Name
                                            "",                                 // Content
                                            "",                                 // Description
                                            "C:/",                           // Path
                                            "C:/Downloads",                        // Target
                                            "",                                 // IconPath
                                            "",                                 // PicPath
                                            ProgramSystemv2.FileExtension.fdl,  //FileExt
                                            0,                                 // FileSize
                                            100,                                // Health
                                            1,                                  // Version
                                            1,                                  // Permissions
                                            ProgramSystemv2.ProgramTypes.Null,  // Programtype
                                            new ResourceManagerSystem()         // ResourceManager
                                        )
                                    );

                                    Files.Add(
                                        new ProgramSystemv2(
                                            "Documents",                              // Name
                                            "",                                 // Content
                                            "",                                 // Description
                                            "C:/",                           // Path
                                            "C:/Documents",                        // Target
                                            "",                                 // IconPath
                                            "",                                 // PicPath
                                            ProgramSystemv2.FileExtension.fdl,  //FileExt
                                            0,                                 // FileSize
                                            100,                                // Health
                                            1,                                  // Version
                                            1,                                  // Permissions
                                            ProgramSystemv2.ProgramTypes.Null,  // Programtype
                                            new ResourceManagerSystem()         // ResourceManager
                                        )
                                    );

                                    Files.Add(
                                        new ProgramSystemv2(
                                            "Programs",                              // Name
                                            "",                                 // Content
                                            "",                                 // Description
                                            "C:/",                           // Path
                                            "C:/Programs",                        // Target
                                            "",                                 // IconPath
                                            "",                                 // PicPath
                                            ProgramSystemv2.FileExtension.fdl,  //FileExt
                                            0,                                 // FileSize
                                            100,                                // Health
                                            1,                                  // Version
                                            1,                                  // Permissions
                                            ProgramSystemv2.ProgramTypes.Null,  // Programtype
                                            new ResourceManagerSystem()         // ResourceManager
                                        )
                                    );

                                    Files.Add(
                                        new ProgramSystemv2(
                                            "System",                              // Name
                                            "",                                 // Content
                                            "",                                 // Description
                                            "C:/",                           // Path
                                            "C:/System",                        // Target
                                            "",                                 // IconPath
                                            "",                                 // PicPath
                                            ProgramSystemv2.FileExtension.fdl,  //FileExt
                                            0,                                 // FileSize
                                            100,                                // Health
                                            1,                                  // Version
                                            1,                                  // Permissions
                                            ProgramSystemv2.ProgramTypes.Null,  // Programtype
                                            new ResourceManagerSystem()         // ResourceManager
                                        )
                                    );


                                    Files.Add(
                                        new ProgramSystemv2(
                                            "Control Panel",              // Name
                                            "",                           // Content
                                            "",                           // Description
                                            "C:/Programs",                // Path
                                            "ControlPanel",               // Target
                                            "",                           // IconPath
                                            "",                           // PicPath
                                            ProgramSystemv2.FileExtension.exe, //FileExt
                                            1,                           // FileSize
                                            100,                          // Health
                                            1,                            // Version
                                            1,                            // Permissions
                                            ProgramSystemv2.ProgramTypes.ControlPanel, // Program type
                                            new ResourceManagerSystem()   // Resource manager
                                        )
                                    );

                                    Files.Add(
                                        new ProgramSystemv2(
                                            "File Manager",              // Name
                                            "",                           // Content
                                            "",                           // Description
                                            "C:/Programs",                // Path
                                            "FileManager",               // Target
                                            "",                           // IconPath
                                            "",                           // PicPath
                                            ProgramSystemv2.FileExtension.exe, //FileExt
                                            1,                           // FileSize
                                            100,                          // Health
                                            1,                            // Version
                                            1,                            // Permissions
                                            ProgramSystemv2.ProgramTypes.FileManager, // Program type
                                            new ResourceManagerSystem()   // Resource manager
                                        )
                                    );

                                    Files.Add(
                                        new ProgramSystemv2(
                                            "CLI",              // Name
                                            "",                           // Content
                                            "",                           // Description
                                            "C:/Programs",                // Path
                                            "CLI",               // Target
                                            "",                           // IconPath
                                            "",                           // PicPath
                                            ProgramSystemv2.FileExtension.exe, //FileExt
                                            1,                           // FileSize
                                            100,                          // Health
                                            1,                            // Version
                                            1,                            // Permissions
                                            ProgramSystemv2.ProgramTypes.CLI, // Program type
                                            new ResourceManagerSystem()   // Resource manager
                                        )
                                    );

                                    Files.Add(
                                        new ProgramSystemv2(
                                            "Task Viewer",              // Name
                                            "",                           // Content
                                            "",                           // Description
                                            "C:/Programs",                // Path
                                            "Task Viewer",               // Target
                                            "",                           // IconPath
                                            "",                           // PicPath
                                            ProgramSystemv2.FileExtension.exe, //FileExt
                                            1,                           // FileSize
                                            100,                          // Health
                                            1,                            // Version
                                            1,                            // Permissions
                                            ProgramSystemv2.ProgramTypes.TaskViewer, // Program type
                                            new ResourceManagerSystem()   // Resource manager
                                        )
                                    );

                                    Files.Add(
                                        new ProgramSystemv2(
                                            "Disk Manager",              // Name
                                            "",                           // Content
                                            "",                           // Description
                                            "C:/Programs",                // Path
                                            "Disk Manager",               // Target
                                            "",                           // IconPath
                                            "",                           // PicPath
                                            ProgramSystemv2.FileExtension.exe, //FileExt
                                            1,                           // FileSize
                                            100,                          // Health
                                            1,                            // Version
                                            1,                            // Permissions
                                            ProgramSystemv2.ProgramTypes.DiskManager, // Program type
                                            new ResourceManagerSystem()   // Resource manager
                                        )
                                    );

                                    Files.Add(
                                        new ProgramSystemv2(
                                            "Gateway Viewer",              // Name
                                            "",                           // Content
                                            "",                           // Description
                                            "C:/Programs",                // Path
                                            "Gateway Viewer",               // Target
                                            "",                           // IconPath
                                            "",                           // PicPath
                                            ProgramSystemv2.FileExtension.exe, //FileExt
                                            1,                           // FileSize
                                            100,                          // Health
                                            1,                            // Version
                                            1,                            // Permissions
                                            ProgramSystemv2.ProgramTypes.GatewayViewer, // Program type
                                            new ResourceManagerSystem()   // Resource manager
                                        )
                                    );

                                    Files.Add(
                                        new ProgramSystemv2(
                                            "Device Manager",              // Name
                                            "",                           // Content
                                            "",                           // Description
                                            "C:/Programs",                // Path
                                            "Device Manager",               // Target
                                            "",                           // IconPath
                                            "",                           // PicPath
                                            ProgramSystemv2.FileExtension.exe, //FileExt
                                            1,                           // FileSize
                                            100,                          // Health
                                            1,                            // Version
                                            1,                            // Permissions
                                            ProgramSystemv2.ProgramTypes.DeviceManager, // Program type
                                            new ResourceManagerSystem()   // Resource manager
                                        )
                                    );

                                    Files.Add(
                                        new ProgramSystemv2(
                                            "System Panel",              // Name
                                            "",                           // Content
                                            "",                           // Description
                                            "C:/Programs",                // Path
                                            "System Panel",               // Target
                                            "",                           // IconPath
                                            "",                           // PicPath
                                            ProgramSystemv2.FileExtension.exe, //FileExt
                                            1,                           // FileSize
                                            100,                          // Health
                                            1,                            // Version
                                            1,                            // Permissions
                                            ProgramSystemv2.ProgramTypes.SystemPanel, // Program type
                                            new ResourceManagerSystem()   // Resource manager
                                        )
                                    );

                                    Files.Add(
                                        new ProgramSystemv2(
                                            "Clock",              // Name
                                            "",                           // Content
                                            "",                           // Description
                                            "C:/Programs",                // Path
                                            "ClockPro",               // Target
                                            "",                           // IconPath
                                            "",                           // PicPath
                                            ProgramSystemv2.FileExtension.exe, //FileExt
                                            1,                           // FileSize
                                            100,                          // Health
                                            1,                            // Version
                                            1,                            // Permissions
                                            ProgramSystemv2.ProgramTypes.ClockPro, // Program type
                                            new ResourceManagerSystem()   // Resource manager
                                        )
                                    );

                                    Files.Add(
                                        new ProgramSystemv2(
                                            "Media Player",              // Name
                                            "",                           // Content
                                            "",                           // Description
                                            "C:/Programs",                // Path
                                            "MediaPlayer",               // Target
                                            "",                           // IconPath
                                            "",                           // PicPath
                                            ProgramSystemv2.FileExtension.exe, //FileExt
                                            1,                           // FileSize
                                            100,                          // Health
                                            1,                            // Version
                                            1,                            // Permissions
                                            ProgramSystemv2.ProgramTypes.MediaPlayer, // Program type
                                            new ResourceManagerSystem()   // Resource manager
                                        )
                                    );

                                    Files.Add(
                                        new ProgramSystemv2(
                                            "Exchange",              // Name
                                            "",                           // Content
                                            "",                           // Description
                                            "C:/Programs",                // Path
                                            "Exchange Viewer",               // Target
                                            "",                           // IconPath
                                            "",                           // PicPath
                                            ProgramSystemv2.FileExtension.exe, //FileExt
                                            1,                           // FileSize
                                            100,                          // Health
                                            1,                            // Version
                                            1,                            // Permissions
                                            ProgramSystemv2.ProgramTypes.ExchangeViewer, // Program type
                                            new ResourceManagerSystem()   // Resource manager
                                        )
                                    );

                                    Files.Add(
                                        new ProgramSystemv2(
                                            "Calculator",              // Name
                                            "",                           // Content
                                            "",                           // Description
                                            "C:/Programs",                // Path
                                            "Calculator",               // Target
                                            "",                           // IconPath
                                            "",                           // PicPath
                                            ProgramSystemv2.FileExtension.exe, //FileExt
                                            1,                           // FileSize
                                            100,                          // Health
                                            1,                            // Version
                                            1,                            // Permissions
                                            ProgramSystemv2.ProgramTypes.Calculator, // Program type
                                            new ResourceManagerSystem()   // Resource manager
                                        )
                                    );

                                    Files.Add(
                                        new ProgramSystemv2(
                                            "Notepad",              // Name
                                            "",                           // Content
                                            "",                           // Description
                                            "C:/Programs",                // Path
                                            "Notepad",               // Target
                                            "",                           // IconPath
                                            "",                           // PicPath
                                            ProgramSystemv2.FileExtension.exe, //FileExt
                                            1,                           // FileSize
                                            100,                          // Health
                                            1,                            // Version
                                            1,                            // Permissions
                                            ProgramSystemv2.ProgramTypes.Notepad, // Program type
                                            new ResourceManagerSystem()   // Resource manager
                                        )
                                    );

                                    Files.Add(
                                        new ProgramSystemv2(
                                            "FileUtility",              // Name
                                            "",                           // Content
                                            "",                           // Description
                                            "C:/Programs",                // Path
                                            "FileUtility",               // Target
                                            "",                           // IconPath
                                            "",                           // PicPath
                                            ProgramSystemv2.FileExtension.exe, //FileExt
                                            1,                           // FileSize
                                            100,                          // Health
                                            1,                            // Version
                                            1,                            // Permissions
                                            ProgramSystemv2.ProgramTypes.FileUtility, // Program type
                                            new ResourceManagerSystem()   // Resource manager
                                        )
                                    );


                                    Files.Add(
                                        new ProgramSystemv2(
                                            "Test Document",              // Name
                                            "Address: www.ping.com" + "\n" + "IP: Test", // Content
                                            "",                           // Description
                                            "C:/Documents",                // Path
                                            "",                             // Target
                                            "",                           // IconPath
                                            "",                           // PicPath
                                            ProgramSystemv2.FileExtension.txt, //FileExt
                                            1,                           // FileSize
                                            100,                          // Health
                                            1,                            // Version
                                            1,                            // Permissions
                                            ProgramSystemv2.ProgramTypes.Null, // Program type
                                            new ResourceManagerSystem()   // Resource manager
                                        )
                                    );

                                }
                            }
                        }
                    }

                    person.Reputation.Add(new RepSystem("REVA", 0, 0, 0, 0, 0, 0));
                }
            }
        }

        RegCheck = true;
    }

    void StartSetup()
    {
        GameControl.control.NewAccount = true;
        ProfileController.procon.ProfilePassWord.Add(PrimaryPassword);
        ProfileController.procon.Profiles.Add(PrimaryUsername);
        ProfileController.procon.ProfileID.Add(1);
        ProfileController.procon.SelectedOS.Add(new OperatingSystems(SelectedOS.Title, SelectedOS.Name));
        ProfileController.procon.PasswordHint.Add(PrimaryPassHint);
        ProfileController.procon.ProfilePic.Add(SelectedProfilePic);
        PrimaryPassword = "";
        PrimaryUsername = "";
        ProfileController.procon.Save();
        Select = ProfileController.procon.Profiles.Count - 1;
        GameControl.control.ProfileName = ProfileController.procon.Profiles[Select];
        GameControl.control.ProfilePicID = ProfileController.procon.ProfilePic[Select];
        GameControl.control.ProfileID = Select;
        GameControl.control.GatewayLocation = SelectedLocation;
        GameControl.control.GatewayPosX = MapX;
        GameControl.control.GatewayPosY = MapY;
        GameControl.control.Rep.Add(new RepSystem("REVA", 0, 0, 0, 0, 0, 0));
        GameControl.control.OSName.Add(new OperatingSystems("Kernal-Sanders",OperatingSystems.OSName.SafeMode));
        GameControl.control.OSName.Add(new OperatingSystems(SelectedOS.Title, SelectedOS.Name));
        GameControl.control.SelectedOS = SelectedOS;
        GameControl.control.ProgramFiles.Add(new ProgramSystem("Kernal-Sanders", "", "", "","","", "Reserved", "","","",ProgramSystem.FileExtension.OS, ProgramSystem.FileExtension.Null,0, 0, 0, 0, 0,0,0, 100, 1,0,0,0,0,0,0,0,false,false,false,false,BlankInfections,BlankFileType));
        GameControl.control.ProgramFiles.Add(new ProgramSystem("" + GameControl.control.SelectedOS.Name, "", "", "", "", "", "C:/System", "", "", "", ProgramSystem.FileExtension.OS, ProgramSystem.FileExtension.Null, 0, 0, 10, 0, 0, 0, 0, 100, 1, 0, 0, 0, 0, 0, 0, 0, false, false, false, false, BlankInfections, BlankFileType));
        GameControl.control.ShortCommands = ShortCommand;
        Customize.cust.ProfileName = ProfileController.procon.Profiles[Select];
        Customize.cust.GatewayName = GatewayName;
        Customize.cust.DoubleClickDelayMenu = 0.5f;
        PersonController.control.ProfileName = ProfileController.procon.Profiles[Select];
        //PersonController.control.People.RemoveRange(0, PersonController.control.People.Count);

        PersonController.control.Global.DateTime.Day = 1;
        PersonController.control.Global.DateTime.DayNumber = 5;
        PersonController.control.Global.DateTime.Month = 1;
        PersonController.control.Global.DateTime.LeapYearCount = 2;
        PersonController.control.Global.DateTime.Year = 1970;
        PersonController.control.Global.DateTime.StartDay = 5;

        string FullDate = "" + PersonController.control.Global.DateTime.Day + "-" + PersonController.control.Global.DateTime.Month + "-" + PersonController.control.Global.DateTime.Year;

        string InstallLocation = "D:/Programs";
        string SysInstallLocation = "C:/Programs";

        BlankFileType.Add(ProgramSystem.FileType.Legal);
        DocumentFileType.Add(ProgramSystem.FileType.Legal);
        DocumentFileType.Add(ProgramSystem.FileType.DocumentReaders);

        GameControl.control.DefaultLaunchedPrograms.Add(new ProgramSystem("Notepad", "", "", "", "", "", SysInstallLocation, "Notepad", "", "", ProgramSystem.FileExtension.Exe, ProgramSystem.FileExtension.Null, 0, 0, 2, 0, 0, 0, 0, 100, 1.0f, 0, 0, 0, 0, 0, 0, 0, false, false, false, false, BlankInfections, DocumentFileType));

        GameControl.control.ProgramFiles.Add(new ProgramSystem("QA Report System", "", "", "", "", "", "Dev", "Bug Report", "", "", ProgramSystem.FileExtension.Exe, ProgramSystem.FileExtension.Null, 0, 0, 0, 0, 0, 0, 0, 100, 2, 0, 0, 0, 0, 0, 0, 0,false,false, false, false, BlankInfections,BlankFileType));

        GameControl.control.ProgramFiles.Add(new ProgramSystem("Music Player", "", "", "", "", "", InstallLocation, "Music Player", "", "", ProgramSystem.FileExtension.Exe, ProgramSystem.FileExtension.Null, 0, 0, 2, 0, 0, 0, 0, 100, 1.0f, 0, 0, 0, 0, 0, 0, 0, false, false, false, false, BlankInfections, BlankFileType));
        GameControl.control.ProgramFiles.Add(new ProgramSystem("Exchange Viewer", "", "", "", "", "", InstallLocation, "Exchange Viewer", "", "", ProgramSystem.FileExtension.Exe, ProgramSystem.FileExtension.Null, 0, 0, 2, 0, 0, 0, 0, 100, 1.0f, 0, 0, 0, 0, 0, 0, 0, false, false, false, false, BlankInfections, BlankFileType));
        GameControl.control.ProgramFiles.Add(new ProgramSystem("CHM", "", "", "", "", "", InstallLocation, "CHM", "", "", ProgramSystem.FileExtension.Exe, ProgramSystem.FileExtension.Null, 0, 0, 2, 0, 0, 0, 0, 100, 1.0f, 0, 0, 0, 0, 0, 0, 0, false, false, false, false, BlankInfections, BlankFileType));

        //GameControl.control.ProgramFiles.Add(new ProgramSystem("Remote Viewer", "", "", "", "", "", SysInstallLocation, "Remote Viewer", "", "", ProgramSystem.FileExtension.Exe, ProgramSystem.FileExtension.Null, 0, 0, 2, 0, 0, 0, 0, 100, 1.0f, 0, 0, 0, 0, 0, 0, 0, false, false, false, false, BlankInfections, BlankFileType));
        GameControl.control.ProgramFiles.Add(new ProgramSystem("Calculator", "", "", "", "", "", SysInstallLocation, "Calculator", "", "", ProgramSystem.FileExtension.Exe, ProgramSystem.FileExtension.Null, 0, 0, 2, 0, 0, 0, 0, 100, 1.0f, 0, 0, 0, 0, 0, 0, 0, false, false, false, false, BlankInfections, BlankFileType));
        GameControl.control.ProgramFiles.Add(new ProgramSystem("Notepad", "", "", "", "", "", SysInstallLocation, "Notepad", "", "", ProgramSystem.FileExtension.Exe, ProgramSystem.FileExtension.Null, 0, 0, 2, 0, 0, 0, 0, 100, 1.0f, 0, 0, 0, 0, 0, 0, 0, false, false, false, false, BlankInfections, DocumentFileType));
        //GameControl.control.ProgramFiles.Add(new ProgramSystem("Notepadv2", "", "", "", "", "", SysInstallLocation, "Notepadv2", "", "", ProgramSystem.FileExtension.Exe, ProgramSystem.FileExtension.Null, 0, 0, 2, 0, 0, 0, 0, 100, 1.0f, 0, 0, 0, 0, 0, 0, 0, false, false, false, false, BlankInfections, DocumentFileType));
        GameControl.control.ProgramFiles.Add(new ProgramSystem("CLI", "", "", "", "", "", SysInstallLocation, "Command Line V3", "", "", ProgramSystem.FileExtension.Exe, ProgramSystem.FileExtension.Null, 0, 0, 2, 0, 0, 0, 0, 100, 1.0f, 0, 0, 0, 0, 0, 0, 0, false, false, false, false, BlankInfections, BlankFileType));
        GameControl.control.ProgramFiles.Add(new ProgramSystem("Task Viewer", "", "", "", "", "", SysInstallLocation, "Task Viewer", "", "", ProgramSystem.FileExtension.Exe, ProgramSystem.FileExtension.Null, 0, 0, 2, 0, 0, 0, 0, 100, 1.0f, 0, 0, 0, 0, 0, 0, 0, false, false, false, false, BlankInfections, BlankFileType));
        GameControl.control.ProgramFiles.Add(new ProgramSystem("Version", "", "", "", "", "", SysInstallLocation, "Version", "", "", ProgramSystem.FileExtension.Exe, ProgramSystem.FileExtension.Null, 0, 0, 2, 0, 0, 0, 0, 100, 1.0f, 0, 0, 0, 0, 0, 0, 0, false, false, false, false, BlankInfections, BlankFileType));
        GameControl.control.ProgramFiles.Add(new ProgramSystem("Disk Manager", "", "", "", "", "", SysInstallLocation, "Disk Manager", "", "", ProgramSystem.FileExtension.Exe, ProgramSystem.FileExtension.Null, 0, 0, 2, 0, 0, 0, 0, 100, 1.0f, 0, 0, 0, 0, 0, 0, 0, false, false, false, false, BlankInfections, BlankFileType));
        GameControl.control.ProgramFiles.Add(new ProgramSystem("Mail", "", "", "", "", "", SysInstallLocation, "Email", "", "", ProgramSystem.FileExtension.Exe, ProgramSystem.FileExtension.Null, 0, 0, 2, 0, 0, 0, 0, 100, 1.0f, 0, 0, 0, 0, 0, 0, 0, false, false, false, false, BlankInfections, BlankFileType));
        GameControl.control.ProgramFiles.Add(new ProgramSystem("Gateway", "", "", "", "", "", SysInstallLocation, "Computer", "", "", ProgramSystem.FileExtension.Exe, ProgramSystem.FileExtension.Null, 0, 0, 2, 0, 0, 0, 0, 100, 1.0f, 0, 0, 0, 0, 0, 0, 0, false, false, false, false, BlankInfections, BlankFileType));
        GameControl.control.ProgramFiles.Add(new ProgramSystem("Device Manager", "", "", "", "", "", SysInstallLocation, "Device Manager", "", "", ProgramSystem.FileExtension.Exe, ProgramSystem.FileExtension.Null, 0, 0, 2, 0, 0, 0, 0, 100, 1.0f, 0, 0, 0, 0, 0, 0, 0, false, false, false, false, BlankInfections, BlankFileType));
        GameControl.control.ProgramFiles.Add(new ProgramSystem("Accounts", "", "", "", "", "", SysInstallLocation, "Account Tracker", "", "", ProgramSystem.FileExtension.Exe, ProgramSystem.FileExtension.Null, 0, 0, 2, 0, 0, 0, 0, 100, 1.0f, 0, 0, 0, 0, 0, 0, 0, false, false, false, false, BlankInfections, BlankFileType));
        GameControl.control.ProgramFiles.Add(new ProgramSystem("Notification Viewer", "", "", "", "", "", SysInstallLocation, "Notification Viewer", "", "", ProgramSystem.FileExtension.Exe, ProgramSystem.FileExtension.Null, 0, 0, 2, 0, 0, 0, 0, 100, 1.0f, 0, 0, 0, 0, 0, 0, 0, false, false, false, false, BlankInfections, BlankFileType));
        GameControl.control.ProgramFiles.Add(new ProgramSystem("Calendar", "", "", "", "", "", SysInstallLocation, "Calendar v2", "", "", ProgramSystem.FileExtension.Exe, ProgramSystem.FileExtension.Null, 0, 0, 2, 0, 0, 0, 0, 100, 1.0f, 0, 0, 0, 0, 0, 0, 0, false, false, false, false, BlankInfections, BlankFileType));
        GameControl.control.ProgramFiles.Add(new ProgramSystem("Event Viewer", "", "", "", "", "", SysInstallLocation, "Event Viewer", "", "", ProgramSystem.FileExtension.Exe, ProgramSystem.FileExtension.Null, 0, 0, 2, 0, 0, 0, 0, 100, 1.0f, 0, 0, 0, 0, 0, 0, 0, false, false, false, false, BlankInfections, BlankFileType));
        GameControl.control.ProgramFiles.Add(new ProgramSystem("Plan Viewer", "", "", "", "", "", SysInstallLocation, "Plan Viewer", "", "", ProgramSystem.FileExtension.Exe, ProgramSystem.FileExtension.Null, 0, 0, 2, 0, 0, 0, 0, 100, 1.0f, 0, 0, 0, 0, 0, 0, 0, false, false, false, false, BlankInfections, BlankFileType));
        GameControl.control.ProgramFiles.Add(new ProgramSystem("Executor", "", "", "", "", "", SysInstallLocation, "Executor", "", "", ProgramSystem.FileExtension.Exe, ProgramSystem.FileExtension.Null, 0, 0, 2, 0, 0, 0, 0, 100, 1.0f, 0, 0, 0, 0, 0, 0, 0, false, false, false, false, BlankInfections, BlankFileType));
        GameControl.control.ProgramFiles.Add(new ProgramSystem("Net Viewer", "", "", "", "", "", SysInstallLocation, "Net Viewer", "", "", ProgramSystem.FileExtension.Exe, ProgramSystem.FileExtension.Null, 0, 0, 2, 0, 0, 0, 0, 100, 1.0f, 0, 0, 0, 0, 0, 0, 0, false, false, false, false, BlankInfections, BlankFileType));
        GameControl.control.ProgramFiles.Add(new ProgramSystem("System Panel", "", "", "", "", "", SysInstallLocation, "System Panel", "", "", ProgramSystem.FileExtension.Exe, ProgramSystem.FileExtension.Null, 0, 0, 2, 0, 0, 0, 0, 100, 1.0f, 0, 0, 0, 0, 0, 0, 0, false, false, false, false, BlankInfections, BlankFileType));
        GameControl.control.ProgramFiles.Add(new ProgramSystem("Gateway Viewer", "", "", "", "", "", SysInstallLocation, "Gateway Viewer", "", "", ProgramSystem.FileExtension.Exe, ProgramSystem.FileExtension.Null, 0, 0, 2, 0, 0, 0, 0, 100, 1.0f, 0, 0, 0, 0, 0, 0, 0, false, false, false, false, BlankInfections, BlankFileType));
        GameControl.control.ProgramFiles.Add(new ProgramSystem("Media Player", "", "", "", "", "", SysInstallLocation, "Media Player", "", "", ProgramSystem.FileExtension.Exe, ProgramSystem.FileExtension.Null, 0, 0, 2, 0, 0, 0, 0, 100, 1.0f, 0, 0, 0, 0, 0, 0, 0, false, false, false, false, BlankInfections, BlankFileType));
        GameControl.control.ProgramFiles.Add(new ProgramSystem("Real Exe Creator", "", "", "", "", "", SysInstallLocation, "Real Exe Creator", "", "", ProgramSystem.FileExtension.Exe, ProgramSystem.FileExtension.Null, 0, 0, 2, 0, 0, 0, 0, 100, 1.0f, 0, 0, 0, 0, 0, 0, 0, false, false, false, false, BlankInfections, BlankFileType));
        GameControl.control.ProgramFiles.Add(new ProgramSystem("Clock", "", "", "", "", "", SysInstallLocation, "ClockPro", "", "", ProgramSystem.FileExtension.Exe, ProgramSystem.FileExtension.Null, 0, 0, 2, 0, 0, 0, 0, 100, 1.0f, 0, 0, 0, 0, 0, 0, 0, false, false, false, false, BlankInfections, DocumentFileType));
        GameControl.control.ProgramFiles.Add(new ProgramSystem("ICQ", "", "", "", "", "", SysInstallLocation, "ICQ", "", "", ProgramSystem.FileExtension.Exe, ProgramSystem.FileExtension.Null, 0, 0, 2, 0, 0, 0, 0, 100, 1.0f, 0, 0, 0, 0, 0, 0, 0, false, false, false, false, BlankInfections, DocumentFileType));
        GameControl.control.ProgramFiles.Add(new ProgramSystem("File Manager", "", "", "", "", "", SysInstallLocation, "FileManager", "", "", ProgramSystem.FileExtension.Exe, ProgramSystem.FileExtension.Null, 0, 0, 2, 0, 0, 0, 0, 100, 1.0f, 0, 0, 0, 0, 0, 0, 0, false, false, false, false, BlankInfections, DocumentFileType));

        string IPAddress = StringGenerator.RandomNumberChar(3, 3) + "." + StringGenerator.RandomNumberChar(3, 3) + "." + StringGenerator.RandomNumberChar(3, 3) + "." + StringGenerator.RandomNumberChar(3, 3);

        //GameControl.control.Gateway.CPUSockets.Add(new CPUSocketSystem("420",88,108,0,null));
        //GameControl.control.Gateway.RAMSlots.Add(new SocketSystem(139, 70));
        //GameControl.control.Gateway.InstalledCPU.Add(new CPUSystem("Zion Z-14", "Zion", "Z-14", "420", 32, 1, 0.5f, 0.1f, 0.5f, 0, 0, 0, 0, 0, 1, 0.01f, 0, 100, 100, 0, "", 0, 20, 0.0025f, 0, 0, 0));
       // GameControl.control.Gateway.InstalledGPU.Add(new GPUSystem("Qividia 970", "PCI-E", 970, 2, 0.1f, 2f, 256, 0, 0, 0, 1, 0.01f, 100, 100, 0, 120, 0.0025f));
      ///  GameControl.control.Gateway.InstalledRAM.Add(new RamSystem("Vortex 2GB", "DDR1", 0, 2048, 0, 0, 100, 0.01f, 100, 100, 0, 0.0025f,0));
      //  GameControl.control.Gateway.InstalledPSU.Add(new PowerSupplySystem("Toughpower-2pack", "", 450, 0, 0, 0.01f, 100, 100, 0, 0.0025f));
       // GameControl.control.Gateway.InstalledModem.Add(new ModemSystem("TUGs Basic Modem", "", "", "", 0.056f, 0.056f, 0.056f, 0.029f, 0, 15, 0.01f, 100, 100, 0, 0.0025f, 150, 0,IPAddress, ModemSystem.ModemConnectionType.DialUp));

        //GameControl.control.Gateway.StorageDevices.Add(new StorageDevice("EasternVirtual 128", "", "", "", 133, 0, 128, 128, 15, 0.001f, 100, 100, 0, 0.0025f, 0.14f, StorageDevice.StorageType.HDD));

       // Partitions.Add(new DiskPartSystem("System", "C", 60,0,0, 0));
       // Partitions.Add(new DiskPartSystem("Storage", "D", 60,0,0, 0));

       // GameControl.control.Gateway.StorageDevices.Add(new StorageDevice("EasternVirtual 128", "", "", "", 133, 0, 128, 128, 15, 0.001f, 100, 100, 0, 0.0025f, 0.14f, StorageDevice.StorageType.HDD));

        int IDay = 1;
        int IMonth = 1;
        int IYear = 1970;
        int DDay = 2;
        int DMonth = 1;
        int DYear = 1970;
        DateSystem InitalPlanDate = new DateSystem(0, 0, 0, 0, IDay, IMonth, IYear, 0, "", false, "", 0, 0, 0, false, "" + IDay.ToString("00") + "/" + IMonth.ToString("00") + "/" + IYear.ToString("0000"), "","", "",false);
        DateSystem DuePlanDate = new DateSystem(0, 0, 0, 0, DDay, DMonth, DYear, 0, "", false, "", 0, 0, 0, false, "" + DDay.ToString("00") + "/" + DMonth.ToString("00") + "/" + DYear.ToString("0000"), "", "", "", false);

        GameControl.control.Plans.Add(new PlanSystem("TUG", "www.tugs.com", "Basic Modem Package", "A basic modem package that comes with dialup and a modem for 150 a month", 150, InitalPlanDate, DuePlanDate));

        GameControl.control.ProgramFiles.Add(new ProgramSystem("C:/", "System", "", "", "", "", "Gateway","C:/", "", "", ProgramSystem.FileExtension.Dir, ProgramSystem.FileExtension.Null, 0, 0, 60, 0, 0, 0, 0, 100, 0, 0, 0, 0, 0, 0, 0, 0, false, false, false, false, BlankInfections, BlankFileType));
        //GameControl.control.Gateway.InstalledStorageDevice[0].UsedSpace += 60;
        //GameControl.control.Gateway.InstalledStorageDevice[0].FreeSpace = GameControl.control.Gateway.InstalledStorageDevice[0].Capacity - GameControl.control.Gateway.InstalledStorageDevice[0].UsedSpace;
        GameControl.control.ProgramFiles.Add(new ProgramSystem("Downloads", "", "", "", "", "", "C:/", "C:/Downloads", "", "", ProgramSystem.FileExtension.Fdl, ProgramSystem.FileExtension.Null, 0, 0, 0, 0, 0, 0, 0, 100, 0, 0, 0, 0, 0, 0, 0, 0, false, false, false, false, BlankInfections, BlankFileType));
        GameControl.control.ProgramFiles.Add(new ProgramSystem("Documents", "", "", "", "", "", "C:/", "C:/Documents", "", "", ProgramSystem.FileExtension.Fdl, ProgramSystem.FileExtension.Null, 0, 0, 0, 0, 0, 0, 0, 100, 0, 0, 0, 0, 0, 0, 0, 0, false, false, false, false, BlankInfections, BlankFileType));
        GameControl.control.ProgramFiles.Add(new ProgramSystem("Programs", "", "", "", "", "", "C:/", "C:/Programs", "", "", ProgramSystem.FileExtension.Fdl, ProgramSystem.FileExtension.Null, 0, 0, 0, 0, 0, 0, 0, 100, 0, 0, 0, 0, 0, 0, 0, 0, false, false, false, false, BlankInfections, BlankFileType));
        GameControl.control.ProgramFiles.Add(new ProgramSystem("System", "", "", "", "", "", "C:/", "C:/System", "", "", ProgramSystem.FileExtension.Fdl, ProgramSystem.FileExtension.Null, 0, 0, 0, 0, 0, 0, 0, 100, 0, 0, 0, 0, 0, 0, 0, 0, false, false, false, false, BlankInfections, BlankFileType));
        GameControl.control.ProgramFiles.Add(new ProgramSystem("Real", "", "", "", "", "", "C:/", "C:/Real", "", "", ProgramSystem.FileExtension.Fdl, ProgramSystem.FileExtension.Null, 0, 0, 0, 0, 0, 0, 0, 100, 0, 0, 0, 0, 0, 0, 0, 0, false, false, false, false, BlankInfections, BlankFileType));

        GameControl.control.ProgramFiles.Add(new ProgramSystem("D:/", "Storage", "", "", "", "", "Gateway", "D:/", "", "", ProgramSystem.FileExtension.Dir, ProgramSystem.FileExtension.Null, 0, 0, 60, 0, 0, 0, 0, 100, 0, 0, 0, 0, 0, 0, 0, 0, false, false, false, false, BlankInfections, BlankFileType));
        //GameControl.control.Gateway.InstalledStorageDevice[0].UsedSpace += 60;
        //GameControl.control.Gateway.InstalledStorageDevice[0].FreeSpace = GameControl.control.Gateway.InstalledStorageDevice[0].Capacity - GameControl.control.Gateway.InstalledStorageDevice[0].UsedSpace;

        GameControl.control.ProgramFiles.Add(new ProgramSystem("Programs", "", "", "", "", "", "D:/", "D:/Programs", "", "", ProgramSystem.FileExtension.Fdl, ProgramSystem.FileExtension.Null, 0, 0, 0, 0, 0, 0, 0, 100, 0, 0, 0, 0, 0, 0, 0, 0, false, false, false, false, BlankInfections, BlankFileType));

        Customize.cust.TerminalCommandCharacterSplit = CommandCharacterCheck;
        Customize.cust.TerminalSpaceCharacterSplit = SpacingCharacterCheck;
        Customize.cust.DoubleClickDelayMenu = 0.5f;
        Customize.cust.EnableSoundTrack = false;
        Customize.cust.SoundtrackVolume = 0.1f;
        Customize.cust.Volume = 1;
        Customize.cust.NotiVolume = 1;
        Customize.cust.TraceBeepsVolume = 0.1f;
        Customize.cust.PlayNotiSound = true;
        Customize.cust.SelectedNotiSound = 7;
        Customize.cust.AutoSaveTime = 60;
        Customize.cust.EnableAutoSave = true;

        GameControl.control.GameVersion.Add("Version: 0.0.397");
        GameControl.control.GameVersion.Add("Iteration: 0");
        GameControl.control.GameVersion.Add("Build: 1");
        GameControl.control.GameVersion.Add("Build State: Internal Dev Build");


        for (int i = 0; i < GameControl.control.ProgramFiles.Count; i++)
        {
            GameControl.control.ProgramFiles[i].Date = FullDate;
        }

        PersonController.control.People.Add(new PeopleSystem("Player", "", "", "", "", "", 0, 0));
        CreatePlayerData = true;
    }

    void Dev()
    {
        GUI.Label(new Rect(Screen.width / 4f, Screen.height / 1.25f, 150, 25), "Red " + buttonColor.r);
        GUI.Label(new Rect(Screen.width / 4f, Screen.height / 1.5f, 150, 25), "Green " + buttonColor.g);
        GUI.Label(new Rect(Screen.width / 4f, Screen.height / 1.75f, 150, 25), "Blue " + buttonColor.b);
        GUI.Label(new Rect(Screen.width / 4f, Screen.height / 2f, 150, 25), "Speed " + MaxTimer.ToString("F4"));
        MaxTimer = GUI.HorizontalSlider(new Rect(Screen.width / 4f, Screen.height / 2.25f, 500, 25), MaxTimer, 0, 0.1f);
    }

    void POST()
    {
        Customize.cust.Load();
        GameControl.control.Load();
        ProfileController.procon.Load();
        SceneManager.LoadScene("Game");
        //Application.LoadLevel("Game");
    }

    void Stream()
    {
        showSetup = true;
        windowRect.width = Screen.width;
        windowRect.height = Screen.height;
        ss.CurrentVolume = 0;
       // GUI.Label(new Rect(Screen.width / 4f, Screen.height / 2f, 150, 25), "FakeNetVM");
       // GUI.Label(new Rect(Screen.width / 4f, Screen.height / 1.75f, 150, 25), " STREAM");
       // GUI.Label(new Rect(Screen.width / 4f, Screen.height / 1.5f, 150, 25), "Pre-Show");
    }

    void ChristmasDevMode()
    {
        MaxTimer = 0.1f;
        ChristmasUpdate();
        showSetup = true;
        GUI.Label(new Rect(Screen.width / 4f, Screen.height / 2f, 150, 25), "Merry Christmas");
        GUI.Label(new Rect(Screen.width / 4f, Screen.height / 1.75f, 150, 25), "From the team");
    }

    void DoMyWindow(int WindowID)
    {

        GUI.backgroundColor = buttonColor;
        GUI.contentColor = fontColor;

        Title.fontSize = 36;
        Title.normal.textColor = Color.white;
        Button.fontSize = 18;
        Button.alignment = TextAnchor.MiddleCenter;
        Button.normal.textColor = Color.grey;
        Button.hover.textColor = Color.white;

        if (SelectedMenu < 11)
        {
            SoundSystem();
        }

        switch (SelectedMenu)
        {
            case 0:
                Lisence();
                showSetup = true;
                break;
            case 1:
                SerialKeySystem();
                break;
            case 2:
                Username();
                break;
            case 3:
                Password();
                break;
            case 4:
                ProfilePicSettings();
                break;
            case 5:
                TimeSettings();
                break;
            case 6:
                CustomSettings();
                break;
            case 7:
                LocationSettings();
                break;
            case 8:
                ExtraSettings();
                break;
            case 9:
                OSSettings();
                break;
            case 10:
                ConfirmationScreen();
                break;
            case 11:
                GUI.skin.label.fontSize = 24;
                GUI.Label(new Rect(Screen.width / 4f, Screen.height / 2.1f, 0, 0), "Please Wait while we setup your OS.");
                break;
            case 12:
                Stream();
                break;
            case 13:
                Dev();
                break;
            case 14:
                ChristmasDevMode();
                break;
        }

        if (RunKeyGen == true)
        {
            PasswordBreakerV2();
        }
    }

    void ExtraSettings()
    {
        float XPos = 40f;
        float YPos = 20f;

        if (GUI.Button(new Rect(Screen.width / XPos + 300, Screen.height / YPos + 400, 75, 25), "Next", Button))
        {
            SelectedMenu = SelectedMenu + 1;
        }

        if (GUI.Button(new Rect(Screen.width / XPos + 200, Screen.height / YPos + 400, 75, 25), "Back", Button))
        {
            SelectedMenu = SelectedMenu - 1;
        }

        if (Event.current.type == EventType.KeyDown && Event.current.keyCode == KeyCode.Return)
        {
            SelectedMenu = SelectedMenu + 1;
        }

        GUI.Label(new Rect(2, 2, 200, 22), "Command Line Interface(CLI) Options", Title);

        if (ShortCommand == true)
        {
            if (GUI.Button(new Rect(5, 40, 175, 22), "Short CLI Commands", Button))
            {
                ShortCommand = !ShortCommand;
            }
            GUI.Label(new Rect(5, 65, 200, 22), "Example", style1);
            GUI.Label(new Rect(5, 90, 200, 22), "dl", style1);
            GUI.Label(new Rect(5, 115, 200, 22), "pwd", style1);
        }
        else
        {
            if (GUI.Button(new Rect(5, 40, 175, 22), "Long CLI Commands ", Button))
            {
                ShortCommand = !ShortCommand;
            }
            GUI.Label(new Rect(5, 65, 200, 22), "Example", style1);
            GUI.Label(new Rect(5, 90, 200, 22), "download", style1);
            GUI.Label(new Rect(5, 115, 200, 22), "printworkingdirectory", style1);
        }

        GUI.Label(new Rect(5, 160, 200, 22), "Command Character Check", style1);
        CommandCharacterCheck = GUI.TextField(new Rect(5, 185, 22, 22), "" + CommandCharacterCheck);

        GUI.Label(new Rect(5, 210, 200, 22), "Spacing Character Check", style1);
        SpacingCharacterCheck = GUI.TextField(new Rect(5, 235, 22, 22), "" + SpacingCharacterCheck);

        GUI.Label(new Rect(5, 260, 200, 22), "run" + CommandCharacterCheck + "Disk" + SpacingCharacterCheck + "Manager", style1);

        if (Event.current.type == EventType.KeyDown && Event.current.keyCode == KeyCode.Backspace)
        {
            SelectedMenu = SelectedMenu - 1;
        }
    }

    void ConfirmationScreen()
    {
        if (Event.current.type == EventType.KeyDown && Event.current.keyCode == KeyCode.Return)
        {
            SelectedMenu = 10;
            StartSetup();
        }

        float XPos = 40f;
        float YPos = 20f;

        if (GUI.Button(new Rect(Screen.width / XPos + 350, Screen.height / YPos + 250, 150, 25), "Start Installation", Button))
        {
            SelectedMenu = 11;
            StartSetup();
        }

        GUI.skin.label.fontSize = 18;
        GUI.Label(new Rect(Screen.width / XPos + 0, Screen.height / YPos + 0, 75, 25), "Username: " + PrimaryUsername);
        GUI.Label(new Rect(Screen.width / XPos + 0, Screen.height / YPos + 50, 75, 25), "Gateway Name: " + GatewayName);
        GUI.Label(new Rect(Screen.width / XPos + 0, Screen.height / YPos + 100, 75, 25), "Gateway Location: " + SelectedLocation);
        GUI.Label(new Rect(Screen.width / XPos + 0, Screen.height / YPos + 150, 75, 25), "Installed OS: " + SelectedOS.Name);
        GUI.Label(new Rect(Screen.width / XPos + 0, Screen.height / YPos + 200, 75, 25), "Resolution: " + Customize.cust.RezX + "x" + Customize.cust.RezY);

        if (Event.current.type == EventType.KeyDown && Event.current.keyCode == KeyCode.Backspace)
        {
            SelectedMenu = SelectedMenu - 1;
        }
    }

    void SoundSystem()
    {
        bool MouseOver = false;
        if (ss.Mute == false)
        {
            if (new Rect(Screen.width - 150, windowRect.y + 22, 22, 22).Contains(Event.current.mousePosition))
            {
                if (ss.CurrentVolume <= 0)
                {
                    if (GUI.Button(new Rect(Screen.width - 150, windowRect.y + 22, 22, 22), VolumeIcon[0]))
                    {
                        ss.Mute = true;
                    }
                }
                else if (ss.CurrentVolume > 0 && ss.CurrentVolume < 0.32f)
                {
                    if (GUI.Button(new Rect(Screen.width - 150, windowRect.y + 22, 22, 22), VolumeIcon[1]))
                    {
                        ss.Mute = true;
                    }
                }
                else if (ss.CurrentVolume < 0.64f)
                {
                    if (GUI.Button(new Rect(Screen.width - 150, windowRect.y + 22, 22, 22), VolumeIcon[2]))
                    {
                        ss.Mute = true;
                    }
                }
                else if (ss.CurrentVolume > 0.64f)
                {
                    if (GUI.Button(new Rect(Screen.width - 150, windowRect.y + 22, 22, 22), VolumeIcon[3]))
                    {
                        ss.Mute = true;
                    }
                }
            }
            else
            {
                if (ss.CurrentVolume <= 0)
                {
                    GUI.DrawTexture(new Rect(Screen.width - 150, windowRect.y + 22, 22, 22), VolumeIcon[0]);
                }
                else if (ss.CurrentVolume > 0 && ss.CurrentVolume < 0.32f)
                {
                    GUI.DrawTexture(new Rect(Screen.width - 150, windowRect.y + 22, 22, 22), VolumeIcon[1]);
                }
                else if (ss.CurrentVolume < 0.64f)
                {
                    GUI.DrawTexture(new Rect(Screen.width - 150, windowRect.y + 22, 22, 22), VolumeIcon[2]);
                }
                else if (ss.CurrentVolume > 0.64f)
                {
                    GUI.DrawTexture(new Rect(Screen.width - 150, windowRect.y + 22, 22, 22), VolumeIcon[3]);
                }
            }
        }
        else
        {
            if (new Rect(Screen.width - 150, windowRect.y + 22, 22, 22).Contains(Event.current.mousePosition))
            {
                if (GUI.Button(new Rect(Screen.width - 150, windowRect.y + 22, 22, 22), VolumeIcon[0]))
                {
                    ss.Mute = false;
                }
            }
            else
            {
                GUI.DrawTexture(new Rect(Screen.width - 150, windowRect.y + 22, 22, 22), VolumeIcon[0]);
            }
        }
        ss.CurrentVolume = GUI.HorizontalSlider(new Rect(Screen.width - 125, windowRect.y + 26, 100, 22), ss.CurrentVolume, 0, 1);
    }

    void PasswordBreakerV2()
    {
        Timer += Time.deltaTime;
        ScoredTime += Time.deltaTime / 2;

        if (Timer > SelectedTime)
        {
            MatchedKey = StringGenerator.RandomCapsWithNumbersChar(SerialKey.Length, SerialKey.Length);
            Timer = 0;

            if (MatchedKey[SelectedCharacter] == SerialKey[SelectedCharacter])
            {
                sc.PlaySound();
                InputtedText += MatchedKey[SelectedCharacter];
                SelectedCharacter++;
            }

            if (InputtedText.Length >= MatchedKey.Length)
            {
                MatchedKey = "";
                RunKeyGen = false;
                Timer = 0;
            }
        }
    }
}
