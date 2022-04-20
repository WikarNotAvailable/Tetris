using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TetrisProject
{
    public class BlockS : Block
    {
        public override int id => 5;

        protected override Position[][] positions => new Position[][]
        {
           new Position[] {new(1,3),new(1,4),new(0,4),new(0,5)},
           new Position[] {new(0,4),new(1,4),new(1,5),new(2,5)},
           new Position[] {new(2,3),new(2,4),new(1,4),new(1,5)},
           new Position[] {new(0,3),new(1,3),new(1,4),new(2,4)}
        };
    }
}
