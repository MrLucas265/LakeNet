﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class PasswordList : MonoBehaviour
{
	public List<string> Words = new List<string>();
	public List<string> Words1 = new List<string>();
	public List<string> PasswordWords = new List<string>();
	// Use this for initialization
	void Start () 
	{
		//		AddPasswordsList();
		//		AddWordDatabase();
		PasswordListResource();
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

//	public void AddWordDatabase()
//	{
//		readTextFile("Assets/Resources/wordlist.txt");
//	}
//
//	public void AddPasswordsList()
//	{
//		readTextFile1("Assets/Resources/wordlist.txt");
//	}

	public void PasswordListResource()
	{
		TextAsset txt = (TextAsset)Resources.Load ("wordlist", typeof(TextAsset));
		PasswordWords = new List<string>(txt.text.Split('\n'));
	}

	void readTextFile(string file_path)
	{
		StreamReader inp_stm = new StreamReader(file_path);

			while(!inp_stm.EndOfStream)
			{
				string inp_ln = inp_stm.ReadLine();
				Words.Add(inp_ln);
			}

		inp_stm.Close( );  
	}

	void readTextFile1(string file_path)
	{
		StreamReader inp_stm = new StreamReader(file_path);

		while(!inp_stm.EndOfStream)
		{
			string inp_ln = inp_stm.ReadLine();
			Words1.Add(inp_ln);
		}

		inp_stm.Close( );  
	}
}
