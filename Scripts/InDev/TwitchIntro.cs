using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwitchIntro : MonoBehaviour 
{
	public List<string> pastlines = new List<string>();

	public string CopyLine;
	public string TypingLine;

	public float TraceTimer;
	public float TraceStartTime;

	public float TypeTimer;
	public float TypeStartTimer;

	public int CurrentLine;
	public int SelectedChar;

	public Vector2 scrollpos = Vector2.zero;
	public int scrollsize;

	public Color32 fontColor = new Color32(0,0,0,0);

	// Use this for initialization
	void Start () 
	{
		TypeStartTimer = 0.075f;

		LoadPresetColors();
	}
		
	void LoadPresetColors()
	{
		fontColor.r = 0;
		fontColor.g = 255;
		fontColor.b = 0;
		fontColor.a = 255;
	}
		
	// Update is called once per frame
	void Update () 
	{
		TypeWriter();
	}

	void OnGUI()
	{
		GUI.contentColor = fontColor;

		scrollpos = GUI.BeginScrollView(new Rect(5, 5, 920, 540), scrollpos, new Rect(0, 0, 0, scrollsize * 20));
		for (scrollsize = 0; scrollsize < pastlines.Count; scrollsize++)
		{
			//GUI.DrawTexture(new Rect(1, 1, 1, 1),FAN);
			GUI.Label (new Rect (10, scrollsize * 20, 300, 21), "" + pastlines[scrollsize]);
		}
		GUI.EndScrollView();

		GUI.Label (new Rect (10, scrollsize * 20 + 21, 300, 21), "" + TypingLine);
	}

	void Tracer()
	{

	}

	void TypeWriter()
	{
		if (TypeTimer >= 0)
		{
			TypeTimer-= Time.deltaTime;
		}
		if (TypeTimer < 0) 
		{
			if (CopyLine != "")
			{
				TypingLine += CopyLine [SelectedChar];
				SelectedChar++;
				TypeTimer = TypeStartTimer;
			}
		}
		if (TypingLine == CopyLine) 
		{
			pastlines.Add (TypingLine);
			TypingLine = "";
			CurrentLine++;
			SelectedChar = 0;
			LineSelection();
		}
	}

	void TextColor()
	{

	}

	void LineSelection()
	{
		switch (CurrentLine) 
		{
		case -1:
			CopyLine = "run^music player";
			break;
		case 0:
			CopyLine = "music player^play^intro";
			break;
		case 1:
			CopyLine = "run^fludicice tracker";
			break;
		case 2:
			CopyLine = "proxy^load";
			break;
		case 3:
			CopyLine = "proxy>loading bounced connections";
			break;
		case 4:
			CopyLine = "proxy>proxy 1 connecting";
			break;
		case 5:
			CopyLine = "proxy>proxy 1 connecting.";
			break;
		case 6:
			if (pastlines.Contains ("proxy>proxy 1 connecting"))
			{
				pastlines.Remove ("proxy>proxy 1 connecting");
			}
			CopyLine = "proxy>proxy 1 connecting..";
			break;
		case 7:
			if (pastlines.Contains ("proxy>proxy 1 connecting."))
			{
				pastlines.Remove ("proxy>proxy 1 connecting.");
			}
			CopyLine = "proxy>proxy 1 connecting...";
			break;
		case 8:
			if (pastlines.Contains ("proxy>proxy 1 connecting.."))
			{
				pastlines.Remove ("proxy>proxy 1 connecting..");
			}
			CopyLine = "proxy>proxy 1 connected";
			break;
		case 9:
			if (pastlines.Contains ("proxy>proxy 1 connecting..."))
			{
				pastlines.Remove ("proxy>proxy 1 connecting...");
			}
			CopyLine = "proxy>proxy 2 connecting";
			break;
		case 10:
			CopyLine = "proxy>proxy 2 connecting.";
			break;
		case 11:
			if (pastlines.Contains ("proxy>proxy 2 connecting"))
			{
				pastlines.Remove ("proxy>proxy 2 connecting");
			}
			CopyLine = "proxy>proxy 2 connecting..";
			break;
		case 12:
			if (pastlines.Contains ("proxy>proxy 2 connecting."))
			{
				pastlines.Remove ("proxy>proxy 2 connecting.");
			}
			CopyLine = "proxy>proxy 2 connecting...";
			break;
		case 13:
			if (pastlines.Contains ("proxy>proxy 2 connecting.."))
			{
				pastlines.Remove ("proxy>proxy 2 connecting..");
			}
			CopyLine = "proxy>proxy 2 connected";
			break;
		case 14:
			if (pastlines.Contains ("proxy>proxy 2 connecting..."))
			{
				pastlines.Remove ("proxy>proxy 2 connecting...");
			}
			CopyLine = "proxy>proxy 3 connecting";
			break;
		case 15:
			CopyLine = "proxy>proxy 3 connecting.";
			break;
		case 16:
			if (pastlines.Contains ("proxy>proxy 3 connecting"))
			{
				pastlines.Remove ("proxy>proxy 3 connecting");
			}
			CopyLine = "proxy>proxy 3 connecting..";
			break;
		case 17:
			if (pastlines.Contains ("proxy>proxy 3 connecting."))
			{
				pastlines.Remove ("proxy>proxy 3 connecting.");
			}
			CopyLine = "proxy>proxy 3 connecting...";
			break;
		case 18:
			if (pastlines.Contains ("proxy>proxy 3 connecting.."))
			{
				pastlines.Remove ("proxy>proxy 3 connecting..");
			}
			CopyLine = "proxy>proxy 3 connected";
			break;
		case 19:
			if (pastlines.Contains ("proxy>proxy 3 connecting..."))
			{
				pastlines.Remove ("proxy>proxy 3 connecting...");
			}
			CopyLine = "proxy>proxy 4 connecting";
			break;
		case 20:
			CopyLine = "proxy>proxy 4 connecting.";
			break;
		case 21:
			if (pastlines.Contains ("proxy>proxy 4 connecting"))
			{
				pastlines.Remove ("proxy>proxy 4 connecting");
			}
			CopyLine = "proxy>proxy 4 connecting..";
			break;
		case 22:
			if (pastlines.Contains ("proxy>proxy 4 connecting."))
			{
				pastlines.Remove ("proxy>proxy 4 connecting.");
			}
			CopyLine = "proxy>proxy 4 connecting...";
			break;
		case 23:
			if (pastlines.Contains ("proxy>proxy 4 connecting.."))
			{
				pastlines.Remove ("proxy>proxy 4 connecting..");
			}
			CopyLine = "proxy>proxy 4 connected";
			break;
		case 24:
			if (pastlines.Contains ("proxy>proxy 4 connecting..."))
			{
				pastlines.Remove ("proxy>proxy 4 connecting...");
			}
			CopyLine = "proxy>proxy 5 connecting";
			break;
		case 25:
			
			CopyLine = "proxy>proxy 5 connecting.";
			break;
		case 26:
			if (pastlines.Contains ("proxy>proxy 5 connecting"))
			{
				pastlines.Remove ("proxy>proxy 5 connecting");
			}
			CopyLine = "proxy>proxy 5 connecting..";
			break;
		case 27:
			if (pastlines.Contains ("proxy>proxy 5 connecting."))
			{
				pastlines.Remove ("proxy>proxy 5 connecting.");
			}
			CopyLine = "proxy>proxy 5 connecting...";
			break;
		case 28:
			if (pastlines.Contains ("proxy>proxy 5 connecting.."))
			{
				pastlines.Remove ("proxy>proxy 5 connecting..");
			}
			CopyLine = "proxy>proxy 5 connected";
			break;
		case 29:
			if (pastlines.Contains ("proxy>proxy 5 connecting..."))
			{
				pastlines.Remove ("proxy>proxy 5 connecting...");
			}
			CopyLine = "proxy>proxy 6 connecting";
			break;
		case 30:
			CopyLine = "proxy>proxy 6 connecting.";
			break;
		case 31:
			if (pastlines.Contains ("proxy>proxy 6 connecting"))
			{
				pastlines.Remove ("proxy>proxy 6 connecting");
			}
			CopyLine = "proxy>proxy 6 connecting..";
			break;
		case 32:
			if (pastlines.Contains ("proxy>proxy 6 connecting."))
			{
				pastlines.Remove ("proxy>proxy 6 connecting.");
			}
			CopyLine = "proxy>proxy 6 connecting...";
			break;
		case 33:
			if (pastlines.Contains ("proxy>proxy 6 connecting.."))
			{
				pastlines.Remove ("proxy>proxy 6 connecting..");
			}
			CopyLine = "proxy>proxy 6 connected";
			break;
		case 34:
			if (pastlines.Contains ("proxy>proxy 6 connecting..."))
			{
				pastlines.Remove ("proxy>proxy 6 connecting...");
			}
			CopyLine = "connect^www.twitch.com";
			break;
		case 35:
			CopyLine = "establishing connection";
			break;
		case 36:
			CopyLine = "establishing connection.";
			break;
		case 37:
			if (pastlines.Contains ("establishing connection"))
			{
				pastlines.Remove ("establishing connection");
			}
			CopyLine = "establishing connection..";
			break;
		case 38:
			if (pastlines.Contains ("establishing connection."))
			{
				pastlines.Remove ("establishing connection.");
			}
			CopyLine = "establishing connection...";
			break;
		case 39:
			if (pastlines.Contains ("establishing connection.."))
			{
				pastlines.Remove ("establishing connection..");
			}
			CopyLine = "establishing connection....";
			break;
		case 40:
			if (pastlines.Contains ("establishing connection..."))
			{
				pastlines.Remove ("establishing connection...");
			}
			CopyLine = "establishing connection.....";
			break;
		case 41:
			if (pastlines.Contains ("establishing connection...."))
			{
				pastlines.Remove ("establishing connection....");
			}
			CopyLine = "connection failed retrying";
			break;
		case 42:
			if (pastlines.Contains ("establishing connection....."))
			{
				pastlines.Remove ("establishing connection.....");
			}
			CopyLine = "retrying connection";
			break;
		case 43:
//			if (pastlines.Contains ("connection failed retrying"))
//			{
//				pastlines.Remove ("connection failed retrying");
//			}
			CopyLine = "retrying connection.";
			break;
		case 44:
//			if (pastlines.Contains ("retrying connection"))
//			{
//				pastlines.Remove ("retrying connection");
//			}
			CopyLine = "retrying connection..";
			break;
		case 45:
			if (pastlines.Contains ("retrying connection."))
			{
				pastlines.Remove ("retrying connection.");
			}
			CopyLine = "retrying connection...";
			break;
		case 46:
			if (pastlines.Contains ("retrying connection.."))
			{
				pastlines.Remove ("retrying connection..");
			}
			CopyLine = "retrying connection....";
			break;
		case 47:
			if (pastlines.Contains ("retrying connection..."))
			{
				pastlines.Remove ("retrying connection...");
			}
			CopyLine = "retrying connection.....";
			break;
		case 48:
			if (pastlines.Contains ("retrying connection...."))
			{
				pastlines.Remove ("retrying connection....");
			}
			CopyLine = "connection established to twitch servers";
			break;
		case 49:
			if (pastlines.Contains ("retrying connection....."))
			{
				pastlines.Remove ("retrying connection.....");
			}
			CopyLine = "run^wigglyflamingo.exe";
			break;
		case 50:
			CopyLine = "executing wigglyflamingo.exe";
			break;
		case 51:
			CopyLine = "executing wigglyflamingo.exe.";
			break;
		case 52:
			CopyLine = "executing wigglyflamingo.exe..";
			break;
		case 53:
			if (pastlines.Contains ("executing wigglyflamingo.exe."))
			{
				pastlines.Remove ("executing wigglyflamingo.exe.");
			}
			CopyLine = "executing wigglyflamingo.exe...";
			break;
		case 54:
			if (pastlines.Contains ("executing wigglyflamingo.exe.."))
			{
				pastlines.Remove ("executing wigglyflamingo.exe..");
			}
			CopyLine = "wigglyflamingo-running";
			break;
		case 55:
			if (pastlines.Contains ("executing wigglyflamingo.exe..."))
			{
				pastlines.Remove ("executing wigglyflamingo.exe...");
			}
			CopyLine = "login^username^FakeNetDev";
			break;
		case 56:
			CopyLine = "wigglyflamingo^crack^login";
			break;
		case 57:
			CopyLine = "wigglyflamingo>cracking User>FakeNetDev";
			break;
		case 58:
			CopyLine = "fludicice tracker>Trace has been detected";
			break;
		case 59:
			CopyLine = "fludicice tracker>5 Miniutes till trace is complete";
			break;
		case 60:
			CopyLine = "wigglyflamingo>password found";
			break;
		case 61:
			CopyLine = "wigglyflamingo>inputting matched password";
			break;
		case 62:
			CopyLine = "login^password^inputted";
			break;
		case 63:
			CopyLine = "signin";
			break;
		case 64:
			CopyLine = "welcome FakeNetDev";
			break;

		}
	}
}

//This is a test file'
//wigglyflamingo>random8chars
//wigglyflamingo>random8chars
//wigglyflamingo>random8chars
//wigglyflamingo>random8chars
//wigglyflamingo>random8chars
//wigglyflamingo>password found
//wigglyflamingo>inputting matched password
//login^password^inputted
//signin
//attempting signin
//attempting signin.
//attempting signin..
//attempting signin...
//attempting signin....
//sign in success
//welcome FakeNetDev
//modeminfo
//Modem Upload 250kb
//Modem Download 1mb
//twitch^livestream^activate
//twitch>activating livestreaming services
//twitch>checking account settings
//twitch>checking authority
//twitch>livestreaming initating
//twitch>livestreaming initating.
//twitch>livestreaming initating..
//twitch>livestreaming initating...
//twitch>livestreaming initating....
//twitch>livestreaming initated
//run^money.exe
//running money services
//giveaways has been activated
