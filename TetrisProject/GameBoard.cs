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
            if (this[row, column] == 0)
                return true;
            else
                return false;
        }
        
        
    }
}
