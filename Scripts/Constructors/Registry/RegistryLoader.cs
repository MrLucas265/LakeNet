using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

class DistinctItemComparer : IEqualityComparer<RegistrySystem>
{
    public bool Equals(RegistrySystem x, RegistrySystem y)
    {
        return x.KeyName == y.KeyName &&
            x.Values == y.Values;
    }

    public int GetHashCode(RegistrySystem obj)
    {
        return obj.KeyName.GetHashCode() ^
            obj.Values.GetHashCode();
    }
}

public class RegistryLoader : MonoBehaviour
{
	public static RegistryLoader RegLoad;

	// Use this for initialization
	public bool RunRegLoad;
	public bool RunRegCheck;

	public List<RegistrySystem> DefaultRegistryKeys = new List<RegistrySystem>();

	public List<RegistrySystem> list1 = new List<RegistrySystem>();

    void Awake()
    {
		DefaultKeys();

		RegLoad = this;
	}


	
	// Update is called once per frame
	void Update ()
	{
		if(RunRegLoad == true)
		{
			CheckRegKeys();
		}
		//if(RunRegCheck == true)
  //      {
		//	CheckNullRegInfo();
  //      }
	}


	void Check2()
	{
		
	}

	void DefaultKeys()
	{
		DefaultRegistryKeys.Add(new RegistrySystem("Core"));
		DefaultRegistryKeys.Add(new RegistrySystem("CLI"));
		DefaultRegistryKeys.Add(new RegistrySystem("FileManager"));
		DefaultRegistryKeys.Add(new RegistrySystem("OS"));
		DefaultRegistryKeys.Add(new RegistrySystem("MediaPlayer"));
		DefaultRegistryKeys.Add(new RegistrySystem("System"));
		DefaultRegistryKeys.Add(new RegistrySystem("ControlPanel"));
		DefaultRegistryKeys.Add(new RegistrySystem("Network"));
		DefaultRegistryKeys.Add(new RegistrySystem("Calculator"));
        DefaultRegistryKeys.Add(new RegistrySystem("Notepad"));
        DefaultRegistryKeys.Add(new RegistrySystem("FileUtility"));
        DefaultRegistryKeys.Add(new RegistrySystem("WindowManager"));
        DefaultRegistryKeys.Add(new RegistrySystem("PlayerData"));

        Parallel.For(0, DefaultRegistryKeys.Count, i =>
		{
			switch (DefaultRegistryKeys[i].KeyName)
			{
                case "Core":
                    DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("InitalRun"));
                    DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("RunProgram"));
                    DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("Clipboard"));
                    DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("InstalledPrograms"));
                    DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("Action"));
                    break;
                case "FileManager":
					DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("InitalRun"));
					DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("TypedDirectory"));
					DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("CurrentDirectory"));
					DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("MainScrollPos"));
					DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("MainScrollSize"));
					DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("UserName"));
					DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("SelectedMenu"));
					DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("LastClick"));
					DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("SelectedFile"));
					DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("FontColor"));
					DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("WindowColor"));
					DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("ButtonColor"));
					DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("Test 1"));
					DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("PageHistory"));
					DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("WindowRect"));
					DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("Ribbon"));
					DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("RenderedRibbon"));
                    DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("SelectedButton"));
                    break;
				case "Calculator":
					DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("InitalRun"));
					DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("Result"));
					DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("Value"));
					DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("Value1"));
					DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("Pastop"));
					DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("Operator"));
					DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("OperatorPressed"));
					DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("FirstNumber"));
					DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("SecondNumber"));
					DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("ResultNumber"));
					break;
				case "CLI":
					DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("InitalRun"));
					DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("WorkingPath"));
					DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("CommandLine"));
					DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("Pinned"));
					DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("CustomPos"));
					DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("SelectedPastCommand"));
					DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("Scrollsize"));
					DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("ScrollPos"));
					DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("CommandHistory"));
					DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("TextFieldPos"));
					DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("Input"));
					DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("Output"));
					DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("Skin"));
					DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("FontColor"));
					DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("WindowColor"));
					DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("ButtonColor"));
					break;
				case "System":
                    DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("InitalRun"));
                    DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("Skin"));
                    DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("FontColor"));
					DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("WindowColor"));
					DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("ButtonColor"));
					DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("SecondaryFontColor"));
					DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("SecondaryWindowColor"));
					DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("SecondaryButtonColor"));
					DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("SelectedBackground"));
					DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("ShowDesktopIcons"));
					DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("ShowDesktopBackground"));
					DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("DesktopBackgroundColor"));
					DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("DoubleClickSpeed"));
					DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("ForcedBackground"));
					DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("UpdatePinLists"));
                    DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("Test"));
                    DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("Aspect"));
                    break;
				case "ControlPanel":
                    DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("ShowResize"));
                    DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("InitalRun"));
					DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("BackgroundField"));
					DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("BackgroundAddress"));
					DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("Window"));
                    DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("Resize"));
                    DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("TypedAddress"));
                    DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("MaxPerRowGrid"));
                    DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("RowCount"));
                    DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("ScrollRect"));
                    break;
				case "OS":
					DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("InitalRun"));
					DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("BackgroundPath"));
					DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("CursorPath"));
					DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("Secondary"));
					DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("ColorOption"));
					DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("SerialKey"));
					DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("DefaultVolumeAddress"));
					break;
				case "MediaPlayer":
					DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("InitalRun"));
					DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("Path"));
					DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("Looping"));
					DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("Loop"));
					DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("SelectedFile"));
					DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("CurrentFilePath"));
					DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("LoadVideo"));
					DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("HideUI"));
					DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("Menu"));
					DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("LoadPlaylist"));
					DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("CloseButton"));
					DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("MiniButton"));
					DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("SettingsButton"));
					DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("ListButton"));
					DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("HomeButton"));
					DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("DefaltBoxSetting"));
					DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("VideoRect"));
					DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("Volume"));
					DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("Fullscreen"));
					DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("Exists"));
					DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("Seek"));
                    DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("Startup"));


					//Scroll
                    DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("ScrollRect"));
                    DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("ScrollPos"));
                    DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("ScrollView"));
                    DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("ScrollSize"));
					//UIControlsPositions
					DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("PlayUI"));
                    DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("BackUI"));
                    DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("FowardUI"));
                    DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("StopUI"));
                    DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("LoopUI"));
                    DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("MuteUI"));
                    DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("SeekTextUI"));
                    DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("VolumeTextUI"));
                    DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("CurrentTrackUI"));
                    DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("FullscreenUI"));
                    break;
				case "Network":
					DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("InitalRun"));
					DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("Remote"));
					DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("Local"));
					break;
                case "Notepad":
                    DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("InitalRun"));
                    DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("Window"));
                    DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("TypedText"));
                    DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("TypedTitle"));
                    DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("SaveLocation"));
                    DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("MenuBar"));
                    DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("SelectedMenu"));
                    DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("OpenedFile"));
                    break;
                case "FileUtility":
                    DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("InitalRun"));
                    DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("Window"));
                    DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("CurrentFile"));
                    break;
                case "WindowManager":
                    DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("InitalRun"));
                    DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("SelectedWindow"));
                    break;
                case "PlayerData":
                    DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("Stats"));
                    break;
            }
		});
		RunRegLoad = true;
	}

	//void CheckNullRegInfo()
	//{
	//	for (int i = 0; i < PersonController.control.People.Count; i++)
	//	{
 //           var Gateway = PersonController.control.People[i].Gateway.Registry;

	//		for(int j = 0;j < Gateway.Count;j++)
 //           {
	//			for (int k = 0; k < Gateway[j].Values.Count; k++)
	//			{
	//				Gateway[j].Values[k].DataString = StringIsNullCheck.NotNull(Gateway[j].Values[k].DataString);
	//				Gateway[j].Values[k].Request.PersonName = StringIsNullCheck.NotNull(Gateway[j].Values[k].Request.PersonName);
	//				Gateway[j].Values[k].Request.ProgramName = StringIsNullCheck.NotNull(Gateway[j].Values[k].Request.ProgramName);
	//				Gateway[j].Values[k].Request.ProgramTarget = StringIsNullCheck.NotNull(Gateway[j].Values[k].Request.ProgramTarget);
	//				Gateway[j].Values[k].ValueName = StringIsNullCheck.NotNull(Gateway[j].Values[k].ValueName);

	//				Gateway[j].Values[k].NetworkData.Command = StringIsNullCheck.NotNull(Gateway[j].Values[k].NetworkData.Command);
	//				Gateway[j].Values[k].NetworkData.DisplayName = StringIsNullCheck.NotNull(Gateway[j].Values[k].NetworkData.DisplayName);
	//				Gateway[j].Values[k].NetworkData.Domain = StringIsNullCheck.NotNull(Gateway[j].Values[k].NetworkData.Domain);
	//				Gateway[j].Values[k].NetworkData.InputAddress = StringIsNullCheck.NotNull(Gateway[j].Values[k].NetworkData.InputAddress);
	//				Gateway[j].Values[k].NetworkData.InternalIPAddress = StringIsNullCheck.NotNull(Gateway[j].Values[k].NetworkData.InternalIPAddress);
	//				Gateway[j].Values[k].NetworkData.IPAddress = StringIsNullCheck.NotNull(Gateway[j].Values[k].NetworkData.IPAddress);
	//				Gateway[j].Values[k].NetworkData.Port = StringIsNullCheck.NotNull(Gateway[j].Values[k].NetworkData.Port);
	//				Gateway[j].Values[k].NetworkData.Status = StringIsNullCheck.NotNull(Gateway[j].Values[k].NetworkData.Status);
	//			}
	//		}
	//	}
	//	RunRegCheck = false;
	//}

	void CheckRegKeys()
	{
		for (int i = 0; i < PersonController.control.People.Count; i++)
		{
			var personPC = PersonController.control.People[i].Gateway;

			if (personPC.Registry.Count == 0)
			{
				for (int j = 0; j < DefaultRegistryKeys.Count; j++)
				{
					personPC.Registry.Add(new RegistrySystem(DefaultRegistryKeys[j].KeyName));

					for (int k = 0; k < DefaultRegistryKeys[j].Values.Count; k++)
					{
						var RegValue = DefaultRegistryKeys[j].Values[k];
						personPC.Registry[j].Values.Add(new RegistryDataSystem(RegValue.ValueName));
					}
				}
			}
			else
            {
				if(personPC.Registry.Count < DefaultRegistryKeys.Count)
                {
					for (int j = 0; j < DefaultRegistryKeys.Count; j++)
					{
						if(j >= personPC.Registry.Count)
                        {
							personPC.Registry.Add(new RegistrySystem(DefaultRegistryKeys[j].KeyName));
						}
						else
                        {
							if (personPC.Registry[j].KeyName != DefaultRegistryKeys[j].KeyName)
							{
								personPC.Registry.Insert(j, new RegistrySystem(DefaultRegistryKeys[j].KeyName));
							}
						}							
					}
                }
				for (int j = 0; j < DefaultRegistryKeys.Count; j++)
				{
					if (personPC.Registry[j].KeyName != DefaultRegistryKeys[j].KeyName)
					{
						personPC.Registry.Insert(j, new RegistrySystem(DefaultRegistryKeys[j].KeyName));
					}

					if(personPC.Registry[j].Values.Count > 0)
                    {
						for (int k = 0; k < DefaultRegistryKeys[j].Values.Count; k++)
						{
							if (k >= personPC.Registry[j].Values.Count)
							{
								var RegValue = DefaultRegistryKeys[j].Values[k];
								personPC.Registry[j].Values.Insert(k, new RegistryDataSystem(RegValue.ValueName));
							}
							else
                            {
								if (personPC.Registry[j].Values[k].ValueName != DefaultRegistryKeys[j].Values[k].ValueName)
								{
									var RegValue = DefaultRegistryKeys[j].Values[k];
									personPC.Registry[j].Values.Insert(k, new RegistryDataSystem(RegValue.ValueName));
								}
							}
						}
					}
					else
                    {
						for (int k = 0; k < DefaultRegistryKeys[j].Values.Count; k++)
						{
							var RegValue = DefaultRegistryKeys[j].Values[k];
							personPC.Registry[j].Values.Add(new RegistryDataSystem(RegValue.ValueName));
						}
					}
				}
			}
		}

		RunRegLoad = false;
		//RunRegCheck = true;
	}
}
