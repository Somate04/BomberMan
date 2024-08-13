using BomberWars_MP.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BomberWars_MP.Model
{
    /// <summary>
    /// Represent board changing after an object move.
    /// The event sends the previous block type on the previous position x, y.
    /// The event also sends the current block type on the current position x, y.
    /// </summary>
    public class BoardChangeEventArgs : EventArgs
    {
        public int _prevPosX;
        public int _prevPosY;
        public BlockTypeEnum _prevtype;
        public int _currentPosX;
        public int _currentPosY;
        public BlockTypeEnum _currenttype;

        /// <summary>
        /// Arguments to give information about: what moved - from which type of field and from where - to where
        /// </summary>
        /// <param name="prevtype">type of the previous block</param>
        /// <param name="prevPosX">previous X position of the moved unit</param>
        /// <param name="prevPosY">previous Y position of the moved unit</param>
        /// <param name="currenttype">type of the current block</param>
        /// <param name="currentPosX">current X position of the moved unit</param>
        /// <param name="currentPosY">current Y position of the moved unit</param>
        public BoardChangeEventArgs(BlockTypeEnum prevtype, int prevPosX, int prevPosY, BlockTypeEnum currenttype, int currentPosX, int currentPosY)
        {
            _prevPosX = prevPosX;
            _prevPosY = prevPosY;
            _prevtype = prevtype;
            _currentPosX = currentPosX;
            _currentPosY = currentPosY;
            _currenttype = currenttype;
        }
    }
}
