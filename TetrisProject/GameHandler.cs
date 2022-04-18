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
        public bool gameState;
        private Block currentBlock;
        private Block nextBlock;
        private Queue queue;

        public GameHandler()
        {
            queue = new Queue();
            board = new GameBoard();
            gameState = true;
            currentBlock = queue[queue.DrawID()];
            nextBlock = queue[queue.DrawID()];
        }
        public Block ReturnCurrentBlock()
        {
            return currentBlock;
        }
        public void MoveLeft()
        {
            currentBlock.Move(0, -1);
            if (!DoesBlockFit())
                currentBlock.Move(0, 1);
        }
        public void MoveRight()
        {
            currentBlock.Move(0, 1);
            if (!DoesBlockFit())
                currentBlock.Move(0, -1);
        }
        public void MoveDown()
        {
            currentBlock.Move(1, 0);
            if (!DoesBlockFit())
                currentBlock.Move(-1, 0);
        }
        public void RotateClockwise()
        {
            currentBlock.RotateClockwise();
            if (!DoesBlockFit())
                currentBlock.RotateCounterClockwise();
        }
        public void RotateCounterClockwise()
        {
            currentBlock.RotateCounterClockwise();
            if (!DoesBlockFit())
                currentBlock.RotateClockwise();
        }
        private bool DoesBlockFit()
        {
            foreach (Position p in currentBlock.TilePositions())
            {
                if (!board.CanMove(p.row,p.column))
                    return false;
            }
            return true;
        }
    }
}
