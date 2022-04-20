using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TetrisProject
{
    public class GameHandler
    {
        private GameBoard board;
        private int currentScore;
        private Block currentBlock;
        private Block nextBlock;
        private Queue queue;

        public GameHandler()
        {
            queue = new Queue();
            board = new GameBoard();
            currentScore = 0;
            currentBlock = queue[queue.DrawID()];
            nextBlock = queue[queue.DrawID()];
        }
        public int ReturnCurrentScore() 
        {
            return currentScore;
        }
        public Block ReturnNextBlock()
        {
            return nextBlock;
        }
        public GameBoard ReturnGameBoard()
        {
            return board;
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
            currentScore += 1;
            if (!DoesBlockFit())
            {
                currentBlock.Move(-1, 0);
                foreach (Position p in currentBlock.TilePositions())
                {
                    board.PlaceTile(p.row, p.column, currentBlock.id);
                }
                currentBlock.ResetBlock();
                currentBlock = nextBlock;
                nextBlock = queue[queue.DrawID()];
            }
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
        public void CheckRows()
        {
           int counter = 0;

            for(int i = 21; i > 0; i--)
            {
                if (board.IsRowEmpty(i))
                {
                    currentScore +=  counter*counter*250;
                    return;
                }

                if (board.IsRowFull(i))
                {
                    counter++;
                    board.EraseRow(i);
                }
                else if (counter !=0)
                    board.MoveRows(i, counter);
            }
        }
        public bool IsGameEnded()
        {
            return !board.IsRowEmpty(1);
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
