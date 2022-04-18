using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TetrisProject
{
    public class GameHandler
    {
        public GameBoard board;
        private Block currentBlock;
        private Block nextBlock;
        private Queue queue;
        bool gameState;
        public GameHandler()
        {
            queue = new Queue();
            board = new GameBoard();
            gameState = true;
            currentBlock = queue[queue.DrawID()];
            nextBlock = queue[queue.DrawID()];
        }
    }
}
