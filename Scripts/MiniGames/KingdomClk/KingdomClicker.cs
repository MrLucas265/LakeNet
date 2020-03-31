using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KingdomClicker : MonoBehaviour 
{
	public string MenuSelection;
	// ACCOUNT INFO
	public bool DisplayNotfications;
	public string Username;
	public string Password;
	public bool LoggedIn;
	public int ProfileIndex;
	public bool ShowLoginButton;
	public bool ShowCreateAccount;
	//Timer
	public float Timer = 1;
	public float CoolDown = 1;
	//Menus
	bool showGrow = false;
	public Vector2 scrollpos = Vector2.zero;
	public int scrollsize;
	public int Select;
	// Build Menus
	public int BuildMenuSelect;
	public int CurrentBuild;
	public string SelectedNotes;
	public float MaxBuildProgress;
	public float BuildingCost;
	// UI
	public Rect TownButton;

	void Start()
	{
		//KingClkSAL.kingsal.Load ();
		TownButton = new Rect(400, 275, 100, 21);
	}

	void Timers()
	{
		if (Timer >= 0)
		{
			Timer -= Time.deltaTime;
		}

		if (Timer < 0)
		{
			TimedSystems();
			KingClkSAL.kingsal.Save();
			Timer = CoolDown;
		}
	}

	void TimedSystems()
	{
		KingClkSAL.kingsal.Health [1] += 0.25f * KingClkSAL.kingsal.BuildingUpgrades[5];
		if (KingClkSAL.kingsal.BuildingUpgrades [0] < KingClkSAL.kingsal.BuildingUpgrades [1])
		{
			if (KingClkSAL.kingsal.Progress [0] >= KingClkSAL.kingsal.MaxHealth[0])
			{
				KingClkSAL.kingsal.BuildingUpgrades[0]+=1;
				KingClkSAL.kingsal.Health[0] = KingClkSAL.kingsal.MaxHealth[0];
				KingClkSAL.kingsal.Progress [0] = 0;
			}

			if (KingClkSAL.kingsal.Progress [0] < KingClkSAL.kingsal.MaxHealth[0]) 
			{
				KingClkSAL.kingsal.Progress [0] += 0.1f;
			}
		}
	}

	void CurrencyCheck()
	{
		if (KingClkSAL.kingsal.NewPlayer == true) 
		{
			// CURRENCY
			if (KingClkSAL.kingsal.CurrencyAmt.Count < 20)
			{
				for (scrollsize = 0; scrollsize < 20; scrollsize++) 
				{
					KingClkSAL.kingsal.CurrencyAmt.Add (0);
				}
			}

			//BUILDINGS
			if (KingClkSAL.kingsal.BuildingUpgrades.Count < 20)
			{
				for (scrollsize = 0; scrollsize < 20; scrollsize++) 
				{
					KingClkSAL.kingsal.BuildingUpgrades.Add (0);
				}
			}

			// TOOLS
			if (KingClkSAL.kingsal.Tools.Count < 20)
			{
				for (scrollsize = 0; scrollsize < 20; scrollsize++) 
				{
					KingClkSAL.kingsal.Tools.Add (1);
				}
			}

			// HEALTH
			if (KingClkSAL.kingsal.Health.Count < 20)
			{
				for (scrollsize = 0; scrollsize < 20; scrollsize++) 
				{
					KingClkSAL.kingsal.Health.Add (0);
				}
			}

			// MAX HEALTH
			if (KingClkSAL.kingsal.MaxHealth.Count < 20)
			{
				for (scrollsize = 0; scrollsize < 20; scrollsize++) 
				{
					KingClkSAL.kingsal.MaxHealth.Add (0);
				}
			}

			// PROGRESS
			if (KingClkSAL.kingsal.Progress.Count < 20)
			{
				for (scrollsize = 0; scrollsize < 20; scrollsize++) 
				{
					KingClkSAL.kingsal.Progress.Add (0);
				}
			}
		}

		if (KingClkSAL.kingsal.Progress.Count >= 20) 
		{
			DefaultStats();
		}
	}

	void DefaultStats()
	{
		if (KingClkSAL.kingsal.BuildingUpgrades [2] <= 0)
		{
			KingClkSAL.kingsal.BuildingUpgrades [1] = 1;
			KingClkSAL.kingsal.BuildingUpgrades [2] = 1;
			KingClkSAL.kingsal.MaxHealth[0] = KingClkSAL.kingsal.Health [0] = 10 * KingClkSAL.kingsal.BuildingUpgrades [2];
		}
	}

	void Lumber()
	{
		if (showGrow == false)
		{
			if (GUI.Button (new Rect (0, 200, 100, 21), "Grow new tree")) 
			{
				showGrow = true;
			}

			if (KingClkSAL.kingsal.BuildingUpgrades[0] >= 1) 
			{
				if (GUI.Button (new Rect (100, 100, 100, 100), "Tree")) 
				{
					if (KingClkSAL.kingsal.Health [0] > 0)
					{
						KingClkSAL.kingsal.CurrencyAmt[3] += KingClkSAL.kingsal.Tools [0];
						KingClkSAL.kingsal.Health [0] -= KingClkSAL.kingsal.Tools [0];
					}

					if (KingClkSAL.kingsal.Health [0] <= 0)
					{
						KingClkSAL.kingsal.BuildingUpgrades [0] -= 1;
						KingClkSAL.kingsal.Health[0] = KingClkSAL.kingsal.MaxHealth[0];
					}
				}
			}
		}

		if (showGrow == true)
		{
			if (GUI.Button (new Rect (0, 200, 100, 21), "Cut Tree")) 
			{
				showGrow = false;
			}

			if (KingClkSAL.kingsal.BuildingUpgrades [0] < KingClkSAL.kingsal.BuildingUpgrades [1])
			{
				if (GUI.Button (new Rect (100, 100, 100, 100), "Grow Tree")) 
				{
					if (KingClkSAL.kingsal.BuildingUpgrades [0] < KingClkSAL.kingsal.BuildingUpgrades [1])
					{
						if (KingClkSAL.kingsal.BuildingUpgrades [0] < KingClkSAL.kingsal.BuildingUpgrades [1])
						{
							if (KingClkSAL.kingsal.Progress [0] >= KingClkSAL.kingsal.MaxHealth[0])
							{
								KingClkSAL.kingsal.Health[0] = KingClkSAL.kingsal.MaxHealth[0];
								KingClkSAL.kingsal.BuildingUpgrades[0]+=1;
								KingClkSAL.kingsal.Progress [0] = 0;
							}

							if (KingClkSAL.kingsal.Progress [0] < KingClkSAL.kingsal.MaxHealth[0]) 
							{
								KingClkSAL.kingsal.Progress [0] += 1;
							}
						}
					}
				}
			}
		}

		if (GUI.Button (new Rect (TownButton), "To Town")) 
		{
			MenuSelection = "Town";
		}
	}

	void Farm()
	{
		if (GUI.Button (new Rect (100, 100, 50, 50), "Sheep")) 
		{
			if (KingClkSAL.kingsal.Health [1] > 0)
			{
				KingClkSAL.kingsal.CurrencyAmt[8] += KingClkSAL.kingsal.Tools [2];
				KingClkSAL.kingsal.Health [1] -= KingClkSAL.kingsal.Tools [2];
			}
		}

		if (GUI.Button (new Rect (TownButton), "To Town")) 
		{
			MenuSelection = "Town";
		}
	}

	void Build()
	{
		if (GUI.Button (new Rect (TownButton), "To Town")) 
		{
			MenuSelection = "Town";
		}

		switch (CurrentBuild)
		{
		case 0:
			GUI.TextArea (new Rect (400, 100, 100, 100), SelectedNotes);

			if (GUI.Button (new Rect (0, 100, 100, 21), "Expand Forest Area")) 
			{
				BuildMenuSelect = 1;
			}

			if (GUI.Button (new Rect (0, 100, 100, 21), "Add more trees")) 
			{
				BuildMenuSelect = 4;
			}

			if (GUI.Button (new Rect (101, 100, 100, 21), "Farm")) 
			{
				BuildMenuSelect = 2;
			}

			if (KingClkSAL.kingsal.BuildingUpgrades [3] > 0) 
			{
				if (GUI.Button (new Rect (100, 122, 100, 21), "Sheep Paddocks")) 
				{
					BuildMenuSelect = 3;
				}
			}
			break;

		case 1:
			if (GUI.Button (new Rect (0, 180, 100, 21), "Expand Area")) 
			{
				if (KingClkSAL.kingsal.Progress[0] < MaxBuildProgress && KingClkSAL.kingsal.CurrencyAmt [3] >= BuildingCost)
				{
					KingClkSAL.kingsal.CurrencyAmt [3] -= BuildingCost;
					KingClkSAL.kingsal.Progress[0] += KingClkSAL.kingsal.Tools [2];
				}
				if (KingClkSAL.kingsal.Progress[0] >= MaxBuildProgress)
				{
					KingClkSAL.kingsal.BuildingUpgrades[1]++;
					KingClkSAL.kingsal.Progress[0] = 0;
					CurrentBuild = 0;
				}
			}
			break;
		}


		switch (BuildMenuSelect)
		{
		case 1:
			SelectedNotes = "This upgrade is to expand the plantable area for trees";
			if (GUI.Button (new Rect (0, 180, 100, 21), "Confirm")) 
			{
				BuildingCost = 500 + KingClkSAL.kingsal.BuildingUpgrades [2] * 1.15f;
				MaxBuildProgress = 1 + KingClkSAL.kingsal.BuildingUpgrades [2] * 1.25f;
				CurrentBuild = 1;
				BuildMenuSelect = 0;
			}
			break;
		}
	}

	void MainMenu()
	{
		if (GUI.Button (new Rect (200, 100, 50, 50), "Lumberyard")) 
		{
			MenuSelection = "Lumberyard";
		}

		if (GUI.Button (new Rect (0, 250, 100, 21), "Build Menu")) 
		{
			MenuSelection = "Build";
		}

		if (KingClkSAL.kingsal.BuildingUpgrades [3] > 0) 
		{
			if (GUI.Button (new Rect (300, 100, 50, 50), "Farm")) 
			{
				MenuSelection = "Farm";
			}
		}
	}

	void Login()
	{
		if(ShowCreateAccount == false && ShowLoginButton == false)
		{
			if (GUI.Button (new Rect (0, 100, 200, 21), "Create Account")) 
			{
				ShowCreateAccount = true;
			}
			if (GUI.Button (new Rect (200, 100, 200, 21), "Already have an account?"))
			{
				ShowLoginButton = true;
			}
		}

		if (DisplayNotfications == true) 
		{
			if (GUI.Button (new Rect (50, 120, 300, 21), "Incorrect information please retry"))
			{
				DisplayNotfications = false;
			}
		}

		if (ShowCreateAccount == true)
		{
			Username = GUI.TextField(new Rect(85, 55, 120, 20), Username, 500);
			Password = GUI.TextField(new Rect(85, 75, 120, 20), Password, 500);
			GUI.Label(new Rect(3, 55, 500, 500), "User Name: ");
			GUI.Label(new Rect(3, 75, 500, 500), "Password: ");


			if (GUI.Button (new Rect (150, 250, 100, 21), "Back"))
			{
				ShowCreateAccount = false;
			}


			if (GUI.Button (new Rect (250, 250, 100, 21), "Create Account")) 
			{
				if (!KingdomProf.kingprof.ProfileName.Contains (Username))
				{
					KingdomProf.kingprof.ProfileName.Add(Username);
					KingdomProf.kingprof.ProfilePass.Add(Password);
					KingdomProf.kingprof.Save();
					ProfileIndex = KingdomProf.kingprof.ProfileName.IndexOf(Username);
					KingClkSAL.kingsal.ProfileName = KingdomProf.kingprof.ProfileName[ProfileIndex];
					KingClkSAL.kingsal.NewPlayer = true;
					KingClkSAL.kingsal.Save();
					ShowCreateAccount = false;
					ShowLoginButton = true;
				}
			}
		}

		if (ShowLoginButton == true)
		{
			Username = GUI.TextField(new Rect(85, 55, 120, 20), Username, 500);
			Password = GUI.TextField(new Rect(85, 75, 120, 20), Password, 500);
			GUI.Label(new Rect(3, 55, 500, 500), "User Name: ");
			GUI.Label(new Rect(3, 75, 500, 500), "Password: ");

			if (GUI.Button (new Rect (150, 250, 100, 21), "Back"))
			{
				ShowLoginButton = false;
			}

			if(GUI.Button(new Rect(250,250,100,21),"Login"))
			{
				ProfileIndex = KingdomProf.kingprof.ProfileName.IndexOf(Username);

				if (!KingdomProf.kingprof.ProfileName.Contains (Username)) 
				{

				}

				if (KingdomProf.kingprof.ProfileName.Contains (Username))
				{
					if (Username != KingdomProf.kingprof.ProfileName[ProfileIndex] || Password != KingdomProf.kingprof.ProfilePass[ProfileIndex])
					{
						DisplayNotfications = true;
					}

					if (Username == KingdomProf.kingprof.ProfileName[ProfileIndex] && Password == KingdomProf.kingprof.ProfilePass[ProfileIndex]) 
					{
						KingClkSAL.kingsal.ProfileName = KingdomProf.kingprof.ProfileName[ProfileIndex];
						KingClkSAL.kingsal.Load();
						DisplayNotfications = false;
						LoggedIn = true;
						MenuSelection = "Town";
						CurrencyCheck();
					}
				}
			}
		}
	}

	public void GameRender()
	{
		if (MenuSelection != "Login")
		{
			if (MenuSelection != "")
			{
				GUI.Box (new Rect (0, 60, 100, 21), "Coin " + KingClkSAL.kingsal.CurrencyAmt[0]);
				GUI.Box (new Rect (101, 60, 100, 21), "Wood " + KingClkSAL.kingsal.CurrencyAmt[3]);
				//GUI.Box (new Rect (202, 60, 100, 21), "Wood " + KingClkSAL.kingsal.CurrencyAmt[3]);
				Timers();
			}
		}

		if (MenuSelection == "") 
		{
			MenuSelection = "Login";
		}

		switch (MenuSelection) 
		{
		case "Login":
			Login();
			break;

		case "Town":
			MainMenu();
			break;

		case "Lumberyard":
			Lumber();
			break;

		case "Farm":
			Farm();
			break;

		case "Build":
			Build();
			break;
		}
	}
}
