using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class COG : MonoBehaviour 
{
	public string MenuSelector;
	public int EnemyCash;
	public int PlayerCash;
	public int PlayerAge;
	public int EnemyAge;

	public Rect PlayerBase;
	public Rect EnemyBase;

	public int T1BH;
	public int T2BH;

	public string GameStatus;

	// Use this for initialization
	void Start () 
	{
		T1BH = 100;
		T2BH = 100;
		PlayerBase = new Rect (20, 225, 50, 50);
		EnemyBase = new Rect (425, 225, 50, 50);
	}

	void GameChecks()
	{
		if (T1BH <= 0)
		{
			GameStatus = "Game Over";
		}

		if (T2BH <= 0)
		{
			GameStatus = "You Won";
		}
	}

	public void MiniGameRender()
	{
		GameChecks();

		GUI.Label (new Rect(5,50,100,22), "T1 Health: ");
		GUI.Label (new Rect(400,50,100,22), "T2 Health: ");
		GUI.Box (PlayerBase, "PB");
		GUI.Box (EnemyBase, "EB");
	}

	void EnemyAI()
	{

	}

	void Player()
	{

	}

	public void GameRender()
	{
		switch (MenuSelector)
		{
		case "Main Menu":
			MiniGameRender();
			break;
		}
	}
}
