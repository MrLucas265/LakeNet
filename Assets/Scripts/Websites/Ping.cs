﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ping : MonoBehaviour 
{
	private GameObject Computer;
	private GameObject Applications;
	private InternetBrowser ib;

	public List<string> ListOfSites = new List<string>();
	public string SearchSites;
	public string Searched;
	public string Inputted;
	public bool SearchDone;
	public int SearchCount;
	public bool UpdateSearchUI;

	public Vector2 scrollpos = Vector2.zero;
	public int scrollsize;

	// Use this for initialization
	void Start () 
	{
		WebSearch();
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	void UpdateSiteList()
	{
		ListOfSites.Clear();
		ListOfSites.Add("LEC Bank");
		ListOfSites.Add("Clicker");
		ListOfSites.Add("Jaildew Corp");
		ListOfSites.Add("Unicom LTD");
		ListOfSites.Add("Becas Systems");
		ListOfSites.Add("Melvena");
		ListOfSites.Add("International Social Database");
        ListOfSites.Add("TUG");
        ListOfSites.Add("Store");
    }

	void SearchCheck()
	{
		for (SearchCount = 0; SearchCount < ListOfSites.Count; SearchCount++) 
		{
			if (!ListOfSites[SearchCount].ToLower().Contains (Inputted.ToLower())) 
			{
				ListOfSites.RemoveAt (SearchCount);
			}
		}
	}

	void EnterSearch()
	{
		switch(Searched)
		{
		case "LEC Bank":
			ib.Inputted = "www.lecbank.com";
			ib.AddressBar = "www.lecbank.com";
			Searched = "";
			Inputted = "";
			SearchSites = "";
			break;
		case "Clicker":
			ib.Inputted = "www.clicker.com";
			ib.AddressBar = "www.clicker.com";
			Searched = "";
			Inputted = "";
			SearchSites = "";
			break;
		case "Jaildew Corp":
			ib.Inputted = "www.jaildew.com";
			ib.AddressBar = "www.jaildew.com";
			Searched = "";
			Inputted = "";
			SearchSites = "";
			break;
		case "Unicom LTD":
			ib.Inputted = "www.unicom.com";
			ib.AddressBar = "www.unicom.com";
			Searched = "";
			Inputted = "";
			SearchSites = "";
			break;
		case "Becas Systems":
			ib.Inputted = "www.becassystems.com";
			ib.AddressBar = "www.becassystems.com";
			Searched = "";
			Inputted = "";
			SearchSites = "";
			break;
		case "Melvena":
			ib.Inputted = "www.melvena.com";
			ib.AddressBar = "www.melvena.com";
			Searched = "";
			Inputted = "";
			SearchSites = "";
			break;
		case "International Social Database":
            ib.Inputted = "www.isd.com";
            ib.AddressBar = "www.isd.com";
            Searched = "";
            Inputted = "";
            SearchSites = "";
            break;
        case "TUG":
            ib.Inputted = "www.tugs.com";
            ib.AddressBar = "www.tugs.com";
            Searched = "";
            Inputted = "";
            SearchSites = "";
            break;
        case "Store":
            ib.Inputted = "www.store.com";
            ib.AddressBar = "www.store.com";
            Searched = "";
            Inputted = "";
            SearchSites = "";
            break;
        }
	}

	void WebSearch()
	{
		Applications = GameObject.Find("Applications");
		ib = Applications.GetComponent<InternetBrowser>();
	}

	public void RenderSite()
	{
		switch(ib.AddressBar)
		{
		case "www.ping.com":

			if(GUI.Button(new Rect(210,75,100,20),"Search"))
			{
				if(SearchSites != "")
				{
					UpdateSiteList();
					SearchDone = false;
					Inputted = SearchSites;
					UpdateSearchUI = true;
				}
			}

			if (Event.current.type == EventType.keyDown && Event.current.keyCode == KeyCode.Return) 
			{
				if(SearchSites != "")
				{
					UpdateSiteList();
					SearchDone = false;
					Inputted = SearchSites;
					UpdateSearchUI = true;
				}
			}

			SearchSites = GUI.TextField(new Rect(5,75,200,20),SearchSites);

			if(SearchSites == "")
			{
				UpdateSearchUI = false;
			}

			if(UpdateSearchUI == true)
			{
				scrollpos = GUI.BeginScrollView(new Rect(5, 100, 150, 100), scrollpos, new Rect(0, 0, 0, scrollsize * 20));
				for (scrollsize = 0; scrollsize < ListOfSites.Count; scrollsize++)
				{
					SearchCheck();
					if(Inputted != "" && ListOfSites.Count > 0)
					{
						if(GUI.Button(new Rect(5,scrollsize * 20,100,20),ListOfSites[scrollsize]))
						{
							Searched = ListOfSites[scrollsize].ToString();
							EnterSearch();
						}
					}
				}
				GUI.EndScrollView();
			}

			break;
		}
	}
}