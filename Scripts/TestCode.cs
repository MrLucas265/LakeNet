using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Text.RegularExpressions;

public class TestCode 
{
	public static void KeywordCheck(string Parse)
    {
		//ParseArray = Regex.Split(Parse, TerminalCommandCharacterSplit, RegexOptions.IgnoreCase);
		//ParseArray = Regex.Split(Parse, "-", RegexOptions.IgnoreCase);

	 string[] ParseArray;
	 string[] ParseValue;
	 int ParseArrayLength;

	ParseArray = Parse.Split(';');
		ParseArrayLength = ParseArray.Length;

		for(int i = 0; i < ParseArrayLength; i++)
        {
			if (ParseArray[i].Contains("Title"))
			{
				ParseValue = ParseArray[i].Split(':');
				Registry.SetStringData("Player", "System", "RunProgram", ParseValue[1]);
			}
			if (ParseArray[i].Contains("Window Color"))
			{
				ParseValue = ParseArray[i].Split(':');

				byte ByteRed = DataConverter.StringToFloatToByte(ParseValue[1]);
				byte ByteGreen = DataConverter.StringToFloatToByte(ParseValue[2]);
				byte ByteBlue = DataConverter.StringToFloatToByte(ParseValue[3]);
				byte ByteAlpha = DataConverter.StringToFloatToByte(ParseValue[4]);

				Registry.SetColorData("Player", "System", "WindowColor",new SColor(new Color32(ByteRed, ByteGreen, ByteBlue, ByteAlpha)));
				Registry.SetBoolData("Player", "System", "WindowColor", true);
			}
			if (ParseArray[i].Contains("Font Color"))
			{
				ParseValue = ParseArray[i].Split(':');

				byte ByteRed = DataConverter.StringToFloatToByte(ParseValue[1]);
				byte ByteGreen = DataConverter.StringToFloatToByte(ParseValue[2]);
				byte ByteBlue = DataConverter.StringToFloatToByte(ParseValue[3]);
				byte ByteAlpha = DataConverter.StringToFloatToByte(ParseValue[4]);

				Registry.SetColorData("Player", "System", "FontColor", new SColor(new Color32(ByteRed, ByteGreen, ByteBlue, ByteAlpha)));
				Registry.SetBoolData("Player", "System", "FontColor", true);
			}
			if (ParseArray[i].Contains("Button Color"))
			{
				ParseValue = ParseArray[i].Split(':');

				byte ByteRed = DataConverter.StringToFloatToByte(ParseValue[1]);
				byte ByteGreen = DataConverter.StringToFloatToByte(ParseValue[2]);
				byte ByteBlue = DataConverter.StringToFloatToByte(ParseValue[3]);
				byte ByteAlpha = DataConverter.StringToFloatToByte(ParseValue[4]);

				Registry.SetColorData("Player", "System", "ButtonColor", new SColor(new Color32(ByteRed, ByteGreen, ByteBlue, ByteAlpha)));
				Registry.SetBoolData("Player", "System", "ButtonColor", true);
			}
			if (ParseArray[i].Contains("Window Width"))
			{
				ParseValue = ParseArray[i].Split(':');
				Registry.SetWidthData("Player", "System", "RunProgram", float.Parse(ParseValue[1]));
			}
			if (ParseArray[i].Contains("Window Height"))
			{
				ParseValue = ParseArray[i].Split(':');
				Registry.SetHeightData("Player", "System", "RunProgram", float.Parse(ParseValue[1]));
			}
			if (ParseArray[i].Contains("Run"))
			{
				ParseValue = ParseArray[i].Split(':');
				Registry.SetRequestData("Player", "System", "RunProgram", new ProgramRequest(ParseValue[1], "Player"));
				Registry.SetBoolData("Player", "System", "RunProgram", true);
				ParseArray[i] = ""; 
			}
			if (ParseArray[i].Contains("SetBackground"))
			{
				ParseValue = ParseArray[i].Split(':');
				Registry.SetStringData("Player", "ControlPanel", "BackgroundField", ParseValue[1]);
				ParseArray[i] = "";
			}
		}
	}
}
