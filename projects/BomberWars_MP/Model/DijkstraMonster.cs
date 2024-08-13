﻿using BomberWars_MP.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BomberWars_MP.Model
{
    /// <summary>
    /// Class <c>DijkstraMonster</c> models a pathfinder monster in the game, that is faster than the basic monster and follows the shortest path to the closest player every time its path gets blocked. In case there is no path to any player, it wanders around.
    /// </summary>
    public class DijkstraMonster : MonsterBase
    {
        #region Private fields
        private List<(int, int)>? _shortestPath;
        private bool _noAvailablePlayer;
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor for Dijkstra monster.
        /// </summary>
        /// <param name="_posX">x position on the board</param>
        /// <param name="_posY">y position on the board</param>
        /// <param name="boardWidth">the board's width</param>
        /// <param name="boardHeight">the board's height</param>
        public DijkstraMonster(int _posX, int _posY, int boardWidth, int boardHeight) : base(_posX, _posY, boardWidth, boardHeight)
        {
            _speed = 20;
            _moveCoolDown = 0;
            _currentBlock = BlockTypeEnum.Path;
            SetDirection();
            _noAvailablePlayer = true;
        }

        /// <summary>
        /// Constructor for loading a monster.
        /// </summary>
        /// <param name="speed">The speed of the monster</param>
        /// <param name="moveCoolDown">The move cool down of the monster</param>
        /// <param name="direction">The direction of the monster</param>
        /// <param name="previousBlock">The previous block of the monster</param>
        /// <param name="_posX">x position on the board</param>
        /// <param name="_posY">y position on the board</param>
        /// <param name="boardWidth">the board's width</param>
        /// <param name="boardHeight">the board's height</param>
        public DijkstraMonster(int speed, int moveCoolDown, int direction, int previousBlock, int _posX, int _posY, int boardWidth, int boardHeight) : base(_posX, _posY, boardWidth, boardHeight)
        {
            _speed = speed;
            _moveCoolDown = moveCoolDown;
            _direction = (DirectionEnum)direction;
            _currentBlock = (BlockTypeEnum)previousBlock;
            _alive = true;
            _noAvailablePlayer = true;
        }
        #endregion

        #region Functions
        /// <summary>
        /// Dijkstra's algorithm implemented to find the shortest path if there are path blocks leading to it. Uses a minimum priority queue. 
        /// In the while loop gets node with minimum tentative distance, skips if already visited, checks the neighboring blocks in 2 short for loops (checks to skip current node and diagonals), then checks for index out of bounds and path or players, and updates distance if shorter path found.
        /// </summary>
        /// <param name="board">BlockTypeEnum matrix.</param>
        public void FindShortestPath(BlockTypeEnum[,] board)
        {
            int[,] d = new int[_boardHeight - 1, _boardWidth - 1];
            (int, int)[,] parent = new (int, int)[_boardHeight - 1, _boardWidth - 1];
            bool[,] visited = new bool[_boardHeight - 1, _boardWidth - 1];
            for (int i = 1; i < _boardHeight - 1; i++)
            {
                for (int j = 1; j < _boardWidth - 1; j++)
                {
                    d[i, j] = 20000;
                    parent[i, j] = (i, j);
                    visited[i, j] = false;
                }
            }
            d[PosX, PosY] = 0;
            PriorityQueue<(int, int), int> q = new PriorityQueue<(int, int), int>();
            q.Enqueue((PosX, PosY), 0);
            int currentX = PosX;
            int currentY = PosY;
            int newX;
            int newY;
            int currentDistance;

            while (q.Count > 0)
            {
                var current = q.Dequeue();
                currentDistance = d[currentX, currentY];
                currentX = current.Item1;
                currentY = current.Item2;

                if (visited[currentX, currentY]) continue;
                visited[currentX, currentY] = true;

                
                for (int dx = -1; dx <= 1; dx++)
                {
                    for (int dy = -1; dy <= 1; dy++)
                    {
                        if (dx == 0 && dy == 0 || dx != 0 && dy != 0)
                        {
                            continue;
                        }
                        newX = currentX + dx;
                        newY = currentY + dy;

                        if (newX > 0 && newX < _boardHeight - 1 && newY > 0 && newY < _boardWidth - 1 && (board[newX, newY] == BlockTypeEnum.Path || board[newX, newY] == BlockTypeEnum.PlayerA || board[newX, newY] == BlockTypeEnum.PlayerB || board[newX, newY] == BlockTypeEnum.PlayerC))
                        {
                            if (d[newX, newY] > currentDistance + 1)
                            {
                                parent[newX, newY] = (currentX, currentY);
                                d[newX, newY] = currentDistance + 1;
                                q.Enqueue((newX, newY), d[newX, newY]);
                            }
                        }
                    }
                }
            }
            _noAvailablePlayer = FindPlayer(board, d, parent);
        }

        /// <summary>
        /// Finds if there is a reachable player and who is the closest out of them. Lists the steps to the closest player and then reconstruct path backwards from the closest Player.
        /// </summary>
        /// <param name="board">BlockTypeEnum matrix.</param>
        /// <param name="d">Value matrix of the blocks.</param>
        /// <param name="parent">Each blocks's parent on the shortest path towards it.</param>
        /// <returns>Returns whether there is a reachable player or not.</returns>
        public bool FindPlayer(BlockTypeEnum[,] board, int[,] d, (int, int)[,] parent)
        {
            int minDistanceToPlayer = 20000;
            for (int i = 1; i < _boardHeight - 1; i++)
            {
                for (int j = 1; j < _boardWidth - 1; j++)
                {
                    if ((board[i, j] == BlockTypeEnum.PlayerA || board[i, j] == BlockTypeEnum.PlayerB || board[i, j] == BlockTypeEnum.PlayerC)
                        && d[i, j] < minDistanceToPlayer)
                    {
                        minDistanceToPlayer = d[i, j];
                        int currentX = i;
                        int currentY = j;
                        _shortestPath = new List<(int, int)>();
                        while (parent[currentX, currentY] != (currentX, currentY))
                        {
                            _shortestPath.Add((currentX, currentY));
                            var parentOf = parent[currentX, currentY];
                            currentX = parentOf.Item1;
                            currentY = parentOf.Item2;
                        }
                    }
                }
            }
            return (minDistanceToPlayer == 20000);
        }

        /// <summary>
        /// Steps the monster to the next step towards the closest player. The last item in the shortestpath list is next step for the monster, unless the path gets blocked in the meantime.
        /// </summary>
        /// <param name="board">BlockTypeEnum matrix.</param>
        public void FollowShortestPath(BlockTypeEnum[,] board)
        {
            if (_shortestPath != null && _shortestPath.Count > 0)
            {
                int last = _shortestPath.Count - 1;
                (int, int) position = _shortestPath[last];
                int posX = position.Item1;
                int posY = position.Item2;
                _shortestPath.RemoveAt(last);

                _currentBlock = board![posX, posY];
                if (_currentBlock == BlockTypeEnum.Explosion)
                {
                    OnBoardChange(BlockTypeEnum.Path, _posX, _posY, BlockTypeEnum.Explosion, posX, posY);
                    Kill();
                    return;
                }
                else if (_currentBlock == BlockTypeEnum.Path
                 || _currentBlock == BlockTypeEnum.PlayerA
                 || _currentBlock == BlockTypeEnum.PlayerB
                 || _currentBlock == BlockTypeEnum.PlayerC)
                {
                    OnBoardChange(BlockTypeEnum.Path, _posX, _posY, BlockTypeEnum.DijkstraMonster, posX, posY);
                    _posX = posX;
                    _posY = posY;
                }
                else _noAvailablePlayer = true;
            }
            else _noAvailablePlayer = true;
        }
        #endregion

        #region Overriden Functions
        /// <summary>
        /// Determines the movements of the monster. If there is a reachable player, the monster follows it. If there isn't a reachable player, wanders around.
        /// </summary>
        /// <param name="board">BlockTypeEnum matrix.</param>
        override public void Move(BlockTypeEnum[,] board)
        {
            if (!_alive) return;

            if (_moveCoolDown % _speed == 0)
            {
                if (_noAvailablePlayer) 
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
                else FollowShortestPath(board);
            }
        }

        /// <summary>
        /// Monster moves upwards. Checks the type of block where the monster wants to step. If the monster explodes, it sets back to path.
        /// </summary>
        /// <param name="board">BlockTypeEnum matrix</param>
        override public void MoveUp(BlockTypeEnum[,] board)
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
                OnBoardChange(BlockTypeEnum.Path, _posX, _posY, BlockTypeEnum.DijkstraMonster, _posX - 1, _posY);
                _posX--;
            }
            else
            {
                FindShortestPath(board);
                if (!_noAvailablePlayer)
                {
                    FollowShortestPath(board);
                    return;
                }
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
        override public void MoveDown(BlockTypeEnum[,] board)
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
                OnBoardChange(BlockTypeEnum.Path, _posX, _posY, BlockTypeEnum.DijkstraMonster, _posX + 1, _posY);
                _posX++;
            }
            else
            {
                FindShortestPath(board);
                if (!_noAvailablePlayer)
                {
                    FollowShortestPath(board);
                    return;
                }
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
        override public void MoveLeft(BlockTypeEnum[,] board)
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
                OnBoardChange(BlockTypeEnum.Path, _posX, _posY, BlockTypeEnum.DijkstraMonster, _posX, _posY - 1);
                _posY--;
            }
            else
            {
                FindShortestPath(board);
                if (!_noAvailablePlayer)
                {
                    FollowShortestPath(board);
                    return;
                }
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
        override public void MoveRight(BlockTypeEnum[,] board)
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
                OnBoardChange(BlockTypeEnum.Path, _posX, _posY, BlockTypeEnum.DijkstraMonster, _posX, _posY + 1);
                _posY++;
            }
            else
            {
                FindShortestPath(board);
                if (!_noAvailablePlayer)
                {
                    FollowShortestPath(board);
                    return;
                }
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
        /// <param name="board">BlockTypeEnum matrix</param>
        override public void OnTimerTick(BlockTypeEnum[,] board)
        {
            _currentBlock = board![this._posX, this._posY];
            if (moved && _currentBlock != BlockTypeEnum.DijkstraMonster)
            {
                Kill();
                return;
            }
            _moveCoolDown++;
            Move(board);
        }

        /// <summary>
        /// Collects all important data of the current DijkstraMonster object for saving. 
        /// This data represented by an integer array.
        /// </summary>
        /// <returns>The integer array that contains all valuable data.</returns>
        override public int[] SaveData()
        {
            int[] returnData = new int[7];

            returnData[0] = 2;
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
