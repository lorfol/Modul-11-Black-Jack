﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameDB;

namespace Modul_11_Black_Jack
{
	class Game
	{
		Card[] arrayOfCards = new Card[52];
		Random rand = new Random();
		int _sumOfPlayerPoints = 0; // user's points
		int _sumOfComputerPoints = 0;

		public Game()
		{
			int index = 0; // проверка на повторение карт
			foreach (SuitName suits in Enum.GetValues(typeof(SuitName))) // вместо двоек вальты, вмето троек дамы, королей, двоек и троек нет....
			{
				foreach (CardName cards in Enum.GetValues(typeof(CardName)))
				{
					arrayOfCards[index] = new Card { suitName = suits, cardName = cards };
					index++;
				}
			}
			// посмотреть содержимое массива
			CheckWhoWillFirst();
		}


		public void CheckWhoWillFirst()
		{
			Card player = arrayOfCards[rand.Next(0, 52)];
			Card computer = arrayOfCards[rand.Next(0, 52)];

			if (player.cardName > computer.cardName)
			{
				Console.WriteLine("U start!");
				Console.WriteLine();
				PlayerStart();
			}
			else if (player.cardName == computer.cardName)
			{
				CheckWhoWillFirst();
			}
			else
			{
				Console.WriteLine("Computer start!");
				Console.WriteLine();
				ComputerStart();
			}
		}

		private void PlayerStart()
		{
			_sumOfPlayerPoints = GetTwoFirstCardsForPlayer();
			if (_sumOfPlayerPoints > 21)
				_sumOfPlayerPoints = 0;

			while (true)
			{
				Console.WriteLine("More? y/n");
				ConsoleKeyInfo key = Console.ReadKey();
				Console.CursorLeft = 0;
				Console.WriteLine(' ');
				if (key.Key == ConsoleKey.Y)
				{
					while (true)
					{
						int index = rand.Next(0, 52);
						if (!arrayOfCards[index].isUsed)
						{
							_sumOfPlayerPoints += (int)arrayOfCards[index].cardName;
							Console.WriteLine($"{arrayOfCards[index].cardName} - {arrayOfCards[index].suitName}");
							arrayOfCards[index].isUsed = true;
							break;
						}
					}
				}
				else break;
			}

			Console.WriteLine($"U have {_sumOfPlayerPoints} points.");
			Console.WriteLine();
			if (_sumOfComputerPoints == 0 && _sumOfPlayerPoints == 21)
				ShowWhoWon();
			if (_sumOfComputerPoints == 0 && _sumOfPlayerPoints != 21)
				ComputerStart();
			Console.WriteLine();
		
			if (_sumOfComputerPoints != 0)
				ShowWhoWon();
		}

		private void ComputerStart()
		{
			_sumOfComputerPoints = GetTwoFirstCardsForComputer();

			while (true)
			{
				if (_sumOfComputerPoints <= 19)
				{
					int index = rand.Next(0, 52);
					if (!arrayOfCards[index].isUsed)
					{
						Console.WriteLine("Comp say: \"MORE\"!");
						_sumOfComputerPoints += (int)arrayOfCards[index].cardName;
						Console.WriteLine($"{arrayOfCards[index].cardName} - {arrayOfCards[index].suitName}");
						arrayOfCards[index].isUsed = true;
					}
				}
				if (_sumOfComputerPoints == 21 || _sumOfComputerPoints > 21 || _sumOfComputerPoints >= 19)
				{
					break;
				}
			}
			Console.WriteLine($"Computer have {_sumOfComputerPoints} points.");
			Console.WriteLine();
			if (_sumOfPlayerPoints == 0 && _sumOfComputerPoints == 21)//11111111111
				ShowWhoWon();
			if (_sumOfPlayerPoints == 0 && _sumOfComputerPoints!=21)
				PlayerStart();
			Console.WriteLine();
			if (_sumOfPlayerPoints != 0)
				ShowWhoWon();
		}

		private int GetTwoFirstCardsForPlayer()
		{
			Console.WriteLine("Yours two first cards:");
			int random = 0;
			for (int i = 0; i < 2; i++)
			{
				bool isIn = false;
				random = rand.Next(0, 52);

				while (!isIn) //в массиве повтор карт. из-за этого первая карта isUsed = true.
				{
					if (!arrayOfCards[random].isUsed)
					{
						_sumOfPlayerPoints += (int)arrayOfCards[random].cardName;
						arrayOfCards[random].isUsed = true;
						Console.WriteLine($"{arrayOfCards[random].cardName} - {arrayOfCards[random].suitName}");
						isIn = true;
					}
				}
			}
			Console.WriteLine("----------");
			if (_sumOfPlayerPoints == 22)
			{
				_sumOfPlayerPoints = 21;
				ShowWhoWon(); //22222
			}
			return _sumOfPlayerPoints;
		}
		private int GetTwoFirstCardsForComputer()
		{
			Console.WriteLine("Computer's two first cards:");
			int random = 0;
			for (int i = 0; i < 2; i++)
			{
				bool isIn = false;
				random = rand.Next(0, 52);

				while (!isIn)
				{
					if (!arrayOfCards[random].isUsed)
					{
						_sumOfComputerPoints += (int)arrayOfCards[random].cardName;
						arrayOfCards[random].isUsed = true;
						Console.WriteLine($"{arrayOfCards[random].cardName} - {arrayOfCards[random].suitName}");
						isIn = true;
					}
				}
			}
			if (_sumOfComputerPoints == 22)
			{
				_sumOfComputerPoints = 21;
				ShowWhoWon(); // узнать как убрать второй вывод на экран
			}
			Console.WriteLine("----------");
			return _sumOfComputerPoints;
		}

		public void ShowWhoWon()
		{
			if ((_sumOfPlayerPoints > _sumOfComputerPoints) && _sumOfPlayerPoints<=21)
				Console.WriteLine("You won! Congratulations!");
			else if((_sumOfPlayerPoints > _sumOfComputerPoints) && _sumOfPlayerPoints > 21)
				Console.WriteLine("Computer won.");
			if ((_sumOfPlayerPoints < _sumOfComputerPoints) && _sumOfComputerPoints <= 21)
				Console.WriteLine("Computer won.");
			else if((_sumOfPlayerPoints < _sumOfComputerPoints) && _sumOfComputerPoints > 21)
				Console.WriteLine("You won! Congratulations!");
			if (_sumOfPlayerPoints == _sumOfComputerPoints)
				Console.WriteLine("Draw.");
		}
		
		string _winer;
		string _looser;
		int winersPoints;
		int loosersPoints;

		public override string ToString()
		{
			if ((_sumOfPlayerPoints > _sumOfComputerPoints) && _sumOfPlayerPoints <= 21)
			{
				_winer = "Player";
				_looser = "Computer";
			}else if((_sumOfPlayerPoints > _sumOfComputerPoints) && _sumOfPlayerPoints > 21)
				{
				_winer = "Computer";
				_looser = "Player";
			}
			if ((_sumOfPlayerPoints < _sumOfComputerPoints) && _sumOfComputerPoints <= 21)
			{
				_winer = "Computer";
				_looser = "Player";
			}else if((_sumOfPlayerPoints < _sumOfComputerPoints) && _sumOfComputerPoints > 21)
			{
				_winer = "Player";
				_looser = "Computer";
			}
			if ((_sumOfPlayerPoints > _sumOfComputerPoints) && _sumOfPlayerPoints <= 21)
			{
				winersPoints = _sumOfPlayerPoints;
				loosersPoints = _sumOfComputerPoints;
			}
			else if ((_sumOfPlayerPoints > _sumOfComputerPoints) && _sumOfPlayerPoints > 21)
			{
				winersPoints = _sumOfComputerPoints;
				loosersPoints = _sumOfPlayerPoints;
			}
			if ((_sumOfPlayerPoints < _sumOfComputerPoints) && _sumOfComputerPoints <= 21)
			{
				winersPoints = _sumOfComputerPoints;
				loosersPoints = _sumOfPlayerPoints;
			}
			else if ((_sumOfPlayerPoints < _sumOfComputerPoints) && _sumOfComputerPoints > 21)
			{
				winersPoints = _sumOfPlayerPoints;
				loosersPoints = _sumOfComputerPoints;
			}

			return ($"Победил {_winer}! Он набрал {winersPoints}, а {_looser} набрал {loosersPoints}");
		}
	}
}