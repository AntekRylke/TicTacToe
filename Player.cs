using System;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace TicTacToe
{
	public class Player
	{
		public string name;
		public bool isPCControlled;
		public bool isCircle;
		public int noOfVictories = 0;
		public Player(string name, bool isCircle)
		{
			this.isCircle = isCircle;
			if (name.Equals("") && isCircle)
			{
				this.name = "Player1";
				isPCControlled = true;
			}
			else if (name.Equals("") && !isCircle)
			{
				this.name = "Player2";
				isPCControlled = true;
			}
			else this.name = name;
		}
		public Player(string name, bool isCircle, bool pcControlled)
		{
			this.isCircle = isCircle;
			if (name.Equals("") && isCircle) this.name = "Player1";
			if (name.Equals("") && !isCircle) this.name = "Player2";
			else this.name = name;
			this.isPCControlled = pcControlled;
		}
		public void Victory()
		{
			noOfVictories++;
			Console.WriteLine($"{name} wins!");
			if (noOfVictories > 1) Console.WriteLine($"{name} won {noOfVictories} times!");
		}

		public int MakeMove(Board board)
		{
			Random random = new Random();
			int choice = random.Next(0, 10);
			if (board.turnCount == 0) choice = 5;
			else if (board.board3X3[0, 0].Equals(board.board3X3[0, 1]))
				if (board.board3X3[0, 2].Equals("3")) choice = 3;
			else if (board.board3X3[1, 0].Equals(board.board3X3[1, 1]))
				if (board.board3X3[1, 2].Equals("6")) choice = 6;
			else if (board.board3X3[2, 0].Equals(board.board3X3[2, 1]))
				if (board.board3X3[2, 2].Equals("9")) choice = 9;
			else if (board.board3X3[0, 0].Equals(board.board3X3[1, 0]))
				if (board.board3X3[2, 0].Equals("7")) choice = 7;
			else if (board.board3X3[0, 1].Equals(board.board3X3[1, 1]))
				if (board.board3X3[2, 1].Equals("8")) choice = 8;
			else if (board.board3X3[0, 2].Equals(board.board3X3[1, 2]))
				if (board.board3X3[2, 2].Equals("9")) choice = 9;
			else if (board.board3X3[0, 0].Equals(board.board3X3[1, 1]))
				if (board.board3X3[2, 2].Equals("9")) choice = 9;
			else if (board.board3X3[0, 2].Equals(board.board3X3[1, 1]))
				if (board.board3X3[2, 0].Equals("7")) choice = 7;

			return choice;
		}
	}
}
