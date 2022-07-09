using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

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
	// Use this for initialization
	public bool RunRegLoad;

	public List<RegistrySystem> DefaultRegistryKeys = new List<RegistrySystem>();

	public List<RegistrySystem> list1 = new List<RegistrySystem>();

	void Awake()
    {
		DefaultKeys();
	}


	
	// Update is called once per frame
	void Update ()
	{
		if(RunRegLoad == true)
		{
			CheckRegKeys();
		}
	}

	void DefaultKeys()
	{
		DefaultRegistryKeys.Add(new RegistrySystem("CLI"));
		DefaultRegistryKeys.Add(new RegistrySystem("FileManager"));
		DefaultRegistryKeys.Add(new RegistrySystem("OS"));
		DefaultRegistryKeys.Add(new RegistrySystem("MediaPlayer"));
		DefaultRegistryKeys.Add(new RegistrySystem("System"));
		DefaultRegistryKeys.Add(new RegistrySystem("Control Panel"));

		for (int i = 0; i < DefaultRegistryKeys.Count; i++)
		{
			switch (DefaultRegistryKeys[i].KeyName)
			{
				case "FileManager":
					DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("TypedDirectory", "", 0, false, 0, new SRect(new Rect()), new SVector3(new Vector3()), new SVector2(new Vector2())));
					DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("CurrentDirectory", "", 0,false,0,new SRect(new Rect()),new SVector3(new Vector3()),new SVector2(new Vector2())));
					DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("MainScrollPos", "", 0, false, 0, new SRect(new Rect()), new SVector3(new Vector3()), new SVector2(new Vector2())));
					DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("MainScrollSize", "", 0, false, 0, new SRect(new Rect()), new SVector3(new Vector3()), new SVector2(new Vector2())));
					DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("UserName", "", 0, false, 0, new SRect(new Rect()), new SVector3(new Vector3()), new SVector2(new Vector2())));
					DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("SelectedMenu", "", 0, false, 0, new SRect(new Rect()), new SVector3(new Vector3()), new SVector2(new Vector2())));
					DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("LastClick", "", 0, false, 0, new SRect(new Rect()), new SVector3(new Vector3()), new SVector2(new Vector2())));
					DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("LastClickThreshold", "", 0, false, 0.5f, new SRect(new Rect()), new SVector3(new Vector3()), new SVector2(new Vector2())));
					DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("SelectedFile", "", 0, false, 0.5f, new SRect(new Rect()), new SVector3(new Vector3()), new SVector2(new Vector2())));
					DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("FontColor", "", 0, false, 0, new SRect(new Rect()), new SVector3(new Vector3()), new SVector2(new Vector2())));
					DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("WindowColor", "", 0, false, 0, new SRect(new Rect()), new SVector3(new Vector3()), new SVector2(new Vector2())));
					DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("ButtonColor", "", 0, false, 0, new SRect(new Rect()), new SVector3(new Vector3()), new SVector2(new Vector2())));
					DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("Test 1", "", 0, false, 0, new SRect(new Rect()), new SVector3(new Vector3()), new SVector2(new Vector2())));
					DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("PageHistory", "", 0, false, 0, new SRect(new Rect()), new SVector3(new Vector3()), new SVector2(new Vector2())));
					break;
				case "CLI":
					DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("WorkingPath", "", 0,false,0, new SRect(new Rect()), new SVector3(new Vector3()), new SVector2(new Vector2())));
					DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("CommandLine", "", 0, false, 0, new SRect(new Rect()), new SVector3(new Vector3()), new SVector2(new Vector2())));
					DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("Pinned", "", 0, false, 0, new SRect(new Rect()), new SVector3(new Vector3()), new SVector2(new Vector2())));
					DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("CustomPos", "", 0, false, 0, new SRect(new Rect()), new SVector3(new Vector3()), new SVector2(new Vector2())));
					DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("SelectedPastCommand", "", 0, false, 0, new SRect(new Rect()), new SVector3(new Vector3()), new SVector2(new Vector2())));
					DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("Scrollsize", "", 0, false, 0, new SRect(new Rect()), new SVector3(new Vector3()), new SVector2(new Vector2())));
					DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("ScrollPos", "", 0, false, 0, new SRect(new Rect()), new SVector3(new Vector3()), new SVector2(new Vector2())));
					DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("CommandHistory", "", 0, false, 0, new SRect(new Rect()), new SVector3(new Vector3()), new SVector2(new Vector2())));
					DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("TextFieldPos", "", 0, false, 0, new SRect(new Rect()), new SVector3(new Vector3()), new SVector2(new Vector2())));
					DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("Input", "", 0, false, 0, new SRect(new Rect()), new SVector3(new Vector3()), new SVector2(new Vector2())));
					DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("Output", "", 0, false, 0, new SRect(new Rect()), new SVector3(new Vector3()), new SVector2(new Vector2())));
					DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("Skin", "", 0, false, 0, new SRect(new Rect()), new SVector3(new Vector3()), new SVector2(new Vector2())));
					DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("FontColor", "", 0, false, 0, new SRect(new Rect()), new SVector3(new Vector3()), new SVector2(new Vector2())));
					DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("WindowColor", "", 0, false, 0, new SRect(new Rect()), new SVector3(new Vector3()), new SVector2(new Vector2())));
					DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("ButtonColor", "", 0, false, 0, new SRect(new Rect()), new SVector3(new Vector3()), new SVector2(new Vector2())));
					break;
				case "System":
					DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("FontColor", "", 0, false, 0, new SRect(new Rect()), new SVector3(new Vector3()), new SVector2(new Vector2())));
					DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("WindowColor", "", 0, false, 0, new SRect(new Rect()), new SVector3(new Vector3()), new SVector2(new Vector2())));
					DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("ButtonColor", "", 0, false, 0, new SRect(new Rect()), new SVector3(new Vector3()), new SVector2(new Vector2())));
					DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("SecondaryFontColor", "", 0, false, 0, new SRect(new Rect()), new SVector3(new Vector3()), new SVector2(new Vector2())));
					DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("SecondaryWindowColor", "", 0, false, 0, new SRect(new Rect()), new SVector3(new Vector3()), new SVector2(new Vector2())));
					DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("SecondaryButtonColor", "", 0, false, 0, new SRect(new Rect()), new SVector3(new Vector3()), new SVector2(new Vector2())));
					DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("SelectedBackground", "", 0, false, 0, new SRect(new Rect()), new SVector3(new Vector3()), new SVector2(new Vector2())));
					DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("SelectedGUIID", "", 0, false, 0, new SRect(new Rect()), new SVector3(new Vector3()), new SVector2(new Vector2())));
					DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("RunProgram", "", 0, false, 0, new SRect(new Rect()), new SVector3(new Vector3()), new SVector2(new Vector2())));
					break;
				case "Control Panel":
					DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("BackgroundField"));
					DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("BackgroundAddress"));
					DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("Window"));
					break;
				case "OS":
					DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("BackgroundPath", "", 0, false, 0, new SRect(new Rect()), new SVector3(new Vector3()), new SVector2(new Vector2())));
					DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("CursorPath", "", 0, false, 0, new SRect(new Rect()), new SVector3(new Vector3()), new SVector2(new Vector2())));
					DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("Secondary", "", 0, false, 0, new SRect(new Rect()), new SVector3(new Vector3()), new SVector2(new Vector2())));
					DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("ColorOption", "", 0, false, 0, new SRect(new Rect()), new SVector3(new Vector3()), new SVector2(new Vector2())));
					break;
				case "MediaPlayer":
					DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("Path", "", 0, false, 0, new SRect(new Rect()), new SVector3(new Vector3()), new SVector2(new Vector2())));
					DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("Looping", "", 0,false,0, new SRect(new Rect()), new SVector3(new Vector3()), new SVector2(new Vector2())));
					DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("Loop", "", 0, false, 0, new SRect(new Rect()), new SVector3(new Vector3()), new SVector2(new Vector2())));
					DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("SelectedFile", "", 0, false, 0, new SRect(new Rect()), new SVector3(new Vector3()), new SVector2(new Vector2())));
					DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("CurrentFilePath", "", 0, false, 0, new SRect(new Rect()), new SVector3(new Vector3()), new SVector2(new Vector2())));
					DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("LoadVideo", "", 0, false, 0, new SRect(new Rect()), new SVector3(new Vector3()), new SVector2(new Vector2())));
					DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("HideUI", "", 0, false, 0, new SRect(new Rect()), new SVector3(new Vector3()), new SVector2(new Vector2())));
					DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("Menu", "", 0, false, 0, new SRect(new Rect()), new SVector3(new Vector3()), new SVector2(new Vector2())));
					DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("LoadPlaylist", "", 0, false, 0, new SRect(new Rect()), new SVector3(new Vector3()), new SVector2(new Vector2())));
					DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("CloseButton", "", 0, false, 0, new SRect(new Rect()), new SVector3(new Vector3()), new SVector2(new Vector2())));
					DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("MiniButton", "", 0, false, 0, new SRect(new Rect()), new SVector3(new Vector3()), new SVector2(new Vector2())));
					DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("SettingsButton", "", 0, false, 0, new SRect(new Rect()), new SVector3(new Vector3()), new SVector2(new Vector2())));
					DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("ListButton", "", 0, false, 0, new SRect(new Rect()), new SVector3(new Vector3()), new SVector2(new Vector2())));
					DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("HomeButton", "", 0, false, 0, new SRect(new Rect()), new SVector3(new Vector3()), new SVector2(new Vector2())));
					DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("DefaltBoxSetting", "", 0, false, 0, new SRect(new Rect()), new SVector3(new Vector3()), new SVector2(new Vector2())));
					DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("VideoRect", "", 0, false, 0, new SRect(new Rect()), new SVector3(new Vector3()), new SVector2(new Vector2())));
					DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("Volume", "", 0, false, 0, new SRect(new Rect()), new SVector3(new Vector3()), new SVector2(new Vector2())));
					break;
			}
		}

		RunRegLoad = true;
	}

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
						personPC.Registry[j].Values.Add(new RegistryDataSystem(RegValue.ValueName, RegValue.DataString, RegValue.DataInt, RegValue.DataBool, RegValue.DataFloat,RegValue.DataRect,RegValue.DataVector3,RegValue.DataVector2));
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
								personPC.Registry[j].Values.Insert(k, new RegistryDataSystem(RegValue.ValueName, RegValue.DataString, RegValue.DataInt, RegValue.DataBool, RegValue.DataFloat, RegValue.DataRect, RegValue.DataVector3, RegValue.DataVector2));
							}
							else
                            {
								if (personPC.Registry[j].Values[k].ValueName != DefaultRegistryKeys[j].Values[k].ValueName)
								{
									var RegValue = DefaultRegistryKeys[j].Values[k];
									personPC.Registry[j].Values.Insert(k, new RegistryDataSystem(RegValue.ValueName, RegValue.DataString, RegValue.DataInt, RegValue.DataBool, RegValue.DataFloat, RegValue.DataRect, RegValue.DataVector3, RegValue.DataVector2));
								}
							}
						}
					}
					else
                    {
						for (int k = 0; k < DefaultRegistryKeys[j].Values.Count; k++)
						{
							var RegValue = DefaultRegistryKeys[j].Values[k];
							personPC.Registry[j].Values.Add(new RegistryDataSystem(RegValue.ValueName, RegValue.DataString, RegValue.DataInt, RegValue.DataBool, RegValue.DataFloat, RegValue.DataRect, RegValue.DataVector3, RegValue.DataVector2));
						}
					}
				}
			}
		}

		RunRegLoad = false;
	}
}
