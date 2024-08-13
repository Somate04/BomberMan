using BomberWars_MP.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BomberWars_MP.Model
{
    /// <summary>
    /// Abstract class <c>MonsterBase</c> models virtual base functions of a monster in the game.
    /// </summary>
    abstract public class MonsterBase : Unit
    {
        #region Protected fields
        protected int _speed;
        protected int _moveCoolDown;
        protected int _boardWidth;
        protected int _boardHeight;
        protected bool moved;
        protected DirectionEnum _direction;
        protected BlockTypeEnum _currentBlock;
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor for the default monster. The different types of monsters inherit from this.
        /// </summary>
        /// <param name="_posX">Row index in the enum block matrix</param>
        /// <param name="_posY">Column index in the enum block matrix</param>
        /// <param name="boardWidth">Column number in the enum block matrix</param>
        /// <param name="boardHeight">Row number in the enum block matrix</param>
        public MonsterBase(int _posX, int _posY, int boardWidth, int boardHeight)
        {
            this._alive = true;
            this._posX = _posX;
            this._posY = _posY;
            this._boardWidth = boardWidth;
            this._boardHeight = boardHeight;
            moved = false;
        }
        #endregion

        #region Functions
        /// <summary>
        /// Virtual move function for monster.
        /// </summary>
        /// <param name="board">BlockTypeEnum matrix</param>
        public virtual void Move(BlockTypeEnum[,] board) { }

        /// <summary>
        /// Virtual function for monster's left movement.
        /// </summary>
        /// <param name="board">BlockTypeEnum matrix</param>
        public virtual void MoveLeft(BlockTypeEnum[,] board) { }

        /// <summary>
        /// Virtual function for monster's right movement.
        /// </summary>
        /// <param name="board">BlockTypeEnum matrix</param>
        public virtual void MoveRight(BlockTypeEnum[,] board) { }

        /// <summary>
        /// Virtual function for monster's up movement.
        /// </summary>
        /// <param name="board">BlockTypeEnum matrix</param>
        public virtual void MoveUp(BlockTypeEnum[,] board) { }

        /// <summary>
        /// Virtual function for monster's down movement.
        /// </summary>
        /// <param name="board">BlockTypeEnum matrix</param>
        public virtual void MoveDown(BlockTypeEnum[,] board) { }

        /// <summary>
        /// Virtual function that sets the direction of the movement.
        /// </summary>
        public virtual void SetDirection() { }

        /// <summary>
        /// Virtual function that changes the direction of the movement.
        /// </summary>
        public virtual void ChangeDirection() { }

        /// <summary>
        /// Virtual function that sets the monster's alive field false.
        /// </summary>
        public virtual void Kill() { }

        /// <summary>
        /// Virtual OnTimerTick function.
        /// </summary>
        /// <param name="board"></param>
        public virtual void OnTimerTick(BlockTypeEnum[,] board) { }

        /// <summary>
        /// Virtual SaveData function for monster.
        /// </summary>
        /// <returns></returns>
        public virtual int[] SaveData() { return new int[0]; }
        #endregion
    }
}
