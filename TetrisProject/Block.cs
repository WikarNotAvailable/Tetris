using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TetrisProject
{
    abstract class Block
    {
        public abstract int id { get; }
        protected abstract Position[][] positions { get; }
        private Position offset;
        private int rotationState;        

        public Block()
        {
            offset = new Position(0, 0);
            rotationState = 0;
        }
        public void Move(int rows, int columns)
        {
            offset.row += rows;
            offset.column += columns;
        }
        public IEnumerable<Position> TilePositions()
        {
            foreach (Position p in positions[rotationState])
            {
                yield return new Position(p.row + offset.row, p.column + offset.column);
            }
        }
        public void RotateClockwise()
        {
            if (rotationState != positions.Length-1)
            {
                rotationState++;
            }
            else
            {
                rotationState = 0;
            }
        }
        public void RotateCounterClockwise()
        {
            if (rotationState == 0)
            {
                rotationState = positions.Length-1;
            }
            else
            {
                rotationState--;
            }
        }
        //
        public void ResetBlock()
        {
            offset.row = 0;
            offset.column = 0;
            rotationState = 0;
        }
    }
}
