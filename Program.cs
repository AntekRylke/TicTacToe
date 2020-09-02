using System;
using System.Diagnostics;
using System.Reflection;

namespace TicTacToe
{
	public class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("Insert Player 1 name or press enter for computer controlled player");
			Player player1 = new Player(Console.ReadLine(), true);
			Console.WriteLine("Insert Player 2 name or press enter for computer controlled player");
			Player player2 = new Player(Console.ReadLine(), false);
			Board board = new Board(player1, player2);
			board.PlayGame();

			
		}
	}
}
