﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Serial : MonoBehaviour
{
    public List<string> Words = new List<string>();
    public List<string> Words1 = new List<string>();
    public List<string> VersionLines = new List<string>();
    public string SerialKey;
    // Use this for initialization
    void Start()
    {
        AddPasswordsList();
        //AddWordDatabase();
    }

    // Update is called once per frame
    void Update()
    {

    }

    //	public void AddWordDatabase()
    //	{
    //		readTextFile(Application.dataPath + "/StreamingAssets");
    //	}

    public void AddPasswordsList()
    {
        TextAsset txt = (TextAsset)Resources.Load("Serials", typeof(TextAsset));
        VersionLines = new List<string>(txt.text.Split('\n'));
        if (VersionLines.Count > 0)
        {
            SerialKey = VersionLines[0];
        }
    }

    void readTextFile(string file_path)
    {
        StreamReader inp_stm = new StreamReader(file_path);

        while (!inp_stm.EndOfStream)
        {
            string inp_ln = inp_stm.ReadLine();
            Words.Add(inp_ln);
        }

        inp_stm.Close();
    }

    void readTextFile1(string file_path)
    {
        StreamReader inp_stm = new StreamReader(file_path);

        while (!inp_stm.EndOfStream)
        {
            string inp_ln = inp_stm.ReadLine();
            Words1.Add(inp_ln);
        }

        inp_stm.Close();
    }
}
