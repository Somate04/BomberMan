using BomberWars_MP.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BomberWars_MP.Model
{
    /// <summary>
    /// Class <c>Monster</c> models a basic monster in the game, that wanders in the field.
    /// </summary>
    public class Monster : MonsterBase
    {
        #region Constructor
        /// <summary>
        /// Constructor for monster. At the first step the monster steps away, leaving a path block behind.
        /// </summary>
        /// <param name="_posX">x position on the board</param>
        /// <param name="_posY">y position on the board</param>
        /// <param name="boardWidth">the board's width</param>
        /// <param name="boardHeight">the board's height</param>
        public Monster(int _posX, int _posY, int boardWidth, int boardHeight) : base(_posX, _posY, boardWidth, boardHeight)
        {
            moved = false;
            _speed = 40;
            _moveCoolDown = 0;
            _currentBlock = BlockTypeEnum.Path;
            SetDirection();
        }

        /// <summary>
        /// Constructor for loading a monster.
        /// </summary>
        /// <param name="speed">The speed of the monster</param>
        /// <param name="moveCoolDown">The move cool down of the monster</param>
        /// <param name="direction">The direction of the monster</param>
        /// <param name="currentBlock">The saved block of the monster</param>
        /// <param name="_posX">x position on the board</param>
        /// <param name="_posY">y position on the board</param>
        /// <param name="boardWidth">the board's width</param>
        /// <param name="boardHeight">the board's height</param>
        public Monster(int speed, int moveCoolDown, int direction, int currentBlock, int _posX, int _posY, int boardWidth, int boardHeight) : base(_posX, _posY, boardWidth, boardHeight)
        {
            this._alive = true;
            moved = false;
            _speed = speed;
            _moveCoolDown = moveCoolDown;
            _direction = (DirectionEnum)direction;
            _currentBlock = (BlockTypeEnum)currentBlock;
        }
        #endregion

        #region Functions
        /// <summary>
        /// Determines the movements of the monster.
        /// </summary>
        /// <param name="board">BlockTypeEnum matrix</param>
        public override void Move(BlockTypeEnum[,] board)
        {
            if (!_alive) return;

            if(_moveCoolDown % _speed == 0) 
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
        /// Monster moves upwards. Checks the type of block where the monster wants to step. If the monster explodes, it sets back to path.
        /// </summary>
        /// <param name="board">BlockTypeEnum matrix</param>
        public override void MoveUp(BlockTypeEnum[,] board)
        {
            _currentBlock = board![_posX - 1, _posY];
            if (_currentBlock == BlockTypeEnum.Explosion)
            {
                OnBoardChange(BlockTypeEnum.Path, _posX, _posY, BlockTypeEnum.Explosion, _posX - 1, _posY);
                Kill();
                return;
            }
            else if (_currentBlock == BlockTypeEnum.Path
             || _currentBlock == BlockTypeEnum.PlayerA
             || _currentBlock == BlockTypeEnum.PlayerB
             || _currentBlock == BlockTypeEnum.PlayerC)
            {
                OnBoardChange(BlockTypeEnum.Path, _posX, _posY, BlockTypeEnum.Monster, _posX - 1, _posY);
                _posX--;
            }
            else
            {
                if (board?[_posX, _posY - 1] == BlockTypeEnum.Path) { MoveLeft(board); _direction = DirectionEnum.Left; }
                else if (board?[_posX, _posY + 1] == BlockTypeEnum.Path) { MoveRight(board); _direction = DirectionEnum.Right; }
                else if (board?[_posX + 1, _posY] == BlockTypeEnum.Path) { MoveDown(board); _direction = DirectionEnum.Down; }
                else return;
            }
        }

        /// <summary>
        /// Monster moves downwards. Checks the type of block where the monster wants to step. If the monster explodes, it sets back to path.
        /// </summary>
        /// <param name="board">BlockTypeEnum matrix</param>
        public override void MoveDown(BlockTypeEnum[,] board)
        {
            _currentBlock = board![_posX + 1, _posY];
            if (_currentBlock == BlockTypeEnum.Explosion)
            {
                OnBoardChange(BlockTypeEnum.Path, _posX, _posY, BlockTypeEnum.Explosion, _posX + 1, _posY);
                Kill();
                return;
            }
            else if (_currentBlock == BlockTypeEnum.Path
             || _currentBlock == BlockTypeEnum.PlayerA
             || _currentBlock == BlockTypeEnum.PlayerB
             || _currentBlock == BlockTypeEnum.PlayerC)
            {
                OnBoardChange(BlockTypeEnum.Path, _posX, _posY, BlockTypeEnum.Monster, _posX + 1, _posY);
                _posX++;
            }
            else
            {
                if (board?[_posX, _posY + 1] == BlockTypeEnum.Path) { MoveRight(board); _direction = DirectionEnum.Right; }
                else if (board?[_posX, _posY - 1] == BlockTypeEnum.Path) { MoveLeft(board); _direction = DirectionEnum.Left; }
                else if (board?[_posX - 1, _posY] == BlockTypeEnum.Path) { MoveUp(board); _direction = DirectionEnum.Up; }
                else return;
            }
        }

        /// <summary>
        /// Monster moves to left. Checks the type of block where the monster wants to step. If the monster explodes, it sets back to path.
        /// </summary>
        /// <param name="board">BlockTypeEnum matrix</param>
        public override void MoveLeft(BlockTypeEnum[,] board)
        {
            _currentBlock = board![_posX, _posY - 1];
            if (_currentBlock == BlockTypeEnum.Explosion)
            {
                OnBoardChange(BlockTypeEnum.Path, _posX, _posY, BlockTypeEnum.Explosion, _posX, _posY - 1);
                Kill();
                return;
            }
            else if (_currentBlock == BlockTypeEnum.Path
             || _currentBlock == BlockTypeEnum.PlayerA
             || _currentBlock == BlockTypeEnum.PlayerB
             || _currentBlock == BlockTypeEnum.PlayerC)
            {
                OnBoardChange(BlockTypeEnum.Path, _posX, _posY, BlockTypeEnum.Monster, _posX, _posY - 1);
                _posY--;
            }
            else
            {
                if (board?[_posX + 1, _posY] == BlockTypeEnum.Path) { MoveDown(board); _direction = DirectionEnum.Down; }
                else if (board?[_posX - 1, _posY] == BlockTypeEnum.Path) { MoveUp(board); _direction = DirectionEnum.Up; }
                else if (board?[_posX, _posY + 1] == BlockTypeEnum.Path) { MoveRight(board); _direction = DirectionEnum.Right; }
                else return;
            }
        }

        /// <summary>
        /// Monster moves to right. Checks the type of block where the monster wants to step. If the monster explodes, it sets back to path.
        /// </summary>
        /// <param name="board">BlockTypeEnum matrix</param>
        public override void MoveRight(BlockTypeEnum[,] board)
        {
            _currentBlock = board![_posX, _posY + 1];
            if (_currentBlock == BlockTypeEnum.Explosion)
            {
                OnBoardChange(BlockTypeEnum.Path, _posX, _posY, BlockTypeEnum.Explosion, _posX, _posY + 1);
                Kill();
                return;
            }
            else if (_currentBlock == BlockTypeEnum.Path
             || _currentBlock == BlockTypeEnum.PlayerA
             || _currentBlock == BlockTypeEnum.PlayerB
             || _currentBlock == BlockTypeEnum.PlayerC)
            {
                OnBoardChange(BlockTypeEnum.Path, _posX, _posY, BlockTypeEnum.Monster, _posX, _posY + 1);
                _posY++;
            }
            else
            {                
                if (board?[_posX - 1, _posY] == BlockTypeEnum.Path) { MoveUp(board); _direction = DirectionEnum.Up; }
                else if (board?[_posX + 1, _posY] == BlockTypeEnum.Path) { MoveDown(board); _direction = DirectionEnum.Down; }
                else if (board?[_posX, _posY - 1] == BlockTypeEnum.Path) { MoveLeft(board); _direction = DirectionEnum.Left; }
                else return;
            }
        }

        /// <summary>
        /// Randomly changes direction.
        /// </summary>
        public override void ChangeDirection()
        {
            if (random.Next(0, 10) == 1) SetDirection();
        }

        /// <summary>
        /// Sets the direction of the movement.
        /// </summary>
        public override void SetDirection()
        {
                int newDirection = random.Next(0, 4);
                switch(newDirection)
                {
                    case 0:
                        _direction = DirectionEnum.Up; break;
                    case 1:
                        _direction = DirectionEnum.Down; break;
                    case 2:
                        _direction = DirectionEnum.Left; break;
                    case 3:
                        _direction = DirectionEnum.Right; break;
                    default : break;
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
        /// Kills the monster if it cannot find itself on the board.
        /// Increases the movement cooldown.
        /// Moves the monster.
        /// </summary>
        /// <param name="board">BlockTypeEnum matrix</param>
        public override void OnTimerTick(BlockTypeEnum[,] board)
        {
            _currentBlock = board![this._posX, this._posY];
            if (moved && _currentBlock != BlockTypeEnum.Monster)
            {
                Kill();
                return;
            }
            _moveCoolDown++;
            Move(board);
        }

        /// <summary>
        /// Collects all important data of the current Monster object for saving. This data represented by an integer array.
        /// </summary>
        /// <returns>The integer array that contains all valuable data.</returns>
        public override int[] SaveData()
        {
            int[] returnData = new int[7];

            returnData[0] = 0;
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
