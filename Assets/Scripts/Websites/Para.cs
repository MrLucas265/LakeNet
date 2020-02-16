//using UnityEngine;
//using System.Collections;
//using System.Collections.Generic;

//public class Para : MonoBehaviour
//{
//    public int ЫефкеСщгте;
//    public List<string> УьфшдЫгиоусе = new List<string>();
//    public List<string> ТщеуЕшеду = new List<string>();

//    const string пднзры = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
//    const string ФссТщ = "1234567890";

//    public bool дщппув;
//    public bool ырщцЬутг;

//    public int Ыудусе;

//    public string ГыкТфьу;
//    public string зфыыцщкв;
//    public string ЫшеуФвьштЗфыы;

//    private GameObject Сщьзгеук;
//    private GameObject Зкщьзеы;
//    private GameObject Фзздшсфешщты;
//    private GameObject Рфслштп;
//    private GameObject Ыныеуь;

//    private InternetBrowser ши;
//    private Computer сщь;
//    private ErrorProm уз;
//    private Tracer екфсу;
//    private SystemMap ыь;
//    private TextReader ек;
//    private Progtive зкщп;
//    private Defalt вуа;

//    private WebSec цы;
//    private PasswordList зд;
//    private CLICommands сдшс;

//    public Color32 игеещтСщдщк = new Color32(0, 0, 0, 0);
//    public Color32 ащтеСЩдщк = new Color32(0, 0, 0, 0);

//    public List<ProgramSystem> ЗфпуАшду = new List<ProgramSystem>();

//    public Vector2 ыскщддзщы = Vector2.zero;
//    public int ыскщддышяу;

//    public int ЬфчЗгидшсАшдуы;
//    public int ЬфчЗкшмфеуАшдуы;

//    public int ЗгидшсСщгте;
//    public int ЗкшмфеуСщгте;

//    public int ЗгидшсАшдуСщгте;
//    public int ЗкшщмфеуАшдуСЩгте;

//    public int ЦуиышеуСЩгте;

//    public bool ПутАшдуы;

//    void Start()
//    {
//        Сщьзгеук = GameObject.Find("Computer");
//        Зкщьзеы = GameObject.Find("Prompts");
//        Фзздшсфешщты = GameObject.Find("Applications");
//        Рфслштп = GameObject.Find("Hacking");
//        Ыныеуь = GameObject.Find("System");
//        ЫефкеСщгте = Random.Range(25, 100);

//        ЬфчЗгидшсАшдуы = Random.Range(25, 50);
//        ЬфчЗкшмфеуАшдуы = Random.Range(25, 50);

//        LoadPresetColors();
//        FileSystemGenerator();
//        Documents();
//        WebSearch();
//    }


//    void LoadPresetColors()
//    {
//        //rgb1.r = 255;
//        //rgb1.g = 255;
//        //rgb1.b = 255;
//        //rgb1.a = 255;

//        игеещтСщдщк.r = 255;
//        игеещтСщдщк.g = 0;
//        игеещтСщдщк.b = 0;
//        игеещтСщдщк.a = 255;

//        ащтеСЩдщк.r = 0;
//        ащтеСЩдщк.g = 255;
//        ащтеСЩдщк.b = 0;
//        ащтеСЩдщк.a = 255;
//    }


//    void WebSearch()
//    {
//        // APPLICATIONS
//        цы = Фзздшсфешщты.GetComponent<WebSec>();
//        ыь = Фзздшсфешщты.GetComponent<SystemMap>();
//        ек = Фзздшсфешщты.GetComponent<TextReader>();
//        ши = Фзздшсфешщты.GetComponent<InternetBrowser>();
//        // PROMPTS
//        уз = Зкщьзеы.GetComponent<ErrorProm>();
//        // HACKING
//        екфсу = Рфслштп.GetComponent<Tracer>();
//        зкщп = Рфслштп.GetComponent<Progtive>();
//        // SYSTEM
//        сщь = Ыныеуь.GetComponent<Computer>();
//        вуа = Ыныеуь.GetComponent<Defalt>();
//        зд = Ыныеуь.GetComponent<PasswordList>();
//        сдшс = Ыныеуь.GetComponent<CLICommands>();
//    }

//    public string GetRandomString(int min, int max)
//    {
//        int charAmount = Random.Range(min, max); //set those to the minimum and maximum length of your string
//        string retMe = "";
//        for (int i = 0; i < charAmount; i++)
//        {
//            retMe += пднзры[Random.Range(0, пднзры.Length)];
//        }
//        return retMe;
//    }

//    public string GetRandomNumber(int min, int max)
//    {
//        int charAmount = Random.Range(min, max); //set those to the minimum and maximum length of your string
//        string retMe = "";
//        for (int i = 0; i < charAmount; i++)
//        {
//            retMe += ФссТщ[Random.Range(0, ФссТщ.Length)];
//        }
//        return retMe;
//    }

//    void Update()
//    {
//        if (ЫшеуФвьштЗфыы == "")
//        {
//            ЫшеуФвьштЗфыы = GetRandomString(8, 8);
//        }

//        if (ЦуиышеуСЩгте <= GameControl.control.WebsiteFiles.Count)
//        {
//            for (int i = 0; i < GameControl.control.WebsiteFiles.Count; i++)
//            {
//                if (GameControl.control.WebsiteFiles[i].Location == "Para Public")
//                {
//                    ЗгидшсАшдуСщгте++;
//                }

//                if (GameControl.control.WebsiteFiles[i].Location == "Para Private")
//                {
//                    ЬфчЗкшмфеуАшдуы++;
//                }
//                ЦуиышеуСЩгте++;
//            }
//        }

//        if (ЦуиышеуСЩгте == GameControl.control.WebsiteFiles.Count)
//        {
//            ПутАшдуы = true;
//        }

//        if (ЗгидшсАшдуСщгте <= ЬфчЗгидшсАшдуы || ЬфчЗкшмфеуАшдуы <= ЬфчЗкшмфеуАшдуы)
//        {
//            if (ПутАшдуы == true)
//            {
//                FileSystemGenerator();
//            }
//        }

//        if (ЗгидшсСщгте == ЬфчЗгидшсАшдуы && ЗкшмфеуСщгте == ЬфчЗкшмфеуАшдуы)
//        {
//            ПутАшдуы = false;
//        }
//    }

//    void FileCheck()
//    {
//        ЗфпуАшду.RemoveRange(0, ЗфпуАшду.Count);

//        for (int i = 0; i < GameControl.control.WebsiteFiles.Count; i++)
//        {
//            if (GameControl.control.WebsiteFiles[i].Location == ши.CurrentLocation)
//            {
//                ЗфпуАшду.Add(GameControl.control.WebsiteFiles[i]);
//            }
//        }
//    }

//    void PasswordSetup()
//    {
//        if (цы.SecLevel > 3)
//        {
//            ЫшеуФвьштЗфыы = GetRandomString(8, 8);
//        }
//        else
//        {
//            int Index = Random.Range(0, зд.Words1.Count);
//            ЫшеуФвьштЗфыы = зд.Words1[Index];
//        }
//    }

//    public void FileSystemGenerator()
//    {
//        for (int i = 0; i < 1; i++)
//        {
//            string FileName = GetRandomString(4, 4);
//            float FileSize = Random.Range(1, 10);
//            float FileTypePick = Random.Range(1, 10);
//            if (FileTypePick <= 5)
//            {
//                if (ЗгидшсСщгте <= ЬфчЗгидшсАшдуы)
//                {
//                    GameControl.control.WebsiteFiles.Add(new ProgramSystem(FileName, "", "", "", "Para Public", "", 0, 0, FileSize, 0, 100, 0, false, ProgramSystem.ProgramType.File));
//                    ЗгидшсСщгте++;
//                }
//            }
//            if (FileTypePick > 5)
//            {
//                if (ЗкшмфеуСщгте <= ЬфчЗкшмфеуАшдуы)
//                {
//                    GameControl.control.WebsiteFiles.Add(new ProgramSystem(FileName, "", "", "", "Para Private", "", 0, 0, FileSize, 0, 100, 0, false, ProgramSystem.ProgramType.File));
//                    ЗкшмфеуСщгте++;
//                }
//            }
//            //GameControl.control.becassystemsFileSystem.Sort();
//        }
//    }

//    public void Documents()
//    {
//        УьфшдЫгиоусе.Add("Secuirty Loophole");
//        УьфшдЫгиоусе.Add("Test 1");
//        УьфшдЫгиоусе.Add("Test 2");
//        ТщеуЕшеду.Add("Important Note");
//    }

//    public void Logs()
//    {

//    }

//    public void RenderSite()
//    {
//        GUI.backgroundColor = игеещтСщдщк;
//        GUI.contentColor = ащтеСЩдщк;

//        switch (ши.AddressBar)
//        {
//            case "www.para.com":
//                if (GUI.Button(new Rect(10, 75, 100, 20), "Public"))
//                {
//                    ши.AddressBar = "www.para.com/public";
//                    ши.showAddressBar = false;
//                    ши.ClearDirContents();
//                    ши.DirContents.Add("Home");
//                    ши.DirContents.Add("Files");
//                }
//                if (GUI.Button(new Rect(10, 120, 100, 20), "Sign in"))
//                {
//                    ши.AddressBar = "www.para.com/login";
//                    ши.showAddressBar = false;
//                    ши.ClearDirContents();
//                }
//                if (ши.Request == true)
//                {
//                    ши.ClearDirContents();
//                    сдшс.PastCommands.Add("www.para.com/public");
//                    сдшс.PastCommands.Add("www.para.com/login");
//                    ши.Request = false;
//                }
//                break;

//            case "www.para.com/public":
//                if (GUI.Button(new Rect(10, 75, 100, 20), "Home"))
//                {
//                    ши.ClearDirContents();
//                    екфсу.UpdateTimer = false;
//                    ши.showAddressBar = true;
//                    дщппув = false;
//                    ГыкТфьу = "";
//                    зфыыцщкв = "";
//                    //sm.BounceIPs.Remove(sm.JaildewIP);
//                    //sm.BouncedConnections.Remove(sm.JaildewPos);
//                    ши.AddressBar = "www.para.com";
//                }

//                if (GUI.Button(new Rect(10, 100, 100, 20), "Temp Files"))
//                {
//                    ши.ClearDirContents();
//                    ши.AddressBar = "www.para.com/tempfiles";
//                    //ib.DirContents.Add(GameControl.control.becassystemsPublicFileSize[scrollsize].ToString("F0"));
//                }
//                if (ши.Request == true)
//                {
//                    ши.ClearDirContents();
//                    сдшс.PastCommands.Add("www.para.com");
//                    сдшс.PastCommands.Add("www.para.com/tempfiles");
//                    ши.Request = false;
//                }
//                break;

//            case "www.para.com/tempfiles":
//                if (GUI.Button(new Rect(245, 30, 50, 20), "Back"))
//                {
//                    ши.ClearDirContents();
//                    ши.AddressBar = "www.para.com/public";
//                }

//                FileCheck();

//                ши.CurrentLocation = "Para Public";

//                if (ши.Request == true)
//                {
//                    int FileCount;
//                    for (FileCount = 0; FileCount < GameControl.control.WebsiteFiles.Count; FileCount++)
//                    {
//                        //ib.DirContents.Add (GameControl.control.becassystemsPublicFileSystem [FileCount].Name);
//                        if (GameControl.control.WebsiteFiles[FileCount].Location == ши.CurrentLocation)
//                        {
//                            сдшс.PastCommands.Add(GameControl.control.WebsiteFiles[FileCount].Name);
//                        }
//                    }

//                    if (FileCount >= GameControl.control.WebsiteFiles.Count)
//                    {
//                        ши.Request = false;
//                    }
//                }

//                GUI.Label(new Rect(115, 50, 500, 500), "File Name");
//                GUI.Label(new Rect(200, 50, 500, 500), "Size");

//                if (ырщцЬутг == true)
//                {
//                    if (GUI.Button(new Rect(10, 105, 100, 20), "Delete " + ЗфпуАшду[Ыудусе].Name))
//                    {
//                        сдшс.CommandLine = "-r▓rm▓" + ЗфпуАшду[Ыудусе].Name;
//                        сдшс.CheckInput();
//                        сдшс.CommandLine = "";
//                        Ыудусе = -1;
//                        ырщцЬутг = false;
//                    }
//                    if (GUI.Button(new Rect(10, 145, 100, 20), "Download " + ЗфпуАшду[Ыудусе].Name))
//                    {
//                        сдшс.CommandLine = "dl▓" + ЗфпуАшду[Ыудусе].Name;
//                        сдшс.CheckInput();
//                        сдшс.CommandLine = "";
//                        Ыудусе = -1;
//                        ырщцЬутг = false;
//                    }
//                }
//                ыскщддзщы = GUI.BeginScrollView(new Rect(130, 75, 150, 100), ыскщддзщы, new Rect(0, 0, 0, ыскщддышяу * 20));

//                if (ыскщддышяу > ЗфпуАшду.Count)
//                {
//                    ыскщддышяу = 0;
//                }

//                if (ЗфпуАшду.Count > 0)
//                {
//                    for (ыскщддышяу = 0; ыскщддышяу < ЗфпуАшду.Count; ыскщддышяу++)
//                    {
//                        if (ЗфпуАшду[ыскщддышяу].Location == "Para Public")
//                        {
//                            if (GUI.Button(new Rect(3, ыскщддышяу * 20, 80, 20), "" + ЗфпуАшду[ыскщддышяу].Name))
//                            {
//                                ырщцЬутг = true;
//                                Ыудусе = ыскщддышяу;
//                            }
//                            GUI.Button(new Rect(85, ыскщддышяу * 20, 40, 20), "" + ЗфпуАшду[ыскщддышяу].Used);
//                        }
//                    }

//                }

//                GUI.EndScrollView();

//                break;

//            case "www.para.com/filesystem":
//                if (дщппув == true)
//                {
//                    FileCheck();
//                    if (GUI.Button(new Rect(5, 55, 100, 20), "Back"))
//                    {
//                        ши.AddressBar = "www.para.com/internal";
//                    }

//                    ши.CurrentLocation = "Para Private";

//                    if (ши.Request == true)
//                    {
//                        int FileCount;
//                        //string FileLocation = "becassystems Private";
//                        for (FileCount = 0; FileCount < ЗфпуАшду.Count; FileCount++)
//                        {
//                            //ib.DirContents.Add (GameControl.control.becassystemsPublicFileSystem [FileCount].Name);
//                            if (ЗфпуАшду[FileCount].Location == ши.CurrentLocation)
//                            {
//                                сдшс.PastCommands.Add(ЗфпуАшду[FileCount].Name);
//                            }
//                        }

//                        if (FileCount >= ЗфпуАшду.Count)
//                        {
//                            ши.Request = false;
//                        }
//                    }

//                    GUI.Label(new Rect(115, 50, 500, 500), "File Name");
//                    GUI.Label(new Rect(200, 50, 500, 500), "Size");

//                    if (ырщцЬутг == true)
//                    {
//                        //					if(GUI.Button(new Rect(10, 105, 100, 20), "Delete " + GameControl.control.JaildewPublicFileName[Select].Name))
//                        //					{
//                        //
//                        //					}
//                        //					if(GUI.Button(new Rect(10,145,100,20),"Copy " + GameControl.control.JaildewPublicFileName[Select].Name))
//                        //					{
//                        //
//                        //					}
//                    }
//                    ыскщддзщы = GUI.BeginScrollView(new Rect(130, 75, 150, 100), ыскщддзщы, new Rect(0, 0, 0, ыскщддышяу * 20));
//                    for (ыскщддышяу = 0; ыскщддышяу < ЗфпуАшду.Count; ыскщддышяу++)
//                    {
//                        if (GUI.Button(new Rect(3, ыскщддышяу * 20, 80, 20), "" + ЗфпуАшду[ыскщддышяу].Name))
//                        {
//                            ырщцЬутг = true;
//                            Ыудусе = ыскщддышяу;
//                        }
//                        GUI.Button(new Rect(85, ыскщддышяу * 20, 40, 20), "" + ЗфпуАшду[ыскщддышяу].Used);
//                    }
//                    GUI.EndScrollView();
//                }
//                break;

//            case "www.para.com/login":

//                if (ГыкТфьу == "Admin")
//                {
//                    ши.Username = ГыкТфьу;
//                }

//                ГыкТфьу = GUI.TextField(new Rect(85, 55, 120, 20), ГыкТфьу, 500);
//                зфыыцщкв = GUI.TextField(new Rect(85, 75, 120, 20), зфыыцщкв, 500);

//                GUI.Label(new Rect(3, 55, 500, 500), "User Name: ");
//                GUI.Label(new Rect(3, 75, 500, 500), "Password: ");
//                ши.showAddressBar = false;

//                if (зкщп.Running == false)
//                {
//                    if (GUI.Button(new Rect(245, 30, 50, 20), "Back"))
//                    {
//                        екфсу.UpdateTimer = false;
//                        ши.showAddressBar = true;
//                        дщппув = false;
//                        ГыкТфьу = "";
//                        зфыыцщкв = "";
//                        //sm.BounceIPs.Remove(sm.JaildewIP);
//                        //sm.BouncedConnections.Remove(sm.JaildewPos);
//                        ши.AddressBar = "www.para.com";
//                    }
//                }

//                if (ГыкТфьу == "Admin" && зфыыцщкв == ЫшеуФвьштЗфыы)
//                {
//                    if (GUI.Button(new Rect(10, 125, 100, 20), "Login"))
//                    {
//                        ши.showAddressBar = false;
//                        дщппув = true;
//                        ши.AddressBar = "www.para.com/internal";
//                        екфсу.UpdateTimer = true;
//                        //log.log.Add(GameControl.control.fullip);
//                    }
//                }
//                break;

//            case "www.para.com/documents/emails":
//                if (дщппув == true)
//                {
//                    ыскщддзщы = GUI.BeginScrollView(new Rect(115, 75, 125, 100), ыскщддзщы, new Rect(0, 0, 0, ыскщддышяу * 20));
//                    for (ыскщддышяу = 0; ыскщддышяу < УьфшдЫгиоусе.Count; ыскщддышяу++)
//                    {
//                        if (GUI.Button(new Rect(3, ыскщддышяу * 20, 120, 20), "" + УьфшдЫгиоусе[ыскщддышяу]))
//                        {
//                            ек.show = true;
//                            ек.Title = УьфшдЫгиоусе[ыскщддышяу];
//                        }
//                    }
//                    GUI.EndScrollView();

//                    if (GUI.Button(new Rect(245, 30, 50, 20), "Back"))
//                    {
//                        ши.AddressBar = "www.para.com/internal";
//                    }
//                }
//                break;

//            case "www.para.com/documents":
//                if (дщппув == true)
//                {
//                    if (GUI.Button(new Rect(10, 75, 100, 20), "Emails"))
//                    {
//                        ши.AddressBar = "www.para.com/documents/emails";
//                    }
//                    if (GUI.Button(new Rect(10, 100, 100, 20), "Notes"))
//                    {
//                        ши.AddressBar = "www.para.com/documents/notes";
//                    }
//                    if (GUI.Button(new Rect(10, 150, 100, 20), "Back"))
//                    {
//                        ши.AddressBar = "www.para.com/internal";
//                    }
//                }
//                break;

//            case "www.para.com/internal":
//                if (дщппув == true)
//                {
//                    if (GUI.Button(new Rect(10, 75, 100, 20), "File System"))
//                    {
//                        ши.AddressBar = "www.para.com/filesystem";
//                    }
//                    if (GUI.Button(new Rect(10, 100, 100, 20), "Documents"))
//                    {
//                        ши.AddressBar = "www.para.com/documents";
//                    }
//                    if (GUI.Button(new Rect(10, 125, 100, 20), "Logs"))
//                    {
//                        ши.AddressBar = "www.para.com/logs";
//                    }
//                    if (GUI.Button(new Rect(10, 150, 100, 20), "Sign Out"))
//                    {
//                        екфсу.stopping = true;
//                        ши.Username = "";
//                        ши.showAddressBar = true;
//                        дщппув = false;
//                        ГыкТфьу = "";
//                        зфыыцщкв = "";
//                        PasswordSetup();
//                        ыь.Disconnect();
//                        ши.AddressBar = "www.para.com";
//                    }
//                }
//                break;
//        }
//    }
//}