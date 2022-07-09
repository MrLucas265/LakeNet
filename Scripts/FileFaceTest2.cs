using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class FileFaceTest2 : MonoBehaviour
{

    public bool run;
    public bool CheckFileCounts;
    public bool MoveingFilePaths;
    public bool PrintFiles;
    public bool CheckExistingPeopleBool;

    public bool AddNewPersonCheck;

    public List<string> TempFaceFilePaths = new List<string>();

    public List<string> AllFaceFilePaths = new List<string>();

    public string DirectoryPath;

    public int FileCount;
    public int TotalFileCount;
    public int FinalFileCount;
    public int PrintFileCount;
    public int PeopleFacesCheckedCount;

    public List<PeopleFaceTestSys> ExistingPeople = new List<PeopleFaceTestSys>();
    public List<int> ExistingFaces = new List<int>();

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (CheckFileCounts == true)
        {
            CheckDirectoryFileCount(DirectoryPath);
        }

        if (run == true)
        {
            AddListOfFilePaths(DirectoryPath);
        }

        if (MoveingFilePaths == true)
        {
            MoveFilePaths();
        }

        if (PrintFiles == true)
        {
            PrintAllFilePaths();
        }

        if (TempFaceFilePaths.Count > 0)
        {
            if (TempFaceFilePaths.Count >= FileCount)
            {
                run = false;
                MoveingFilePaths = true;
            }
        }

        if (FinalFileCount >= FileCount)
        {
            MoveingFilePaths = false;
            ClearFileCounts();
        }

        if (PrintFileCount >= TotalFileCount)
        {
            PrintFiles = false;
        }

        if (CheckExistingPeopleBool == true)
        {
            CheckExistingPeople();
        }

        if (ExistingFaces.Count >= PeopleFacesCheckedCount)
        {
            CheckExistingPeopleBool = false;
        }

        if (AddNewPersonCheck == true)
        {
            AddNewPerson();
        }
    }

    void CheckDirectoryFileCount(string FilePath)
    {
        DirectoryInfo dir = new DirectoryInfo(FilePath);
        FileInfo[] info = dir.GetFiles("*.png");
        FileCount = info.Length;
        Array.Clear(info, 0, info.Length);
        CheckFileCounts = false;
        run = true;

    }

    void PrintAllFilePaths()
    {
        for (int i = 0; i < AllFaceFilePaths.Count; i++)
        {
            PrintFileCount = PrintFileCount + 1;
            print(AllFaceFilePaths[i]);
        }
    }

    void ClearFileCounts()
    {
        TotalFileCount += FileCount;
        FileCount = 0;
        FinalFileCount = 0;
        MoveingFilePaths = false;
        CheckExistingPeopleBool = true;
    }

    void AddListOfFilePaths(string FilePath)
    {
        foreach (string file in Directory.GetFiles(FilePath))
        {
            if (file.Contains(".png"))
            {
                TempFaceFilePaths.Add(file);
            }
        }
    }

    void MoveFilePaths()
    {
        for (int i = 0; i < TempFaceFilePaths.Count; i++)
        {
            FinalFileCount = FinalFileCount + 1;
            AllFaceFilePaths.Add(TempFaceFilePaths[i]);
        }
    }

    void CheckExistingPeople()
    {
        for (int i = 0; i < ExistingPeople.Count; i++)
        {
            if (!ExistingFaces.Contains(ExistingPeople[i].Photo))
            {
                PeopleFacesCheckedCount = PeopleFacesCheckedCount + 1;
                ExistingFaces.Add(ExistingPeople[i].Photo);
            }
        }
    }

    void AddNewPerson()
    {
        int SelectedFileName = UnityEngine.Random.Range(0, AllFaceFilePaths.Count);

        if (ExistingFaces.Contains(SelectedFileName))
        {
            SelectedFileName = UnityEngine.Random.Range(0, AllFaceFilePaths.Count);
        }
        else
        {
            ExistingPeople.Add(new PeopleFaceTestSys("test", SelectedFileName));
        }

        AddNewPersonCheck = false;
    }
}