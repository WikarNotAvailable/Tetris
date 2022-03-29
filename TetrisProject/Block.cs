using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TetrisProject
{
    abstract class Block
    {
        public abstract int ID { get; }
        public abstract Position[] positions { get; set; }
        public abstract void move();
        public abstract void rotate();

    }
}
