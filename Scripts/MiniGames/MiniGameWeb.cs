using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class MiniGameWeb : MonoBehaviour 
{
	
	public Vector2 scrollpos = Vector2.zero;
	public int scrollsize;

	public int Select;
	public string Selectedgame;

	public List<string> GamesList = new List<string>();

	private DragRacer dr;
	private BaseDef bd;
	private KingdomClicker kd;
	private TypingGame tg;
	private GetRevengeMain grm;
	private EnergyCrysis ec;
	private COG cog;
	private TerrorBirdsUI tbui;
	private BlackJack bj;
	private RPGMain rpg;
//	private BrickBreaker brickDestroyer;

	public bool showMenu;

	// Use this for initialization
	void Start () 
	{
		dr = GetComponent<DragRacer>();
		bd = GetComponent<BaseDef>();
		kd = GetComponent<KingdomClicker>();
		tg = GetComponent<TypingGame>();
		grm = GetComponent<GetRevengeMain>();
		ec = GetComponent<EnergyCrysis>();
		cog = GetComponent<COG>();
		tbui = GetComponent<TerrorBirdsUI>();
		bj = GetComponent<BlackJack>();
		rpg = GetComponent<RPGMain>();
//		brickDestroyer = GetComponent<BrickBreaker>();
		UpdateGamesList();
	}

	void UpdateGamesList()
	{
		GamesList.Add ("Clicker");
		GamesList.Add ("Mafia");
		GamesList.Add ("Drag Racer");
		GamesList.Add ("Base Defense");
		GamesList.Add ("Kingdom Clicker");
		GamesList.Add ("Typing Game");
		GamesList.Add ("GET REVENGE!!!");
		GamesList.Add ("Energy Crysis");
		GamesList.Add ("Conflict of Generations");
		GamesList.Add ("Terror Birds");
		GamesList.Add ("Brick Destroyer");
		GamesList.Add ("Pong");
		GamesList.Add ("Blackjack");
		GamesList.Add ("RPG");
	}

	public void RenderSite()
	{
		switch (Selectedgame) 
		{
		case "None":
			showMenu = true;
			break;
		case "Clicker":
			break;
		case "Drag Racer":
			dr.GameRender();
			break;
		case "Base Defense":
			showMenu = false;
			bd.GameRender();
			bd.GameRended = true;
			break;
		case "Kingdom Clicker":
			showMenu = false;
			kd.GameRender ();
			KingdomProf.kingprof.Load();
			break;
		case "Typing Game":
			showMenu = false;
			tg.GameRender ();
			tg.MenuSelector = "Main Menu";
			break;
		case "GET REVENGE!!!":
			showMenu = false;
			grm.GameRender ();
			grm.MenuSelector = "Main Menu";
			break;
		case "Energy Crysis":
			showMenu = false;
			ec.GameRender ();
			ec.MenuSelector = "Main Menu";
			break;
		case "Conflict of Generations":
			showMenu = false;
			cog.GameRender ();
			cog.MenuSelector = "Main Menu";
			break;
		case "Terror Birds":
			showMenu = false;
			tbui.GameRender ();
			break;
		case "Brick Destroyer":
			showMenu = false;
//			brickDestroyer.GameRender();
			break;
		case "Pong":
			showMenu = false;
			//brickDestroyer.GameRender();
			break;
		case "Blackjack":
			showMenu = false;
			bj.GameRender();
			break;
		case "RPG":
			showMenu = false;
			rpg.GameRender();
			break;
		}

		if (showMenu == true)
		{
			scrollpos = GUI.BeginScrollView(new Rect(10, 75, 175, 150), scrollpos, new Rect(0, 0, 0, scrollsize*30));
			for (scrollsize = 0; scrollsize < GamesList.Count; scrollsize++)
			{
				if(GUI.Button(new Rect(0, scrollsize * 30, 150, 30), "" + GamesList[scrollsize]))
				{
					Selectedgame = GamesList[scrollsize];
				}
			}
			GUI.EndScrollView();
		}
	}
}
