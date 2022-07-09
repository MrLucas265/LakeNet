using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ConsoleCommand
{
    private List<string> _aliases = new List<string>();

    public List<string> GetAliases()
    {
        return _aliases;
    }

    public void AddAlias(string alias)
    {
        // make sure there are not duplicates
        foreach (string a in _aliases)
        {
            if (a == alias)
            {
                return;
            }
        }

        _aliases.Add(alias);
    }

    public void RemoveAlias(string alias)
    {
        _aliases.RemoveAll(x => ((string)x) == alias);
    }

    // virtual

    public virtual string GetName()
    {
        return "";
    }

    public virtual string GetDesc()
    {
        return "";
    }

    public virtual int Evaluate(string PersonsName, int PID, string KeyName, string ValueName,List<string> args)
    {
        Debug.Log("Invalid command");

        // let's pretend 0 is a default success (real programs do this)
        return 0;
    }

    public static string GetOutput(string PlayerInput)
    {
        return PlayerInput;
    }
}

public class EchoCommand : ConsoleCommand
{
    private string _name = "echo";
    private string _desc = "echoes string to console";

    public override string GetName()
    {
        return _name;
    }

    public override string GetDesc()
    {
        return _desc;
    }

    public override int Evaluate(string PersonsName, int PID, string KeyName, string ValueName,List<string> args)
    {
        string output = "";

        foreach (string phrase in args)
        {
            output += phrase;
            output += ' ';
        }

        LocalRegistry.SetStringData(PersonsName, PID, KeyName, "Output", output);

        return 0;
    }
}

public class ClearCommand : ConsoleCommand
{
    private string _name = "clear";
    private string _desc = "clears the whole console";

    public override string GetName()
    {
        return _name;
    }

    public override string GetDesc()
    {
        return _desc;
    }

    public override int Evaluate(string PersonsName, int PID, string KeyName, string ValueName, List<string> args)
    {
        LocalRegistry.RemoveAllStringData(PersonsName, PID, KeyName, "CommandHistory");

        return 0;
    }
}

public class Restart : ConsoleCommand
{
    private string _name = "restart";
    private string _desc = "color ui";

    public override string GetName()
    {
        return _name;
    }

    public override string GetDesc()
    {
        return _desc;
    }

    public override int Evaluate(string PersonsName, int PID, string KeyName, string ValueName, List<string> args)
    {
        GameObject prompt;
        prompt = GameObject.Find("Prompts");

        ShutdownProm ShutDownWindow;

        ShutDownWindow = prompt.GetComponent<ShutdownProm>();

        ShutDownWindow.Restart();

        return 0;
    }
}

public class Error : ConsoleCommand
{
    private string _name = "error";
    private string _desc = "color ui";

    public override string GetName()
    {
        return _name;
    }

    public override string GetDesc()
    {
        return _desc;
    }

    public override int Evaluate(string PersonsName, int PID, string KeyName, string ValueName, List<string> args)
    {
        ErrorProm ep;
        GameObject prompt;
        GameObject Puter;

        AppMan appman;


    prompt = GameObject.Find("Prompts");

        ep = prompt.GetComponent<ErrorProm>();
        Puter = GameObject.Find("System");

        appman = Puter.GetComponent<AppMan>();

        appman.PromptTitle = "Test";
        appman.PromptMessage = "Simple Test";
        appman.ProgramRequest("Error", "Error Prompt", "Player");

        //ep.enabled = true;
        //ep.show = true;
        //ep.playsound = true;
        //ep.ErrorTitle = "File Utility Error - 85";
        //ep.ErrorMsg = "A file with that name already exists in that directory.";
        //ep.AddNewError();

        return 0;
    }
}

public class Quit : ConsoleCommand
{
    private string _name = "quit";
    private string _desc = "color ui";

    public override string GetName()
    {
        return _name;
    }

    public override string GetDesc()
    {
        return _desc;
    }

    public override int Evaluate(string PersonsName, int PID, string KeyName, string ValueName, List<string> args)
    {
        WindowManager.QuitProgram(PersonsName,KeyName, PID);

        return 0;
    }
}

public class SysCrash : ConsoleCommand
{
    private GameObject Crash;
    private SysCrashMan SCM;

    private NotfiPrompt noti;

    private GameObject Desktops;
    private GameObject prompt;

    private string _name = "syscrash";
    private string _desc = "color ui";

    public override string GetName()
    {
        return _name;
    }

    public override string GetDesc()
    {
        return _desc;
    }

    public override int Evaluate(string PersonsName, int PID, string KeyName, string ValueName, List<string> args)
    {
        Crash = GameObject.Find("Crash");
        prompt = GameObject.Find("Prompts");
        Desktops = GameObject.Find("Desktops");

        SCM = Crash.GetComponent<SysCrashMan>();
        noti = prompt.GetComponent<NotfiPrompt>();


        noti.ForcedMusicSetting = true;
        noti.ForcedMusicOption = false;
        noti.NewNotification("Critical Error", "OS Kernal Panic", "This was done by the user through CLI.");
        SCM.StopCodeWord = "MANUAL_LAUNCH_CRASH";
        SCM.StopCodeNumber = "0xD34DD13D";
        SCM.CodeDetail = "K3RN31-94N1C-C11-U23R-D3F";
        SCM.ExtraDetail = "14M-7H3-D327R0Y3R-0F-7H12-02";
        SCM.enabled = true;
        Desktops.SetActive(false);

        return 0;
    }
}

public class Background : ConsoleCommand
{
    private string _name = "background";
    private string _desc = "color ui";

    public override string GetName()
    {
        return _name;
    }

    public override string GetDesc()
    {
        return _desc;
    }

    public override int Evaluate(string PersonsName, int PID, string KeyName, string ValueName, List<string> args)
    {
        GameObject system;
        system = GameObject.Find("System");
        SystemPanel sp;
        sp = system.GetComponent<SystemPanel>();

        GameControl.control.SelectedOS.FPC.BackgroundAddress = args[0];
        sp.ApplyBackgrounds();

        return 0;
    }
}

public class Uwu : ConsoleCommand
{
    private string _name = "uwu";
    private string _desc = "color ui";

    public override string GetName()
    {
        return _name;
    }

    public override string GetDesc()
    {
        return _desc;
    }

    public override int Evaluate(string PersonsName, int PID, string KeyName, string ValueName, List<string> args)
    {
        GameObject system;
        system = GameObject.Find("System");
        SystemPanel sp;
        Computer com;
        DesktopEnviroment os;
        com = system.GetComponent<Computer>();
        os = system.GetComponent<DesktopEnviroment>();

        Registry.SetIntData(PersonsName, "System", "SelectedGUIID", GameControl.control.GUIID);

        Registry.SetColorData(PersonsName,  "System", "SecondaryWindowColor",new SColor(new Color32(255,1,255,52)));
        Registry.SetColorData(PersonsName, "System", "SecondaryButtonColor", new SColor(new Color32(228, 156, 255, 255)));
        Registry.SetColorData(PersonsName, "System", "SecondaryFontColor", new SColor(new Color32(255, 255, 255, 255)));

        Registry.SetIntData(PersonsName, "System", "SelectedBackground", GameControl.control.SelectedOS.FPC.SelectedBackground);

        Registry.SetBoolData(PersonsName, "OS", "ColorOption", false);

        com.colors[1] = new Color32(Registry.GetRedColorData(PersonsName, "System", "SecondaryFontColor"), Registry.GetGreenColorData(PersonsName, "System", "SecondaryFontColor"), Registry.GetBlueColorData(PersonsName, "System", "SecondaryFontColor"), Registry.GetAlphaColorData(PersonsName, "System", "SecondaryFontColor"));
        com.colors[2] = new Color32(Registry.GetRedColorData(PersonsName, "System", "SecondaryButtonColor"), Registry.GetGreenColorData(PersonsName, "System", "SecondaryButtonColor"), Registry.GetBlueColorData(PersonsName, "System", "SecondaryButtonColor"), Registry.GetAlphaColorData(PersonsName, "System", "SecondaryButtonColor"));
        com.colors[3] = new Color32(Registry.GetRedColorData(PersonsName, "System", "SecondaryWindowColor"), Registry.GetGreenColorData(PersonsName, "System", "SecondaryWindowColor"), Registry.GetBlueColorData(PersonsName, "System", "SecondaryWindowColor"), Registry.GetAlphaColorData(PersonsName, "System", "SecondaryWindowColor"));

        os.pic[2] = Resources.Load<Texture2D>("DesktopBackgrounds/" + "uwu1");

        for(int i = 0; i < com.Skin.Count; i++)
        {
            if(com.Skin[i].name == "Uwu")
            {
                GameControl.control.GUIID = i;
                Customize.cust.GUIID = i;
            }
        }

        return 0;
    }
}

public class Unuwu : ConsoleCommand
{
    private string _name = "unuwu";
    private string _desc = "color ui";

    public override string GetName()
    {
        return _name;
    }

    public override string GetDesc()
    {
        return _desc;
    }

    public override int Evaluate(string PersonsName, int PID, string KeyName, string ValueName, List<string> args)
    {
        GameObject system;
        system = GameObject.Find("System");
        SystemPanel sp;
        Computer com;
        DesktopEnviroment os;
        sp = system.GetComponent<SystemPanel>();
        com = system.GetComponent<Computer>();
        os = system.GetComponent<DesktopEnviroment>();

        Registry.SetBoolData(PersonsName, "OS", "ColorOption", true);

        com.colors[1] = new Color32(Registry.GetRedColorData(PersonsName, "System", "FontColor"), Registry.GetGreenColorData(PersonsName, "System", "FontColor"), Registry.GetBlueColorData(PersonsName, "System", "FontColor"), Registry.GetAlphaColorData(PersonsName, "System", "FontColor"));
        com.colors[2] = new Color32(Registry.GetRedColorData(PersonsName, "System", "ButtonColor"), Registry.GetGreenColorData(PersonsName, "System", "ButtonColor"), Registry.GetBlueColorData(PersonsName, "System", "ButtonColor"), Registry.GetAlphaColorData(PersonsName, "System", "ButtonColor"));
        com.colors[3] = new Color32(Registry.GetRedColorData(PersonsName, "System", "WindowColor"), Registry.GetGreenColorData(PersonsName, "System", "WindowColor"), Registry.GetBlueColorData(PersonsName, "System", "WindowColor"), Registry.GetAlphaColorData(PersonsName, "System", "WindowColor"));

        os.pic[2] = os.ListOfBackgroundImages[Registry.GetIntData(PersonsName, "System", "SelectedBackground")];

        GameControl.control.GUIID = Registry.GetIntData(PersonsName, "System", "SelectedGUIID");
        Customize.cust.GUIID = Registry.GetIntData(PersonsName, "System", "SelectedGUIID");

        return 0;
    }
}

public class Run : ConsoleCommand
{
    private string _name = "run";
    private string _desc = "color ui";

    public override string GetName()
    {
        return _name;
    }

    public override string GetDesc()
    {
        return _desc;
    }

    public override int Evaluate(string PersonsName, int PID, string KeyName, string ValueName, List<string> args)
    {
        string InputCheck = "Run:";
        TestCode.KeywordCheck(InputCheck + args[0] + ";");
        return 0;
    }
}

public class ColorCommand : ConsoleCommand
{
    private string _name = "color";
    private string _desc = "color ui";

    public override string GetName()
    {
        return _name;
    }

    public override string GetDesc()
    {
        return _desc;
    }

    public override int Evaluate(string PersonsName, int PID, string KeyName, string ValueName, List<string> args)
    {
        byte red = 0;
        byte green = 0;
        byte blue = 0;
        byte alpha = 0;

        if (args[0] == "font")
        {
            if(args[1]=="red")
            {
                byte.TryParse(args[2], out red);
                Registry.SetRedColorData(PersonsName, "System", "FontColor", red);
            }
            else if(args[1] == "green")
            {
                byte.TryParse(args[2], out green);
                Registry.SetGreenColorData(PersonsName, "System", "FontColor", green);
            }
            else if (args[1] == "blue")
            {
                byte.TryParse(args[2], out blue);
                Registry.SetBlueColorData(PersonsName, "System", "FontColor", blue);
            }
            else if (args[1] == "alpha")
            {
                byte.TryParse(args[2], out alpha);
                Registry.SetAlphaColorData(PersonsName, "System", "FontColor", alpha);
            }
            else
            {
                byte.TryParse(args[1], out red);
                Registry.SetRedColorData(PersonsName, "System", "FontColor", red);
                byte.TryParse(args[2], out green);
                Registry.SetGreenColorData(PersonsName, "System", "FontColor", green);
                byte.TryParse(args[3], out blue);
                Registry.SetBlueColorData(PersonsName, "System", "FontColor", blue);
                if (args.Count >= 5)
                {
                    if (byte.TryParse(args[4], out alpha))
                    {
                        Registry.SetAlphaColorData(PersonsName, "System", "FontColor", alpha);
                    }
                    else
                    {
                        byte.TryParse("255", out alpha);
                        Registry.SetAlphaColorData(PersonsName, "System", "FontColor", alpha);
                    }
                }
                else
                {
                    byte.TryParse("255", out alpha);
                    Registry.SetAlphaColorData(PersonsName, "System", "FontColor", alpha);
                }
            }
            SetFontColor(PersonsName, Registry.GetColorData(PersonsName, "System", "FontColor"));
        }
        if (args[0] == "window")
        {
            if (args[1] == "red")
            {
                byte.TryParse(args[2], out red);
                Registry.SetRedColorData(PersonsName, "System", "WindowColor", red);
            }
            else if (args[1] == "green")
            {
                byte.TryParse(args[2], out green);
                Registry.SetGreenColorData(PersonsName, "System", "WindowColor", green);
            }
            else if (args[1] == "blue")
            {
                byte.TryParse(args[2], out blue);
                Registry.SetBlueColorData(PersonsName, "System", "WindowColor", blue);
            }
            else if (args[1] == "alpha")
            {
                byte.TryParse(args[2], out alpha);
                Registry.SetAlphaColorData(PersonsName, "System", "WindowColor", alpha);
            }
            else
            {
                byte.TryParse(args[1], out red);
                Registry.SetRedColorData(PersonsName, "System", "WindowColor", red);
                byte.TryParse(args[2], out green);
                Registry.SetGreenColorData(PersonsName, "System", "WindowColor", green);
                byte.TryParse(args[3], out blue);
                Registry.SetBlueColorData(PersonsName, "System", "WindowColor", blue);
                if (args.Count >= 5)
                {
                    if (byte.TryParse(args[4], out alpha))
                    {
                        Registry.SetAlphaColorData(PersonsName, "System", "WindowColor", alpha);
                    }
                    else
                    {
                        byte.TryParse("255", out alpha);
                        Registry.SetAlphaColorData(PersonsName, "System", "WindowColor", alpha);
                    }
                }
                else
                {
                    byte.TryParse("255", out alpha);
                    Registry.SetAlphaColorData(PersonsName, "System", "WindowColor", alpha);
                }
            }
            SetWindowColor(PersonsName, Registry.GetColorData(PersonsName, "System", "WindowColor"));
        }
        if (args[0] == "button")
        {
            if (args[1] == "red")
            {
                byte.TryParse(args[2], out red);
                Registry.SetRedColorData(PersonsName, "System", "ButtonColor", red);
            }
            else if (args[1] == "green")
            {
                byte.TryParse(args[2], out green);
                Registry.SetGreenColorData(PersonsName, "System", "ButtonColor", green);
            }
            else if (args[1] == "blue")
            {
                byte.TryParse(args[2], out blue);
                Registry.SetBlueColorData(PersonsName, "System", "ButtonColor", blue);
            }
            else if (args[1] == "alpha")
            {
                byte.TryParse(args[2], out alpha);
                Registry.SetAlphaColorData(PersonsName, "System", "ButtonColor", alpha);
            }
            else
            {
                byte.TryParse(args[1], out red);
                Registry.SetRedColorData(PersonsName, "System", "ButtonColor", red);
                byte.TryParse(args[2], out green);
                Registry.SetGreenColorData(PersonsName, "System", "ButtonColor", green);
                byte.TryParse(args[3], out blue);
                Registry.SetBlueColorData(PersonsName, "System", "ButtonColor", blue);
                if (args.Count >= 5)
                {
                    if (byte.TryParse(args[4], out alpha))
                    {
                        Registry.SetAlphaColorData(PersonsName, "System", "ButtonColor", alpha);
                    }
                    else
                    {
                        byte.TryParse("255", out alpha);
                        Registry.SetAlphaColorData(PersonsName, "System", "ButtonColor", alpha);
                    }
                }
                else
                {
                    byte.TryParse("255", out alpha);
                    Registry.SetAlphaColorData(PersonsName, "System", "ButtonColor", alpha);
                }
            }
            SetButtonColor(PersonsName, Registry.GetColorData(PersonsName, "System", "ButtonColor"));
        }
        return 0;
    }

    public void SetFontColor(string Name, SColor data)
    {
        Registry.SetRedColorData(Name, "System", "FontColor", data.r);
        Registry.SetGreenColorData(Name, "System", "FontColor", data.g);
        Registry.SetBlueColorData(Name, "System", "FontColor", data.b);
        Registry.SetAlphaColorData(Name, "System", "FontColor", data.a);
        Registry.SetBoolData(Name, "System", "FontColor", true);
    }

    public void SetButtonColor(string Name,SColor data)
    {
        Registry.SetRedColorData(Name, "System", "ButtonColor", data.r);
        Registry.SetGreenColorData(Name, "System", "ButtonColor", data.g);
        Registry.SetBlueColorData(Name, "System", "ButtonColor", data.b);
        Registry.SetAlphaColorData(Name, "System", "ButtonColor", data.a);
        Registry.SetBoolData(Name, "System", "ButtonColor", true);
    }

    public void SetWindowColor(string Name,SColor data)
    {
        Registry.SetRedColorData(Name, "System", "WindowColor", data.r);
        Registry.SetGreenColorData(Name, "System", "WindowColor", data.g);
        Registry.SetBlueColorData(Name, "System", "WindowColor", data.b);
        Registry.SetAlphaColorData(Name, "System", "WindowColor", data.a);
        Registry.SetBoolData(Name, "System", "WindowColor", true);
    }
}

class GlobalStuff
{
    public static ConsoleCommand[] gConsoleCommandObjects = {
        new EchoCommand(),
        new ClearCommand(),
        new ColorCommand(),
        new SysCrash(),
        new Error(),
        new Restart(),
        new Background(),
        new Quit(),
        new Uwu(),
        new Unuwu(),
        new Run()
    };

    public static void RunCommand(string PersonsName, int PID, string KeyName, string ValueName, string commandString)
    {
        List<string> args = commandString.Split('-').ToList();

        for(int i = 0; i < args.Count;i++)
        {
            List<string> args1 = args[i].Split(' ').ToList();

            Debug.Log(args[i]);

            string command;
            int errorcode = 0;

            command = args1[0];

            // remove the command from the args list
            args1.RemoveAt(0);

            // this could be optimized by using a hash table or indexed list, but probably doesn't matter
            foreach (ConsoleCommand commandObject in gConsoleCommandObjects)
            {
                // if the current object is the command the user is trying to run, or it is aliases with the current command
                if (commandObject.GetName() == command || commandObject.GetAliases().IndexOf(command) != -1)
                {
                    // run the command

                    Debug.Log(args1);

                    errorcode = commandObject.Evaluate(PersonsName, PID, KeyName, ValueName, args1);
                    if (errorcode != 0)
                    {
                        Debug.Log("Program exited with code " + errorcode + "!");
                    }
                    break;
                }
            }
        }
    }
}