using BomberWars_MP.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BomberWars_MP.Model
{
    /// <summary>
    /// Class <c>GhostMonster</c> models a ghost monster in the game, that can move through walls and boxes and it is slower than the basic monster.
    /// </summary>
    public class GhostMonster : MonsterBase
    {
        #region Private Fields
        private BlockTypeEnum _underTheMonsterBlock;
        #endregion
        #region Constructor
        /// <summary>
        /// Constructor for ghostmonster.
        /// </summary>
        /// <param name="_posX">x position on the board</param>
        /// <param name="_posY">y position on the board</param>
        /// <param name="boardWidth">the board's width</param>
        /// <param name="boardHeight">the board's height</param>
        public GhostMonster(int _posX, int _posY, int boardWidth, int boardHeight) : base(_posX, _posY, boardWidth, boardHeight)
        {
            _speed = 60;
            _moveCoolDown = 0;
            _currentBlock = BlockTypeEnum.Path;
            _underTheMonsterBlock = BlockTypeEnum.Path;
            SetDirection();
        }

        /// <summary>
        /// Constructor for loading a ghostmonster.
        /// </summary>
        /// <param name="speed">The speed of the monster</param>
        /// <param name="moveCoolDown">The move cool down of the monster</param>
        /// <param name="direction">The direction of the monster</param>
        /// <param name="currentBlock">The previous block of the monster</param>
        /// <param name="_posX">x position on the board</param>
        /// <param name="_posY">y position on the board</param>
        /// <param name="boardWidth">the board's width</param>
        /// <param name="boardHeight">the board's height</param>
        public GhostMonster(int speed, int moveCoolDown, int direction, int currentBlock, int _posX, int _posY, int boardWidth, int boardHeight) : base(_posX, _posY, boardWidth, boardHeight)
        {
            _speed = speed;
            _moveCoolDown = moveCoolDown;
            _direction = (DirectionEnum)direction;
            _currentBlock = (BlockTypeEnum)currentBlock;
            _underTheMonsterBlock = _currentBlock;
            _alive = true;
        }
        #endregion

        #region Functions
        /// <summary>
        /// Checks whether the coordinates can cause index out of bounds error.
        /// </summary>
        /// <param name="posX">Row index</param>
        /// <param name="posY">Column index</param>
        /// <returns>Whether the coordinates can cause index out of bounds error.</returns>
        private bool InRange(int posX, int posY)
        {
            if (0 < posX && posX < _boardHeight-1 && 0 < posY && posY < _boardWidth-1)
            {
                return true;
            }
            return false;
        }
        #endregion

        #region Overriden functions
        /// <summary>
        /// Determines the movements of the monster. It can move through walls and boxes.
        /// </summary>
        /// <param name="board">BlockTypeEnum matrix.</param>
        public override void Move(BlockTypeEnum[,] board)
        {
            if (!_alive) return;

            if (_moveCoolDown % _speed == 0)
            {
                ChangeDirection();
                try
                {
                    switch (_direction)
                    {
                        case DirectionEnum.Up:
                            MoveUp(board);
                            break;

                        case DirectionEnum.Down:
                            MoveDown(board);
                            break;

                        case DirectionEnum.Left:
                            MoveLeft(board);
                            break;

                        case DirectionEnum.Right:
                            MoveRight(board);
                            break;

                        default:
                            break;
                    }
                    moved = true;
                }
                catch (IndexOutOfRangeException)
                {
                    return;
                }
            }
        }

        /// <summary>
        /// Monster moves upwards. Checks the type of block where the monster wants to step.
        /// Saves the type of block, that the monster is about to step to. If the monster explodes, it sets back to the field it was before the monster stepped there.
        /// </summary>
        /// <param name="board">BlockTypeEnum matrix.</param>
        override public void MoveUp(BlockTypeEnum[,] board)
        {
            _currentBlock = board![_posX - 1, _posY];
            if (_currentBlock == BlockTypeEnum.Explosion)
            {
                OnBoardChange(_underTheMonsterBlock, _posX, _posY, BlockTypeEnum.Explosion, _posX - 1, _posY);
                Kill();
                return;
            }
            else if (_currentBlock == BlockTypeEnum.Path)
            {
                OnBoardChange(_underTheMonsterBlock, _posX, _posY, BlockTypeEnum.GhostMonsterOnPath, _posX - 1, _posY);
                _underTheMonsterBlock = _currentBlock;
                _posX--;
                return;
            }
            else if (_currentBlock == BlockTypeEnum.Wall && InRange(_posX - 1, _posY))
            {
                OnBoardChange(_underTheMonsterBlock, _posX, _posY, BlockTypeEnum.GhostMonsterOnWall, _posX - 1, _posY);
                _underTheMonsterBlock = _currentBlock;
                _posX--;
                return;
            }
            else if (_currentBlock == BlockTypeEnum.Box || _currentBlock == BlockTypeEnum.BoxPlacedByPlayer)
            {
                OnBoardChange(_underTheMonsterBlock, _posX, _posY, BlockTypeEnum.GhostMonsterOnBox, _posX - 1, _posY);
                _underTheMonsterBlock = _currentBlock;
                _posX--;
                return;
            }
            else if (_currentBlock == BlockTypeEnum.PlayerA || _currentBlock == BlockTypeEnum.PlayerB || _currentBlock == BlockTypeEnum.PlayerC)
            {
                OnBoardChange(BlockTypeEnum.Path, _posX, _posY, BlockTypeEnum.GhostMonsterOnPath, _posX - 1, _posY);
                _underTheMonsterBlock = BlockTypeEnum.Path;
                _posX--;
                return;
            }
            else SetDirection();
        }

        /// <summary>
        /// Monster moves downwards. Checks the type of block where the monster wants to step.
        /// Saves the type of block, that the monster is about to step to. If the monster explodes, it sets back to the field it was before the monster stepped there.
        /// </summary>
        /// <param name="board">BlockTypeEnum matrix.</param>
        override public void MoveDown(BlockTypeEnum[,] board)
        {
            _currentBlock = board![_posX + 1, _posY];
            if (_currentBlock == BlockTypeEnum.Explosion)
            {
                OnBoardChange(_underTheMonsterBlock, _posX, _posY, BlockTypeEnum.Explosion, _posX + 1, _posY);
                Kill();
                return;
            }
            else if (_currentBlock == BlockTypeEnum.Path)
            {
                OnBoardChange(_underTheMonsterBlock, _posX, _posY, BlockTypeEnum.GhostMonsterOnPath, _posX + 1, _posY);
                _underTheMonsterBlock = _currentBlock;
                _posX++;
                return;
            }
            else if (_currentBlock == BlockTypeEnum.Wall && InRange(_posX + 1, _posY))
            {
                OnBoardChange(_underTheMonsterBlock, _posX, _posY, BlockTypeEnum.GhostMonsterOnWall, _posX + 1, _posY);
                _underTheMonsterBlock = _currentBlock;
                _posX++;
                return;
            }
            else if (_currentBlock == BlockTypeEnum.Box || _currentBlock == BlockTypeEnum.BoxPlacedByPlayer)
            {
                OnBoardChange(_underTheMonsterBlock, _posX, _posY, BlockTypeEnum.GhostMonsterOnBox, _posX + 1, _posY);
                _underTheMonsterBlock = _currentBlock;
                _posX++;
                return;
            }
            else if (_currentBlock == BlockTypeEnum.PlayerA || _currentBlock == BlockTypeEnum.PlayerB || _currentBlock == BlockTypeEnum.PlayerC)
            {
                OnBoardChange(BlockTypeEnum.Path, _posX, _posY, BlockTypeEnum.GhostMonsterOnPath, _posX + 1, _posY);
                _underTheMonsterBlock = BlockTypeEnum.Path;
                _posX++;
                return;
            }
            else SetDirection();
        }

        /// <summary>
        /// Monster moves to left. Checks the type of block where the monster wants to step.
        /// Saves the type of block, that the monster is about to step to. If the monster explodes, it sets back to the field it was before the monster stepped there.
        /// </summary>
        /// <param name="board">BlockTypeEnum matrix.</param>
        override public void MoveLeft(BlockTypeEnum[,] board)
        {
            _currentBlock = board![_posX, _posY - 1];
            if (_currentBlock == BlockTypeEnum.Explosion)
            {
                OnBoardChange(_underTheMonsterBlock, _posX, _posY, BlockTypeEnum.Explosion, _posX, _posY - 1);
                Kill();
                return;
            }
            else if (_currentBlock == BlockTypeEnum.Path)
            {
                OnBoardChange(_underTheMonsterBlock, _posX, _posY, BlockTypeEnum.GhostMonsterOnPath, _posX, _posY - 1);
                _underTheMonsterBlock = _currentBlock;
                _posY--;
                return;
            }
            else if (_currentBlock == BlockTypeEnum.Wall && InRange(_posX, _posY - 1))
            {
                OnBoardChange(_underTheMonsterBlock, _posX, _posY, BlockTypeEnum.GhostMonsterOnWall, _posX, _posY - 1);
                _underTheMonsterBlock = _currentBlock;
                _posY--;
                return;
            }
            else if (_currentBlock == BlockTypeEnum.Box || _currentBlock == BlockTypeEnum.BoxPlacedByPlayer)
            {
                OnBoardChange(_underTheMonsterBlock, _posX, _posY, BlockTypeEnum.GhostMonsterOnBox, _posX, _posY - 1);
                _underTheMonsterBlock = _currentBlock;
                _posY--;
                return;
            }
            else if (_currentBlock == BlockTypeEnum.PlayerA || _currentBlock == BlockTypeEnum.PlayerB || _currentBlock == BlockTypeEnum.PlayerC)
            {
                OnBoardChange(BlockTypeEnum.Path, _posX, _posY, BlockTypeEnum.GhostMonsterOnPath, _posX, _posY - 1);
                _underTheMonsterBlock = BlockTypeEnum.Path;
                _posY--;
                return;
            }
            else SetDirection();
        }

        /// <summary>
        /// Monster moves to right. Checks the type of block where the monster wants to step.
        /// Saves the type of block, that the monster is about to step to. If the monster explodes, it sets back to the field it was before the monster stepped there.
        /// </summary>
        /// <param name="board">BlockTypeEnum matrix.</param>
        override public void MoveRight(BlockTypeEnum[,] board)
        {
            _currentBlock = board![_posX, _posY + 1];
            if (_currentBlock == BlockTypeEnum.Explosion)
            {
                OnBoardChange(_underTheMonsterBlock, _posX, _posY, BlockTypeEnum.Explosion, _posX, _posY + 1);
                Kill();
                return;
            }
            else if (_currentBlock == BlockTypeEnum.Path)
            {
                OnBoardChange(_underTheMonsterBlock, _posX, _posY, BlockTypeEnum.GhostMonsterOnPath, _posX, _posY + 1);
                _underTheMonsterBlock = _currentBlock;
                _posY++;
                return;
            }
            else if (_currentBlock == BlockTypeEnum.Wall && InRange(_posX, _posY + 1))
            {
                OnBoardChange(_underTheMonsterBlock, _posX, _posY, BlockTypeEnum.GhostMonsterOnWall, _posX, _posY + 1);
                _underTheMonsterBlock = _currentBlock;
                _posY++;
                return;
            }
            else if (_currentBlock == BlockTypeEnum.Box || _currentBlock == BlockTypeEnum.BoxPlacedByPlayer)
            {
                OnBoardChange(_underTheMonsterBlock, _posX, _posY, BlockTypeEnum.GhostMonsterOnBox, _posX, _posY + 1);
                _underTheMonsterBlock = _currentBlock;
                _posY++;
                return;
            }                     
            else if (_currentBlock == BlockTypeEnum.PlayerA || _currentBlock == BlockTypeEnum.PlayerB || _currentBlock == BlockTypeEnum.PlayerC)
            {
                OnBoardChange(BlockTypeEnum.Path, _posX, _posY, BlockTypeEnum.GhostMonsterOnPath, _posX, _posY + 1);
                _underTheMonsterBlock = BlockTypeEnum.Path;
                _posY++;
                return;
            }
            else SetDirection();
        }

        /// <summary>
        /// Randomly changes direction.
        /// </summary>
        public override void ChangeDirection()
        {
            if (random.Next(0, 5) == 1) SetDirection();
        }

        /// <summary>
        /// Sets the direction.
        /// </summary>
        public override void SetDirection()
        {
            int newDirection = random.Next(0, 4);
            switch (newDirection)
            {
                case 0:
                    _direction = DirectionEnum.Up; break;
                case 1:
                    _direction = DirectionEnum.Down; break;
                case 2:
                    _direction = DirectionEnum.Left; break;
                case 3:
                    _direction = DirectionEnum.Right; break;
                default: break;
            }
        }

        /// <summary>
        /// This function is called if the monster is killed.
        /// </summary>
        public override void Kill()
        {
            this._alive = false;
        }

        /// <summary>
        /// Checks if the monster is alive. Increases the movement cooldown. Moves the monster.
        /// </summary>
        /// <param name="board">BlockTypeEnum matrix.</param>
        override public void OnTimerTick(BlockTypeEnum[,] board)
        {
            _currentBlock = board![this._posX, this._posY];
            if (moved && _currentBlock != BlockTypeEnum.GhostMonsterOnWall && _currentBlock != BlockTypeEnum.GhostMonsterOnPath && _currentBlock != BlockTypeEnum.GhostMonsterOnBox)
            {
                Kill();
                return;
            }
            _moveCoolDown++;
            Move(board);
        }
        /// <summary>
        /// Collects all important data of the current GhostMonster object for saving. This data represented by an integer array.
        /// </summary>
        /// <returns>The integer array that contains all valuable data.</returns>
        override public int[] SaveData()
        {
            int[] returnData = new int[7];

            returnData[0] = 1;
            returnData[1] = _speed;
            returnData[2] = _moveCoolDown;
            returnData[3] = (int)_direction;
            returnData[4] = _posX;
            returnData[5] = _posY;

            if (_alive)
            {
                returnData[6] = 1;
            }
            else
            {
                returnData[6] = 0;
            }


            return returnData;
        }
        #endregion
    }
}
