using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text.RegularExpressions;

public class AnimeImageLoaderBackup : MonoBehaviour
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
	public List<Texture2D> FoundTextures1 = new List<Texture2D>();
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

	public bool RunThreadedFunc;
	public List<string> FoundFilesTemp = new List<string>();
	public bool DoesHave;
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
		for (int i = 0; i < 18; i++)
		{
			FoundFilesTemp.Add("");
		}
	}

	void FolderPaths()
	{
		folderpaths.Add("E:/Anime Faces/Extracted Images/psi-0.3/");
		folderpaths.Add("E:/Anime Faces/Extracted Images/psi-0.4/");
		folderpaths.Add("E:/Anime Faces/Extracted Images/psi-0.5/");
		folderpaths.Add("E:/Anime Faces/Extracted Images/psi-0.6/");
		folderpaths.Add("E:/Anime Faces/Extracted Images/psi-0.7/");
		folderpaths.Add("E:/Anime Faces/Extracted Images/psi-0.8/");
		folderpaths.Add("E:/Anime Faces/Extracted Images/psi-0.9/");
		folderpaths.Add("E:/Anime Faces/Extracted Images/psi-1.0/");
		folderpaths.Add("E:/Anime Faces/Extracted Images/psi-1.1/");
		folderpaths.Add("E:/Anime Faces/Extracted Images/psi-1.2/");
		folderpaths.Add("E:/Anime Faces/Extracted Images/psi-1.3/");
		folderpaths.Add("E:/Anime Faces/Extracted Images/psi-1.4/");
		folderpaths.Add("E:/Anime Faces/Extracted Images/psi-1.5/");
		folderpaths.Add("E:/Anime Faces/Extracted Images/psi-1.6/");
		folderpaths.Add("E:/Anime Faces/Extracted Images/psi-1.7/");
		folderpaths.Add("E:/Anime Faces/Extracted Images/psi-1.8/");
		folderpaths.Add("E:/Anime Faces/Extracted Images/psi-1.9/");
		folderpaths.Add("E:/Anime Faces/Extracted Images/psi-2.0/");
	}

	void ThreadedFileCheck()
	{
		var FileList = new List<string>();
		string path = "E:/Anime Faces/Extracted Images/";
		string worldsFolder = path;

		var info = new DirectoryInfo(path);
		var fileInfo = info.GetFiles();

		string[] files;
		files = Directory.GetFiles(path);

		//Checks all files and stores all WAV files into the Files list.
		for (int i = 0; i < files.Length; i++)
		{
			if (files[i].EndsWith(".wav"))
			{
				FileList.Add(files[i]);
			}

		}

		if (FileList.Count > 0)
		{
			print(FileList[0]);

			RunThreadedFunc = false;
		}

		/*for (int i = 0; i < 1800090; i++)
		{
			var j = i;
			var sync = new object();
			var myNewList = new List<string>();

			foreach(string words in myNewList)
			{
				// Some other code...
				// More other code...
				lock (sync)
				{
					myNewList.Add("");
				}
				// Even more code...
			}
		}*/
	}
	void FileCheck()
	{
		for (int i = 0; i < tmp.Length; i++)
		{
			count++;
			if (tmp[i] == CurrentPath + "seed" + FileName + ".png")
			{
				//if (!FoundFilesTemp.Contains(CurrentPath + "seed" + FileName + ".png"))
				if (FoundFilesTemp[CurrentIndex] != CurrentPath + "seed" + FileName + ".png")
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
	}

	void AddFile()
	{
		tmp = Directory.GetFiles(CurrentPath);
		QThread.MakeThread(FileCheck);

		if (WaitToLoad == true)
		{
			if (FoundFilesTemp[FoundFilesTemp.Count - 1] != "")
			{
				for (int i = 0; i < FoundFilesTemp.Count; i++)
				{
					if (!FoundFiles.Contains(FoundFilesTemp[i]))
					{
						FoundFiles.Add(FoundFilesTemp[i]);
						FoundTextures.Add(TextureLoader.LoadPNG(FoundFilesTemp[i]));
					}
				}
			}
		}
		else
		{
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
				if (SelectedImage == i)
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
			if (Search == true)
			{
				GUI.Box(new Rect(windowRect.width / 2 - 100, windowRect.height / 2, 100, 23), "Loading images");
			}
		}
	}
}
