using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseDef : MonoBehaviour 
{
	private MiniGameWeb mg;

	public float RespawnTimer;
	public float RespawnCoolDown;

	public float MoveTimer;
	public float MoveCoolDown;

	public float BaseHealth;
	public float BaseArmor;
	public float BaseDamage;

	public Rect basePOS;

	public Rect PlayerRect;

	public List<string> enemyName = new List<string>();
	public List<float> enemyHealth = new List<float>();
	public List<float> enemyPOSX = new List<float>();
	public List<float> enemyPOSY = new List<float>();
	public List<float> enemyMulti = new List<float>();
	public List<float> enemyDamage = new List<float>();
	public List<float> enemyDamageCoolDown = new List<float>();
	public List<float> enemyDamageCountDown = new List<float>();
	public List<bool> enemyHasGun = new List<bool>();
	public List<float> Range = new List<float>();

	public List<float> BulletPOSX = new List<float>();
	public List<float> BulletPOSY = new List<float>();
	public List<float> BulletDamage = new List<float>();
	public List<float> ABulletDamage = new List<float>(); 

	public List<Vector2> enemyPOS = new List<Vector2>();
	public List<Vector2> BulletPOS = new List<Vector2>();
	public int NameIndex;

	public float enemySpeed;

	public int Selected;

	public bool Check;

	public string GameMode;

	public bool GameRended;

	public float StartingPoint;

	// LevelSelected
	public int LevelSelect;

	// MenuSystem Stuff
	public string MainMenu;
	public string SubMenu;

	// Gamemode Stuff
	public bool StartGamemode;

	// KillCount Gamemode Stuff
	public bool EnableGamemodeKillCount;
	public int Kills;
	public int KillsNeeded;
	public int TotalKills;

	// Timed Gamemode
	public bool EnableGamemodeTimer;
	public float StartingTime;
	public float FinnishedTime;
	public float Timer;
	public float RemainingTime;
	public string TimerType;

	// Waved Survied Gamemode
	public bool EnableGamemodeWaves;
	public int CurrentWaves;
	public int MissionWaves;
	public int WavesLeft;

	// Resources Earned
	public bool EnableGamemodeResources;
	public int CurrentResources;
	public int ResourcesNeeded;
	public int ResourcesRemaining;

	// Base Upgrades
	public float BaseHealthUpgrade;
	// Weapon Upgrades
	// Extra Upgrades

	void Start () 
	{
		StartingPoint = 5;
		mg = GetComponent<MiniGameWeb>();
		basePOS = new Rect(460,100,40,200);
	}

	void Update ()
	{
		if (MainMenu == "") 
		{
			MainMenu = "Main Menu";
		}
		if (GameRended == true && MainMenu == "Game") 
		{
			Timers();
			BaseInteraction();
			BulletMath();

			if (StartGamemode == true)
			{
				GameModeSelect();
			}

			if (enemyPOS.Count > 0) 
			{
				if (enemyHealth [Selected] <= 0) 
				{
					enemyPOSX.RemoveAt(Selected);
					enemyPOSY.RemoveAt (Selected);
					enemyMulti.RemoveAt(Selected);
					enemyHealth.RemoveAt(Selected);
					enemyDamage.RemoveAt(Selected);
					enemyPOS.RemoveAt(Selected);
					enemyHasGun.RemoveAt(Selected);
					BulletDamage.RemoveAt(Selected);
					Range.RemoveAt(Selected);
					enemyDamageCountDown.RemoveAt(Selected);
					enemyDamageCoolDown.RemoveAt(Selected);
					enemyName.RemoveAt(Selected);

					Kills++;
					Selected = 0;
				}
			}
		}
	}

	void BaseInteraction()
	{
		for (int i = 0; i < enemyPOS.Count; i++) 
		{
			if (enemyPOSX[i] >= basePOS.x && enemyHasGun[i] == false)
			{
				enemyMulti [i] = 0;
				if (enemyDamageCountDown[i] >= 0)
				{
					enemyDamageCountDown[i] -= Time.deltaTime;
				}
				if (enemyDamageCountDown[i] < 0) 
				{
					BaseHealth -= enemyDamage [i];
					enemyDamageCountDown[i] = enemyDamageCoolDown[i];
				}
			}
			if (enemyPOSX[i] >= basePOS.x - 100 && enemyHasGun[i] == true)
			{
				enemyMulti [i] = 0;
				if (enemyDamageCountDown[i] >= 0)
				{
					enemyDamageCountDown[i] -= Time.deltaTime;
				}
				if (enemyDamageCountDown[i] < 0) 
				{
					BulletPOS.Add(new Vector2 (enemyPOSX[i], enemyPOSY[i]));
					BulletPOSX.Add(enemyPOSX[i]);
					BulletPOSY.Add(enemyPOSY[i]);
					ABulletDamage.Add(BulletDamage[i]);
					enemyDamageCountDown[i] = enemyDamageCoolDown[i];
				}
			}
		}

		if (BaseHealth <= 0) 
		{
			MainMenu = "Failed";
		}
	}

	void BulletMath()
	{
		if (BulletPOS.Count > 0) 
		{
			for (int i = 0; i < BulletPOS.Count; i++) 
			{
				//BulletPOSX[i] = enemyPOS[NameIndex].x;
				BulletPOSX[i] += 10 * Time.deltaTime * 25;

				if (BulletPOSX [i] >= basePOS.x) 
				{
					BaseHealth -= ABulletDamage[i];
					BulletPOS.RemoveAt(i);
					BulletPOSX.RemoveAt(i);
					BulletPOSY.RemoveAt(i);
					ABulletDamage.RemoveAt(i);
				}
			}
		}
	}

	void GameModeSelect()
	{
		switch (GameMode) 
		{
		case "Wave":

			break;

		case "Kill Count":
			if (Kills >= KillsNeeded) 
			{
				MainMenu = "Success";
			}
			break;

		case "Kill Count Countdown":
			if (Kills >= KillsNeeded && Timer >= 0)
			{
				MainMenu = "Success";
			}
			if (Timer < 0 && Kills < KillsNeeded) 
			{
				MainMenu = "Failed";
			}
			break;

		case "Kill Count Countup":
			if (Kills >= KillsNeeded && Timer < FinnishedTime)
			{
				MainMenu = "Success";
			}
			if (Timer >= FinnishedTime && Kills < KillsNeeded) 
			{
				MainMenu = "Failed";
			}
			break;

		case "Kill Count Holdout Countup":
			if (Kills >= KillsNeeded && Timer >= FinnishedTime)
			{
				MainMenu = "Success";
			}
			if (Timer >= FinnishedTime && Kills < KillsNeeded) 
			{
				MainMenu = "Failed";
			}
			break;

		case "Kill Count Holdout Countdown":
			if (Kills >= KillsNeeded && Timer <= 0)
			{
				MainMenu = "Success";
			}
			if (Timer <= 0 && Kills < KillsNeeded) 
			{
				MainMenu = "Failed";
			}
			break;

		case "Resource":

			break;
		}
	}

	void CreateEnemy()
	{
		int Select = 0;
		int PosY = 0;
		int MaxY = 260;

		Select = Random.Range (1, 4);

		switch (Select)
		{
		case 1:
			enemyName.Add ("Tank");
			//enemyPOSY.Add (Random.Range (100, 280));
			PosY = Random.Range (100, MaxY);
			enemyPOSY.Add (PosY);
			enemyPOSX.Add (StartingPoint);
			enemyMulti.Add (0.75f);
			enemyHealth.Add (2);
			BulletDamage.Add (0);
			enemyDamage.Add (0.1f);
			enemyDamageCoolDown.Add (0.05f);
			enemyDamageCountDown.Add (0);
			enemyHasGun.Add (false);
			Range.Add(0);
			//enemyPOS.Add (new Vector2(0,enemyPOSY));
			enemyPOS.Add (new Vector2(0,PosY));
			break;
		case 2:
			enemyName.Add("Scout");
			//enemyPOSY.Add(Random.Range (100, 280));
			PosY = Random.Range (100, MaxY);
			enemyPOSY.Add (PosY);
			enemyPOSX.Add(StartingPoint);
			enemyMulti.Add(1f);
			enemyHealth.Add(1);
			BulletDamage.Add (0);
			enemyDamage.Add (0.1f);
			enemyDamageCoolDown.Add(0.15f);
			enemyDamageCountDown.Add(0);
			enemyHasGun.Add (false);
			Range.Add(0);
			//enemyPOS.Add (new Vector2(0,enemyPOSY));
			enemyPOS.Add (new Vector2(0,PosY));
			break;
		case 3:
			enemyName.Add("Rifleman");
			// Movement Stuff
			PosY = Random.Range (100, MaxY);
			enemyPOSY.Add (PosY);
			enemyPOSX.Add(StartingPoint);
			enemyMulti.Add(1.5f);
			// Health
			enemyHealth.Add(1);
			// Damage Stuff
			enemyDamage.Add (0);
			enemyDamageCoolDown.Add(Random.Range(0.75f,1.25f));
			enemyDamageCountDown.Add(0);
			// Bullet Stuff
			BulletDamage.Add (1);
			enemyHasGun.Add (true);
			Range.Add(Random.Range(200,300));
			// Extra Pos
			enemyPOS.Add (new Vector2(0,PosY));
			break;
		}
	}

	void Timers()
	{
		
		if(enemyPOS.Count <= 50)
		{
			RespawnTimer += Time.deltaTime;

			if (RespawnTimer >= RespawnCoolDown)
			{
				RespawnTimer = 0;
				CreateEnemy();
			}
		}

		if (enemyPOS.Count > 0) 
		{
			for (int i = 0; i < enemyPOS.Count; i++) 
			{
				enemyPOSX [i] += enemySpeed * Time.deltaTime * enemyMulti[i];
			}
		}

		if (EnableGamemodeTimer == true)
		{
			if (TimerType == "Countdown")
			{
				Timer -= Time.deltaTime;
			}
			if (TimerType == "Countup")
			{
				Timer += Time.deltaTime;
			}
		}

	}

	public void MiniGameRender()
	{
		GUI.Box(basePOS,"Base");

		if (enemyPOS.Count > 0) 
		{
			for(int i = 0; i < enemyPOS.Count; i++)
			{
				if (GUI.Button (new Rect (enemyPOSX[i], enemyPOS[i].y, 20, 20),"" +enemyHealth[i])) 
				{
					Selected = i;
					enemyHealth[Selected] -= BaseDamage;
				}
			}
		}


		if (BulletPOS.Count > 0) 
		{
			for (int i = 0; i < BulletPOS.Count; i++) 
			{
				GUI.Button (new Rect (BulletPOSX [i], BulletPOS[i].y, 20, 20), "-");
			}
		}

		// RENDER UI
		if (EnableGamemodeTimer == true) 
		{
			GUI.Button(new Rect(155,55,80,21),"Time: " + Timer.ToString("F0"));
		}

		if (EnableGamemodeWaves == true) 
		{
			GUI.Button(new Rect(1,55,80,21),"Waves: " + CurrentWaves.ToString("F0") + "/" + MissionWaves.ToString("F0"));
		}
//
//		if (EnableGamemodeResources == true) 
//		{
//			GUI.Button(new Rect(1,55,80,21),"Time: " + Timer.ToString("F0"));
//		}
		GUI.Button(new Rect(1,55,150,21),"Base Health: " + BaseHealth.ToString("F0"));
		GUI.Button(new Rect(305,55,80,21),"Kills: " + Kills);
	}

	public void GameRender()
	{
		if (MainMenu == "Game")
		{
			MiniGameRender();
		}
		switch (MainMenu) 
		{
		case "Main Menu":
			if(GUI.Button(new Rect(1, 100, 150, 22), "Level Select"))
			{
				MainMenu = "Level Select";
			}

			if(GUI.Button(new Rect(1, 125, 150, 22), "Shop"))
			{
				MainMenu = "Shop";
			}

			if(GUI.Button(new Rect(1, 150, 150, 22), "Quit"))
			{
				mg.Selectedgame = "None";
			}
			break;

		case "Shop":
			if(GUI.Button(new Rect(1, 100, 150, 22), "Base Upgrades"))
			{
				SubMenu = "Base Upgrades";
			}

			if(GUI.Button(new Rect(1, 125, 150, 22), "Weapon Upgrades"))
			{
				SubMenu = "Weapon Upgrades";
			}

			if(GUI.Button(new Rect(1, 150, 150, 22), "Extra Upgrades"))
			{
				SubMenu = "Extra Upgrades";
			}

			if(GUI.Button(new Rect(1, 175, 150, 22), "Main Menu"))
			{
				MainMenu = "Main Menu";
			}
			break;

		case "Level Select":
			if(GUI.Button(new Rect(1, 100, 100, 22), "Main Menu"))
			{
				MainMenu = "Main Menu";
			}

			if(GUI.Button(new Rect(105, 100, 150, 22), "1 - Kill Count"))
			{
				BaseHealth = 1000 + BaseHealthUpgrade;
				LevelSelect = 1;
				MainMenu = "Game";
				GameRended = true;
				GameMode = "Kill Count";
				KillsNeeded = 15;
				StartGamemode = true;
			}

			if(GUI.Button(new Rect(105, 123, 150, 22), "2 - Kill Count Countdown"))
			{
				BaseHealth = 1000 + BaseHealthUpgrade;
				LevelSelect = 2;
				MainMenu = "Game";
				GameRended = true;
				GameMode = "Kill Count Holdout Countdown";
				KillsNeeded = 15;
				StartingTime = 60;
				Timer = StartingTime;
				EnableGamemodeTimer = true;
				TimerType = "Countdown";
				StartGamemode = true;
			}

			if(GUI.Button(new Rect(105, 145, 150, 22), "3 - Kill Count Countup"))
			{
				BaseHealth = 1000 + BaseHealthUpgrade;
				LevelSelect = 3;
				MainMenu = "Game";
				GameRended = true;
				GameMode = "Kill Count Holdout Countup";
				KillsNeeded = 15;
				StartingTime = 0;
				Timer = StartingTime;
				FinnishedTime = 60;
				EnableGamemodeTimer = true;
				TimerType = "Countup";
				StartGamemode = true;
			}
			break;

		case "Failed":
			LevelSelect = 0;

			GUI.Label (new Rect (100, 50, 100, 100), "MISSION FAILED");

			if(GUI.Button(new Rect(100, 100, 150, 22), "Main Menu"))
			{
				MainMenu = "Main Menu";
			}
			break;

		case "Success":
			LevelSelect = 0;

			GUI.Label (new Rect (100, 50, 100, 100), "MISSION SUCCESSUL");

			if(GUI.Button(new Rect(100, 100, 150, 22), "Main Menu"))
			{
				MainMenu = "Main Menu";
			}
			break;

		}

		if (MainMenu == "Success" || MainMenu == "Failed")
		{
			switch(LevelSelect)
			{
			case 0:
				StartGamemode = false;

				// KillCount Gamemode Stuff
				Kills = 0;
				KillsNeeded = 0;

				// Timed Gamemode
				StartingTime = 0;
				FinnishedTime = 0;
				Timer = 0;
				RemainingTime = 0;
				EnableGamemodeTimer = false;
				TimerType = "";

				// Waved Survied Gamemode
				CurrentWaves = 0;
				MissionWaves = 0;
				WavesLeft = 0;

				// Resources Earned
				CurrentResources = 0;
				ResourcesNeeded = 0;
				ResourcesRemaining = 0;

				if (BulletPOS.Count >= 0) 
				{
					ABulletDamage.RemoveRange(0,BulletPOS.Count);
					BulletPOSX.RemoveRange(0,BulletPOS.Count);
					BulletPOSY.RemoveRange(0,BulletPOS.Count);
					BulletPOS.RemoveRange(0,BulletPOS.Count);
				}
				if (enemyPOS.Count >= 0)
				{
					BulletDamage.RemoveRange(0,BulletPOS.Count);
					enemyName.RemoveRange(0,enemyPOS.Count);
					enemyDamageCoolDown.RemoveRange(0,enemyPOS.Count);
					enemyDamageCountDown.RemoveRange(0,enemyPOS.Count);
					enemyPOSY.RemoveRange(0,enemyPOS.Count);
					enemyPOSX.RemoveRange(0,enemyPOS.Count);
					enemyMulti.RemoveRange(0,enemyPOS.Count);
					enemyHealth.RemoveRange(0,enemyPOS.Count);
					enemyDamage.RemoveRange (0,enemyPOS.Count);
					BulletDamage.RemoveRange (0,enemyPOS.Count);
					enemyHasGun.RemoveRange (0,enemyPOS.Count);
					Range.RemoveRange (0,enemyPOS.Count);
					enemyPOS.RemoveRange (0,enemyPOS.Count);
				}
				break;
			}
		}
	}

}
