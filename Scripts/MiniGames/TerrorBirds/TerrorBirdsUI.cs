using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrorBirdsUI : MonoBehaviour 
{
	public string MenuSelector;

	private MiniGameWeb mgw;

	public Rect PlayerRect;

	public float PlayerX;
	public int PlayerMod;
	public int EnemyMod;

	public List<float> BulletPOSX = new List<float>();
	public List<float> BulletPOSY = new List<float>();
	public List<float> BulletDamage = new List<float>();

	public List<float> enemyPOSX = new List<float>();
	public List<float> enemyPOSY = new List<float>();
	public List<Rect> enemyPOS = new List<Rect>();

	public float Timer;

	public bool MoveRight;
	public bool moveDown;
	public int current;

	public int X;
	public int Y;

	public bool BulletExist;
	public float PlayerBulletX;
	public float PlayerBulletY;
	public List<Rect> PlayerBullet = new List<Rect>();

	public List<Texture2D> Bird1 = new List<Texture2D>();

	public int bulletMod;

	public int ImageSelector;
	public bool WingsUp;

	public List<Color> Colors = new List<Color>();
	public Color32 rgb1 = new Color32(0,0,0,0);
	public Color32 buttonColor = new Color32(0,0,0,0);
	public Color32 fontColor = new Color32(0,0,0,0);

	public float WingTime;
	public float Refire;

	// Use this for initialization
	void Start ()
	{
		X = 11;
		Y = 5;
		EnemyMod = 30;
		mgw = GetComponent<MiniGameWeb>();
		LoadPresetColors();
	}

	void LoadPresetColors()
	{
		//rgb1.r = 255;
		//rgb1.g = 255;
		//rgb1.b = 255;
		//rgb1.a = 255;

		buttonColor.r = 255;
		buttonColor.g = 255;
		buttonColor.b = 255;
		buttonColor.a = 255;

		fontColor.r = 255;
		fontColor.g = 255;
		fontColor.b = 255;
		fontColor.a = 255;
	}

	void EnemyCreator()
	{
		for (int i = 0; i < X; i++) 
		{
			for (int j = 0; j < Y; j++) 
			{
				enemyPOSX.Add (10 + 25 * i);
				enemyPOSY.Add (65 + 20 * j);
				enemyPOS.Add (new Rect(enemyPOSX[i], enemyPOSY[j], 30, 30));
			}
		}
	}

	void InGame()
	{
		PlayerRect = new Rect (PlayerX, 288, 30, 10);
		GUI.Box(new Rect (PlayerRect), "P");

		Timers();
		EnemyController();

		if (enemyPOS.Count <= 0) 
		{
			MenuSelector = "VICTORY";
			PlayerBullet.RemoveAt(0);
		}

		if (PlayerBullet.Count > 0)
		{
			PlayerBullet [0] = new Rect (PlayerBulletX, PlayerBulletY, 7, 15);
			GUI.Box (new Rect (PlayerBullet[0]),"|");

			PlayerBulletY = PlayerBulletY - 1 * Time.deltaTime * 125;


			if (PlayerBulletY < 20) 
			{
				PlayerBullet.RemoveAt (0);
			}
		}
	}

	void Timers()
	{
		WingTime -= 1 * Time.deltaTime;

		if (WingTime <= 0)
		{
			WingsUp = !WingsUp;
			WingTime = 0.5f;
		}
		if (Refire > 0)
		{
			Refire -= 1 * Time.deltaTime;
		}
	}

	void EnemyController()
	{
		if (enemyPOSX.Count > 0) 
		{
			for (int j = 0; j < enemyPOSX.Count; j++) 
			{
				if (PlayerBullet.Count > 0)
				{
					if (enemyPOS[j].Contains (PlayerBullet[0].position)) 
					{
						enemyPOSX.RemoveAt (j);
						enemyPOSY.RemoveAt (j);
						enemyPOS.RemoveAt(j);
						PlayerBullet.RemoveAt(0);
					}
				}
			}

			for(int i = 0; i < enemyPOSX.Count; i++)
			{
				enemyPOS [i] = new Rect (enemyPOSX [i], enemyPOSY [i], 20, 15);
				GUI.DrawTexture (new Rect (enemyPOS[i]), Bird1[ImageSelector]);

				if (enemyPOSY [i] >= 280)
				{
					MenuSelector = "DEFEAT";
					PlayerBullet.RemoveAt(0);
				}

				if (enemyPOSX [i] > 466 && MoveRight == true)
				{
					MoveRight = false;
					moveDown = true;
				}

				if (enemyPOSX [i] < 6 && MoveRight == false)
				{
					MoveRight = true;
					moveDown = true;
				}


				if (MoveRight == true) 
				{
					enemyPOSX [i] += 1 * Time.deltaTime * EnemyMod;
					if (WingsUp) 
					{
						ImageSelector = 2;
					} 
					if (!WingsUp)
					{
						ImageSelector = 3;
					}
				} 
				else 
				{
					enemyPOSX [i] -= 1 * Time.deltaTime * EnemyMod;
					if (WingsUp) 
					{
						ImageSelector = 0;
					} 
					if (!WingsUp)
					{
						ImageSelector = 1;
					}
				}
			}

			if (moveDown == true)
			{
				for (current = 0; current < enemyPOSY.Count; current++) 
				{
					enemyPOSY[current] += 10;
				}
				if (current == enemyPOSY.Count)
				{
					moveDown = false;
					current = 0;
				}
			}
		}
	}

	public void GameRender()
	{
		GUI.backgroundColor = buttonColor;
		GUI.contentColor = fontColor;

		switch (MenuSelector)
		{
		case "Main Menu":
			if (GUI.Button (new Rect (1, 100, 150, 22), "Start")) 
			{
				EnemyCreator();
				MenuSelector = "Game";
			}

			if (GUI.Button (new Rect (1, 150, 150, 22), "Quit")) 
			{
				mgw.Selectedgame = "None";
			}
			break;

		case "DEFEAT":
			GUI.Label (new Rect (100, 100, 150, 22), "YOU HAVE BEEN DEFEATED!!");
			if (GUI.Button (new Rect (100, 150, 150, 22), "Main Menu")) 
			{
				enemyPOSX.RemoveRange(0,enemyPOSX.Count);
				enemyPOSY.RemoveRange(0,enemyPOSY.Count);
				enemyPOS.RemoveRange(0,enemyPOS.Count);
				MenuSelector = "Main Menu";
			}
			break;

		case "VICTORY":
			GUI.Label (new Rect (100, 100, 150, 22), "YOU ARE VICTORIUS!!");
			if (GUI.Button (new Rect (100, 150, 150, 22), "Main Menu")) 
			{
				MenuSelector = "Main Menu";
			}
			if (GUI.Button (new Rect (100, 200, 150, 22), "Play Again?")) 
			{
				MenuSelector = "Game";
				EnemyCreator();
			}
			break;
		}

		if (MenuSelector == "Game")
		{
			InGame();

			if (Input.GetKey (KeyCode.A) && PlayerX > 2) 
			{
				PlayerX -= 1 * Time.deltaTime * PlayerMod;
			}

			if (Input.GetKey (KeyCode.D) && PlayerX < 468) 
			{
				PlayerX += 1 * Time.deltaTime * PlayerMod;
			}

			if (Input.GetKey (KeyCode.Space)) 
			{
				if (Refire <= 0)
				{
					if (PlayerBullet.Count <= 0) 
					{
						PlayerBulletX = PlayerX + 14;
						PlayerBulletY = 288;
						PlayerBullet.Add (new Rect (PlayerBulletX, PlayerBulletY, 0, 0));
						Refire = 0.75f;
					}
				}
			}
		}
		
	}
}
