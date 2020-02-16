using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetRevengeMain : MonoBehaviour 
{
	public string MenuSelector;
	public Vector2 PlayerPos;
	public Rect Enemy;
	public float Multi;

	public Vector2 Distiance;
	public Vector2 Goto;

	public Rect Flower;

	public int Score;
	// Use this for initialization
	void Start () 
	{
		Flower.x = Random.Range (50, 150);
		Flower.y = Random.Range (50, 150);
		Flower.width = 20;
		Flower.height = 20;
		Enemy.width = 20;
		Enemy.height = 20;
	}

	void PlayerControls()
	{
		if (Input.GetKey (KeyCode.A) || Input.GetKey (KeyCode.LeftArrow)) 
		{
			PlayerPos.x -= 1 * Time.deltaTime * Multi;
		}
		if (Input.GetKey (KeyCode.D) || Input.GetKey (KeyCode.RightArrow)) 
		{
			PlayerPos.x += 1 * Time.deltaTime * Multi;
		}
		if (Input.GetKey (KeyCode.S) || Input.GetKey (KeyCode.DownArrow)) 
		{
			PlayerPos.y += 1 * Time.deltaTime * Multi;
		}
		if (Input.GetKey (KeyCode.W) || Input.GetKey (KeyCode.UpArrow)) 
		{
			PlayerPos.y -= 1 * Time.deltaTime * Multi;
		}
	}

	void EnemyActions()
	{
		Distiance.x = PlayerPos.x - Enemy.x;
		Distiance.y = PlayerPos.y- Enemy.y;

		Enemy.x += Distiance.x * Time.deltaTime * 0.25f;
		Enemy.y += Distiance.y * Time.deltaTime * 0.25f;

		if (Enemy.Contains(PlayerPos)) 
		{
			Enemy.x = Random.Range (50, 150);
			Enemy.y = Random.Range (50, 150);
			Score -= 1;
		}
	}

	void FlowerActions()
	{
		if (Flower.Contains(PlayerPos)) 
		{
			Flower.x = Random.Range (50, 150);
			Flower.y = Random.Range (50, 150);
			Score += 1;
		}
	}

	public void MiniGameRender()
	{
		PlayerControls();
		EnemyActions();
		FlowerActions();

		GUI.Box(new Rect(PlayerPos.x,PlayerPos.y,20,20),"P");
		GUI.Box(new Rect(Enemy),"E");
		GUI.Box(new Rect(Flower),"F");
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
