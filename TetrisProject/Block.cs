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
        public abstract Position[][] positions { get; }
        public Position offset;
        public int rotationState;        

        public Block()
        {
            offset = new Position(0, 0);
            rotationState = 0;
        }
        public void MoveLeft()
        {
            offset.column -= 1;
        }
        public void MoveRight()
        {
            offset.column += 1;
        }
        public void MoveDown() // only to test, probably will be deleted later
        {
            offset.row += 1;
        }
        public void RotateClockwise()
        {

        }
        public void RotateCounterClockwise()
        {

        }

    }
}
