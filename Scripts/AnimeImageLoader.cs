using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text.RegularExpressions;

public class AnimeImageLoader : MonoBehaviour
{
	public List<string> folderpaths = new List<string>();
	public List<string> folderpaths1 = new List<string>();
	public string FileName;
	public bool Search;
	public bool Searching;
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

	public string filetest;
	public int count;

	public List<string> FoundFilesTemp = new List<string>();
	public int FileCount;
	string[] tmp;
	public bool WaitToLoad;


	void Start()
	{
		windowID = 86;
		FolderPaths();
		windowRect.width = 500;
		windowRect.height = 500;
		IconHeight = 120;
		IconWidth = 120;
		show = true;
		for(int i = 0; i < 18; i++)
        {
			FoundFilesTemp.Add("");
        }
	}

	void FolderPaths()
	{
		for (int i = 3; i <= 20; i++)
		{
			string folderPath = "I:/Anime Faces/Extracted Images/psi-" + (i * 0.1).ToString("F1") + "/";
			folderpaths.Add(folderPath);
		}
	}


	void FileCheck()
	{
		string targetPath = string.Concat(CurrentPath, "seed", FileName, ".png");
		for (int i = 0; i < tmp.Length; i++)
		{
			count++;
			if (tmp[i] == targetPath && FoundFilesTemp[CurrentIndex] != targetPath)
			{
				FoundFilesTemp[CurrentIndex] = tmp[i];
				if (CurrentIndex < folderpaths.Count - 1)
				{
					CurrentIndex++;
				}
				CurrentPath = folderpaths[CurrentIndex];
			}
		}
	}

	void CheckFoundFiles()
	{
        if (FoundFilesTemp[FoundFilesTemp.Count - 1] != "")
        {
            //Parallel.For(0, FoundFilesTemp.Count, i =>
            for (int i = 0; i < FoundFilesTemp.Count; i++)
            {
                if (!FoundFiles.Contains(FoundFilesTemp[i]))
                {
                    FoundFiles.Add(FoundFilesTemp[i]);
                    //FoundTextures.Add(TextureLoader.LoadPNG(FoundFilesTemp[i]));
                }
            }
        }
    }

	void AddFile()
	{
		tmp = Directory.GetFiles(CurrentPath);

		QThread.MakeThread(FileCheck);
		//FileCheck();

		if (WaitToLoad == true)
        {
			QThread.MakeThread(CheckFoundFiles);

			if (FoundFiles.Count >= folderpaths.Count)
			{
				for (int i = 0; i < FoundFiles.Count; i++)
				{
					FoundTextures.Add(TextureLoader.LoadPNG(FoundFiles[i]));
				}
			}
		}
		else
        {
			//Parallel.For(0, FoundFilesTemp.Count, i =>
			for (int i = 0; i < FoundFilesTemp.Count; i++)
			{
				if (!FoundFiles.Contains(FoundFilesTemp[i]) && FoundFilesTemp[i] != "")
				{
					FoundFiles.Add(FoundFilesTemp[i]);
					FoundTextures.Add(TextureLoader.LoadPNG(FoundFilesTemp[i]));
				}
			}
		}


		if (CurrentIndex >= folderpaths.Count)
		{
			CurrentIndex = 0;
			Search = false;
		}

		if (FoundTextures.Count >= folderpaths.Count)
		{
			Search = false;
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
			for (int i = 0; i < FoundFilesTemp.Count; i++)
			{
                FoundFilesTemp[i] = "";
			}
			count = 0;
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
			FileName = FileNumber.ToString("D4");
			SelectedImage = 0;
			clear = true;
			Search = true;
		}
		if (GUIKeyDown(KeyCode.UpArrow))
		{
			FileNumber++;
			FileName = FileNumber.ToString("D4");
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
			File.Copy(Path.Combine(folderpaths[SelectedImage], "seed" + FileName + ".png"), Path.Combine("E:/Anime Faces/Hand Picked", SelectedImage + "seed" + FileName + ".png"));
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
			FileName = FileNumber.ToString("D4");
			SelectedImage = 0;
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
			FileName = FileNumber.ToString("D4");
			SelectedImage = 0;
			clear = true;
			Search = true;
		}

		if (GUI.Button(new Rect(146, 21, 60, 21), "Search"))
		{
			FileName = FileNumber.ToString("D4");
			SelectedImage = 0;
			clear = true;
			Search = true;
		}

		GUI.Box(new Rect(208, 21, 100, 21), "Current Pic: " + SelectedImage);

		GUI.Box(new Rect(310, 21, 120, 21), "Count: " + count.ToString("n0"));

		IconWidth = windowRect.width / FoundTextures.Count;
		IconHeight = IconWidth;

		if (FoundTextures.Count > 0)
		{
			for (int i = 0; i < FoundTextures.Count; i++)
			{
				if(SelectedImage == i)
                {
					GUI.contentColor = Color.white;
                }
				else
                {
					GUI.contentColor = Color.gray;
				}
				if (GUI.Button(new Rect(1 + IconWidth * i, 40, IconWidth, IconHeight), FoundTextures[i]))
				{
					SelectedImage = i;
				}
			}

			GUI.DrawTexture(new Rect(1, 40 + IconHeight, windowRect.width, windowRect.height - IconHeight - 40), FoundTextures[SelectedImage]);
		}
		else
        {
			if(Search == true)
            {
				GUI.Box(new Rect(windowRect.width / 2 - 100, windowRect.height / 2, 100, 23), "Loading images");
			}
		}
	}
}
