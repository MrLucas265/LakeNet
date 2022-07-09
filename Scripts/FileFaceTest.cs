using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class FileFaceTest : MonoBehaviour 
{
	public bool AddNewPersonCheck;
	public Texture2D[] Tex2;

	public List<PeopleFaceTestSys> ExistingPeople = new List<PeopleFaceTestSys>();
	public List<int> ExistingFaces = new List<int>();

	public int SelectedExistingPerson;

	public int ExistCount;

	void Awake()
	{
		Tex2 = Resources.LoadAll<Texture2D>("Extracted Images/psi-0.3");
		Resources.UnloadUnusedAssets();
	}
	// Use this for initialization
	void Start () 
	{

	}
	
	// Update is called once per frame
	void Update () 
	{
		if(AddNewPersonCheck == true)
		{
			CheckExistingPeople();

			if (ExistCount >= ExistingPeople.Count)
			{
				AddNewPerson();
			}
		}
	}

	void CheckExistingPeople()
	{
		for (int i = 0; i < ExistingPeople.Count; i++)
		{
			if (!ExistingFaces.Contains(ExistingPeople[i].Photo))
			{
				ExistCount = ExistCount + 1;
				ExistingFaces.Add(ExistingPeople[i].Photo);
			}
		}
	}

	void AddNewPerson()
	{
		int SelectedFileName = UnityEngine.Random.Range(0, Tex2.Length);

		if (ExistingFaces.Contains(SelectedFileName))
		{
			SelectedFileName = UnityEngine.Random.Range(0, Tex2.Length);
		}
		else
		{
			ExistingPeople.Add(new PeopleFaceTestSys("test", SelectedFileName));
		}

		AddNewPersonCheck = false;
	}

	void OnGUI()
	{
		if (ExistingPeople.Count > 0)
		{
			if (Tex2.Length > 0)
			{
				int selectedPersonPhoto = ExistingPeople[SelectedExistingPerson].Photo;
				GUI.DrawTexture(new Rect(Screen.width / 2, Screen.height / 2, 128, 128), Tex2[selectedPersonPhoto]);
			}

			if (GUI.Button(new Rect(Screen.width / 1.6f, Screen.height / 2, 32, 32), ">"))
			{
				if (SelectedExistingPerson < ExistingPeople.Count - 1)
				{
					SelectedExistingPerson++;
				}
			}

			if (GUI.Button(new Rect(Screen.width / 1.6f, Screen.height / 2.3f, 64, 32), "S: " + SelectedExistingPerson))
			{
				SelectedExistingPerson = UnityEngine.Random.Range(0, ExistingPeople.Count - 1);
			}

			if (GUI.Button(new Rect(Screen.width / 1.4f, Screen.height / 2.3f, 64, 32), "Add New"))
			{
				AddNewPersonCheck = true;
			}

			if (GUI.Button(new Rect(Screen.width / 2.2f, Screen.height / 2, 32, 32), "<"))
			{
				if (SelectedExistingPerson > 0)
				{
					SelectedExistingPerson--;
				}
			}
		}
	}
}
