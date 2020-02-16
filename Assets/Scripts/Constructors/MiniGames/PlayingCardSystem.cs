using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayingCardSystem
{
	public int Value;
	public CardSuit Suit;
	public FaceCards Face;

	public enum FaceCards
	{
		None,
		Ace,
		Jack,
		Queen,
		King,
		Joker
	}

	public enum CardSuit
	{
		Clubs,
		Spades,
		Hearts,
		Diamonds,
		None
	}

	public PlayingCardSystem(CardSuit suit,FaceCards face,int value)
	{
		Suit = suit;
		Face = face;
		Value = value;
	}
}