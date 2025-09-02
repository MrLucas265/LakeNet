using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RPGMain : MonoBehaviour 
{
	public string CurrentScreen;

	public Rect Player;

	private MiniGameWeb mgw;

	public float Health;

	public float CurrentTime;
	public float AMPMTime;
	public float TimeRemaining;

    public float MoveSpeed;

    public bool Up;
    public bool Down;
    public bool Left;
    public bool Right;
    public bool Sprint;

    public List<InventorySystem> PlayerStats = new List<InventorySystem>();
	public List<InventorySystem> EnemyStats = new List<InventorySystem>();
	public List<InventorySystem> PlayerInventory = new List<InventorySystem>();
	public List<InventorySystem> FurnitureShop = new List<InventorySystem>();
	public List<InventorySystem> ConvientStore = new List<InventorySystem>();
	public List<InventorySystem> CarDealer = new List<InventorySystem>();
	public List<InventorySystem> GunStore = new List<InventorySystem>();
	public List<InventorySystem> RealEstate = new List<InventorySystem>();
	public List<InventorySystem> DrugDealer = new List<InventorySystem>();
	public List<InventorySystem> Bar = new List<InventorySystem>();
	public List<InventorySystem> PawnShop = new List<InventorySystem>();

	public bool ShowInventory;
	// Use this for initialization
	void Start ()
	{
		mgw = GetComponent<MiniGameWeb>();
		CurrentScreen = "Main Menu";
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	void Shop()
	{
		
	}

	void School()
	{

	}

    void Bank()
    {

    }

    void RealEstates()
    {

    }

	void Inventory()
	{
		GUI.Box (new Rect (2,35,100,100), "");
	}

	void Hud()
	{
		if (ShowInventory == true) 
		{
			Inventory();
		}
	}

	void Controls()
	{
        if (Up == true)
        {
            Player.y -= MoveSpeed * Time.deltaTime;
        }
        if (Down == true)
        {
            Player.y += MoveSpeed * Time.deltaTime;
        }
        if (Left == true)
        {
            Player.x -= MoveSpeed * Time.deltaTime;
        }
        if (Right == true)
        {
            Player.x += MoveSpeed * Time.deltaTime;
        }

        if (Sprint == true)
        {
            MoveSpeed = 50;
        }
        else
        {
            MoveSpeed = 25;
        }

        if (Event.current.type == EventType.KeyDown)
        {
            if (Event.current.keyCode == KeyCode.W || Event.current.keyCode == KeyCode.UpArrow)
            {
                Up = true;
            }

            if (Event.current.keyCode == KeyCode.S || Event.current.keyCode == KeyCode.DownArrow)
            {
                Down = true;
            }

            if (Event.current.keyCode == KeyCode.D || Event.current.keyCode == KeyCode.RightArrow)
            {
                Right = true;
            }

            if (Event.current.keyCode == KeyCode.A || Event.current.keyCode == KeyCode.LeftArrow)
            {
                Left = true;
            }

            if (Event.current.keyCode == KeyCode.LeftShift)
            {
                Sprint = true;
            }

            if (Event.current.keyCode == KeyCode.I)
            {
                ShowInventory = !ShowInventory;
            }
        }


        if (Event.current.type == EventType.KeyUp)
        {
            if (Event.current.keyCode == KeyCode.W || Event.current.keyCode == KeyCode.UpArrow)
            {
                Up = false;
            }

            if (Event.current.keyCode == KeyCode.S || Event.current.keyCode == KeyCode.DownArrow)
            {
                Down = false;
            }

            if (Event.current.keyCode == KeyCode.D || Event.current.keyCode == KeyCode.RightArrow)
            {
                Right = false;
            }

            if (Event.current.keyCode == KeyCode.A || Event.current.keyCode == KeyCode.LeftArrow)
            {
                Left = false;
            }

            if (Event.current.keyCode == KeyCode.LeftShift)
            {
                Sprint = false;
            }
        }
    }

	void CharacterCreation()
	{

	}

	void Game()
	{
		Controls();
		Hud();

		GUI.Box (new Rect (Player.x,Player.y,22,21), "P");
	}

	void MainMenu()
	{
		if (GUI.Button (new Rect (20, 40, 100, 22), "Play")) 
		{
			CurrentScreen = "Game";
		}

		if (GUI.Button (new Rect (20, 70, 100, 22), "Quit")) 
		{
			mgw.showMenu = true;
			mgw.Selectedgame = "";
		}
	}

	public void GameRender()
	{
		switch (CurrentScreen) 
		{
		case"Main Menu":
			MainMenu();
			break;
		case"Game":
			Game();
			break;
		}
	}
}
