using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TetrisProject
{
    class BlockO : Block
    {
        public override int id => 4;

        public override Position[][] positions => new Position[][]
        {
           new Position[] {new(0,4),new(0,5),new(1,4),new(1,5)}
        };
    }
}
