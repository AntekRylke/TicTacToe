using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Threading;

namespace TicTacToe
{
	public class Board
	{
		bool gameFinished = false;
		bool player1Turn = true;
		public int turnCount = 0;
		int gameCount = 0;
		public string[,] board3X3 = new string[3, 3];
		Player player1;
		Player player2;
		public Board(Player player1, Player player2)
		{
			// assign players
			this.player1 = player1;
			this.player2 = player2;
			Reset();
		}

		void RefreshBoard()
		{
			Console.Clear();
			Console.WriteLine($"Turn number {turnCount + 1} \n");
			Console.WriteLine($"   |   |   ");
			Console.WriteLine($" {board3X3[0, 0]} | {board3X3[0, 1]} | {board3X3[0, 2]} ");
			Console.WriteLine($"___|___|___");
			Console.WriteLine($"   |   |   ");
			Console.WriteLine($" {board3X3[1, 0]} | {board3X3[1, 1]} | {board3X3[1, 2]} ");
			Console.WriteLine($"___|___|___");
			Console.WriteLine($"   |   |   ");
			Console.WriteLine($" {board3X3[2, 0]} | {board3X3[2, 1]} | {board3X3[2, 2]} ");
			Console.WriteLine($"   |   |   ");
		}

		void Reset()
		{
			this.gameFinished = false;
			this.turnCount = 0;
			// populate board with numbers
			for (int i = 0; i < board3X3.GetLength(0); i++)
			{
				for (int j = 0; j < board3X3.GetLength(1); j++)
				{
					board3X3[i, j] = ((i * 3) + (j + 1)).ToString();
				}
			}
		}

		void DisplayResults(bool isTie)
		{
			gameFinished = true;
			if (isTie) Console.WriteLine("It's a tie!");
			else if (player1Turn) player1.Victory();
			else player2.Victory();
			// ask wether to play again
			Console.WriteLine("Press 'R' for rematch or any other key to close");
			string input = Console.ReadKey().Key.ToString();
			if (input.Equals("R"))
			{
				gameCount++;
				player1Turn = !player1Turn;
				Reset();
				PlayGame();
			}
			else DisplayScoreboard();
			Console.WriteLine("Press any key to quit");
			Console.ReadKey();
		}

		void DisplayScoreboard()
		{
			Console.Clear();
			Console.WriteLine($"{player1.name}: {player1.noOfVictories} victories");
			Console.WriteLine($"{player2.name}: {player2.noOfVictories} victories");
		}

		void VerifyIfGameFinished()
		{
			// end game if horizontal line is checked
			if (board3X3[0, 0].Equals(board3X3[0, 1]) && board3X3[0, 1].Equals(board3X3[0, 2])) DisplayResults(false);
			if (board3X3[1, 0].Equals(board3X3[1, 1]) && board3X3[1, 1].Equals(board3X3[1, 2])) DisplayResults(false);
			if (board3X3[2, 0].Equals(board3X3[2, 1]) && board3X3[2, 1].Equals(board3X3[2, 2])) DisplayResults(false);
			// end game if vertical line is checked
			if (board3X3[0, 0].Equals(board3X3[1, 0]) && board3X3[1, 0].Equals(board3X3[2, 0])) DisplayResults(false);
			if (board3X3[0, 1].Equals(board3X3[1, 1]) && board3X3[1, 1].Equals(board3X3[2, 1])) DisplayResults(false);
			if (board3X3[0, 2].Equals(board3X3[1, 2]) && board3X3[1, 2].Equals(board3X3[2, 2])) DisplayResults(false);
			// end game if diagonal line is checked
			if (board3X3[0, 0].Equals(board3X3[1, 1]) && board3X3[1, 1].Equals(board3X3[2, 2])) DisplayResults(false);
			if (board3X3[0, 2].Equals(board3X3[1, 1]) && board3X3[1, 1].Equals(board3X3[2, 0])) DisplayResults(false);
			// end game if board is full
			if (!gameFinished)
			{
				if (turnCount >= 8) DisplayResults(true);
			}
		}

		public void PlayGame()
		{
			int spaceNo;
			RefreshBoard();
			while (!gameFinished)
			{
			if (player1Turn)
				{
					Console.WriteLine($"{player1.name}, choose space to mark");
					if (player1.isPCControlled)
					{
						Thread.Sleep(300);
						try
						{
							MarkSpace(false, player1.MakeMove(this));
							turnCount++;
							player1Turn = false;
						}
						catch (ArgumentException)
						{
							continue;
						}
					}
					else if (int.TryParse(Console.ReadKey().KeyChar.ToString(), out spaceNo))
					{
						try
						{
							MarkSpace(false, spaceNo);
							turnCount++;
							player1Turn = false;
						}
						catch (ArgumentException)
						{
							Console.WriteLine($"Space is already occupied by {player2.name}");
							continue;
						}
					}
					else
					{
						Console.WriteLine("Invalid space number");
						continue;
					}
				}
			else
				{
					Console.WriteLine($"{player2.name}, choose space to mark");
					if (player2.isPCControlled)
					{
						Thread.Sleep(300);
						try
						{
							MarkSpace(true, player2.MakeMove(this));
							turnCount++;
							player1Turn = true;
						}
						catch (ArgumentException)
						{
							continue;
						}
					}
					else if (int.TryParse(Console.ReadKey().KeyChar.ToString(), out spaceNo))
					{
						try
						{
							MarkSpace(true, spaceNo);
							turnCount++;
							player1Turn = true;
						}
						catch (ArgumentException)
						{
							Console.WriteLine($"Space is already occupied by {player1.name}");
							continue;
						}
					}
					else
					{
						Console.WriteLine("Invalid space number");
						continue;
					}
				}
			}
		}

		void MarkSpace(bool player, int spaceNo)
		// false: -1: player 1, true: -2: player 2
		{
			switch (spaceNo)
			{
				case 1:
					if (!board3X3[0, 0].Equals("x") && !board3X3[0, 0].Equals("o"))
					{
						if (player) board3X3[0, 0] = "x";
						else board3X3[0, 0] = "o";
					}
					else throw new ArgumentException();
					break;
				case 2:
					if (!board3X3[0, 1].Equals("x") && !board3X3[0, 1].Equals("o"))
					{
						if (player) board3X3[0, 1] = "x";
						else board3X3[0, 1] = "o";
					}
					else throw new ArgumentException();
					break;
				case 3:
					if (!board3X3[0, 2].Equals("x") && !board3X3[0, 2].Equals("o"))
					{
						if (player) board3X3[0, 2] = "x";
						else board3X3[0, 2] = "o";
					}
					else throw new ArgumentException();
					break;
				case 4:
					if (!board3X3[1, 0].Equals("x") && !board3X3[1, 0].Equals("o"))
					{
						if (player) board3X3[1, 0] = "x";
						else board3X3[1, 0] = "o";
					}
					else throw new ArgumentException();
					break;
				case 5:
					if (!board3X3[1, 1].Equals("x") && !board3X3[1, 1].Equals("o"))
					{
						if (player) board3X3[1, 1] = "x";
						else board3X3[1, 1] = "o";
					}
					else throw new ArgumentException();
					break;
				case 6:
					if (!board3X3[1, 2].Equals("x") && !board3X3[1, 2].Equals("o"))
					{
						if (player) board3X3[1, 2] = "x";
						else board3X3[1, 2] = "o";
					}
					else throw new ArgumentException();
					break;
				case 7:
					if (!board3X3[2, 0].Equals("x") && !board3X3[2, 0].Equals("o"))
					{
						if (player) board3X3[2, 0] = "x";
						else board3X3[2, 0] = "o";
					}
					else throw new ArgumentException();
					break;
				case 8:
					if (!board3X3[2, 1].Equals("x") && !board3X3[2, 1].Equals("o"))
					{
						if (player) board3X3[2, 1] = "x";
						else board3X3[2, 1] = "o";
					}
					else throw new ArgumentException();
					break;
				case 9:
					if (!board3X3[2, 2].Equals("x") && !board3X3[2, 2].Equals("o"))
					{
						if (player) board3X3[2, 2] = "x";
						else board3X3[2, 2] = "o";
					}
					else throw new ArgumentException();
					break;
				default:
					break;
			}
			RefreshBoard();
			VerifyIfGameFinished();
		}
	}
}
