﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameDB;

namespace Modul_11_Black_Jack
{
	class Program
	{
		static void Main(string[] args)
		{
			void StartGame()
			{
				Game game = new Game();
				Console.WriteLine(game.ToString());
			}
			
			Console.WriteLine("Do u want to start new game? y/n");
			ConsoleKeyInfo key = Console.ReadKey();
			Console.CursorLeft = 0;
			Console.WriteLine(' ');
			if (key.Key == ConsoleKey.Y)
			{
				StartGame();
			}
			if (key.Key == ConsoleKey.N)
				System.Environment.Exit(1);

			while (true)
			{
				Console.WriteLine("Play againe? y/n");
				ConsoleKeyInfo key1 = Console.ReadKey();
				Console.CursorLeft = 0;
				Console.WriteLine(' ');
				if (key1.Key == ConsoleKey.Y)
				{
					StartGame();
				}
				if (key1.Key == ConsoleKey.N)
					System.Environment.Exit(1);
			}
		}
	}
}

