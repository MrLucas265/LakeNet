using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;
using System.Security.Cryptography;
using System.Linq;
using System.Linq.Expressions;

public class TestCode 
{
    public static void KeywordCheck(string PersonsName,int PID, string Parse)
    {
        //ParseArray = Regex.Split(Parse, TerminalCommandCharacterSplit, RegexOptions.IgnoreCase);
        //ParseArray = Regex.Split(Parse, "-", RegexOptions.IgnoreCase);


        //TODO: Add a way to make vars exist and calling them by using the $ char

        string[] ParseArray;
        string[] ParseValue;

        ParseArray = Parse.Split(';');

        for (int i = 0; i < ParseArray.Length; i++)
        {
            if (ParseArray[i].Contains("Title"))
            {
                ParseValue = ParseArray[i].Split(':');
                Registry.SetStringData(PersonsName, "Core", "RunProgram", ParseValue[1]);
            }
        }
    }
    
    public static void KeywordCheck(string PersonsName,string Parse)
    {
		//ParseArray = Regex.Split(Parse, TerminalCommandCharacterSplit, RegexOptions.IgnoreCase);
		//ParseArray = Regex.Split(Parse, "-", RegexOptions.IgnoreCase);


		//TODO: Add a way to make vars exist and calling them by using the $ char

	 string[] ParseArray;
	 string[] ParseValue;

	ParseArray = Parse.Split(';');

		for(int i = 0; i < ParseArray.Length; i++)
        {
			if (ParseArray[i].Contains("Title"))
			{
				ParseValue = ParseArray[i].Split(':');
				Registry.SetStringData(PersonsName, "Core", "RunProgram", ParseValue[1]);
			}
            if (ParseArray[i].Contains("SaveAs"))
			{
				ParseValue = ParseArray[i].Split('?');
				var Type = ProgramSystemv2.FileExtension.Null;
				var Type1 = FileUtilitySystem.ProgramType.Null;
				switch(ParseValue[3])
				{
                    case "Txt":
                        Type = ProgramSystemv2.FileExtension.txt;
                        Type1 = FileUtilitySystem.ProgramType.Save;
                        break;
                    case "Code":
                        Type = ProgramSystemv2.FileExtension.scl;
                        Type1 = FileUtilitySystem.ProgramType.Save;
                        break;
                }
                Registry.SetStringData(PersonsName, "Core", "Action", ParseValue[0]);
				Registry.SetFMSData(PersonsName, "Core", "Action", new FileUtilitySystem(ParseValue[1], ParseValue[2],Type1,new ProgramSystemv2(ParseValue[1], "", "", "", ParseValue[4], "", ParseValue[2], "", "", "",
                    Type, ProgramSystemv2.FileExtension.Null, 0, 0, 200, 0, 100, 0, 0, 0, 0, 0, 0, 0, 0,
					false, false, false, false, false, false, false, new ResourceManagerSystem())));
            }
			if(ParseArray[i].Contains("Quit"))
			{
                ParseValue = ParseArray[i].Split(':');

                Registry.SetStringData(PersonsName, "Core", "Action", ParseValue[0] + ":" + ParseValue[1]);
            }
            if (ParseArray[i].Contains("Show") || ParseArray[i].Contains("Hide"))
            {
                ParseValue = ParseArray[i].Split(':');

                Registry.SetStringData(PersonsName, "Core", "Action", "Show" + ":" + ParseValue[1]);
            }
            if (ParseArray[i].Contains("Window Color"))
			{
				ParseValue = ParseArray[i].Split(':');

				byte ByteRed = DataConverter.StringToFloatToByte(ParseValue[1]);
				byte ByteGreen = DataConverter.StringToFloatToByte(ParseValue[2]);
				byte ByteBlue = DataConverter.StringToFloatToByte(ParseValue[3]);
				byte ByteAlpha = DataConverter.StringToFloatToByte(ParseValue[4]);

				Registry.SetColorData(PersonsName, "System", "WindowColor",new SColor(new Color32(ByteRed, ByteGreen, ByteBlue, ByteAlpha)));
				Registry.SetBoolData(PersonsName, "System", "WindowColor", true);
			}
            if (ParseArray[i].Contains("Desktop Background Color"))
			{
				ParseValue = ParseArray[i].Split(':');

				byte ByteRed = DataConverter.StringToFloatToByte(ParseValue[1]);
				byte ByteGreen = DataConverter.StringToFloatToByte(ParseValue[2]);
				byte ByteBlue = DataConverter.StringToFloatToByte(ParseValue[3]);
				byte ByteAlpha = DataConverter.StringToFloatToByte(ParseValue[4]);

				Registry.SetColorData(PersonsName, "System", "DesktopBackgroundColor", new SColor(new Color32(ByteRed, ByteGreen, ByteBlue, ByteAlpha)));
				Registry.SetBoolData(PersonsName, "System", "DesktopBackgroundColor", true);
			}
			if (ParseArray[i].Contains("Font Color"))
			{
				ParseValue = ParseArray[i].Split(':');

				byte ByteRed = DataConverter.StringToFloatToByte(ParseValue[1]);
				byte ByteGreen = DataConverter.StringToFloatToByte(ParseValue[2]);
				byte ByteBlue = DataConverter.StringToFloatToByte(ParseValue[3]);
				byte ByteAlpha = DataConverter.StringToFloatToByte(ParseValue[4]);

				Registry.SetColorData(PersonsName, "System", "FontColor", new SColor(new Color32(ByteRed, ByteGreen, ByteBlue, ByteAlpha)));
				Registry.SetBoolData(PersonsName, "System", "FontColor", true);
			}
            if (ParseArray[i].Contains("Button Color"))
			{
				ParseValue = ParseArray[i].Split(':');

				byte ByteRed = DataConverter.StringToFloatToByte(ParseValue[1]);
				byte ByteGreen = DataConverter.StringToFloatToByte(ParseValue[2]);
				byte ByteBlue = DataConverter.StringToFloatToByte(ParseValue[3]);
				byte ByteAlpha = DataConverter.StringToFloatToByte(ParseValue[4]);

				Registry.SetColorData(PersonsName, "System", "ButtonColor", new SColor(new Color32(ByteRed, ByteGreen, ByteBlue, ByteAlpha)));
				Registry.SetBoolData(PersonsName, "System", "ButtonColor", true);
			}
			if (ParseArray[i].Contains("Window Width"))
			{
				ParseValue = ParseArray[i].Split(':');
				Registry.SetWidthData(PersonsName, "Core", "RunProgram", DataConverter.StringToFloat(ParseValue[1]));

			}
			if (ParseArray[i].Contains("Window Height"))
			{
				ParseValue = ParseArray[i].Split(':');
				Registry.SetHeightData(PersonsName, "Core", "RunProgram", DataConverter.StringToFloat(ParseValue[1]));
			}
			if (ParseArray[i].Contains("Run"))
			{
				ParseValue = ParseArray[i].Split(':');
				Registry.SetRequestData(PersonsName, "Core", "RunProgram", new ProgramRequest(ParseValue[1], PersonsName));
				Registry.SetBoolData(PersonsName, "Core", "RunProgram", true);
				ParseArray[i] = ""; 
			}
            if (ParseArray[i].Contains("New Directory"))
            {
                ParseValue = ParseArray[i].Split(':');

                string DirName = ParseValue[1];
                string DirLocation = ParseValue[2];
                string DirTarget = DirLocation + DirName;

                FileSystemFunctions.AddFile(PersonsName, new ProgramSystemv2(DirName, DirLocation, DirTarget, ProgramSystemv2.FileExtension.dir));
            }
            if (ParseArray[i].Contains("Copy"))
            {
                ParseValue = ParseArray[i].Split(':');

				//Registry.AddProgramData(PersonsName, "System", "Clipboard",);
                ParseArray[i] = "";
            }
            if (ParseArray[i].Contains("SetBackground"))
			{
				ParseValue = ParseArray[i].Split('|');
				Registry.SetStringData(PersonsName, "ControlPanel", "BackgroundField", ParseValue[1]);
				ParseArray[i] = "";
			}
            if (ParseArray[i].Contains("Aspect"))
            {
                ParseValue = ParseArray[i].Split(':');
                if (ParseValue[1] == "Fit")
                {
                    Registry.SetStringData("Player", "System", "Aspect", "Fit");
                }
                if (ParseValue[1] == "Scale")
                {
                    Registry.SetStringData("Player", "System", "Aspect", "Scale");
                }
                if (ParseValue[1] == "Default")
                {
                    Registry.SetStringData("Player", "System", "Aspect", "Default");
                }
                ParseArray[i] = "";
            }
            if (ParseArray[i].Contains("ToggleCustomBackground"))
            {
                ParseValue = ParseArray[i].Split(':');
                if (ParseValue[1] == "")
                {
                    Registry.SetBoolData("Player", "System", "SelectedBackground", !Registry.GetBoolData("Player", "System", "SelectedBackground"));
                }
                if (ParseValue[1] == "true")
                {
                    Registry.SetBoolData("Player", "System", "SelectedBackground", false);
                }
                if (ParseValue[1] == "false")
                {
                    Registry.SetBoolData("Player", "System", "SelectedBackground", true);
                }
                ParseArray[i] = "";
            }

            if (ParseArray[i].Contains("Connect"))
			{
				ParseValue = ParseArray[i].Split(':');
				Registry.SetRemote(PersonsName, "Network", "Remote",new NetworkSystem(ParseValue[1],"connect"));
				ParseArray[i] = "";
			}
			if (ParseArray[i].Contains("Download"))
			{
				ParseValue = ParseArray[i].Split(':');
				Registry.SetRemote(PersonsName, "Network", "Remote", new NetworkSystem(ParseValue[1], "download"));
				ParseArray[i] = "";
			}
			if (ParseArray[i].Contains("Disconnect"))
			{
				ParseValue = ParseArray[i].Split(':');
				Registry.SetRemote(PersonsName, "Network", "Remote", new NetworkSystem(ParseValue[1], "disconnect"));
				ParseArray[i] = "";
			}
		}
	}
}
