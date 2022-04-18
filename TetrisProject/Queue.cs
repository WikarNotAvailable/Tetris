using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TetrisProject
{
    class Queue
    {
        private Random rd;
        private Block[] blocks;
        private int DrawID()
        {
            return rd.Next(1, 8);
        }
        public Queue()
        {
            rd = new Random();
            blocks = new Block[]
            {
                new BlockI(),
                new BlockJ(),
                new BlockL(),
                new BlockO(),
                new BlockS(),
                new BlockT(),
                new BlockZ()
            };
        }
        public Block this[int id]
        {
            get => blocks[id];
        }
    }
}
