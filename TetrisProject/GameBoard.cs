using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TetrisProject
{
    public class GameBoard
    {
        private int[,] grid; 

        public GameBoard()
        {
            grid = new int[22, 10];

            for (int r=0; r< 22; r++)
            {
                for (int c = 0; c < 10; c++)
                {
                    grid[r,c] = 0;
                }
            }
        }

        public int this [int row, int column]
        {
            get => grid[row, column];
            set => grid[row, column] = value;
        }
  
        public void PlaceTile(int row, int column, int ID)
        {
            this[row, column] = ID;
        }
        public bool CanMove (int row, int column)
        {
            if (column < 0 || column > 9 || row > 21 || row<0)
                return false;
            else if (grid[row, column] != 0)
                return false;
            else
                return true;
        }
        public bool IsRowFull(int row)
        {
            for (int i = 0; i < 10; i++)
            {
                if (grid[row, i] == 0)
                    return false;
            }
            return true;
        }
        public bool IsRowEmpty(int row)
        {
            for (int i = 0; i < 10; i++)
            {
                if (grid[row, i] != 0)
                    return false;
            }
            return true;
        }
        public void EraseRow(int row)
        {
            for(int i = 0; i < 10; i++)
            {
                grid[row, i] = 0;
            }
        }
        public void MoveRows(int row, int counter)
        {
            for(int i = row; i > 0; i--)
            {
                for(int j = 0; j < 10; j++)
                {
                    grid[i + counter, j] = grid[i, j];
                }
            }
        }
    }
}
