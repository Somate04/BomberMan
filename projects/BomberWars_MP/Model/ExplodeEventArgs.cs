using BomberWars_MP.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BomberWars_MP.Model
{
    /// <summary>
    /// Represent board changing after explosion.
    /// The event sends the block type on position x, y.
    /// </summary>
    public class ExplodeEventArgs : EventArgs
    {
        private int _posX;
        private int _posY;
        private BlockTypeEnum _blockType;


        /// <summary>
        /// Arguments to give information about a block and its place.
        /// </summary>
        /// <param name="blocktype">type of the block</param>
        /// <param name="posX"> X position of the moved unit</param>
        /// <param name="posY"> Y position of the moved unit</param>
        public ExplodeEventArgs(BlockTypeEnum blockType, int posX, int posY)
        {
            PosX = posX;
            PosY = posY;
            BlockType = blockType;
        }

        public int PosX { get => _posX; set => _posX = value; }
        public int PosY { get => _posY; set => _posY = value; }
        public BlockTypeEnum BlockType { get => _blockType; set => _blockType = value; }
    }
}
