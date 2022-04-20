using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TetrisProject
{
    public class Queue
    {
        private Random rd;
        private Block[] blocks;
        public Queue()
        {
            rd = new Random();
            blocks = new Block[]
            {
                new BlockI(),
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
        public int DrawID()
        {
            return rd.Next(1, 8);
        }
    }
}
