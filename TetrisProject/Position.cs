using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TetrisProject
{
    public class Position
    {
        public int column { set; get; }
        public int row { set; get; }
        public Position(int x, int y) 
        {
            column = y;
            row = x;
        }
    }
}
