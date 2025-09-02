using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text.RegularExpressions;
using System.Text;

public class AnimeImageLoaderOrginal : MonoBehaviour
{
	public List<string> folderpaths = new List<string>();
	public List<string> folderpaths1 = new List<string>();
	public string FileName;
	public bool Search;
	public List<string> FoundFiles = new List<string>();
	public string CurrentPath;
	public int CurrentIndex;
	public List<Texture2D> FoundTextures = new List<Texture2D>();
	public bool clear;

	// UI VARS
	public bool show;

	public int windowID;
	public Rect windowRect = new Rect(100, 100, 200, 200);
	public int ImageScale;
	public int MaxIconsPerRow;
	public float IconHeight;
	public float IconWidth;
	public int SelectedImage;
	public int FileNumber;

	void Start()
	{
		windowID = 86;
		FolderPaths();
		windowRect.width = 500;
		windowRect.height = 500;
		IconHeight = 120;
		IconWidth = 120;
	}

	//void FolderPaths()
	//{
	//	folderpaths.Add("I:/Anime Faces/Extracted Images/psi-0.3/");
	//	folderpaths.Add("I:/Anime Faces/Extracted Images/psi-0.4/");
	//	folderpaths.Add("I:/Anime Faces/Extracted Images/psi-0.5/");
	//	folderpaths.Add("I:/Anime Faces/Extracted Images/psi-0.6/");
	//	folderpaths.Add("I:/Anime Faces/Extracted Images/psi-0.7/");
	//	folderpaths.Add("I:/Anime Faces/Extracted Images/psi-0.8/");
	//	folderpaths.Add("I:/Anime Faces/Extracted Images/psi-0.9/");
	//	folderpaths.Add("I:/Anime Faces/Extracted Images/psi-1.0/");
	//	folderpaths.Add("I:/Anime Faces/Extracted Images/psi-1.1/");
	//	folderpaths.Add("I:/Anime Faces/Extracted Images/psi-1.2/");
	//	folderpaths.Add("I:/Anime Faces/Extracted Images/psi-1.3/");
	//	folderpaths.Add("I:/Anime Faces/Extracted Images/psi-1.4/");
	//	folderpaths.Add("I:/Anime Faces/Extracted Images/psi-1.5/");
	//	folderpaths.Add("I:/Anime Faces/Extracted Images/psi-1.6/");
	//	folderpaths.Add("I:/Anime Faces/Extracted Images/psi-1.7/");
	//	folderpaths.Add("I:/Anime Faces/Extracted Images/psi-1.8/");
	//	folderpaths.Add("I:/Anime Faces/Extracted Images/psi-1.9/");
	//	folderpaths.Add("I:/Anime Faces/Extracted Images/psi-2.0/");

	//	folderpaths1.Add("I:/Anime Faces/Extracted Images/psi-0.3-pt2/");
	//	folderpaths1.Add("I:/Anime Faces/Extracted Images/psi-0.4-pt2/");
	//	folderpaths1.Add("I:/Anime Faces/Extracted Images/psi-0.5-pt2/");
	//	folderpaths1.Add("I:/Anime Faces/Extracted Images/psi-0.6-pt2/");
	//}

    void FolderPaths()
    {
        for (int i = 3; i <= 20; i++)
        {
            StringBuilder sb = new StringBuilder("I:/Anime Faces/Extracted Images/psi-");
            sb.Append((i * 0.1).ToString("F1"));
            sb.Append("/");
            folderpaths.Add(sb.ToString());
        }
    }
    string currentFile = "";

    void CurrentFolder()
	{

	}

	void AddFile()
	{
		foreach (string file in Directory.GetFiles(CurrentPath))
		{
			if (file.Contains("seed" + FileName + ".png"))
			{
				if (!FoundFiles.Contains(CurrentPath + "seed" + FileName + ".png"))
				//if(!FoundFiles.Contains(Path.Combine(CurrentPath, "seed" + FileName + ".png")))
				{
					FoundFiles.Add(file);
					FoundTextures.Add(TextureLoader.LoadPNG(file));
					CurrentIndex++;
					CurrentPath = folderpaths[CurrentIndex];
				}
			}
		}

		if (CurrentIndex >= folderpaths.Count)
		{
			CurrentIndex = 0;
			Search = false;
		}
	}

	bool GUIKeyDown(KeyCode key)
	{
		if (Event.current.type == EventType.KeyDown)
			return (Event.current.keyCode == key);
		return false;

	}

	void GUIControls()
	{
		if (GUIKeyDown(KeyCode.DownArrow))
		{
			FileNumber--;
			FileName = FileNumber.ToString();
			SelectedImage = 0;
			clear = true;
			Search = true;
		}
		if (GUIKeyDown(KeyCode.UpArrow))
		{
			FileNumber++;
			FileName = FileNumber.ToString();
			SelectedImage = 0;
			clear = true;
			Search = true;
		}
		if (GUIKeyDown(KeyCode.RightArrow))
		{
			if (SelectedImage <= FoundFiles.Count - 2)
			{
				SelectedImage++;
			}
		}
		if (GUIKeyDown(KeyCode.LeftArrow))
		{
			if (SelectedImage >= 1)
			{
				SelectedImage--;
			}
		}
		if (GUIKeyDown(KeyCode.Space))
		{
			File.Copy(Path.Combine(folderpaths[SelectedImage], "seed" + FileName + ".png"), Path.Combine("E:/Anime Faces/Extracted Images/Hand Picked", SelectedImage + "seed" + FileName + ".png"));
		}
	}

	// Update is called once per frame
	void Update()
	{
		if (clear == true)
		{
			CurrentPath = "";
			FoundFiles.RemoveRange(0, FoundFiles.Count);
			FoundTextures.RemoveRange(0, FoundTextures.Count);
			CurrentIndex = -1;
			Resources.UnloadUnusedAssets();
			clear = false;
		}
		if (Search == true && clear == false)
		{
			if (CurrentPath == "")
			{
				if (CurrentIndex == -1)
				{
					CurrentIndex = 0;

				}
				CurrentPath = folderpaths[CurrentIndex];
			}
			else
			{
				AddFile();
			}
		}
	}

	void OnGUI()
	{
		//GUI.skin = GameControl.control.Skins[Registry.GetIntData("Player", "System", "Skin")];

		//float rx = Screen.width / native_width;
		//float ry = Screen.height / native_height;

		//GUI.matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, new Vector3(rx, ry, 1));

		if (show == true)
		{
			windowRect = WindowClamp.ClampToScreen(GUI.Window(windowID, windowRect, DoMyWindow, ""));
		}
	}

	void DoMyWindow(int WindowID)
	{
		GUIControls();
		if (GUI.Button(new Rect(windowRect.width - 23, 2, 21, 21), "X"))
		{
			show = false;
		}

		//GUI.backgroundColor = Registry.Get32ColorData("Player", "System", "ButtonColor");
		//GUI.contentColor = Registry.Get32ColorData("Player", "System", "FontColor");

		GUI.DragWindow(new Rect(2, 2, windowRect.width - 26, 21));
		GUI.Box(new Rect(2, 2, windowRect.width - 26, 21), "Anime Image Browser");

		if (GUI.Button(new Rect(2, 21, 21, 21), "<"))
		{
			FileNumber--;
			FileName = FileNumber.ToString();
			clear = true;
			Search = true;
		}

		FileName = GUI.TextField(new Rect(24, 21, 100, 21), FileName);
		FileName = Regex.Replace(FileName, @"[^0-9]", "");

		if (FileName != "")
		{
			FileNumber = int.Parse(FileName);
		}

		if (GUI.Button(new Rect(124, 21, 21, 21), ">"))
		{
			FileNumber++;
			FileName = FileNumber.ToString();
			clear = true;
			Search = true;
		}

		if (GUI.Button(new Rect(146, 21, 60, 21), "Search"))
		{
			FileName = FileNumber.ToString();
			clear = true;
			Search = true;
		}

		IconWidth = windowRect.width / FoundTextures.Count;
		IconHeight = IconWidth;

		if (FoundTextures.Count > 0)
		{
			for (int i = 0; i < FoundTextures.Count; i++)
			{
				if (GUI.Button(new Rect(1 + IconWidth * i, 40, IconWidth, IconHeight), FoundTextures[i]))
				{
					SelectedImage = i;
				}
			}

			GUI.DrawTexture(new Rect(1, 40 + IconHeight, windowRect.width, windowRect.height - IconHeight - 40), FoundTextures[SelectedImage]);
		}
	}
}