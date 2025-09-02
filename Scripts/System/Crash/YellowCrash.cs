using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class YellowCrash : MonoBehaviour
{
	public string StopCodeNumber;
	public string StopCodeWord;
	public string CodeDetail;
	public string ExtraDetail;
	public float Timer;
	public Rect windowRect;
	public int windowID;
	public GUISkin crashskin;

	public Color32 Color1 = new Color32(0, 0, 0, 0);
	public Color32 Color2 = new Color32(0, 0, 0, 0);

	public string Text;

	// Use this for initialization
	void Start()
	{
		windowRect.width = Screen.width;
		windowRect.height = Screen.height;
		LoadPresetColors();

		Text = "The Gateway System has detected a fault and ChamoSYS has been shut down to prevent damage to the stream: " + "\n" +
		"\n" +
		"The problem seems to be caused by the following: Dragon_Name.sys" + "\n" +
		"\n" +
		"If this is the first time you seen this stop error screen," + "\n" +
		"dont worry the system will automatially restart and " + "\n" +
		"reset to default assigned values." + "\n" +
		"\n" +
		"Check to make sure any new hardware or software is propley installed." + "\n" +
		"If this is a new installation, ask your hardware or software manufacture for any updates." + "\n" +
		"\n" +
		"If problems continue, disable or remove any newly named files,installed hardware or software." + "\n" +
		"Disable BIOS Memory options such as caching or batching." + "\n" +
		"If you need to use SafeMode to remove or disable system compoents." + "\n" +
		"let the OS restart then press 8 then select Kernal - Sanders when displayed" + "\n" +
		"\n" +
		"Technical Information" + "\n" +
		"\n" +
		"STOP: " + "0x54686520 0x64726167 0x6f6e2077 0x6173206e 0x616d6564";
	}

	// Update is called once per frame
	void Update()
	{

	}

	public void Timers()
	{
		if (Timer > 0)
		{
			Timer -= 1 * Time.deltaTime;
		}

		if (Timer <= 0)
		{
			GameControl.control.GatewayStatus.Booted = false;
			GameControl.control.GatewayStatus.Terminal = false;
            SceneManager.LoadScene("Game");
		}
	}

	void LoadPresetColors()
	{
		Color1.r = 255;
		Color1.g = 255;
		Color1.b = 0;
		Color1.a = 255;

		Color2.r = 70;
		Color2.g = 239;
		Color2.b = 153;
		Color2.a = 255;
	}

	void OnGUI()
	{
		//GUI.Box (new Rect (0, 0, Screen.width, Screen.height), "");
		GUI.skin = crashskin;
		//GUI.color = Color1;
		GUI.backgroundColor = Color1;
		windowRect = GUI.Window(windowID, windowRect, DoMyWindow, "");
	}

	void DoMyWindow(int WindowID)
	{
		GUI.backgroundColor = Color.yellow;
		GUI.contentColor = Color2;
		GUI.Box(new Rect(0, 0, windowRect.width, windowRect.height), "");
		//GUI.Label(new Rect(0, 50, 500, 22), "The Gateway System has detected a fault and " + "ChamoSYS" + " has shutdown to prevent furthur damage.");
		//GUI.Label(new Rect(0, 100, 500, 22), "STOP_CODE: " + StopCodeWord);
		//GUI.Label(new Rect(0, 125, 500, 22), "Error: " + CodeDetail);
		GUI.Label(new Rect(0, 50, 500, 300), Text);

		GUI.Label(new Rect(0, windowRect.height - 24, 500, 22), "Automatic Restart in " + Timer.ToString("F0"));

		GUI.contentColor = Color.black;

		GUI.Label(new Rect(2, 50, 500, 300), Text);

		GUI.Label(new Rect(2, windowRect.height-24, 500, 22), "Automatic Restart in " + Timer.ToString("F0"));
	}
}
