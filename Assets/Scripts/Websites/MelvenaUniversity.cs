﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;

public class MelvenaUniversity : MonoBehaviour
{
    private GameObject Computer;
    private InternetBrowser ib;

    public Vector2 scrollpos = Vector2.zero;
    public int scrollsize;

    public string UsrName;
    public string password;
    public string SiteAdminPass;

    public int Index;

    public bool logged;

    public int SelectedAccount;

    public float Timer;
    public float ResetTime;

    public bool Searching;
    public string EntryName;

    public int SelectedIndex;

    public string CurrentName;

    public int FoundPostion;

    public bool RandomName;

    public string RandomNameS;


    public int TestIndex;
    public bool DisplayInfo;
    public bool Search;
    public string Name;

    public List<string> Names = new List<string>();

    public string Name1;
    public string Name2;

    public bool SameName;

    public bool Found;

    public float widthTest;
    public float heightTest;

    public string TempClass;
    public string TempGrade;

    private GameObject System;
    private AppMan appman;

    public List<WebSecSystem> Secuirty = new List<WebSecSystem>();

    // Use this for initialization
    void Start()
    {
        Computer = GameObject.Find("Applications");
        System = GameObject.Find("System");
        ib = Computer.GetComponent<InternetBrowser>();
        appman = System.GetComponent<AppMan>();
        //WebSearch();
        //PlayerInfo();
        ResetTime = 0.15f;
        Timer = ResetTime;
        SelectedIndex = -1;
        EntryName = "";
        Name = "";
        RandomNameS = "";
    }

    // Update is called once per frame
    void Update()
    {

    }

    void SecuritySystems()
    {

    }

    public void RenderSite()
    {
        switch (ib.AddressBar)
        {
            case "www.melvena.com":

                //Test();
                DatabaseSearch();

                break;
        }
    }

    void DatabaseSearch()
    {
        if (Names.Count < PersonController.control.People.Count)
        {
            for (int NameIndex = 0; NameIndex < PersonController.control.People.Count; NameIndex++)
            {
                Names.Add(PersonController.control.People[NameIndex].Name);
            }
        }

        if (EntryName == CurrentName)
        {
            Searching = false;
            Timer = ResetTime;
            Index = 0;
        }

        Name1 = EntryName;
        Name2 = CurrentName;

        if (Name1 == Name2)
        {
            SameName = true;
        }
        else
        {
            SameName = false;
        }

        if (DisplayInfo == false)
        {
            EntryName = GUI.TextField(new Rect(10, 35, 150, 22), EntryName);

            if (GUI.Button(new Rect(160, 35, 22, 22), "Search"))
            {
                if (EntryName != "")
                {
                    Searching = true;
                    SelectedIndex = -1;
                    DisplayInfo = true;
                }
            }
        }
        else
        {

            if (GUI.Button(new Rect(10, 35, 75, 22), "< Back"))
            {
                DisplayInfo = false;
                Searching = false;
                Found = false;
            }

            if (Found == true)
            {
                //TempMaritalStatus = PersonController.control.People[FoundPostion].Academics.CollageQualifications + " " + PersonController.control.People[FoundPostion].Academics.CollageGrade;

                GUI.DrawTexture(new Rect(20, 100, 125, 150), PersonController.control.People[FoundPostion].Photo);

                GUI.TextField(new Rect(10, 70, 150, 22), Names[FoundPostion]);

                string Day = PersonController.control.People[FoundPostion].DOB.Day.ToString();
                string Month = PersonController.control.People[FoundPostion].DOB.Month.ToString();
                string Year = PersonController.control.People[FoundPostion].DOB.Year.ToString();

                GUI.TextField(new Rect(200, 100, 150, 22), Day + "-" + Month + "-" + Year);

                GUI.TextField(new Rect(200, 130, 150, 22), PersonController.control.People[FoundPostion].PersonID);

                TempClass = GUI.TextField(new Rect(200, 160, 150, 22), TempClass);

                TempGrade = GUI.TextField(new Rect(200, 190, 150, 22), TempGrade);

                if (GUI.Button(new Rect(200, 230, 100, 22), "Apply"))
                {
                    PersonController.control.People[FoundPostion].University.Qualifications = TempClass;

                    PersonController.control.People[FoundPostion].University.Grade = TempGrade;

                    appman.PromptTitle = "Committed Changes";
                    appman.PromptMessage = "Changes has been successfully been committed.";
                    appman.SelectedApp = "Error Prompt";
                    appman.SoundSelect = 0;
                    appman.PlaySound = true;
                }

            }
            else
            {
                GUI.DrawTexture(new Rect(20, 100, 125, 150), PersonController.control.People[Index].Photo);

                GUI.TextField(new Rect(10, 70, 150, 22), Names[Index]);

                string Day = PersonController.control.People[Index].DOB.Day.ToString();
                string Month = PersonController.control.People[Index].DOB.Month.ToString();
                string Year = PersonController.control.People[Index].DOB.Year.ToString();

                GUI.TextField(new Rect(200, 100, 150, 22), Day + "-" + Month + "-" + Year);

                GUI.TextField(new Rect(200, 130, 150, 22), PersonController.control.People[Index].PersonID);

                GUI.TextField(new Rect(200, 160, 150, 22), PersonController.control.People[Index].PersonalStatus);

                GUI.TextField(new Rect(200, 190, 150, 22), PersonController.control.People[Index].MaritalStatus);
            }
        }

        for (int i = 0; i < PersonController.control.People.Count; i++)
        {
            if (EntryName == Names[i])
            {
                FoundPostion = i;
            }
        }
        //FoundPostion = PersonController.control.People.IndexOf(EntryName);

        if (Index < Names.Count)
        {
            CurrentName = Names[Index];
        }

        if (Index >= Names.Count)
        {
            Searching = false;
            Timer = ResetTime;
            Index = 0;
        }

        if (Names[Index] == EntryName)
        {
            TempClass = PersonController.control.People[FoundPostion].University.Qualifications;

            TempGrade = PersonController.control.People[FoundPostion].University.Grade;

            SelectedIndex = Index;
            Searching = false;
            Found = true;
        }

        if (Searching == true)
        {
            if (Names.Count > 0)
            {
                Timer -= Time.deltaTime;

                if (Timer <= 0)
                {
                    Index++;
                    Timer = ResetTime;
                }

                //GUI.TextField(new Rect(10, 50, 150, 22), Names[Index]);
            }
        }

        if (SelectedIndex != -1)
        {
            Searching = false;

            //GUI.TextField(new Rect(10, 50, 150, 22), Names[SelectedIndex]);
        }
    }
}
