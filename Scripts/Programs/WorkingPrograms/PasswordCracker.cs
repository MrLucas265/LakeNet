using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PasswordCracker : MonoBehaviour 
{
	public bool show;
	public int windowID;
	public Rect windowRect;
	public float native_width = 1920;
	public float native_height = 1080;

	private Defalt defalt;
	private WebSec ws;
	private ErrorProm ep;
	private InternetBrowser ib;
	private Tracer trace;
	private Computer com;
	private SoundControl sc;

	private GameObject Hardware;
	private GameObject Prompts;
	private GameObject SysSoftware;
	private GameObject AppSoftware;
	private GameObject HackingSoftware;

	public string glyphs;

	public int DictionaryVersion;

	public string InputtedText;
	public string SerialKey;
	public string MatchedKey;
	public string MatchedKey1;
	public bool RunKeyGen;
	public float Timer;
	public int Count;
	public int SelectedCharacter;
	public float SelectedTime;
	public float ScoredTime;

    public string SelectedAccount;
    public string SelectedAccountPassword;

    public bool HasUAC;
    public bool HasIDS;

    // Progtive is the one at a time sequential cracker
    // Use this for initialization
    void Start () 
	{
		Hardware = GameObject.Find("Hardware");
		Prompts = GameObject.Find("Prompts");
		SysSoftware = GameObject.Find("System");
		HackingSoftware = GameObject.Find("Hacking");
		AppSoftware = GameObject.Find("Applications");

		ep = Prompts.GetComponent<ErrorProm>();
		com = SysSoftware.GetComponent<Computer>();
		trace = HackingSoftware.GetComponent<Tracer>();
		defalt = SysSoftware.GetComponent<Defalt>(); 
		ib = AppSoftware.GetComponent<InternetBrowser>(); 
		ws = AppSoftware.GetComponent<WebSec>();
		sc = SysSoftware.GetComponent<SoundControl>();
		windowRect.x = Customize.cust.windowx[windowID];
		windowRect.y = Customize.cust.windowy[windowID];
		native_height = Customize.cust.native_height;
		native_width = Customize.cust.native_width;

		DictionaryVersion = 1;


		windowRect.height = 130;
	}

	void OnGUI()
	{
		Customize.cust.windowx[windowID] = windowRect.x;
		Customize.cust.windowy[windowID] = windowRect.y;
		GUI.skin = com.Skin[GameControl.control.GUIID];

		if(show == true)
		{
			GUI.color = com.colors[Customize.cust.WindowColorInt];
			windowRect = WindowClamp.ClampToScreen(GUI.Window(windowID,windowRect,DoMyWindow,""));
		}
	}

	void DoMyWindow(int WindowID)
	{
		GUI.backgroundColor = com.colors[Customize.cust.ButtonColorInt];
		GUI.contentColor = com.colors[Customize.cust.FontColorInt];

		GUI.TextField(new Rect(5, 30, 170, 20), ib.Username, 500);
		GUI.TextField(new Rect(5, 55, 170, 20), InputtedText, 500);
		GUI.TextField(new Rect(5, 80, 170, 20), MatchedKey, 500);

		GUI.DragWindow(new Rect(5, 5, 170, 21));
		GUI.Box(new Rect(5, 5, 170, 21), "Password Breaker");

		if (GUI.Button(new Rect(175, 5, 21, 21), "X"))
		{
			show = false;
		}

		if (GUI.Button(new Rect(5, 105, 50, 21), "RUN"))
		{
            for (int a = 0; a < ib.CurrentAccounts.Count; a++)
            {
                if (ib.CurrentAccounts[a].UserName == ib.Username)
                {
                    SelectedAccountPassword = ib.CurrentAccounts[a].Password;
                }
            }
            Inital();
			Dictionary();
		}

		if (RunKeyGen == true && SerialKey != "") 
		{
			Run();
		}

		if (ib.Username == "") 
		{
			InputtedText = "";
		}
	}

	void Hacking()
	{
		Timer += Time.deltaTime;
		ScoredTime += Time.deltaTime/2;

		if (Timer > SelectedTime) 
		{
			
			MatchedKey = GetRandomString(SerialKey.Length,SerialKey.Length);
			//InputtedText = GetRandomString(SerialKey.Length, SerialKey.Length);
			Timer = 0;

			if (MatchedKey [SelectedCharacter] == SerialKey [SelectedCharacter])
			{
				sc.SoundSelect = 5;
				sc.PlaySound();
				InputtedText += MatchedKey [SelectedCharacter];
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

	public string GetRandomString(int min, int max)
	{
		int charAmount = Random.Range(min, max); //set those to the minimum and maximum length of your string
		string retMe = "";
		for(int i=0; i<charAmount; i++)
		{
			retMe += glyphs[Random.Range(0, glyphs.Length)];
		}
		return retMe;
	}

	void Dictionary()
	{
		switch (DictionaryVersion) 
		{
		case 1:
			glyphs = "abcdefghijklmnopqrstuvwxyz";
			break;
        case 2:
            glyphs = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
            break;
        case 3:
			glyphs = "abcdefghijklmnopqrstuvwxyz1234567890";
			break;
		case 4:
			glyphs = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz1234567890";
			break;
		case 5:
			glyphs = "!@#$%^&*()_+-=<>?:{}[]';/.,ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz1234567890";
			break;
        case 6:
            glyphs = "!@#$%^&*()_+-=<>?:{}[]';/.,ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
            break;
        }
	}

    void Inital()
    {
        SelectedTime = 0.05f;
        InputtedText = "";
        MatchedKey = "";
        SerialKey = SelectedAccountPassword;
        SelectedCharacter = 0;
        RunKeyGen = true;
        ScoredTime = 0;
        SelectedTime = SelectedTime * SerialKey.Length;
    }
    public void Test()
    {
        for(int i = 0; i < ib.CurrentSecurity.Count; i++)
        {
            if(ib.CurrentSecurity[i].Type == WebSecSystem.SecType.UAC)
            {
                HasUAC = true;
            }

            if (ib.CurrentSecurity[i].Type == WebSecSystem.SecType.IDS)
            {
                HasIDS = true;
            }
        }

        if (HasUAC == true)
        {
            for (int a = 0; a < ib.CurrentAccounts.Count; a++)
            {
                if (ib.CurrentAccounts[a].UserName == ib.Username)
                {
                    if (HasIDS)
                    {
                        trace.UpdateTimer = true;
                        Hacking();
                    }
                    else
                    {
                        Hacking();
                    }
                }
                else
                {
                    //RunKeyGen = false;
                }
            }
        }
    }

	public void Run()
	{
        Test();
	}

}
