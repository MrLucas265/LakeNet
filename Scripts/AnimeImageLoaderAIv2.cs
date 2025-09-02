using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text.RegularExpressions;
using System.Text;
using System.Linq;
using System.Threading;

public class AnimeImageLoaderAIv2 : MonoBehaviour
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

    HashSet<string> FoundFilesTemp = new HashSet<string>();
    public int FileCount;
    string[] tmp;
    public bool WaitToLoad;


    void Start()
    {
        FolderPaths();
        windowID = 86;
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
        for (int i = 3; i <= 20; i++)
        {
            StringBuilder sb = new StringBuilder("I:/Anime Faces/Extracted Images/psi-");
            sb.Append((i * 0.1).ToString("F1"));
            sb.Append("/");
            folderpaths.Add(sb.ToString());
        }
    }
    string currentFile = "";

    void FileCheck()
    {
        string targetPath = string.Concat(CurrentPath, "seed", FileName, ".png");
        for(int i = 0; i < tmp.Length; i++)
        {
            string file = tmp[i];
            if (file == targetPath && file != currentFile)
            {
                lock (FoundFilesTemp)
                {
                    FoundFilesTemp.Add(file);
                }
                currentFile = file;
                if (CurrentIndex < folderpaths.Count - 1)
                {
                    CurrentIndex++;
                }
                CurrentPath = folderpaths[CurrentIndex];
            }
        }
    }

    void AddFile()
    {
        tmp = Directory.GetFiles(CurrentPath);

        List<string> foundFilesTempList;

        //QThread.MakeThread(FileCheck);
        ThreadPool.QueueUserWorkItem(task =>
        {
            Debug.Log("Thread ID: " + Thread.CurrentThread.ManagedThreadId);
            //tmp = Directory.GetFiles(CurrentPath);
            FileCheck();
        });

        lock (FoundFilesTemp)
        {
            foundFilesTempList = new List<string>(FoundFilesTemp); // Create a copy of FoundFilesTemp
        }

        HashSet<string> foundFilesTempCopy = new HashSet<string>(foundFilesTempList); // Create a new HashSet from the copied list

        List<Texture2D> newTextures = new List<Texture2D>(); // Create a new list to hold the textures temporarily

        lock (FoundFiles) // Lock the FoundFiles collection to prevent concurrent modification
        {
            foreach (string file in foundFilesTempCopy)
            {
                if (!FoundFiles.Contains(file) && file != "")
                {
                    FoundFiles.Add(file);
                    newTextures.Add(TextureLoader.LoadPNG(file)); // Add texture to the temporary list
                }
            }
        }

        lock (FoundTextures) // Lock the FoundTextures list to prevent concurrent modification
        {
            FoundTextures.AddRange(newTextures); // Add the temporary list of textures to FoundTextures
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
            FoundFilesTemp.Clear();
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
