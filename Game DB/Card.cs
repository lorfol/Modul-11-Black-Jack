using System;
using System.Collections.Generic;
using System.Text;

namespace GameDB
{
	public enum CardName
	{
		TWO = 2, THREE, FOUR, FIVE, SIX, SEVEN, EIGHT, NINE, TEN, JACK = 2, QUEEN, KING, ACE = 11
	}

	public enum SuitName
	{
		CLUBS, SPADES, HEARTS, DIAMOND
	}

	public struct Card
	{
		public CardName cardName;
		public SuitName suitName;
		public bool isUsed;
	};
}