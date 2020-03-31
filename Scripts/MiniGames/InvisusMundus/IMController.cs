using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IMController : MonoBehaviour 
{
	private GameObject Mundus;
	private IMMainGame Game;

	public GameState State;
	public bool Quit;

	public enum GameState
	{
		MainMenu,
		Game
	}

	// Use this for initialization
	void Start ()
	{
		Mundus = GameObject.Find("Invisus Mundus");
		Game = Mundus.GetComponent<IMMainGame>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	public void RenderGame()
	{
		if(State == GameState.MainMenu)
		{
			if(GUI.Button(new Rect(2,50,100,22),"Play"))
			{
				State = GameState.Game;
			}

			if (GUI.Button(new Rect(2, 70, 100, 22), "Quit"))
			{
				Quit = true;
			}
		}

		if (State == GameState.Game)
		{
			Game.enabled = true;
			Game.GameScreen();
		}
	}
}
