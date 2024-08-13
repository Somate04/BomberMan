using BomberWars_MP.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BomberWars_MP.Model
{
    /// <summary>
    /// All moving object, that needs position and alive fields, are inhereted by this abstract class.
    /// Also this class has OnBoardChangeEventHandler that raises after moving.
    /// </summary>
    public abstract class Unit
    {
        protected int _posX;
        protected int _posY;
        protected bool _alive;
        protected Random random = new Random();
        public int PosX { get { return _posX; } }
        public int PosY { get { return _posY; } }
        public bool Alive { get { return _alive; } }

        /// <summary>
        /// Eventhandler that gets a BoardChangeEventArgs with the arguments of: what moved - from which type of field and from where - to where
        /// </summary>
        public EventHandler<BoardChangeEventArgs>? OnBoardChangeEventHandler;

        /// <summary>
        /// The model signs up for this event and gives information about: what moved - from which type of field and from where - to where
        /// </summary>
        /// <param name="prev">type of the previous block</param>
        /// <param name="prevPosX">previous X position of unit</param>
        /// <param name="prevPosY">previous Y position of unit</param>
        /// <param name="curr">type of the current block</param>
        /// <param name="currPosX">current X position of unit</param>
        /// <param name="currPosY">current Y position of unit</param>
        protected virtual void OnBoardChange(BlockTypeEnum prev, int prevPosX, int prevPosY, BlockTypeEnum curr, int currPosX, int currPosY)
        {
            OnBoardChangeEventHandler?.Invoke(this, new BoardChangeEventArgs(prev, prevPosX, prevPosY, curr, currPosX, currPosY));
        }

    }
}
