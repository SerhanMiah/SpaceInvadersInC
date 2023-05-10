using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace SpaceInvaders
{
	public class GameGrid
	{

		private readonly int[,] grid;

		public int Rows { get; }
		public int Columns { get; }


		public GameGrid(int rows, int columns)
		{
			Rows = rows;
			Columns = columns;
			grid = new int[rows, Columns];
		}
	}
}