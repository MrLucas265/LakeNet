//using UnityEngine;
//using System.Collections;
//using System.Collections.Generic;

//public class Complier : MonoBehaviour
//{
//    public string PersonName;
//    public string ProgramNameForWinMan;
//    public string ProgramName;

//    void Start()
//    {
//        ProgramNameForWinMan = "Notepad";
//        ProgramName = "Notepad";
//        PersonName = "Player";
//    }

//    void OnGUI()
//    {
//        GUI.skin = GameControl.control.Skins[Registry.GetIntData("Player", "System", "Skin")];
//        GUI.color = Registry.Get32ColorData("Player", "System", "WindowColor");

//        for (int PersonCount = 0; PersonCount < PersonController.control.People.Count; PersonCount++)
//        {
//            var pwinman = PersonController.control.People[PersonCount].Gateway;

//            if (pwinman.RunningPrograms.Count > 0)
//            {
//                for (int i = 0; i < pwinman.RunningPrograms.Count; i++)
//                {
//                    if (pwinman.RunningPrograms[i].ProgramName == ProgramNameForWinMan)
//                    {
//                        //ColorUI(pwinman.RunningPrograms[i].WPN);
//                        //GUI.color = new Color32(LocalRegistry.GetRedColorData(PersonName, pwinman.RunningPrograms[i].WPN, ProgramName, "WindowColor"),
//                        //	LocalRegistry.GetGreenColorData(PersonName, pwinman.RunningPrograms[i].WPN, ProgramName, "WindowColor"),
//                        //	LocalRegistry.GetBlueColorData(PersonName, pwinman.RunningPrograms[i].WPN, ProgramName, "WindowColor"),
//                        //	LocalRegistry.GetAlphaColorData(PersonName, pwinman.RunningPrograms[i].WPN, ProgramName, "WindowColor"));

//                        pwinman.RunningPrograms[i].windowRect = WindowClamp.ClampToScreen(GUI.Window(pwinman.RunningPrograms[i].WID, pwinman.RunningPrograms[i].windowRect, DoMyWindow, ""));

//                        LocalRegistry.SetRectData(PersonName, i, ProgramNameForWinMan, "Window", pwinman.RunningPrograms[i].windowRect);
//                    }
//                }
//            }
//        }
//    }

//    public void Open()
//    {
//        CurrentWorkingTitle = GameControl.control.ProgramFiles[SelectedDocument].Name;
//        TypedText = GameControl.control.ProgramFiles[SelectedDocument].Content;
//        TypedTitle = GameControl.control.ProgramFiles[SelectedDocument].Name;
//        SaveLocation = GameControl.control.ProgramFiles[SelectedDocument].Location;
//        showSave = true;
//        ShowFileContent = true;
//        ShowFileNameMaker = false;
//        ShowFileOpen = false;
//        SelectedMenu = 0;
//    }

//    void Save()
//    {
//        if (GameControl.control.ProgramFiles.Count > 0)
//        {
//            if (CurrentWorkingTitle == GameControl.control.ProgramFiles[SelectedDocument].Name)
//            {
//                GameControl.control.ProgramFiles[SelectedDocument].Content = TypedText;
//                GameControl.control.ProgramFiles[SelectedDocument].Used = FileSize;
//            }
//            else
//            {
//                if (GameControl.control.ProgramFiles[SelectedDocument].Name != TypedTitle)
//                {
//                    GameControl.control.ProgramFiles.Insert(0, new ProgramSystem(TypedTitle, "", "", GameControl.control.Time.DayName, TypedText, "", SaveLocation, "", "", "", ProgramSystem.FileExtension.Txt, ProgramSystem.FileExtension.Null, 0, 0, FileSize, 0, 0, 0, 0, 100, 0f, 0, 0, 0, 0, 0, 0, 0, false, false, false, false, BlankInfections, BlankFileType));
//                    GameControl.control.ProgramFiles[0].Used = FileSize;
//                }
//            }
//        }
//        else
//        {
//            GameControl.control.ProgramFiles.Insert(0, new ProgramSystem(TypedTitle, "", "", GameControl.control.Time.DayName, TypedText, "", SaveLocation, "", "", "", ProgramSystem.FileExtension.Txt, ProgramSystem.FileExtension.Null, 0, 0, FileSize, 0, 0, 0, 0, 100, 0f, 0, 0, 0, 0, 0, 0, 0, false, false, false, false, BlankInfections, BlankFileType));
//            GameControl.control.ProgramFiles[0].Used = FileSize;
//        }
//    }

//    void DiskCheck()
//    {
//        Name.RemoveRange(0, Name.Count);
//        Location.RemoveRange(0, Location.Count);
//        FileIndex.RemoveRange(0, FileIndex.Count);
//        Files.RemoveRange(0, Files.Count);
//        for (int i = 0; i < GameControl.control.ProgramFiles.Count; i++)
//        {
//            if (GameControl.control.ProgramFiles[i].Extension == ProgramSystem.FileExtension.Txt)
//            {
//                if (Name.Contains(GameControl.control.ProgramFiles[i].Name))
//                {
//                    if (!Location.Contains(GameControl.control.ProgramFiles[i].Location))
//                    {
//                        Name.Add(GameControl.control.ProgramFiles[i].Name);
//                        Location.Add(GameControl.control.ProgramFiles[i].Location);
//                        FileIndex.Add(i);
//                        Files.Add(GameControl.control.ProgramFiles[i]);
//                    }
//                }
//                else
//                {
//                    Name.Add(GameControl.control.ProgramFiles[i].Name);
//                    Location.Add(GameControl.control.ProgramFiles[i].Location);
//                    FileIndex.Add(i);
//                    Files.Add(GameControl.control.ProgramFiles[i]);
//                }
//            }
//        }
//    }

//    void DoMyWindow(int windowID)
//    {
//        GUI.backgroundColor = Registry.Get32ColorData("Player", "System", "ButtonColor");
//        GUI.contentColor = Registry.Get32ColorData("Player", "System", "FontColor");

//        GUI.DragWindow(new Rect(2, 2, 271, 21));
//        GUI.Box(new Rect(2, 2, 271, 21), "" + CurrentWorkingTitle + " - Notepad");

//        if (GUI.Button(new Rect(windowRect.width - 23, 2, 21, 21), "X"))
//        {
//            //show = false;
//            //this.enabled = false;
//            appman.SelectedApp = "Notepad";
//        }

//        switch (SelectedMenu)
//        {
//            case 0:
//                TextAreaRect = new Rect(1, 47, windowRect.width - 2, windowRect.height - 48);
//                TypedText = GUI.TextArea(new Rect(TextAreaRect), TypedText, 500);
//                break;
//            case 1:
//                TextAreaRect = new Rect(115, 25, 150, 128);
//                //TypedText = GUI.TextArea(new Rect(TextAreaRect), TypedText, 500);
//                break;
//            case 2:
//                TextAreaRect = new Rect(windowRect.width - 151, 47, 150, windowRect.height - 48);
//                TypedText = GUI.TextArea(new Rect(TextAreaRect), TypedText, 500);
//                break;
//            case 3:
//                TextAreaRect = new Rect(115, 25, 150, 128);
//                break;
//        }

//        if (CurrentWorkingTitle == "")
//        {
//            CurrentWorkingTitle = "Untitled";
//        }

//        if (ShowFileNameMaker == true)
//        {
//            GUI.Label(new Rect(5, 50, 150, 21), "File Name");
//            TypedTitle = GUI.TextField(new Rect(5, 100, 140, 21), TypedTitle);

//            GUI.Label(new Rect(5, 150, 150, 21), "File Location");
//            SaveLocation = GUI.TextField(new Rect(5, 200, 140, 21), SaveLocation);

//            if (TypedTitle != "")
//            {
//                if (SaveLocation != "")
//                {
//                    if (GUI.Button(new Rect(50, 250, 80, 21), "Add File"))
//                    {
//                        CurrentWorkingTitle = TypedTitle;

//                        FileSize = 0;
//                        FileSize = TypedTitle.Length + TypedText.Length;
//                        FileSize = FileSize / 100;
//                        Save();

//                        ShowFileContent = true;
//                        ShowFileNameMaker = false;
//                        ShowFileOpen = false;
//                        SelectedMenu = 0;
//                    }
//                }
//            }
//        }

//        if (ShowFileContent == true)
//        {
//            if (GUI.Button(new Rect(144, 24, 37, 21), "Save"))
//            {
//                FileSize = 0;
//                FileSize = TypedTitle.Length + TypedText.Length;
//                FileSize = FileSize / 100;
//                Save();
//            }

//            if (GUI.Button(new Rect(183, 24, 45, 21), "Compile"))
//            {
//                TestCode.KeywordCheck("Player", TypedText);
//            }

//            if (SelectedMenu > 0)
//            {
//                if (GUI.Button(new Rect(182, 24, 50, 21), "Cancel"))
//                {
//                    if (showSave == true)
//                    {
//                        ShowFileContent = true;
//                    }
//                    ShowFileNameMaker = false;
//                    ShowFileOpen = false;
//                    SelectedMenu = 0;
//                }
//            }
//        }
//        else
//        {
//            if (SelectedMenu > 0)
//            {
//                if (GUI.Button(new Rect(144, 24, 50, 21), "Cancel"))
//                {
//                    if (showSave == true)
//                    {
//                        ShowFileContent = true;
//                    }
//                    ShowFileNameMaker = false;
//                    ShowFileOpen = false;
//                    SelectedMenu = 0;
//                }
//            }
//        }

//        if (GUI.Button(new Rect(83, 24, 60, 21), "Save As"))
//        {
//            if (SaveLocation == "")
//            {
//                SaveLocation = "C:/Documents";
//            }
//            ShowFileNameMaker = true;
//            ShowFileContent = false;
//            ShowFileOpen = false;
//            SelectedMenu = 2;
//        }

//        if (GUI.Button(new Rect(2, 24, 37, 21), "New"))
//        {
//            ShowFileNameMaker = false;
//            ShowFileContent = false;
//            ShowFileOpen = false;
//            CurrentWorkingTitle = "Untitled";
//            SaveLocation = "";
//            TypedTitle = "";
//            TypedText = "";
//            SelectedMenu = 0;
//        }

//        if (GUI.Button(new Rect(40, 24, 42, 21), "Open"))
//        {
//            ShowFileOpen = true;
//            ShowFileNameMaker = false;
//            SelectedMenu = 2;
//        }

//        if (ShowFileOpen == true)
//        {
//            DiskCheck();
//            if (Files.Count > 0)
//            {
//                scrollpos = GUI.BeginScrollView(new Rect(2, 50, 120, 100), scrollpos, new Rect(0, 0, 0, scrollsize * 24));
//                for (scrollsize = 0; scrollsize < Files.Count; scrollsize++)
//                {
//                    if (GUI.Button(new Rect(0, 24 * scrollsize, 100, 21), Files[scrollsize].Name))
//                    {
//                        SelectedDocument = FileIndex[scrollsize];
//                        Open();
//                    }
//                }
//                GUI.EndScrollView();
//            }
//        }
//    }
//}
