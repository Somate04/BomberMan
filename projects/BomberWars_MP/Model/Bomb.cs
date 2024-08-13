using BomberWars_MP.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BomberWars_MP.Model
{
    /// <summary>
    /// Class <c>Bomb</c> models a bomb in the game.
    /// </summary>
    public class Bomb : Unit
    {
        #region Private fields
        private int _time;
        private int _range;
        private int? _boardHeight;
        private int? _boardWidth;
        private BlockTypeEnum[] _previousBlocksAbove;
        private BlockTypeEnum[] _previousBlocksBelow;
        private BlockTypeEnum[] _previousBlocksToLeft;
        private BlockTypeEnum[] _previousBlocksToRight;
        private int _explosionCoolDown;
        private BlockTypeEnum _previousBlock;
        private bool _blockedAbove;
        private bool _blockedBelow;
        private bool _blockedLeft;
        private bool _blockedRight;
        private int _spreadrange;
        #endregion

        #region Properties
        /// <summary>
        /// Property for the timer of the bomb.
        /// </summary>
        public int Time { get => _time; set => _time = value; }
        #endregion

        #region Events
        /// <summary>
        /// Sends data of explosion to the player.
        /// </summary>
        public event EventHandler<ExplodeEventArgs>? ExplosionEventHandler;

        /// <summary>
        /// Sends to the player which block has exploded
        /// </summary>
        /// <param name="blockType">Type of the block</param>
        /// <param name="posX">Row index in the matrix</param>
        /// <param name="posY">Column index in the matrix</param>
        protected void OnExplosion(BlockTypeEnum blockType, int posX, int posY)
        {
            ExplosionEventHandler?.Invoke(this, new ExplodeEventArgs(blockType, posX, posY));
        }
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor of the bomb.
        /// </summary>
        /// <param name="range">Range of the bomb</param>
        /// <param name="posX">Row index in the matrix</param>
        /// <param name="posY">Column index in the matrix</param>
        public Bomb(int range, int posX, int posY)
        {
            _time = 100;
            _range = range;
            _posX = posX;
            _posY = posY;
            _alive = true;
            _previousBlocksAbove = new BlockTypeEnum[range];
            _previousBlocksBelow = new BlockTypeEnum[range];
            _previousBlocksToLeft = new BlockTypeEnum[range];
            _previousBlocksToRight = new BlockTypeEnum[range];
            _blockedAbove = false;
            _blockedBelow = false;
            _blockedLeft = false;
            _blockedRight = false;
            _explosionCoolDown = -1;
            _spreadrange = -1;
        }
        #endregion

        #region Functions
        /// <summary>
        /// Explodes within range in all directions through direction functions and changes the board through events.
        /// Event sends to the player which block has exploded. 
        /// The cooldown for the explosion is set when the block of the bomb is exploding.
        /// </summary>
        /// <param name="board">BlockTypeEnum matrix</param>
        private void Explode(BlockTypeEnum[,] board)
        {            
            if (_spreadrange == -1)
            {
                _explosionCoolDown = _range * 6;
                if (_range == 0) _explosionCoolDown = 6;
                OnExplosion(BlockTypeEnum.Explosion, _posX, _posY);
                _spreadrange++;
                return;
            }
            if (_range == 0) return;
            _spreadrange++;
            if(!_blockedAbove) ExplodeAbove(board);
            if(!_blockedBelow) ExplodeBelow(board);
            if(!_blockedLeft) ExplodeLeft(board);
            if(!_blockedRight) ExplodeRight(board);
        }

        /// <summary>
        /// Blow up the block above the bomb,
        /// save previous block,
        /// send explosion to the board,
        /// then blow up the block above it too,
        /// save previous block,
        /// send explosion to the board.
        /// </summary>
        /// <param name="board">BlockTypeEnum matrix</param>
        private void ExplodeAbove(BlockTypeEnum[,] board)
        {
            _previousBlock = board[_posX - _spreadrange, _posY];
            if (_previousBlock != BlockTypeEnum.Wall)
            {
                _blockedAbove = _previousBlock == BlockTypeEnum.Box;
                _previousBlocksAbove[_spreadrange - 1] = _previousBlock;
                OnExplosion(BlockTypeEnum.Explosion, _posX - _spreadrange, _posY);
            }
            else _blockedAbove = true;
        }

        /// <summary>
        /// Blow up the block below the bomb,
        /// save previous block,
        /// send explosion to the board,
        /// the blow up the block below it too,
        /// save previous block,
        /// send explosion to the board.
        /// </summary>
        /// <param name="board">BlockTypeEnum matrix</param>
        private void ExplodeBelow(BlockTypeEnum[,] board)
        {
            _previousBlock = board[_posX + _spreadrange, _posY];
            if (_previousBlock != BlockTypeEnum.Wall)
            {
                _blockedBelow = _previousBlock == BlockTypeEnum.Box;
                _previousBlocksBelow[_spreadrange - 1] = _previousBlock;
                OnExplosion(BlockTypeEnum.Explosion, _posX + _spreadrange, _posY);
            }
            else _blockedBelow = true;
        }

        /// <summary>
        /// Blow up the block left to the bomb,
        /// save previous block,
        /// send explosion to the board,
        /// then blow up the block left to it too,
        /// save previous block,
        /// send explosion to the board.
        /// </summary>
        /// <param name="board">BlockTypeEnum matrix</param>
        private void ExplodeLeft(BlockTypeEnum[,] board)
        {
            _previousBlock = board[_posX, _posY - _spreadrange];
            if (_previousBlock != BlockTypeEnum.Wall)
            {
                _blockedLeft = _previousBlock == BlockTypeEnum.Box;
                _previousBlocksToLeft[_spreadrange - 1] = _previousBlock;
                OnExplosion(BlockTypeEnum.Explosion, _posX, _posY - _spreadrange);
            }
            else _blockedLeft = true;
        }

        /// <summary>
        /// Blow up the block right to the bomb,
        /// save previous block,
        /// send explosion to the board,
        /// then blow up the block right to it too,
        /// save previous block,
        /// send explosion to the board.
        /// </summary>
        /// <param name="board">BlockTypeEnum matrix</param>
        private void ExplodeRight(BlockTypeEnum[,] board)
        {
            _previousBlock = board[_posX, _posY + _spreadrange];
            if (_previousBlock != BlockTypeEnum.Wall)
            {
                _blockedRight = _previousBlock == BlockTypeEnum.Box;
                _previousBlocksToRight[_spreadrange - 1] = _previousBlock;
                OnExplosion(BlockTypeEnum.Explosion, _posX, _posY + _spreadrange);
            }
            else _blockedRight = true;
        }

        /// <summary>
        /// Player calls to detonate the bomb,
        /// the function then calls the Explode function.
        /// </summary>
        /// <param name="board">BlockTypeEnum matrix</param>
        public void Detonate(BlockTypeEnum[,] board)
        {
            _time = 0;
            Explode(board);
        }

        /// <summary>
        /// If the explosion needs to end, this function sends to the player to change the board back to its previous types, or if there were PowerUp boxes, to PowerUps, (if PowerUp, not affected), or if monster/player, to path.
        /// </summary>
        /// <param name="board">BlockTypeEnum matrix</param>
        private void ExplosionEnds(BlockTypeEnum[,] board)
        {
            _alive = false;
            OnExplosion(BlockTypeEnum.Path, _posX, _posY);
            if (_range == 0) return;

            for (int range = 1; range <= _range; range++)
            {
                if (IsInRange(_posX - range, _posY) && board[_posX - range, _posY] == BlockTypeEnum.Explosion)
                {
                    _previousBlocksAbove[range - 1] = DidTheBlockChange(_previousBlocksAbove[range - 1]);
                    OnExplosion(_previousBlocksAbove[range - 1], _posX - range, _posY);
                }
                
                if (IsInRange(_posX + range, _posY) && board[_posX + range, _posY] == BlockTypeEnum.Explosion)
                {
                    _previousBlocksBelow[range - 1] = DidTheBlockChange(_previousBlocksBelow[range - 1]);
                    OnExplosion(_previousBlocksBelow[range - 1], _posX + range, _posY);
                }
                
                if (IsInRange(_posX, _posY - range) && board[_posX, _posY - range] == BlockTypeEnum.Explosion)
                {
                    _previousBlocksToLeft[range - 1] = DidTheBlockChange(_previousBlocksToLeft[range - 1]);
                    OnExplosion(_previousBlocksToLeft[range - 1], _posX, _posY - range);
                }
                
                if (IsInRange(_posX, _posY + range) && board[_posX, _posY + range] == BlockTypeEnum.Explosion)
                {
                    _previousBlocksToRight[range - 1] = DidTheBlockChange(_previousBlocksToRight[range - 1]);
                    OnExplosion(_previousBlocksToRight[range - 1], _posX, _posY + range);
                }
            }
        }

        /// <summary>
        /// Checks if the X or Y position is within the range of indexes.
        /// </summary>
        /// <param name="posX">X position</param>
        /// <param name="posY">Y position</param>
        /// <returns></returns>
        private bool IsInRange(int posX, int posY)
        {
            if (0 < posX && posX < _boardHeight && 0 < posY && posY < _boardWidth)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Sets which blocktype should appear after the explosion ends.
        /// PowerUps and Bombs are not changed by explosions.
        /// </summary>
        /// <param name="explodedType">Which blocktype exploded.</param>
        /// <returns></returns>
        private BlockTypeEnum DidTheBlockChange(BlockTypeEnum explodedType)
        {
            if (explodedType == BlockTypeEnum.Box)
            {
                int prob = random.Next(4);
                if (prob == 0)
                {
                    return BlockTypeEnum.PowerUp;
                }
                return BlockTypeEnum.Path;
            }
            if (explodedType == BlockTypeEnum.PowerUp || explodedType == BlockTypeEnum.Bomb) 
            {
                return explodedType;
            }
            return BlockTypeEnum.Path;
        }

        /// <summary>
        /// Called from the player's OnTimerTick function.
        /// If the bomb's time drops to 0, or a nearby bombs explosion hits this bomb, it explodes.
        /// If it exploded already, there is a cooldown for the explosion that slows down the spreading, and if it drops to 0, the explosion stops and the ExplosionEnds function is called.
        /// </summary>
        /// <param name="board">Board out of BlockTypeEnum matrix</param>
        /// <param name="boardHeight">Height of the board</param>
        /// <param name="boardWidth">Width of the board</param>
        public void OnTimerTick(BlockTypeEnum[,] board, int boardHeight, int boardWidth)
        {
            if (!_alive) return;

            _time--;
            if (_boardHeight != boardHeight || _boardWidth != boardWidth)
            {
                _boardHeight = boardHeight;
                _boardWidth = boardWidth;
            }

            if (_time == 0) Explode(board);
            else if (_explosionCoolDown >= 0) 
            { 
                if (_spreadrange < _range && _explosionCoolDown%2 == 0) Explode(board);
                if (_explosionCoolDown == 0) ExplosionEnds(board);
                else _explosionCoolDown--;
            }

            if (board[_posX, _posY] == BlockTypeEnum.Explosion && _explosionCoolDown == -1)
            {
                Detonate(board);
            }
        }


        /// <summary>
        /// Collects all important data of the current Bomb object for saving. This data represented by an integer array.
        /// If time drops to 0, detonate the bomb when loading the game.
        /// </summary>
        /// <returns>The integer array that contains all valuable data.</returns>
        public int[] SaveData(int playerId)
        {

            int[] returnData = new int[5];

            returnData[0] = playerId;
            returnData[1] = _time;
            returnData[2] = _range;
            returnData[3] = _posX;
            returnData[4] = _posY;

            return returnData;
        }

        #endregion
    }
}
