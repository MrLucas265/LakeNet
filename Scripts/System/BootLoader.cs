using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;
public class BootLoader : MonoBehaviour 
{


	public int windowID;
	public Rect windowRect;
	public float native_width = 1920;
	public float native_height = 1080;
	public bool show;
	public Vector2 scrollpos = Vector2.zero;
	public int scrollsize;


	public GUISkin POSTSkin;
	public GUISkin BIOSSkin;

	public List<string> BootInfo = new List<string>();
	public List<ProgramSystem> BootableOS = new List<ProgramSystem>();

	private Boot boot;
	private DesktopEnviroment os;

	public bool ChangeOS;

	public string SelectedOS;

	public int FoundBootableDisk;
	public int Count;


	// Use this for initialization
	void Start ()
	{
		boot = GetComponent<Boot>();
		os = GetComponent<DesktopEnviroment>();

		windowRect = new Rect(0, 0, Customize.cust.RezX, Customize.cust.RezY);

		if (Application.isEditor == true) 
		{
			windowRect.width = Screen.width;
			windowRect.height = Screen.height;
		} 
		else 
		{
			windowRect.width = Customize.cust.RezX;
			windowRect.height = Customize.cust.RezY;
		}
    }

	void OnGUI()
	{
		GUI.depth = -30;
		GUI.skin = POSTSkin;
			
		Customize.cust.windowx[windowID] = windowRect.x;
		Customize.cust.windowy[windowID] = windowRect.y;

		if(show == true)
		{
			windowRect = GUI.Window(windowID,windowRect,DoMyWindow,""); 
		}
	}

	void DoMyWindow(int WindowID)
	{
		if(ChangeOS == true)
		{
			ShowBootOptions();
		}
		else
		{
			AutoBoot();
		}
	}

	void AutoBoot()
	{
		var person = PersonController.control.People.FirstOrDefault(x => x.Name == "Player");

		for(Count = 0; Count < person.Gateway.StorageDevices.Count; Count++)
		{
			for (int j = 0; j < person.Gateway.StorageDevices[Count].OS.Count; j++)
			{
				if(person.Gateway.StorageDevices[Count].OS[j].Options.Selected == true)
				{
					WipeAllSelectedOS(j, Count);
				}
			}
		}

		if(person.Gateway.CurrentOS.Options.Selected == false)
		{
			if (Count >= person.Gateway.StorageDevices.Count)
			{
				if (person.Gateway.StorageDevices.Last().OS.Count > 0)
				{
					if (person.Gateway.StorageDevices.Last().OS.Last().Options.Selected == false)
					{
						GUI.Label(new Rect(10, Screen.height - 75, 500, 20), "No preffered operating system found. Press any key to restart");
						GUI.Label(new Rect(10, Screen.height - 50, 500, 20), "You will automatically be put into the BIOS");
						GUI.Label(new Rect(10, Screen.height - 25, 500, 20), "After you exit from the BIOS press 8");

						if (Input.anyKeyDown)
						{
							person.Gateway.Status.BIOS = true;
							SceneManager.LoadScene("Game");
						}
					}
				}
				else
				{
					GUI.Label(new Rect(10, Screen.height - 75, 500, 20), "No preffered operating system found. Press any key to restart");
					GUI.Label(new Rect(10, Screen.height - 50, 500, 20), "You will automatically be put into the BIOS");
					GUI.Label(new Rect(10, Screen.height - 25, 500, 20), "After you exit from the BIOS press 8");

					if (Input.anyKeyDown)
					{
						person.Gateway.Status.BIOS = true;
						SceneManager.LoadScene("Game");
					}
				}
			}
		}
	}

	void ShowBootOptions()
	{
		var person = PersonController.control.People.FirstOrDefault(x => x.Name == "Player");

		for (int i = 0; i < person.Gateway.StorageDevices.Count; i++)
		{
			if (person.Gateway.StorageDevices[i].OS.Count > 0)
			{
				FoundBootableDisk = i;
				break;
			}
		}
		scrollpos = GUI.BeginScrollView(new Rect(100, 100, 920, 540), scrollpos, new Rect(0, 0, 0, scrollsize * 22));
		for (scrollsize = 0; scrollsize < person.Gateway.StorageDevices[FoundBootableDisk].OS.Count; scrollsize++)
		{
			if (GUI.Button(new Rect(0, scrollsize * 22, 200, 21), "" + person.Gateway.StorageDevices[FoundBootableDisk].OS[scrollsize].Name))
			{
				WipeAllSelectedOS(scrollsize, FoundBootableDisk);
			}
		}
		GUI.EndScrollView();
	}
	void WipeAllSelectedOS(int SelectedOS, int SelectedDisk)
	{
		var person = PersonController.control.People.FirstOrDefault(x => x.Name == "Player");

		for (int i = 0; i < person.Gateway.StorageDevices.Count; i++)
		{
			for (int j = 0; j < person.Gateway.StorageDevices[i].OS.Count; j++)
			{
				person.Gateway.StorageDevices[i].OS[j].Options.Selected = false;
			}

			person.Gateway.StorageDevices[SelectedDisk].OS[SelectedOS].Options.Selected = true;
			Registry.SetStringData("Player", "OS", "SerialKey", person.Gateway.StorageDevices[SelectedDisk].OS[SelectedOS].SerialKey);
		}

		this.enabled = false;
		boot.enabled = true;
	}

	void DoMyWindowB(int WindowID)
	{
		for (int i = 0; i < GameControl.control.ProgramFiles.Count; i++)
		{
			if (GameControl.control.ProgramFiles[i].Extension == ProgramSystem.FileExtension.OS)
			{
				if(!BootableOS.Contains(GameControl.control.ProgramFiles[i]))
				{
					//if(GameControl.control.ProgramFiles[i].Location != "Reserved")
					//{
					//	BootableOS.Add(GameControl.control.ProgramFiles[i]);
					//}
					BootableOS.Add(GameControl.control.ProgramFiles[i]);
				}
			}
		}

		if (ChangeOS == true)
		{
			if (BootableOS.Count <= 0)
			{
				if (GameControl.control.SelectedOS.Name == OperatingSystems.OSName.SafeMode)
				{
					boot.enabled = true;
					this.enabled = false;
					GameControl.control.GatewayStatus.Terminal = true;
					ChangeOS = false;
					show = false;
				}
				else
				{
					GUI.Label(new Rect(10, Screen.height - 25, 500, 20), "No boot device found. Press any key to restart and boot into safe mode.");

					if (Input.anyKeyDown)
					{
						GameControl.control.SelectedOS.Name = OperatingSystems.OSName.SafeMode;
						GameControl.control.GatewayStatus.Booted = false;
						GameControl.control.GatewayStatus.SafeMode = true;
						SceneManager.LoadScene("Game");
					}
				}
			}
			else
			{
				GameControl.control.GatewayStatus.Terminal = false;

				scrollpos = GUI.BeginScrollView(new Rect(100, 100, 920, 540), scrollpos, new Rect(0, 0, 0, scrollsize * 22));
				for (scrollsize = 0; scrollsize < BootableOS.Count; scrollsize++)
				{
					if (GUI.Button(new Rect(0, scrollsize * 22, 200, 21), "" + BootableOS[scrollsize].Name))
					{
						SelectedOS = BootableOS[scrollsize].Name;
					}
				}
				GUI.EndScrollView();

				for (int i = 0; i < GameControl.control.OSName.Count; i++)
				{
					if (GameControl.control.OSName[i].Title == SelectedOS)
					{
						if(SelectedOS == "Kernal-Sanders")
						{
							GameControl.control.GatewayStatus.Terminal = true;
						}
						GameControl.control.SelectedOS = GameControl.control.OSName[i];
						GameControl.control.SelectedOS.Colour = GameControl.control.OSName[i].Colour;
						ChangeOS = false;
					}
				}
			}
		}
		else
		{
			if(BootableOS.Count <= 0)
			{
				if (GameControl.control.SelectedOS.Name == OperatingSystems.OSName.SafeMode)
				{
					boot.enabled = true;
					this.enabled = false;
					GameControl.control.GatewayStatus.Terminal = true;
					ChangeOS = false;
					show = false;
				}
				else
				{
					GUI.Label(new Rect(10, Screen.height - 25, 500, 20), "No boot device found. Press any key to restart and boot into safe mode.");

					if (Input.anyKeyDown)
					{
						GameControl.control.SelectedOS.Name = OperatingSystems.OSName.SafeMode;
						GameControl.control.GatewayStatus.Booted = false;
						SceneManager.LoadScene("Game");
					}
				}
			}
			else
			{
				if (BootableOS.Count == 0)
				{
					this.enabled = false;
					boot.enabled = true;
					GameControl.control.GatewayStatus.Terminal = true;
				}
				else
				{
					this.enabled = false;
					boot.enabled = true;
				}
			}
		}
		scrollpos = GUI.BeginScrollView(new Rect(5, 5, 920, 540), scrollpos, new Rect(0, 0, 0, scrollsize * 20));
		for (scrollsize = 0; scrollsize < BootInfo.Count; scrollsize++)
		{
			GUI.Label (new Rect (10, scrollsize * 20, 300, 21), "" + BootInfo[scrollsize]);
		}
		GUI.EndScrollView();
	}
}
