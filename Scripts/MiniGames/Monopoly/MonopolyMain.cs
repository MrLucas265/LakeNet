using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MonopolyMain : MonoBehaviour
{
    public Rect windowRect = new Rect(100, 100, 200, 200);
    public float native_width = 1920;
    public float native_height = 1080;
    public int windowID;
    public bool show;
    public bool Game;
    private Computer com;

    public string Name;
    public bool Search;
    public bool DisplayInfo;
    public int i;

    public string DegreeEdit;
    public string NameEdit;

    public string Test;

    public int TotalPlayers;
    public int CurrentPlayer;
    public int RemainingMoves;
    public int PlayerPos;
    public int TotalDice;
    public bool FinnishedMoving;
    public bool Roll;

    public bool ShowMainUI;
    public bool ShowPurchaseUI;

    public float MoveTimer;
    public float MoveCooldown;

    public int Dice1;
    public int Dice2;

    public int SetCount;

    public List<Texture2D> PlayerIcons = new List<Texture2D>();

    public List<MonopolyPropSystem> Tiles = new List<MonopolyPropSystem>();

    public List<MonopolyPropSystem> Board = new List<MonopolyPropSystem>();

    public List<MonopolyPropSystem> Sets = new List<MonopolyPropSystem>();

    public List<MonopolyPlayerSystem> Players = new List<MonopolyPlayerSystem>();

    public List<MonopolyPropSystem> CurrentSet = new List<MonopolyPropSystem>();

    public MonopolyPropSystem CurrentDeed;

    public int side;
    public int side1;
    public int side2;

    public int x;
    public int y;

    public int CurrentTile;

    public GUISkin Skin;

    public float DiceTimer;
    public float DiceCooldown;
    public float DiceRollTimer;
    public float DiceRollCooldown;
    public bool RollingDice;

    public int OwnedSetCount;
    public bool Paid;
    public int PaidRent;

    public bool DoesPlayerOwnAllSet;

    public int DoubleCount;
    public bool DisplayDouble;

    public float ShowSpecialTimer;
    public float SpecialCoolDown;


    // Use this for initialization
    void Start()
    {
        com = GetComponent<Computer>();
        Players.Add(new MonopolyPlayerSystem("P1",0,2500,false,0,0,0,0,0));
        Players.Add(new MonopolyPlayerSystem("P2", 0, 2500, false, 0, 0, 0, 0,0));

        Roll = true;

        SpecialCoolDown = 2;
        ShowSpecialTimer = SpecialCoolDown;

        TileCreator();
    }

    void OnGUI()
    {
        GUI.skin = Skin;

        Customize.cust.windowx[windowID] = windowRect.x;
        Customize.cust.windowy[windowID] = windowRect.y;

        if (Game == true)
        {
            GUI.skin = com.Skin[GameControl.control.GUIID];
        }

        ////set up scaling
        //float rx = Screen.width / native_width;
        //float ry = Screen.height / native_height;

        //GUI.matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, new Vector3(rx, ry, 1));

        if (show == true)
        {
            windowRect = GUI.Window(windowID, windowRect, DoMyWindow, "");
        }
    }

    void DoMyWindow(int WindowID)
    {
        //GUI.DragWindow(new Rect(5, 5, 370, 21));
        //GUI.Box(new Rect(5, 5, 370, 21), "Database View - Test");
        //if (GUI.Button(new Rect(375, 5, 21, 21), "X"))
        //{
        //    show = false;
        //}

        if (Players[CurrentPlayer].CurrentPos > 39)
        {
            Players[CurrentPlayer].CurrentPos = 0;
            Players[CurrentPlayer].Cash += 200;
        }

        if (RollingDice == true)
        {
            FinnishedMoving = false;
            Dice();
        }

        PlayerInfo();

        if(ShowPurchaseUI == true)
        {
            ShowMainUI = false;
            Purchase();
        }

        if (ShowMainUI == true)
        {
            GUI.Label(new Rect(250, 200, 100, 100), "Current Player: " + Players[CurrentPlayer].Name);

            GUI.Label(new Rect(250, 250, 100, 100), "" + TotalDice);

            if (Roll == false)
            {
                if(Players[CurrentPlayer].Jailed == true)
                {
                    JailSystem();
                }
                else
                {
                    if (RemainingMoves <= 0)
                    {
                        for (int t = 0; t < Tiles.Count; t++)
                        {
                            if (Players[CurrentPlayer].CurrentPos == t)
                            {
                                if (Tiles[t].Ownable == true)
                                {
                                    if (Tiles[t].Owned == false)
                                    {
                                        ShowPurchaseUI = true;
                                        //Purchase
                                        //Auction
                                    }
                                    else
                                    {
                                        if (Tiles[t].Owner != CurrentPlayer)
                                        {
                                            if (FinnishedMoving == true)
                                            {
                                                Rent();
                                            }
                                        }
                                        EndOfTurnUI();
                                    }
                                }
                                else
                                {
                                    if (CurrentDeed.Type == MonopolyPropSystem.PropType.Police)
                                    {
                                        GotoJail();
                                        EndOfTurnUI();
                                    }
                                    else
                                    {
                                        EndOfTurnUI();
                                    }
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                if (RemainingMoves <= 0)
                {
                    if (CurrentDeed.Type == MonopolyPropSystem.PropType.Police)
                    {
                        GotoJail();
                        NextPlayer();
                    }
                    else
                    {
                        if (GUI.Button(new Rect(200, 290, 50, 50), "Roll"))
                        {
                            RollingDice = true;
                            DiceRollCooldown = Random.Range(0.5f, 1.5f);
                            DiceRollTimer = DiceRollCooldown;
                        }
                    }
                }
            }
        }

        GameController();

        RenderBoard();

        BoardMaker();

        if (DoubleCount >= 3)
        {
            GotoJail();
        }

        if(Players[CurrentPlayer].JailRolls > 2)
        {
            GetOutOfJail();
        }

        if(DisplayDouble == true)
        {
            DisplayDoubleUI();
        }

        if(Players[CurrentPlayer].Jailed == true)
        {
            JailSystem();
        }

        CurrentDeed = Tiles[Players[CurrentPlayer].CurrentPos];

        if (RemainingMoves > 0)
        {
            PlayerMove();
        }
        else
        {
            if(Roll == false)
            {
                FinnishedMoving = true;
            }
        }

        //if(Roll == true)
        //{
        //    Dice();
        //    Roll = false;
        //}
    }

    void GetOutOfJail()
    {
        Players[CurrentPlayer].Cash -= 50;
        Players[CurrentPlayer].Jailed = false;
        Players[CurrentPlayer].JailRolls = 0;
    }

    void ResetCurrentTurn()
    {
        CurrentSet.RemoveRange(0, CurrentSet.Count);
        FinnishedMoving = false;
        Roll = true;
        Paid = false;
    }

    void GotoJail()
    {
        Players[CurrentPlayer].Jailed = true;
        Players[CurrentPlayer].CurrentPos = 10;
        NextPlayer();
    }

    void NextPlayer()
    {
        ResetCurrentTurn();
        if (CurrentPlayer >= Players.Count - 1)
        {
            CurrentPlayer = 0;
        }
        else
        {
            CurrentPlayer++;
        }
        DoubleCount = 0;
    }

    void JailSystem()
    {
        if(Roll == false)
        {
            if (GUI.Button(new Rect(200, 290, 50, 50), "Done"))
            {
                Players[CurrentPlayer].JailRolls++;
                EndTurn();
            }
        }
        else
        {
            if (GUI.Button(new Rect(100, 270, 50, 50), "Pay"))
            {
                GetOutOfJail();
            }
        }
    }

    void DisplayDoubleUI()
    {
        if(DisplayDouble == true)
        {
            ShowSpecialTimer -= Time.deltaTime;
            GUI.Label(new Rect(windowRect.width/2, windowRect.height / 2, 100, 22), "DOUBLE!");
        }

        if(ShowSpecialTimer <= 0)
        {
            DisplayDouble = false;
            ShowSpecialTimer = SpecialCoolDown;
        }
    }

    void EndOfTurnUI()
    {
        if(Roll == false)
        {
            if (Dice1 == Dice2)
            {
                EndTurn();
            }
            else
            {
                if (GUI.Button(new Rect(200, 290, 50, 50), "Done"))
                {
                    EndTurn();
                }
            }
        }
    }

    void EndTurn()
    {
        if(Dice1 == Dice2)
        {
            if (CurrentDeed.Type == MonopolyPropSystem.PropType.Police)
            {
                GotoJail();
                NextPlayer();
            }
            else
            {
                ResetCurrentTurn();
            }
        }
        else
        {
            NextPlayer();
        }
    }

    void PlayerInfo()
    {
        if(Players.Count > 0)
        {
            GUI.Label(new Rect(100, 75, 75, 20), Players[0].Name);
            GUI.Label(new Rect(100, 100, 75, 20),"$" + Players[0].Cash);

            GUI.Label(new Rect(175, 75, 75, 20), Players[1].Name);
            GUI.Label(new Rect(175, 100, 75, 20), "$" + Players[1].Cash);
        }

        if (Players.Count > 2)
        {
            GUI.Label(new Rect(250, 75, 75, 20), Players[2].Name);
            GUI.Label(new Rect(250, 100, 75, 20), "$" + Players[2].Cash);
        }

        if (Players.Count > 3)
        {
            GUI.Label(new Rect(325, 75, 75, 20), Players[2].Name);
            GUI.Label(new Rect(325, 100, 75, 20), "$" + Players[2].Cash);
        }
    }

    void NormalSetCheck()
    {
        for (SetCount = 0; SetCount < Tiles.Count; SetCount++)
        {
            if (Tiles[SetCount].Colour == Tiles[Players[CurrentPlayer].CurrentPos].Colour)
            {
                if (Tiles[SetCount].Type == MonopolyPropSystem.PropType.Normal)
                {
                    if (!CurrentSet.Contains(Tiles[SetCount]))
                    {
                        CurrentSet.Add(Tiles[SetCount]);
                    }
                }
            }
        }
    }

    void TrainSetCheck()
    {
        for (SetCount = 0; SetCount < Tiles.Count; SetCount++)
        {
            if (Tiles[SetCount].Type == MonopolyPropSystem.PropType.Train)
            {
                if (!CurrentSet.Contains(Tiles[SetCount]))
                {
                    CurrentSet.Add(Tiles[SetCount]);
                }
            }
        }
    }

    private bool CheckPlayerOwnAllSet()
    {
        OwnedSetCount = 0;
        for (int i = 0; i < CurrentSet.Count; i++)
        {
            int Check = i - 1;
            if (CurrentSet[i].Owner == Tiles[Players[CurrentPlayer].CurrentPos].Owner)
            {
                if (CurrentSet[i].Owned == true)
                {
                    OwnedSetCount++;

                    if (i >= CurrentSet.Count-1)
                    {
                        if(OwnedSetCount == CurrentSet.Count)
                        {
                            return true;
                        }
                    }
                }
            }
        }

        return false;
    }

    void PayRent()
    {
        if (Tiles[Players[CurrentPlayer].CurrentPos].Type == MonopolyPropSystem.PropType.Normal)
        {
            NormalSetCheck();

            DoesPlayerOwnAllSet = CheckPlayerOwnAllSet();
            if (Paid == false)
            {
                if (DoesPlayerOwnAllSet == true)
                {
                    if(Tiles[Players[CurrentPlayer].CurrentPos].TotalHouses == 0)
                    {
                        PaidRent = Tiles[Players[CurrentPlayer].CurrentPos].Rent * 2;
                        Players[CurrentPlayer].Cash -= PaidRent;
                        Players[Tiles[Players[CurrentPlayer].CurrentPos].Owner].Cash += PaidRent;
                        Paid = true;
                    }
                    else
                    {
                        PaidRent = Tiles[Players[CurrentPlayer].CurrentPos].Rent;
                        Players[CurrentPlayer].Cash -= PaidRent;
                        Players[Tiles[Players[CurrentPlayer].CurrentPos].Owner].Cash += PaidRent;
                        Paid = true;
                    }
                }
                else
                {
                    PaidRent = Tiles[Players[CurrentPlayer].CurrentPos].Rent;
                    Players[CurrentPlayer].Cash -= PaidRent;
                    Players[Tiles[Players[CurrentPlayer].CurrentPos].Owner].Cash += PaidRent;
                    Paid = true;
                }
            }
        }
        else if (Tiles[Players[CurrentPlayer].CurrentPos].Type == MonopolyPropSystem.PropType.Train)
        {
            TrainSetCheck();

            DoesPlayerOwnAllSet = CheckPlayerOwnAllSet();
            if (Paid == false)
            {
                PaidRent = Tiles[Players[CurrentPlayer].CurrentPos].Rent * OwnedSetCount;
                Players[CurrentPlayer].Cash -= PaidRent;
                Players[Tiles[Players[CurrentPlayer].CurrentPos].Owner].Cash += PaidRent;
                Paid = true;
            }
        }
    }

    void Rent()
    {
        if(Paid == false)
        {
            PayRent();
        }
        GUI.Label(new Rect(100, 150, 75, 20), Players[CurrentPlayer].Name + " > ");
        GUI.Label(new Rect(200, 150, 75, 20), Players[Tiles[Players[CurrentPlayer].CurrentPos].Owner].Name);
        GUI.Label(new Rect(150, 175, 75, 20), "$" + PaidRent);
    }

    void Purchase()
    {
        if(Players[CurrentPlayer].Cash >= Tiles[Players[CurrentPlayer].CurrentPos].PurchasePrice)
        {
            DisplayColor();
            GUI.Box(new Rect(200, 150, 50, 100), Tiles[Players[CurrentPlayer].CurrentPos].Name + "\n $" + Tiles[Players[CurrentPlayer].CurrentPos].PurchasePrice);
            GUI.backgroundColor = Color.white;
            if(GUI.Button(new Rect(300,300,50,50),"Buy"))
            {
                Tiles[Players[CurrentPlayer].CurrentPos].Owned = true;
                Tiles[Players[CurrentPlayer].CurrentPos].Owner = CurrentPlayer;
                Players[CurrentPlayer].Cash -= Tiles[Players[CurrentPlayer].CurrentPos].PurchasePrice;
                ShowMainUI = true;
                ShowPurchaseUI = false;
            }
        }
    }

    void Dice()
    {
        DiceRollCooldown = 1;
        DiceCooldown = 0.15f;
        if (DiceRollTimer > 0)
        {
            DiceRollTimer -= Time.deltaTime;
            if (DiceTimer > 0)
            {
                DiceTimer -= Time.deltaTime;
            }
            if (DiceTimer <= 0)
            {
                Dice1 = Random.Range(1, 7);
                Dice2 = Random.Range(1, 7);
                TotalDice = Dice1 + Dice2;
                DiceTimer = DiceCooldown;
            }
        }
        if (DiceRollTimer <= 0)
        {
            DiceRollTimer = DiceRollCooldown;
            if (Players[CurrentPlayer].Jailed == true)
            {
                if (Dice1 == Dice2)
                {
                    DisplayDouble = true;
                    DoubleCount++;
                    Players[CurrentPlayer].Jailed = false;
                }
                else
                {
                    Players[CurrentPlayer].JailRolls++;
                }
            }
            else
            {
                if (Dice1 == Dice2)
                {
                    DisplayDouble = true;
                    DoubleCount++;
                }
                RemainingMoves = TotalDice;
            }
            RollingDice = false;
            Roll = false;
        }
    }

    void PlayerMove()
    {
        if(MoveTimer < MoveCooldown)
        {
            MoveTimer += Time.deltaTime;
        }
        if(MoveTimer > MoveCooldown)
        {
            Players[CurrentPlayer].CurrentPos++;
            RemainingMoves--;
            MoveTimer = 0;
        }
    }

    void TileCreator()
    {
        //CORNERS
        Board.Add(new MonopolyPropSystem("Start", 0, 0, 0, 0, 0, false, 0, false, 0, false, 0, MonopolyPropSystem.PropType.Start, MonopolyPropSystem.PropColor.None));
        Board.Add(new MonopolyPropSystem("Jail", 0, 0, 0, 0, 0, false, 0, false, 0, false, 10, MonopolyPropSystem.PropType.Jail, MonopolyPropSystem.PropColor.None));
        Board.Add(new MonopolyPropSystem("Club \n Incubus", 0, 0, 0, 0, 0, false, 0, false, 0, false, 20, MonopolyPropSystem.PropType.FreeParking, MonopolyPropSystem.PropColor.None));
        Board.Add(new MonopolyPropSystem("WebSec", 0, 0, 0, 0, 0, false, 0, false, 0, false, 30, MonopolyPropSystem.PropType.Police, MonopolyPropSystem.PropColor.None));
        //Subways
        Board.Add(new MonopolyPropSystem("Subway \n South", 0, 0, 200, 50, 0, false, 0, true, 0, false, 5, MonopolyPropSystem.PropType.Train, MonopolyPropSystem.PropColor.None));
        Board.Add(new MonopolyPropSystem("Subway \n West", 0, 0, 200, 50, 0, false, 0, true, 0, false, 15, MonopolyPropSystem.PropType.Train, MonopolyPropSystem.PropColor.None));
        Board.Add(new MonopolyPropSystem("Subway \n North", 0, 0, 200, 50, 0, false, 0, true, 0, false, 25, MonopolyPropSystem.PropType.Train, MonopolyPropSystem.PropColor.None));
        Board.Add(new MonopolyPropSystem("Subway \n East", 0, 0, 200, 50, 0, false, 0, true, 0, false, 35, MonopolyPropSystem.PropType.Train, MonopolyPropSystem.PropColor.None));
        //CHANCE CARDS
        Board.Add(new MonopolyPropSystem("Chance", 0, 0, 0, 0, 0, false, 0, false, 0, false, 2, MonopolyPropSystem.PropType.Chance, MonopolyPropSystem.PropColor.None));
        Board.Add(new MonopolyPropSystem("Chance", 0, 0, 0, 0, 0, false, 0, false, 0, false, 22, MonopolyPropSystem.PropType.Chance, MonopolyPropSystem.PropColor.None));
        Board.Add(new MonopolyPropSystem("Chance", 0, 0, 0, 0, 0, false, 0, false, 0, false, 37, MonopolyPropSystem.PropType.Chance, MonopolyPropSystem.PropColor.None));
        //CHEST
        Board.Add(new MonopolyPropSystem("Chest", 0, 0, 0, 0, 0, false, 0, false, 0, false, 7, MonopolyPropSystem.PropType.Chest, MonopolyPropSystem.PropColor.None));
        Board.Add(new MonopolyPropSystem("Chest", 0, 0, 0, 0, 0, false, 0, false, 0, false, 17, MonopolyPropSystem.PropType.Chest, MonopolyPropSystem.PropColor.None));
        Board.Add(new MonopolyPropSystem("Chest", 0, 0, 0, 0, 0, false, 0, false, 0, false, 33, MonopolyPropSystem.PropType.Chest, MonopolyPropSystem.PropColor.None));
        //TAX
        Board.Add(new MonopolyPropSystem("Corprate \n Tax", 0, 0, 0, 0, 0, false, 0, false, 0, false, 4, MonopolyPropSystem.PropType.Tax, MonopolyPropSystem.PropColor.None));
        //Red
        Board.Add(new MonopolyPropSystem("Noodle WooBox", 0, 0, 60, 1, 0, false, 0, true, 0, false, 1, MonopolyPropSystem.PropType.Normal, MonopolyPropSystem.PropColor.Red));
        Board.Add(new MonopolyPropSystem("Xeros \n Cafe", 0, 0, 60, 1, 0, false, 0, true, 0, false, 3, MonopolyPropSystem.PropType.Normal, MonopolyPropSystem.PropColor.Red));
        //Green
        Board.Add(new MonopolyPropSystem("SolTech", 0, 0, 100, 1, 0, false, 0, true, 0, false, 6, MonopolyPropSystem.PropType.Normal, MonopolyPropSystem.PropColor.Green));
        Board.Add(new MonopolyPropSystem("BioPlex", 0, 0, 100, 1, 0, false, 0, true, 0, false, 8, MonopolyPropSystem.PropType.Normal, MonopolyPropSystem.PropColor.Green));
        Board.Add(new MonopolyPropSystem("Qufot Wares", 0, 0, 120, 1, 0, false, 0, true, 0, false, 9, MonopolyPropSystem.PropType.Normal, MonopolyPropSystem.PropColor.Green));
        //Pink
        Board.Add(new MonopolyPropSystem("Unicom", 0, 0, 100, 1, 0, false, 0, true, 0, false,11, MonopolyPropSystem.PropType.Normal, MonopolyPropSystem.PropColor.Pink));
        Board.Add(new MonopolyPropSystem("Ivory \n Gear", 0, 0, 100, 1, 0, false, 0, true, 0, false, 13, MonopolyPropSystem.PropType.Normal, MonopolyPropSystem.PropColor.Pink));
        Board.Add(new MonopolyPropSystem("Paragon \n Design", 0, 0, 120, 1, 0, false, 0, true, 0, false, 14, MonopolyPropSystem.PropType.Normal, MonopolyPropSystem.PropColor.Pink));
        //Orange
        Board.Add(new MonopolyPropSystem("Vidusp  Plaza", 0, 0, 100, 1, 0, false, 0, true, 0, false, 16, MonopolyPropSystem.PropType.Normal, MonopolyPropSystem.PropColor.Orange));
        Board.Add(new MonopolyPropSystem("Cryo Solutions", 0, 0, 100, 1, 0, false, 0, true, 0, false, 18, MonopolyPropSystem.PropType.Normal, MonopolyPropSystem.PropColor.Orange));
        Board.Add(new MonopolyPropSystem("Aura \n Media", 0, 0, 120, 1, 0, false, 0, true, 0, false, 19, MonopolyPropSystem.PropType.Normal, MonopolyPropSystem.PropColor.Orange));
        //Blue
        Board.Add(new MonopolyPropSystem("Uplink \n Corp", 0, 0, 100, 1, 0, false, 0, true, 0, false, 21, MonopolyPropSystem.PropType.Normal, MonopolyPropSystem.PropColor.Blue));
        Board.Add(new MonopolyPropSystem("Nero  Motors", 0, 0, 100, 1, 0, false, 0, true, 0, false, 23, MonopolyPropSystem.PropType.Normal, MonopolyPropSystem.PropColor.Blue));
        Board.Add(new MonopolyPropSystem("Sidera \n Security", 0, 0, 120, 1, 0, false, 0, true, 0, false, 24, MonopolyPropSystem.PropType.Normal, MonopolyPropSystem.PropColor.Blue));
        //Yellow
        Board.Add(new MonopolyPropSystem("Night Indust", 0, 0, 100, 1, 0, false, 0, true, 0, false, 26, MonopolyPropSystem.PropType.Normal, MonopolyPropSystem.PropColor.Yellow));
        Board.Add(new MonopolyPropSystem("Nova Phones", 0, 0, 100, 1, 0, false, 0, true, 0, false, 27, MonopolyPropSystem.PropType.Normal, MonopolyPropSystem.PropColor.Yellow));
        Board.Add(new MonopolyPropSystem("Photon Network", 0, 0, 120, 1, 0, false, 0, true, 0, false, 29, MonopolyPropSystem.PropType.Normal, MonopolyPropSystem.PropColor.Yellow));
        //Cyan
        Board.Add(new MonopolyPropSystem("P.S.A.T", 0, 0, 100, 1, 0, false, 0, true, 0, false, 31, MonopolyPropSystem.PropType.Normal, MonopolyPropSystem.PropColor.Cyan));
        Board.Add(new MonopolyPropSystem("Accent Group", 0, 0, 100, 1, 0, false, 0, true, 0, false, 32, MonopolyPropSystem.PropType.Normal, MonopolyPropSystem.PropColor.Cyan));
        Board.Add(new MonopolyPropSystem("Nemo \n Intel", 0, 0, 120, 1, 0, false, 0, true, 0, false, 34, MonopolyPropSystem.PropType.Normal, MonopolyPropSystem.PropColor.Cyan));
        //Grey
        Board.Add(new MonopolyPropSystem("DMA", 0, 0, 100, 1, 0, false, 0, true, 0, false, 37, MonopolyPropSystem.PropType.Normal, MonopolyPropSystem.PropColor.Grey));
        Board.Add(new MonopolyPropSystem("Letni", 0, 0, 120, 1, 0, false, 0, true, 0, false, 39, MonopolyPropSystem.PropType.Normal, MonopolyPropSystem.PropColor.Grey));

        return;
    }

    void BoardMaker()
    {
        for(int i = 0; i < Board.Count; i++)
        {
            if (!Tiles.Contains(Board[i]))
            {
                Tiles.RemoveAt(Board[i].TileLocation);
                Tiles.Insert(Board[i].TileLocation, Board[i]);
            }
        }
    }

    void GameController()
    {

    }

    void DisplayColor()
    {
        if (Tiles[Players[CurrentPlayer].CurrentPos].Colour == MonopolyPropSystem.PropColor.Blue)
        {
            GUI.backgroundColor = new Color32(0, 125, 255, 255);
        }
        if (Tiles[Players[CurrentPlayer].CurrentPos].Colour == MonopolyPropSystem.PropColor.Red)
        {
            GUI.backgroundColor = new Color32(224, 60, 49, 255);
        }
        if (Tiles[Players[CurrentPlayer].CurrentPos].Colour == MonopolyPropSystem.PropColor.Cyan)
        {
            GUI.backgroundColor = Color.cyan;
        }
        if (Tiles[Players[CurrentPlayer].CurrentPos].Colour == MonopolyPropSystem.PropColor.Green)
        {
            GUI.backgroundColor = Color.green;
        }
        if (Tiles[Players[CurrentPlayer].CurrentPos].Colour == MonopolyPropSystem.PropColor.Yellow)
        {
            GUI.backgroundColor = new Color32(255, 255, 0, 255);
        }
        if (Tiles[Players[CurrentPlayer].CurrentPos].Colour == MonopolyPropSystem.PropColor.Pink)
        {
            GUI.backgroundColor = new Color32(255, 192, 203, 255);
        }
        if (Tiles[Players[CurrentPlayer].CurrentPos].Colour == MonopolyPropSystem.PropColor.Grey)
        {
            GUI.backgroundColor = Color.gray;
        }
        if (Tiles[Players[CurrentPlayer].CurrentPos].Colour == MonopolyPropSystem.PropColor.Black)
        {
            GUI.backgroundColor = Color.black;
        }
        if (Tiles[Players[CurrentPlayer].CurrentPos].Colour == MonopolyPropSystem.PropColor.Orange)
        {
            GUI.backgroundColor = new Color32(255, 160, 0, 255);
        }
        if (Tiles[Players[CurrentPlayer].CurrentPos].Colour == MonopolyPropSystem.PropColor.Purple)
        {
            GUI.backgroundColor = new Color32(128, 0, 128, 255);
        }
    }

    void TileColor()
    {
        if (Tiles[CurrentTile].Colour == MonopolyPropSystem.PropColor.Blue)
        {
            GUI.backgroundColor = new Color32(0, 125, 255, 255);
        }
        if (Tiles[CurrentTile].Colour == MonopolyPropSystem.PropColor.Red)
        {
            GUI.backgroundColor = new Color32(224, 60, 49, 255);
        }
        if (Tiles[CurrentTile].Colour == MonopolyPropSystem.PropColor.Cyan)
        {
            GUI.backgroundColor = Color.cyan;
        }
        if (Tiles[CurrentTile].Colour == MonopolyPropSystem.PropColor.Green)
        {
            GUI.backgroundColor = Color.green;
        }
        if (Tiles[CurrentTile].Colour == MonopolyPropSystem.PropColor.Yellow)
        {
            GUI.backgroundColor = new Color32(255, 255, 0, 255);
        }
        if (Tiles[CurrentTile].Colour == MonopolyPropSystem.PropColor.Pink)
        {
            GUI.backgroundColor = new Color32(255, 192, 203, 255);
        }
        if (Tiles[CurrentTile].Colour == MonopolyPropSystem.PropColor.Grey)
        {
            GUI.backgroundColor = Color.gray;
        }
        if (Tiles[CurrentTile].Colour == MonopolyPropSystem.PropColor.Black)
        {
            GUI.backgroundColor = Color.black;
        }
        if (Tiles[CurrentTile].Colour == MonopolyPropSystem.PropColor.Orange)
        {
            GUI.backgroundColor = new Color32(255, 160, 0,255);
        }
        if (Tiles[CurrentTile].Colour == MonopolyPropSystem.PropColor.Purple)
        {
            GUI.backgroundColor = new Color32(128, 0, 128, 255);
        }
    }

    void RenderBoard()
    {
        if (Tiles.Count > 0)
        {
            side = 0;
            side1 = 0;
            side2 = 0;
            for (int i = 0; i < Tiles.Count; i++)
            {
                if (i >= 0 && i < 10)
                {
                    if (Tiles[i].Colour != MonopolyPropSystem.PropColor.None)
                    {
                        CurrentTile = i;
                        TileColor();
                    }
                    if(i == 0)
                    {
                        if (GUI.Button(new Rect(windowRect.width - 50 - 50 * i, windowRect.height - 50, 50, 50), "" + Tiles[i].Name))
                        {

                        }
                    }
                    else
                    {
                        if (Tiles[i].Ownable == true)
                        {
                            if (GUI.Button(new Rect(windowRect.width - 50 - 40 * i, windowRect.height - 50, 40, 50), "" + Tiles[i].Name + "\n $" + Tiles[i].PurchasePrice))
                            {

                            }

                            if(Tiles[i].Owned == true)
                            {
                                GUI.Box(new Rect(windowRect.width - 40 - 40 * i, windowRect.height - 60, 30, 10),"" + Tiles[i].Owner);
                            }
                        }
                        else
                        {
                            if (GUI.Button(new Rect(windowRect.width - 50 - 40 * i, windowRect.height - 50, 40, 50), "" + Tiles[i].Name))
                            {

                            }
                        }
                    }
                    GUI.backgroundColor = Color.white;
                    for (int p = 0; p < Players.Count; p++)
                    {
                        if (Players[p].CurrentPos == i)
                        {
                            GUI.Box(new Rect(windowRect.width - 50 - 40 * i, windowRect.height - 50, 30, 30), PlayerIcons[Players[p].SelectedIcon],Skin.customStyles[3]);
                        }
                    }
                }

                else if (i >= 10 && i < 20)
                {
                    if (Tiles[i].Colour != MonopolyPropSystem.PropColor.None)
                    {
                        CurrentTile = i;
                        TileColor();
                    }
                    if(side == 0)
                    {
                        if (GUI.Button(new Rect(0, windowRect.height - 50 - 50 * side, 50, 50), "" + Tiles[i].Name))
                        {

                        }
                    }
                    else
                    {
                        if (Tiles[i].Ownable == true)
                        {
                            if (GUI.Button(new Rect(0, windowRect.height - 50 - 40 * side, 50, 40), "" + Tiles[i].Name + "\n $" + Tiles[i].PurchasePrice))
                            {

                            }

                            if (Tiles[i].Owned == true)
                            {
                                GUI.Box(new Rect(50, windowRect.height - 40 - 40 * side, 10, 30),"" + Tiles[i].Owner);
                            }
                        }
                        else
                        {
                            if (GUI.Button(new Rect(0, windowRect.height - 50 - 40 * side, 50, 40), "" + Tiles[i].Name))
                            {

                            }
                        }
                    }
                    GUI.backgroundColor = Color.white;
                    for (int p = 0; p < Players.Count; p++)
                    {
                        if (Players[p].CurrentPos == i)
                        {
                            GUI.Box(new Rect(0, windowRect.height - 50 - 40 * side, 30, 30), PlayerIcons[Players[p].SelectedIcon], Skin.customStyles[3]);
                        }
                    }
                    side++;
                }

               else if (i >= 20 && i < 30)
                {
                    if (Tiles[i].Colour != MonopolyPropSystem.PropColor.None)
                    {
                        CurrentTile = i;
                        TileColor();
                    }
                    if (side1 == 0)
                    {
                        if (GUI.Button(new Rect(50 * side1, 0, 50, 50), "" + Tiles[i].Name))
                        {

                        }
                    }
                    else
                    {
                        if (Tiles[i].Ownable == true)
                        {
                            if (GUI.Button(new Rect(10 + 40 * side1, 0, 40, 50), "" + Tiles[i].Name + "\n $" + Tiles[i].PurchasePrice))
                            {

                            }

                            if (Tiles[i].Owned == true)
                            {
                                GUI.Box(new Rect(10 + 40 * side1, 50, 30, 10), "" + Tiles[i].Owner);
                            }
                        }
                        else
                        {
                            if (GUI.Button(new Rect(10 + 40 * side1, 0, 40, 50), "" + Tiles[i].Name))
                            {

                            }
                        }
                    }
                    GUI.backgroundColor = Color.white;
                    for (int p = 0; p < Players.Count; p++)
                    {
                        if (Players[p].CurrentPos == i)
                        {
                            GUI.Box(new Rect(10 + 40 * side1, 0, 30, 30), PlayerIcons[Players[p].SelectedIcon], Skin.customStyles[3]);
                        }
                    }
                    side1++;
                }

                else if (i >= 30 && i < 40)
                {
                    if (Tiles[i].Colour != MonopolyPropSystem.PropColor.None)
                    {
                        CurrentTile = i;
                        TileColor();
                    }
                    if (side2 == 0)
                    {
                        if (GUI.Button(new Rect(windowRect.width - 50, 50 * side2, 50, 50), "" + Tiles[i].Name))
                        {

                        }
                    }
                    else
                    {
                        if (Tiles[i].Ownable == true)
                        {
                            if (GUI.Button(new Rect(windowRect.width - 50, 10 + 40 * side2, 50, 40), "" + Tiles[i].Name + "\n $" + Tiles[i].PurchasePrice))
                            {

                            }

                            if (Tiles[i].Owned == true)
                            {
                                GUI.Box(new Rect(windowRect.width - 60, 10 + 40 * side2, 10, 30), "" + Tiles[i].Owner);
                            }
                        }
                        else
                        {
                            if (GUI.Button(new Rect(windowRect.width - 50, 10 + 40 * side2, 50, 40), "" + Tiles[i].Name))
                            {

                            }
                        }
                    }
                    GUI.backgroundColor = Color.white;
                    for (int p = 0; p < Players.Count; p++)
                    {
                        if (Players[p].CurrentPos == i)
                        {
                            GUI.Box(new Rect(windowRect.width - 50, 10 + 40 * side2, 30, 30), PlayerIcons[Players[p].SelectedIcon], Skin.customStyles[3]);
                        }
                    }
                    side2++;
                }
            }
        }
    }
}
