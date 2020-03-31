using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackJack : MonoBehaviour 
{
	public List<PlayingCardSystem> CardDeck = new List<PlayingCardSystem>();
	public List<PlayingCardSystem> DealersHand = new List<PlayingCardSystem>();
	public List<PlayingCardSystem> PlayersHand = new List<PlayingCardSystem>();
	public int Count;
	public string SelectedGen;
	public int SelectedCard;
	public bool restart;
	public bool dealcards;
	public int DealerTotal;
	public int PlayerTotal;
	public string Status;
	public bool PlayerStands;
	public bool PlayerHit;
	public bool DealerStands;

	public string MenuSelector;
	private MiniGameWeb mgw;
	// Use this for initialization
	void Start () 
	{
		MenuSelector = "Main Menu";
		mgw = GetComponent<MiniGameWeb>();
		SelectedGen = "Clubs";
		CardDeck.Add (new PlayingCardSystem (PlayingCardSystem.CardSuit.None, PlayingCardSystem.FaceCards.Joker, 0));
	}

	void Game()
	{
		if (CardDeck.Count < 58) 
		{
			CardGen();
		}

		if (restart == true) 
		{
			Restart();
		}

		if (dealcards == true) 
		{
			Dealing();
		}

		AI ();
		Player();

		if (PlayerStands == true && DealerStands == true) 
		{
			GameStatus();
		}
	}

	void CardCount()
	{
		DealerTotal = 0;
		PlayerTotal = 0;
		for (int i = 0; i < DealersHand.Count; i++) 
		{
			DealerTotal += DealersHand [i].Value;
			if (DealersHand[i].Face == PlayingCardSystem.FaceCards.Ace) 
			{
				if (DealerTotal < 21) 
				{
					DealersHand [i].Value = 11;
				} 
				else 
				{
					DealersHand [i].Value = 1;
				}
			}
		}

		for (int j = 0; j < PlayersHand.Count; j++) 
		{
			PlayerTotal += PlayersHand [j].Value;
			if (PlayersHand[j].Face == PlayingCardSystem.FaceCards.Ace) 
			{
				if (PlayerTotal < 21) 
				{
					PlayersHand [j].Value = 11;
				} 
				else 
				{
					PlayersHand [j].Value = 1;
				}
			}
		}

		if (PlayerTotal >= 21)
		{
			PlayerStands = true;
			DealerStands = true;
			GameStatus();
		}

		if (DealerTotal >= 21)
		{
			PlayerStands = true;
			DealerStands = true;
			GameStatus();
		}

		if (DealerTotal == PlayerTotal)
		{
			PlayerStands = true;
			DealerStands = true;
			GameStatus();
		}

	}

	void GameStatus()
	{
		if (PlayerTotal > 21)
		{
			Status = "Player Bust";
		}
		if (DealerTotal > 21)
		{
			Status = "Dealer Bust";
		}
		if (DealerTotal > PlayerTotal && DealerTotal < 21)
		{
			Status = "Dealer Wins";
		}
		if (PlayerTotal > DealerTotal && PlayerTotal < 21)
		{
			Status = "Player Wins";
		}

		if (DealerTotal == PlayerTotal)
		{
			Status = "No Win";
		}
	}

	void AI()
	{
		if (PlayerStands == true && DealerStands == false) 
		{
			if (DealerTotal < PlayerTotal)
			{
				SelectedCard = Random.Range (1, CardDeck.Count);
				DealersHand.Add (CardDeck [SelectedCard]);
				CardDeck.RemoveAt (SelectedCard);
				CardCount();
			}
			if (DealerTotal > 17)
			{
				if (DealerTotal > PlayerTotal)
				{
					DealerStands = true;
				}
			}
		}
	}

	void Player()
	{
		if (PlayerHit == true)
		{
			SelectedCard = Random.Range (1, CardDeck.Count);
			PlayersHand.Add (CardDeck [SelectedCard]);
			CardDeck.RemoveAt (SelectedCard);
			CardCount();
			PlayerHit = false;
		}
	}

	void Dealing()
	{
		SelectedCard = Random.Range (1, CardDeck.Count);
		DealersHand.Add (CardDeck [SelectedCard]);
		CardDeck.RemoveAt (SelectedCard);

		SelectedCard = Random.Range (1, CardDeck.Count);
		PlayersHand.Add (CardDeck [SelectedCard]);
		CardDeck.RemoveAt (SelectedCard);

		SelectedCard = Random.Range (1, CardDeck.Count);
		DealersHand.Add (CardDeck [SelectedCard]);
		CardDeck.RemoveAt (SelectedCard);

		SelectedCard = Random.Range (1, CardDeck.Count);
		PlayersHand.Add (CardDeck [SelectedCard]);
		CardDeck.RemoveAt (SelectedCard);

		CardCount();

		dealcards = false;
	}

	void Restart()
	{
		CardDeck.RemoveRange (1,CardDeck.Count-1);
		PlayersHand.RemoveRange (0,PlayersHand.Count);
		DealersHand.RemoveRange (0,DealersHand.Count);
		SelectedGen = "Clubs";
		DealerTotal = 0;
		PlayerTotal = 0;
		PlayerStands = false;
		DealerStands = false;
		restart = false;
		Status = " ";
	}

	void CardGen()
	{
		for (int i = 0; CardDeck.Count < 57; i++) 
		{
			if (SelectedGen == "Clubs") 
			{
				if(CardDeck[i].Suit != PlayingCardSystem.CardSuit.Clubs)
				{
					if(CardDeck[i].Value == 0)
					{
						if (Count < 10) 
						{
							Count++;
							CardDeck.Add (new PlayingCardSystem (PlayingCardSystem.CardSuit.Clubs, PlayingCardSystem.FaceCards.None, Count));

						}
						if (Count == 10) 
						{
							CardDeck.Add (new PlayingCardSystem (PlayingCardSystem.CardSuit.Clubs, PlayingCardSystem.FaceCards.Ace, Count));
							CardDeck.Add (new PlayingCardSystem (PlayingCardSystem.CardSuit.Clubs, PlayingCardSystem.FaceCards.Jack, Count));
							CardDeck.Add (new PlayingCardSystem (PlayingCardSystem.CardSuit.Clubs, PlayingCardSystem.FaceCards.Queen, Count));
							CardDeck.Add (new PlayingCardSystem (PlayingCardSystem.CardSuit.Clubs, PlayingCardSystem.FaceCards.King, Count));
							Count = 0;
							SelectedGen = "Diamonds";
						}
					}
				}
			}
			if (SelectedGen == "Diamonds") 
			{
				if(CardDeck[i].Suit != PlayingCardSystem.CardSuit.Diamonds)
				{
					if(CardDeck[i].Value == 0)
					{
						if (Count < 10) 
						{
							Count++;
							CardDeck.Add (new PlayingCardSystem (PlayingCardSystem.CardSuit.Diamonds, PlayingCardSystem.FaceCards.None, Count));

						}
						if (Count == 10) 
						{
							CardDeck.Add (new PlayingCardSystem (PlayingCardSystem.CardSuit.Diamonds, PlayingCardSystem.FaceCards.Ace, Count));
							CardDeck.Add (new PlayingCardSystem (PlayingCardSystem.CardSuit.Diamonds, PlayingCardSystem.FaceCards.Jack, Count));
							CardDeck.Add (new PlayingCardSystem (PlayingCardSystem.CardSuit.Diamonds, PlayingCardSystem.FaceCards.Queen, Count));
							CardDeck.Add (new PlayingCardSystem (PlayingCardSystem.CardSuit.Diamonds, PlayingCardSystem.FaceCards.King, Count));
							Count = 0;
							SelectedGen = "Spades";
						}
					}
				}
			}
			if (SelectedGen == "Spades") 
			{
				if(CardDeck[i].Suit != PlayingCardSystem.CardSuit.Spades)
				{
					if(CardDeck[i].Value == 0)
					{
						if (Count < 10) 
						{
							Count++;
							CardDeck.Add (new PlayingCardSystem (PlayingCardSystem.CardSuit.Spades, PlayingCardSystem.FaceCards.None, Count));

						}
						if (Count == 10) 
						{
							CardDeck.Add (new PlayingCardSystem (PlayingCardSystem.CardSuit.Spades, PlayingCardSystem.FaceCards.Ace, Count));
							CardDeck.Add (new PlayingCardSystem (PlayingCardSystem.CardSuit.Spades, PlayingCardSystem.FaceCards.Jack, Count));
							CardDeck.Add (new PlayingCardSystem (PlayingCardSystem.CardSuit.Spades, PlayingCardSystem.FaceCards.Queen, Count));
							CardDeck.Add (new PlayingCardSystem (PlayingCardSystem.CardSuit.Spades, PlayingCardSystem.FaceCards.King, Count));
							Count = 0;
							SelectedGen = "Hearts";
						}
					}
				}
			}
			if (SelectedGen == "Hearts") 
			{
				if(CardDeck[i].Suit != PlayingCardSystem.CardSuit.Hearts)
				{
					if(CardDeck[i].Value == 0)
					{
						if (Count < 10) 
						{
							Count++;
							CardDeck.Add (new PlayingCardSystem (PlayingCardSystem.CardSuit.Hearts, PlayingCardSystem.FaceCards.None, Count));

						}
						if (Count == 10) 
						{
							CardDeck.Add (new PlayingCardSystem (PlayingCardSystem.CardSuit.Hearts, PlayingCardSystem.FaceCards.Ace, Count));
							CardDeck.Add (new PlayingCardSystem (PlayingCardSystem.CardSuit.Hearts, PlayingCardSystem.FaceCards.Jack, Count));
							CardDeck.Add (new PlayingCardSystem (PlayingCardSystem.CardSuit.Hearts, PlayingCardSystem.FaceCards.Queen, Count));
							CardDeck.Add (new PlayingCardSystem (PlayingCardSystem.CardSuit.Hearts, PlayingCardSystem.FaceCards.King, Count));
							Count = 0;
							SelectedGen = "Hearts";
						}
					}
				}
			}
		}
	}

	public void GameRender()
	{
		//GUI.backgroundColor = buttonColor;
		//GUI.contentColor = fontColor;

		Game();

		switch (MenuSelector)
		{
		case "Main Menu":
			if (GUI.Button (new Rect (1, 100, 150, 22), "Start")) 
			{
				MenuSelector = "Game";
			}

			if (GUI.Button (new Rect (1, 150, 150, 22), "Quit")) 
			{
				mgw.Selectedgame = "None";
			}
			break;
		}

		if (MenuSelector == "Game")
		{
			GUI.Label (new Rect (100, 100, 100, 22), Status);

			GUI.Button (new Rect (5, 50, 200, 22),"Dealers Total: " + DealerTotal);
			GUI.Button (new Rect (5, 75, 200, 22),"Players Total: " + PlayerTotal);

			if (DealerStands == true)
			{
				if (GUI.Button (new Rect (100, 150, 70, 21), "Buy In")) 
				{
					restart = true;
				}

				if (GUI.Button (new Rect (175, 150, 70, 21), "Fold")) 
				{
					MenuSelector = "Main Menu";
				}
			} 
			else
			{
				if (PlayersHand.Count < 2)
				{
					if (GUI.Button (new Rect (100, 150, 70, 21), "Deal")) 
					{
						dealcards = true;
					}

					if (Input.GetKey (KeyCode.D))
					{
						dealcards = true;
					}
				}
				else 
				{
					if (GUI.Button (new Rect (100, 150, 70, 21), "Hit")) 
					{
						PlayerHit = true;
					}


					if (GUI.Button (new Rect (175, 150, 70, 21), "Stand")) 
					{
						PlayerStands = true;
					}


					if (Input.GetKey (KeyCode.S))
					{
						PlayerStands = true;
					}


					if (Input.GetKey (KeyCode.Space)) 
					{
						PlayerHit = true;
					}
				}
			}
		}
	}
}